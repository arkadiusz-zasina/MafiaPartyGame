import { HubConnectionBuilder } from '@microsoft/signalr'
import { PlayerTypesEnum } from './../enums/PlayerTypesEnum';
import { MobileStatesEnum } from './../enums/MobileStatesEnum';

const connection = new HubConnectionBuilder().withUrl("https://10.3.77.241:45456/hub").build();


export function connect(store) {
    connection.start();


    connection.on("OnGameStarted", function(type) {
        var pType;
        var nextState;
        switch(type) {
            case 0:
                pType = PlayerTypesEnum.MAFIA;
                nextState = MobileStatesEnum.MAFIA_INFO_STATE;
                break;
            case 1:
                pType = PlayerTypesEnum.CITIZEN;
                nextState = MobileStatesEnum.CITIZEN_INFO_STATE;
                break;
            case 2:
                pType = PlayerTypesEnum.AGENT;
                nextState = MobileStatesEnum.AGENT_INFO_STATE;
                break;
            default:
                pType = PlayerTypesEnum.UNDEFINED;
        }
        store.commit('Players/setMyType', pType);
        store.commit('States/changeCurrentMobileState', nextState);

    });

    connection.on("OnPlayersReady", function(data) {
        store.commit('States/changeNextStateAfterSleep', data);
        store.commit('States/changeCurrentMobileState', MobileStatesEnum.SLEEP_STATE);
    }) 

    connection.on("OnNextState", function() {
        var type = store.state.Players.myType;
        if (store.state.States.nextStateAfterSleep == "AgentChecksState" && type == PlayerTypesEnum.AGENT) store.commit('States/changeCurrentMobileState', MobileStatesEnum.AGENT_CHECKS_STATE);
        else if (store.state.States.nextStateAfterSleep == "MafiaKillsState" && type == PlayerTypesEnum.MAFIA) store.commit('States/changeCurrentMobileState', MobileStatesEnum.MAFIA_KILLS_STATE);
        else if (store.state.States.nextStateAfterSleep == "DiscussionState") {store.commit('Voting/setAmIReadyForFinalVoting', false); store.commit('States/changeCurrentMobileState', MobileStatesEnum.DISCUSSION_STATE);}
        else if (store.state.States.nextStateAfterSleep == "GameOverState") store.commit('States/changeCurrentMobileState', MobileStatesEnum.GAME_OVER_STATE);
        else if (store.state.States.nextStateAfterSleep == "FinalBeforeGameOverState") store.commit('States/changeCurrentMobileState', MobileStatesEnum.FINAL_BEFORE_GAME_OVER_STATE);
        else if (store.state.States.nextStateAfterSleep == "FinalState") store.commit('States/changeCurrentMobileState', MobileStatesEnum.FINAL_STATE);
        else if (store.state.States.nextStateAfterSleep == "DrawState") store.commit('States/changeCurrentMobileState', MobileStatesEnum.DRAW_STATE);
    }) 

    connection.on("OnGetKilled", function() {
        store.commit('Players/setIsDead', true);
    }) 

    connection.on("OnMafiaVotingFinished", function(data) {
        store.commit('Players/setSelectedPlayer', data);
    }) 

    connection.on("OnReceivePlayers", function(data) {
        console.log(data); 
        store.commit('Players/setPlayersForPick', data);       
    }) 

    connection.on("OnCheckedIfMafia", function(data) {
        store.commit('Players/setIsSelectedPlayerMafia', data);
        store.commit('States/changeCurrentMobileState', MobileStatesEnum.AGENT_CHECKED_STATE);   
    })
    
    connection.on("OnPlayerProtected", function() {
        store.commit('States/changeCurrentMobileState', MobileStatesEnum.AGENT_PROTECTED_STATE);   
    })

    connection.on("OnDiscussionReadyChanged", function(data) {
        store.commit('Voting/setAmIReadyForFinalVoting', data);   
    })

    connection.on("OnVotingStarted", function() {
        store.commit('States/changeCurrentMobileState', MobileStatesEnum.VOTING_STATE);
    })

    connection.on("OnMainVotingFinished", function(data) {
        store.commit('States/changeNextStateAfterSleep', data);
        store.commit('States/changeCurrentMobileState', MobileStatesEnum.STAY_TUNED_STATE);
    })

    connection.on("OnGetDrawPossibleVotes", function(data) {
        store.commit('Voting/setVotingDrawPossibleVotes', data);
    })

    connection.on("OnGameOver", function(data) {
        store.commit('Voting/setHaveMafiaWon', data);
        store.commit('States/changeCurrentMobileState', MobileStatesEnum.GAME_OVER_STATE);
    })

    connection.on("OnGameOverWithoutTransition", function(data) {
        store.commit('Voting/setHaveMafiaWon', data);
    })

}

