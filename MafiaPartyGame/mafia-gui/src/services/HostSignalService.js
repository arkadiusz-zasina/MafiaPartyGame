import { HubConnectionBuilder } from '@microsoft/signalr';
import { StatesEnum } from './../enums/StatesEnum';

const connection = new HubConnectionBuilder().withUrl("https://10.3.77.241:45456/hub").build();

export function connect(store) {
    connection.start().then(() => {
        connection.invoke("CreateRoom");
    })

    connection.on("OnRoomCreated", function(gameCode) {
        store.commit('Connection/setGameCode', gameCode);
    });

    connection.on("OnPlayerConnected", function(list) {
        store.commit('Players/setPlayers', list);
    });

    connection.on("OnUpdatePlayersList", function(list) {
        store.commit('Players/setPlayers', list);
    });

    connection.on("OnGameStarted", function() {
        console.log("Game Began");

        store.commit('States/changeCurrentState', StatesEnum.AWAITING_PLAYERS_READY_STATE);
        store.commit('HostUI/switchLogo');
    }) 

    connection.on("OnPlayersReady", function(data) {
        store.commit('States/changeNextStateAfterSleep', data);
        store.commit('States/changeCurrentState', StatesEnum.SLEEPING_STATE);
        store.commit('Voting/setVotingReady', []);
    })

    connection.on("OnMainVotingFinished", function(data) {
        store.commit('States/changeNextStateAfterSleep', data);
        store.commit('HostUI/setIsOverlayVisible', true);
        store.commit('Voting/setVotingReady', []);
    })

    connection.on("OnOnePlayerReady", function(data) {
        var names = [];
        data.forEach(element => {
            names.push(element.voting.name);
        });

        store.commit('Voting/setVotingReady', names);
    })

    connection.on("OnNextState", function() {
        var nextState = "";
        if (store.state.States.nextStateAfterSleep == "AgentChecksState") nextState = StatesEnum.AGENT_CHECKS_STATE;
        else if (store.state.States.nextStateAfterSleep == "MafiaKillsState") nextState = StatesEnum.MAFIA_KILLS_STATE;
        else if (store.state.States.nextStateAfterSleep == "DiscussionState") nextState = StatesEnum.DISCUSSION_STATE;
        else if (store.state.States.nextStateAfterSleep == "GameOverState") nextState = StatesEnum.GAME_OVER_STATE;
        else if (store.state.States.nextStateAfterSleep == "FinalState") nextState = StatesEnum.FINAL_STATE;
        else if (store.state.States.nextStateAfterSleep == "FinalBeforeGameOverState") nextState = StatesEnum.FINAL_BEFORE_GAME_OVER_STATE;

        if (nextState == StatesEnum.GAME_OVER_STATE) {
            store.commit('HostUI/setIsOverlayVisible', true);
            setTimeout(() => {
                store.commit('States/changeCurrentState', nextState);
            }, 3000)
        }
        else {
            store.commit('States/changeCurrentState', nextState);

            setTimeout(() => {
                store.commit('HostUI/setIsOverlayVisible', false);
            }, 1000)
        }
    })

    connection.on("OnMafiaVotingFinished", function(killed) {
        store.commit('Players/setLastlyKilled', killed);
    }) 

    connection.on("OnVotingStarted", function() {
        store.commit('Voting/setVotingMain', []);
        store.commit('States/changeCurrentState', StatesEnum.VOTING_STATE);
    })
    
    connection.on("OnHasResultsOfVoting", function(votingResult) {
        store.commit("Voting/setVotingMainResult", votingResult);
    })

    connection.on("OnMainVoted", function(votes) {
        store.commit("Voting/setVotingMain", votes);
    })

    connection.on("OnGameOver", function(data) {
        store.commit('Voting/setHaveMafiaWon', data);
    })
}


export function BeginGame(store) {
    console.log("Begin game");
    connection.invoke("BeginGame", store.state.Connection.gameCode);
}