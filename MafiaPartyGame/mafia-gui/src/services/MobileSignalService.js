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
}

export function ConnectToGame(store, gameCode, name) {
    console.log(gameCode + " " + name);
    connection.invoke("ConnectToGame", parseInt(gameCode), name).then(() => {
       store.commit('States/changeCurrentMobileState', MobileStatesEnum.WAITING_FOR_START_STATE);
    })
}