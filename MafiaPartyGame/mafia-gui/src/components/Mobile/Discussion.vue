<template>
  <div class="discussion">
      <CommonButton class="remindButton" text="Przypomnij mi kim jestem" :click="goToDescription"/>
      <div class="firstRow"> Spróbuj dowiedzieć się który z uczestników jest <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }"> MAFIĄ</div>. </div>
      <div class="secondRow"> Gdy podejmiesz finalną decyzję, kliknij przycisk GOTOWOŚĆ. </div>
      <div class="spacer"/>
      <img v-if="ready" class="readyMark" src="@/assets/ready-mark.png"/>
      <img v-if="!ready" class="readyMark" src="@/assets/unready-mark.png"/>
      <div class="spacer"/>
      <CommonButton class="readyButton" text="Gotowość" :click="onReady"/>
  </div>
</template>

<script>
import CommonButton from './Elements/CommonButton'
import { PlayerTypesColors } from './../../enums/PlayerTypesColors'
import { MobileStatesEnum } from './../../enums/MobileStatesEnum'
import { PlayerTypesEnum } from './../../enums/PlayerTypesEnum'
import  * as MobileSignalService from './../../services/MobileSignalService'

export default {
  name: 'Discussion',
  components: {
    CommonButton
  },
  computed: {
    ready() {
      return this.$store.state.Voting.amIReadyForFinalVoting;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
  },
  methods: {
    goToDescription() {
        this.$store.commit('States/changeIsInDiscussion', true);
        var type = this.$store.state.Players.myType;
        if (type == PlayerTypesEnum.AGENT) this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.AGENT_INFO_STATE);
        if (type == PlayerTypesEnum.CITIZEN) this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.CITIZEN_INFO_STATE);
        if (type == PlayerTypesEnum.MAFIA) this.$store.commit('States/changeCurrentMobileState', MobileStatesEnum.MAFIA_INFO_STATE);
        
    },
    onReady() {
        MobileSignalService.OnPlayerReadyForFinalVoting(this.$store);
    }
  }
}
</script>

<style scoped>
  .discussion {
    display: flex;
    height: 100%;
    width: 100%;
    flex-direction: column;

    align-items: center;

    animation: slideIn .3s;
  }

  .readyMark {
    max-width: 40%;
    max-height: 30%;
  }

  .spacer {
    flex: 1;
  }

  .readyButton {
    width: 90%;
    margin-bottom: 1rem;
  }

  .remindButton {
    width: 90%;
    margin-top: 1rem;
    margin-bottom: 1rem;
  }

  .firstRow, .secondRow {
    font-size: 1.3rem;
    width: 80%;
    text-align: center;
    margin-top: 1rem;
    margin-bottom: 1rem;
  }

</style>
