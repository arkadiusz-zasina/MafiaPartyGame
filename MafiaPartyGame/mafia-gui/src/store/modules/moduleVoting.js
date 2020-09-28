export default {
    namespaced: true,
    state: {
        votingReady: [],
        amIReadyForFinalVoting: false,
        votingMain: [],
        votingMainResult: []
    },
    mutations: {
        setVotingReady(state, votes) {
            state.votingReady = votes;
        },
        setAmIReadyForFinalVoting(state, ready) {
            state.amIReadyForFinalVoting = ready;
        },
        setVotingMain(state, votes) {
            state.votingMain = votes;
        },
        setVotingMainResult(state, result) {
            state.votingMainResult = result;
        }
    },
    actions: {

    }
}