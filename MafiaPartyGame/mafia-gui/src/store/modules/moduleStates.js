import { StatesEnum } from "../../enums/StatesEnum";
import { MobileStatesEnum } from "../../enums/MobileStatesEnum";

export default {
    namespaced: true,
    state: {
        actualState: StatesEnum.AWAITING_PLAYERS_STATE,
        actualMobileState: MobileStatesEnum.NOT_JOINED_STATE,
        nextStateAfterSleep: null,
        isInDiscussion: false
    },
    mutations: {
        changeCurrentState(state, actualState) {
            state.actualState = actualState;
        },
        changeCurrentMobileState(state, actualState) {
            state.actualMobileState = actualState;
        },
        changeNextStateAfterSleep(state, nextState) {
            state.nextStateAfterSleep = nextState;
        },
        changeIsInDiscussion(state, isInDiscussion) {
            state.isInDiscussion = isInDiscussion;
        }
    },
    actions: {

    }
}