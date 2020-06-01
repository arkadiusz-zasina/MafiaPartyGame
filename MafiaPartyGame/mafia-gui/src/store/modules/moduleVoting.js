export default {
    namespaced: true,
    state: {
        votingReady: []
    },
    mutations: {
        setVotingReady(state, votes) {
            state.votingReady = votes;
        }
    },
    actions: {

    }
}