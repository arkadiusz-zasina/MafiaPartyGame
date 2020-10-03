<template>
  <div class="votingResult">
    <div class="title" :style="{ color: PlayerTypesColors.AGENT}">GŁOSOWANIE</div>
    <div class="spacer1"/>
    <div class="yourVoteLabel">Twój głos to:</div>
    <PlayerTagShort class="playerTag" :playerName="player.name" :color="'#' + player.color"/>
    <div class="waitingLabel">Oczekiwanie na resztę uczestników.</div>
    <div class="spacer2"/>
    <CommonButton class="changeVoteButton" text="Zmień głos" :click="onChangeVote"/>
  </div>
</template>

<script>

import CommonButton from './Elements/CommonButton'
import { PlayerTypesColors } from './../../enums/PlayerTypesColors'
import  * as MobileSignalService from './../../services/MobileSignalService'
import PlayerTagShort from './../PlayerTagShort'
export default {
  name: 'VotingResult',
  components: {
    CommonButton,
    PlayerTagShort
  },
  computed: {
    player() {
      return this.$store.state.Players.selectedPlayer;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
  },
  methods: {
    onChangeVote() {
      MobileSignalService.CancelVote(this.$store, this.player);
    }
  }
}
</script>

<style scoped>
  .votingResult{
    height: 100%;
    display: flex;
    flex-direction: column;

    align-items: center;

    animation: slideIn .3s;
  }

  .title {
    font-size: 3rem;
    margin-top: 1rem;
    margin-bottom: 1rem;
  }

  .yourVoteLabel {
    font-size: 1rem;
  }

  .waitingLabel {
    margin-top: 1rem;
    font-size: 1rem;
  }

  .spacer1 {
    flex: 1;
  }

  .spacer2 {
    flex: 2;
  }

  .changeVoteButton {
    width: 90%;
    margin-bottom: 1rem;
  }
</style>
