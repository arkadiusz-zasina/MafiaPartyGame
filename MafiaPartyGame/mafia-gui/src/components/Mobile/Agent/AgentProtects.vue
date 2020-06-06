<template>
  <div class="agentProtects">
    <div class="agentTitle" :style="{ color: PlayerTypesColors.AGENT}">AGENT</div>
    <div class="description">Wybierz osobę, którą chcesz 
      <div :style="{ color: PlayerTypesColors.AGENT, display: 'inline'}">ochronić</div>.
    </div>
    <PlayerPicker class="playerPicker" :players="players" :onPlayerClicked="onPlayerClicked"/>
  </div>
</template>

<script>
import PlayerPicker from './../Elements/PlayerPicker'
import { PlayerTypesColors } from './../../../enums/PlayerTypesColors'
import * as MobileSignalService from './../../../services/MobileSignalService'

export default {
  name: 'AgentProtects',
  components: {
    PlayerPicker
  },
  methods: {
    onPlayerClicked(player) {
      this.$store.commit('Players/setSelectedPlayer', player);
      MobileSignalService.ProtectPlayer(this.$store, player.connID);
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
    MobileSignalService.GetAgentProtectsPlayers(this.$store);
  }


  
}
</script>

<style scoped>
  .agentProtects{
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

  .agentTitle {
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