export function ConnectToGame(store, gameCode, name) {
    console.log(gameCode + " " + name);
    connection.invoke("ConnectToGame", parseInt(gameCode), name).then(() => {
       store.commit('States/changeCurrentMobileState', MobileStatesEnum.WAITING_FOR_START_STATE);
       store.commit('Connection/setGameCode', gameCode);
       store.commit('Connection/setMyName', name);
    })
}

export function OnPlayerReady(store) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.WAITING_FOR_START_STATE);
    connection.invoke("OnPlayerReady", parseInt(gameCode));
}

export function OnPlayerReadyForNextRound(store) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.WAITING_FOR_START_STATE);
    connection.invoke("OnPlayerReadyForNextRound", parseInt(gameCode));
}

export function OnPlayerReadyForFinalVoting(store) {
    var gameCode = store.state.Connection.gameCode;
    connection.invoke("OnPlayerDiscussionReadyUnready", parseInt(gameCode));
}

export function GetAgentChecksPlayers(store) {
    var gameCode = store.state.Connection.gameCode;
    connection.invoke("GetPlayers", parseInt(gameCode), false, true);
}

export function GetAgentProtectsPlayers(store) {
    var gameCode = store.state.Connection.gameCode;
    connection.invoke("GetPlayers", parseInt(gameCode), true, true);
}

export function GetMafiaEliminatesPlayers(store) {
    var gameCode = store.state.Connection.gameCode;
    connection.invoke("GetPlayers", parseInt(gameCode), false, false);
}

export function CheckIfMafia(store, connId) {
    var gameCode = store.state.Connection.gameCode;
    connection.invoke("CheckIfMafia", parseInt(gameCode), connId);
}

export function ProtectPlayer(store, connId) {
    var gameCode = store.state.Connection.gameCode;
    connection.invoke("ProtectPlayer", parseInt(gameCode), connId);
}

export function MafiaEliminate(store, connId) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.MAFIA_KILLED_STATE);
    connection.invoke("MafiaEliminate", parseInt(gameCode), connId);
}

export function VoteMain(store, player) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('Players/setSelectedPlayer', player);
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.VOTING_RESULT_STATE);
    connection.invoke("OnVotingMainVotedUnvoted", parseInt(gameCode), player.connID);
}

export function VoteDraw(store, player) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('Players/setSelectedPlayer', player);
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.VOTING_RESULT_DRAW_STATE);
    connection.invoke("OnVotingDrawVotedUnvoted", parseInt(gameCode), player.connID);
}

export function CancelVote(store, player) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('Players/setSelectedPlayer', null);
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.VOTING_STATE);
    connection.invoke("OnVotingMainVotedUnvoted", parseInt(gameCode), player.connID);
}

export function CancelVoteDraw(store, player) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('Players/setSelectedPlayer', null);
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.DRAW_STATE);
    connection.invoke("OnVotingDrawVotedUnvoted", parseInt(gameCode), player.connID);
}

export function OnAgentFinished(store) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.SLEEP_STATE);
    connection.invoke("OnAgentFinished", parseInt(gameCode));
}