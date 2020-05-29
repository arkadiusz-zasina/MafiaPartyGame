import { HubConnectionBuilder } from '@microsoft/signalr'

const connection = new HubConnectionBuilder().withUrl("https://localhost:44385/hub").build();

export function connect(store) {
    connection.start();

    connection.on("ReceiveBulbState", function(turnedOn) {
        store.commit('setBulb', turnedOn)
    });
}

export function switchLight() {
    connection.invoke("SwitchLightbulb")
}

export function ConnectToGame(gameCode, name) {
    console.log(gameCode + " " + name);
    connection.invoke("ConnectToGame", parseInt(gameCode), name);
}