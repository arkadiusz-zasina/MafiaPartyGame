<template>
  <div class="mafiaEliminates">
    <div class="mafiaTitle" :style="{ color: PlayerTypesColors.MAFIA}">MAFIA</div>
    <div class="description">Wybierz osobę, którą chcesz 
      <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline'}">wyeliminować</div>.
    </div>
    <PlayerPicker class="playerPicker" :players="players" :onPlayerClicked="onPlayerClicked"/>
  </div>
</template>

<script>
import PlayerPicker from './../Elements/PlayerPicker'
import { PlayerTypesColors } from './../../../enums/PlayerTypesColors'
import * as MobileSignalService from './../../../services/MobileSignalService'

export default {
  name: 'MafiaEliminates',
  components: {
    PlayerPicker
  },
  methods: {
    onPlayerClicked(player) {
      MobileSignalService.MafiaEliminate(this.$store, player.connID);
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
    MobileSignalService.GetMafiaEliminatesPlayers(this.$store);
  }


  
}
</script>

<style scoped>
  .mafiaEliminates{
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

  .mafiaTitle {
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
