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

export default {
  name: 'Discussion',
  components: {
    CommonButton
  },
  computed: {
    ready() {
      return true;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    }
  },
  methods: {
    goToDescription() {

    },
    onReady() {

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
  }

  .readyMark {
    width: 40%;
  }

  .spacer {
    flex: 1;
  }

  .readyButton {
    width: 90%;
    margin-bottom: 10%;
  }

  .remindButton {
    width: 90%;
    margin-top: 5%;
    margin-bottom: 5%;
  }

  .firstRow, .secondRow {
    font-size: 1.3rem;
    width: 80%;
    text-align: center;
    margin-top: 1rem;
    margin-bottom: 1rem;
  }

</style>
