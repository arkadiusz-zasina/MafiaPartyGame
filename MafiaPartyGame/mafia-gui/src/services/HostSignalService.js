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
}


export function BeginGame(store) {
    console.log("Begin game");
    connection.invoke("BeginGame", store.state.Connection.gameCode);
}