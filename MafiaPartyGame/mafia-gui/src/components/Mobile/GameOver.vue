<template>
  <div class="gameOver">
      <div v-if="haveIWon" class="gameOver-whoWon" :style="{ color: PlayerTypesColors.AGENT}">Wygrywasz!</div>
      <div v-if="!haveIWon" class="gameOver-whoWon" :style="{ color: PlayerTypesColors.MAFIA}">Przegrywasz.</div>
  </div>
</template>

<script>

import { PlayerTypesColors } from './../../enums/PlayerTypesColors'
import { PlayerTypesEnum } from './../../enums/PlayerTypesEnum'
export default {
  name: 'GameOver',
  components: {

  },
  methods: {

  },
  computed: {
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
    haveMafiaWon() {
      return this.$store.state.Voting.haveMafiaWon;
    },
    haveIWon() {
      return (this.haveMafiaWon && this.$store.state.Players.myType == PlayerTypesEnum.MAFIA) || (!this.haveMafiaWon && this.$store.state.Players.myType != PlayerTypesEnum.MAFIA);
    }
  }
  
}
</script>

<style scoped>
  .gameOver{
    display: flex;

    justify-content: center;
    align-items: center;

    height: 100%;
    animation: slideIn .3s;
  }

  .gameOver-whoWon {
    font-size: 4rem;
  }

</style>
