<template>
  <div class="voting">
    <div class="title" :style="{ color: PlayerTypesColors.AGENT}">GŁOSOWANIE</div>
    <div class="description">Kto jest 
      <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline'}">mafią</div>?
    </div>
    <PlayerPicker v-if="amIAllowedToVote" class="playerPicker" :players="players" :onPlayerClicked="isDraw ? onPlayerClickedDraw : onPlayerClicked"/>
    <div v-if="!amIAllowedToVote" class="replacementMessage">Jesteś na celowniku. Broń się!</div>
  </div>
</template>

<script>
import PlayerPicker from './Elements/PlayerPicker'
import { PlayerTypesColors } from './../../enums/PlayerTypesColors'
import * as MobileSignalService from './../../services/MobileSignalService'

export default {
  name: 'Voting',
  props: {
    isDraw: Boolean
  },
  components: {
    PlayerPicker
  },
  methods: {
    onPlayerClicked(player) {
      MobileSignalService.VoteMain(this.$store, player);
    },
    onPlayerClickedDraw(player) {
      MobileSignalService.VoteDraw(this.$store, player);
    }
  },
  computed: {
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
    players() {
      return !this.isDraw ? this.$store.state.Players.playersForPick : this.$store.state.Voting.votingDrawPossibleVotes;
    },
    amIAllowedToVote() {
      if (!this.isDraw) return true;
      if (!this.$store.state.Voting.votingDrawPossibleVotes) return true;
      if (this.$store.state.Voting.votingDrawPossibleVotes.find(x => x.name == this.$store.state.Connection.myName) != null) return false;
      return true;
    }
  },
  created() {
    MobileSignalService.GetAgentChecksPlayers(this.$store);
  }


  
}
</script>

<style scoped>
  .voting{
    height: 100%;
    display: flex;
    flex-direction: column;

    align-items: center;

    animation: slideIn .3s;
  }

  .playerPicker {
    flex: 1;
    width: 100%;
  }

  .replacementMessage {
    flex: 1;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .title {
    font-size: 3rem;
    margin-top: 1rem;
    margin-bottom: 1rem;
  }

  .description {
    font-size: 1.5rem;
    width: 80%;
    text-align: center;
  }
</style>