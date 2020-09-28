<template>
  <div class="voting">
    <div class="title" :style="{ color: PlayerTypesColors.AGENT}">GŁOSOWANIE</div>
    <div class="description">Kto jest 
      <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline'}">mafią</div>?
    </div>
    <PlayerPicker class="playerPicker" :players="players" :onPlayerClicked="onPlayerClicked"/>
  </div>
</template>

<script>
import PlayerPicker from './Elements/PlayerPicker'
import { PlayerTypesColors } from './../../enums/PlayerTypesColors'
import * as MobileSignalService from './../../services/MobileSignalService'

export default {
  name: 'Voting',
  components: {
    PlayerPicker
  },
  methods: {
    onPlayerClicked(player) {
      MobileSignalService.VoteMain(this.$store, player);
    }
  },
  computed: {
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
    players() {
      return this.$store.state.Players.playersForPick;
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