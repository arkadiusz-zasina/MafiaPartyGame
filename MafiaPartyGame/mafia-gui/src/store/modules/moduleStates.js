import { StatesEnum } from "../../enums/StatesEnum";
import { MobileStatesEnum } from "../../enums/MobileStatesEnum";

export default {
    namespaced: true,
    state: {
        actualState: StatesEnum.AWAITING_PLAYERS_STATE,
        actualMobileState: MobileStatesEnum.NOT_JOINED_STATE
    },
    mutations: {
        changeCurrentState(state, actualState) {
            state.actualState = actualState;
        },
        changeCurrentMobileState(state, actualState) {
            state.actualMobileState = actualState;
        }
    },
    actions: {

    }
}