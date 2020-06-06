<template>
  <div class="agentCheckResult">
    <div class="agentTitle" :style="{ color: PlayerTypesColors.AGENT}">AGENT</div>
    <div class="spacer1"/>
    <PlayerTagShort class="playerTag" :playerName="player.name" :color="'#' + player.color"/>
    <div class="result">{{ isMafia }}</div>
    <div class="mafiaLabel" :style="{ color: PlayerTypesColors.MAFIA }">MAFIĄ</div>
    <div class="spacer2"/>
    <CommonButton class="button" text="Dalej" :click="onClicked"/>
  </div>
</template>

<script>
import { PlayerTypesColors } from './../../../enums/PlayerTypesColors'
import PlayerTagShort from './../../PlayerTagShort'
import CommonButton from './../Elements/CommonButton'
import { MobileStatesEnum } from './../../../enums/MobileStatesEnum'

export default {
  name: 'AgentCheckResult',
  components: {
    PlayerTagShort,
    CommonButton
  },
  methods: {
    onClicked() {
      this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.AGENT_PROTECTS_STATE);
    }
  },
  computed: {
    player() {
      return this.$store.state.Players.selectedPlayer;
    },
    isMafia() {
      return this.$store.state.Players.isSelectedPlayerMafia ? 'JEST' : 'NIE JEST';
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    }
  }

}
</script>

<style scoped>
  .agentCheckResult{
    height: 100%;
    display: flex;
    flex-direction: column;

    align-items: center;

    animation: slideIn .3s;
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

  .mafiaLabel {
    font-size: 2rem;
  }

  .result {
    font-size: 3rem;
    margin-top: 2rem;
    margin-bottom: 2rem;

    animation: reveal 4s;
  }

  .spacer1 {
    flex: 1;
  }

  .spacer2 {
    flex: 2;
  }

  .button {
    width: 90%;
    margin-bottom: 10%;
  }

  @keyframes reveal {
    0% {opacity: 0;}
    50% {opacity: 0;}
    100% {opacity: 1;}
  }
</style>
