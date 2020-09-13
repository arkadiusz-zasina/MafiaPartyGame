export default {
    namespaced: true,
    state: {
        votingReady: [],
        amIReadyForFinalVoting: false
    },
    mutations: {
        setVotingReady(state, votes) {
            state.votingReady = votes;
        },
        setAmIReadyForFinalVoting(state, ready) {
            state.amIReadyForFinalVoting = ready;
        }
    },
    actions: {

    }
}