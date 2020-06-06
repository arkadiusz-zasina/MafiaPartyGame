import { HubConnectionBuilder } from '@microsoft/signalr';
import { StatesEnum } from './../enums/StatesEnum';

const connection = new HubConnectionBuilder().withUrl("https://localhost:44385/hub").build();

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

    connection.on("OnGameStarted", function() {
        console.log("Game Began");

        store.commit('States/changeCurrentState', StatesEnum.AWAITING_PLAYERS_READY_STATE);
        store.commit('HostUI/switchLogo');
    }) 

    connection.on("OnPlayersReady", function(data) {
        store.commit('States/changeNextStateAfterSleep', data);
        store.commit('States/changeCurrentState', StatesEnum.SLEEPING_STATE);
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
        store.commit('States/changeCurrentState', nextState);
    }) 

    connection.on("OnMafiaVotingFinished", function(killed) {
        store.commit('Players/setLastlyKilled', killed);
    }) 
}


export function BeginGame(store) {
    console.log("Begin game");
    connection.invoke("BeginGame", store.state.Connection.gameCode);
}