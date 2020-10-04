export default {
    namespaced: true,
    state: {
        gameCode: null,
        myName: ""
    },
    mutations: {
        setGameCode(state, code) {
            state.gameCode = code;
        },
        setMyName(state, name) {
            state.myName = name;
        }
    },
    actions: {

    }
}