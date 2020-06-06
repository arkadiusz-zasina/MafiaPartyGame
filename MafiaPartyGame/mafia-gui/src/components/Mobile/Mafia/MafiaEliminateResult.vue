<template>
  <div class="mafiaEliminateResult">
    <div class="mafiaTitle" :style="{ color: PlayerTypesColors.MAFIA}">MAFIA</div>
    <div class="spacer1"/>
    <div class="eliminateLabel">Eliminujesz</div>
    <PlayerTagShort class="playerTag" :playerName="player.name" :color="'#' + player.color"/>
    <div class="goToSleepLabel" :style="{ color: PlayerTypesColors.MAFIA }">Idź spać!</div>
    <div class="spacer2"/>
  </div>
</template>

<script>
import { PlayerTypesColors } from './../../../enums/PlayerTypesColors'
import PlayerTagShort from './../../PlayerTagShort'

export default {
  name: 'MafiaEliminateResult',
  components: {
    PlayerTagShort
  },
  computed: {
    player() {
      return this.$store.state.Players.selectedPlayer;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    }
  }

}
</script>

<style scoped>
  .mafiaEliminateResult{
    height: 100%;
    display: flex;
    flex-direction: column;

    align-items: center;

    animation: slideIn .3s;
  }

  .mafiaTitle {
    font-size: 3rem;
    margin-top: 1rem;
    margin-bottom: 1rem;
  }

  .eliminateLabel {
    font-size: 2rem;
  }

  .goToSleepLabel {
    font-size: 4rem;
    margin-top: 3rem;
    margin-bottom: 3rem;
  }

  .spacer1 {
    flex: 1;
  }

  .spacer2 {
    flex: 2;
  }

</style>
