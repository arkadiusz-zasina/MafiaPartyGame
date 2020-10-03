export default {
    namespaced: true,
    state: {
        isLogoLeft: false,
        isOverlayVisible: false
    },
    mutations: {
        switchLogo(state) {
            state.isLogoLeft = !state.isLogoLeft;
        },
        setIsOverlayVisible(state, isFinished) {
            state.isOverlayVisible = isFinished;
        }
    },
    actions: {

    }
}