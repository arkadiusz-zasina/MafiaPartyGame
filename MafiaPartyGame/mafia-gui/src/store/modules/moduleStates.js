import { StatesEnum } from "../../enums/StatesEnum";

export default {
    namespaced: true,
    state: {
        actualState: StatesEnum.AWAITING_PLAYERS_STATE
    },
    mutations: {
        changeCurrentState(state, actualState) {
            state.actualState = actualState;
        }
    },
    actions: {

    }
}