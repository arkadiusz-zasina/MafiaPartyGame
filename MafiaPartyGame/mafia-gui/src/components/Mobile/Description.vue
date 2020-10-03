<template>
  <div class="description">
    <div class="headerRow">
      <div class="headerFirst">Jesteś</div>
      <div class="headerSecond">
        <div v-if="type == PlayerTypesEnum.AGENT" :style="{ color: PlayerTypesColors.AGENT }">AGENTEM</div>
        <div v-if="type == PlayerTypesEnum.CITIZEN" :style="{ color: PlayerTypesColors.CITIZEN }">MIESZKAŃCEM</div>
        <div v-if="type == PlayerTypesEnum.MAFIA" :style="{ color: PlayerTypesColors.MAFIA }">MAFIĄ</div>
      </div>
    </div>
    <div class="descriptionContent">
      <div v-if="type == PlayerTypesEnum.MAFIA">
        <div class="descriptionContent-paragraph">Razem z Tobą mafią jest PLACEHOLDER</div>
        <div class="descriptionContent-paragraph">Twoim zadaniem jest <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline'}"> wyeliminowanie</div> wszystkich <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline'}">mieszkańców</div> miasta. </div>
        <div class="descriptionContent-paragraph">Kiedy przyjdzie pora, obudź się w nocy i wybierz z innymi mafiozami osobę, którą chcecie wyeliminować.</div>
        <div class="descriptionContent-paragraph">Wygrywasz, gdy mafiozów jest tyle samo, co mieszkańców.</div>
      </div>
      <div v-if="type == PlayerTypesEnum.CITIZEN">
        <div class="descriptionContent-paragraph">Twoim zadaniem jest <div :style="{ color: PlayerTypesColors.CITIZEN, display: 'inline'}"> zidentyfikowanie</div> i <div :style="{ color: PlayerTypesColors.CITIZEN, display: 'inline'}">wyeliminowanie</div> każdego mafioza. </div>
        <div class="descriptionContent-paragraph">Obserwuj uważnie pozostałych graczy oraz szukaj podejrzanych zachowań.</div>
        <div class="descriptionContent-paragraph">Wygrywasz, gdy każdy mafiozo zostanie wyeliminowany.</div>
      </div>
      <div v-if="type == PlayerTypesEnum.AGENT">
        <div class="descriptionContent-paragraph">Twoim zadaniem jest <div :style="{ color: PlayerTypesColors.AGENT, display: 'inline'}"> zidentyfikowanie</div> i <div :style="{ color: PlayerTypesColors.AGENT, display: 'inline'}">wyeliminowanie</div> każdego mafioza. </div>
        <div class="descriptionContent-paragraph">Kiedy przyjdzie pora, obudź się w nocy i wybierz gracza, którego chcesz<div :style="{ color: PlayerTypesColors.AGENT, display: 'inline'}"> sprawdzić </div>czy jest mafiozem oraz gracza, którego chcesz<div :style="{ color: PlayerTypesColors.AGENT, display: 'inline'}"> obronić </div>przed atakiem.</div>
        <div class="descriptionContent-paragraph">Wygrywasz, gdy każdy mafiozo zostanie wyeliminowany.</div>
      </div>
    </div>
    <div class="spacer"/>
    <CommonButton class="button" text="Rozumiem" :click="this.onProceed"/>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";
import * as MobileSignalService from './../../services/MobileSignalService'
import CommonButton from "./Elements/CommonButton";
import { MobileStatesEnum } from './../../enums/MobileStatesEnum'

export default {
  name: 'Description',
  props: {
    type: String
  },
  components: {
    CommonButton
  },
  computed: {
    PlayerTypesEnum() {
      return PlayerTypesEnum;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    }
  },
  methods: {
    onProceed() {
      var isDiscussion = this.$store.state.States.isInDiscussion;
      var isFinal = this.$store.state.States.isInFinal;
      if (!isDiscussion) {
        if (isFinal) {
          this.$store.commit('States/changeIsInFinal', false);
          MobileSignalService.OnPlayerReadyForNextRound(this.$store);
          return;
        }

        MobileSignalService.OnPlayerReady(this.$store);
      }
      else
      {
        this.$store.commit('States/changeIsInDiscussion', false);
        this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.DISCUSSION_STATE);
      }
    }
  },
  created() {
  //TODO: get another mafia info
  }
}
</script>

<style scoped>
  .headerRow {
    display: flex;
    justify-content: center;
    align-items: baseline;
    margin-top: 1rem;
  }

  .headerFirst {
    margin-right: .6rem;
  }

  .headerSecond {
    font-size: 2.4rem;
  }
  .descriptionContent-paragraph {
    margin-top: 1.5rem;
  }
  .descriptionContent {
    margin-top: 4rem;
    width: 80%;
    margin-left: auto;
    margin-right: auto;
    text-align: justify;
  }
  .description {
    display: flex;
    flex-direction: column;
    height: 100%;

    animation: slideIn .3s;
  }
  .spacer {
    flex: 1;
  }
  .button {
    width: 80%;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: 3rem;
  }
</style>
