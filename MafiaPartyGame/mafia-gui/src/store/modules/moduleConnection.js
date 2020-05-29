export default {
    namespaced: true,
    state: {
        gameCode: null
    },
    mutations: {
        setGameCode(state, code) {
            state.gameCode = code;
        }
    },
    actions: {

    }
}