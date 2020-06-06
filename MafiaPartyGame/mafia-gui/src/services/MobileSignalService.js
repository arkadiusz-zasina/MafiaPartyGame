import { HubConnectionBuilder } from '@microsoft/signalr'
import { PlayerTypesEnum } from './../enums/PlayerTypesEnum';
import { MobileStatesEnum } from './../enums/MobileStatesEnum';

const connection = new HubConnectionBuilder().withUrl("https://localhost:44385/hub").build();


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
        else if (store.state.States.nextStateAfterSleep == "DiscussionState") store.commit('States/changeCurrentMobileState', MobileStatesEnum.DISCUSSION_STATE);
        else if (store.state.States.nextStateAfterSleep == "GameOverState") store.commit('States/changeCurrentMobileState', MobileStatesEnum.GAME_OVER_STATE);
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

}

export function ConnectToGame(store, gameCode, name) {
    console.log(gameCode + " " + name);
    connection.invoke("ConnectToGame", parseInt(gameCode), name).then(() => {
       store.commit('States/changeCurrentMobileState', MobileStatesEnum.WAITING_FOR_START_STATE);
       store.commit('Connection/setGameCode', gameCode);
    })
}

export function OnPlayerReady(store) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.WAITING_FOR_START_STATE);
    connection.invoke("OnPlayerReady", parseInt(gameCode));
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

export function OnAgentFinished(store) {
    var gameCode = store.state.Connection.gameCode;
    store.commit('States/changeCurrentMobileState', MobileStatesEnum.SLEEP_STATE);
    connection.invoke("OnAgentFinished", parseInt(gameCode));
}