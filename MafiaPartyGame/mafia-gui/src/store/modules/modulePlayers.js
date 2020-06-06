import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";

export default {
    namespaced: true,
    state: {
        playersList: [],
        playersForPick: [],
        myType: PlayerTypesEnum.UNDEFINED,
        isSelectedPlayerMafia: false,
        selectedPlayer: null,
        isDead: false,
        lastlyKilled: null
    },
    mutations: {
        setPlayers(state, players) {
            state.playersList = players;
        },
        setMyType(state, type) {
            state.myType = type;
        },
        setPlayersForPick(state, players) {
            state.playersForPick = players;
        },
        setIsSelectedPlayerMafia(state, isMafia) {
            state.isSelectedPlayerMafia = isMafia;
        },
        setSelectedPlayer(state, player) {
            state.selectedPlayer = player;
        },
        setIsDead(state, isDead) {
            state.isDead = isDead;
        },
        setLastlyKilled(state, killed) {
            state.lastlyKilled = killed;
        }
    },
    actions: {

    }
}