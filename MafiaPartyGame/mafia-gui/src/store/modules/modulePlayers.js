import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";

export default {
    namespaced: true,
    state: {
        playersList: [],
        myType: PlayerTypesEnum.UNDEFINED
    },
    mutations: {
        setPlayers(state, players) {
            state.playersList = players;
        },
        setMyType(state, type) {
            state.myType = type;
        }
    },
    actions: {

    }
}