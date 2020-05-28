export default {
    namespaced: true,
    state: {
        isLogoLeft: false
    },
    mutations: {
        switchLogo(state) {
            state.isLogoLeft = !state.isLogoLeft;
        }
    },
    actions: {

    }
}