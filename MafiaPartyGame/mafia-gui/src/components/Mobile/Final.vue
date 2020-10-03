<template>
  <div class="final">
    <div class="spacer"/>
    <CommonButton class="button" text="Przypomnij mi kim jestem" :click="goToDescription"/>
    <CommonButton class="button" text="Dalej" :click="this.onProceed"/>
    <div class="spacer"/>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";
import * as MobileSignalService from './../../services/MobileSignalService'
import CommonButton from "./Elements/CommonButton";
import { MobileStatesEnum } from './../../enums/MobileStatesEnum'

export default {
  name: 'Final',
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
        MobileSignalService.OnPlayerReadyForNextRound(this.$store);
    },
    goToDescription() {
        this.$store.commit('States/changeIsInFinal', true);
        var type = this.$store.state.Players.myType;
        if (type == PlayerTypesEnum.AGENT) this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.AGENT_INFO_STATE);
        if (type == PlayerTypesEnum.CITIZEN) this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.CITIZEN_INFO_STATE);
        if (type == PlayerTypesEnum.MAFIA) this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.MAFIA_INFO_STATE);
    }
  },
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
  .finalContent-paragraph {
    margin-top: 1.5rem;
  }
  .finalContent {
    margin-top: 4rem;
    width: 80%;
    margin-left: auto;
    margin-right: auto;
    text-align: justify;
  }
  .final {
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
