import { HubConnectionBuilder } from '@microsoft/signalr'

const connection = new HubConnectionBuilder().withUrl("https://localhost:44385/hub").build();

export function connect(store) {
    connection.start().then(() => {
        connection.invoke("CreateRoom");
    })

    connection.on("OnRoomCreated", function(gameCode) {
        store.commit('Connection/setGameCode', gameCode);
        console.log(gameCode, store);
    });

    connection.on("OnPlayerConnected", function(list) {
        console.log(list);
    });
}

export function switchLight() {
    connection.invoke("SwitchLightbulb")
}