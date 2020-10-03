<template>
  <div class="gameOver">
    <div v-if="haveMafiaWon" class="gameOver-whoWon" :style="{ color: PlayerTypesColors.MAFIA}">MAFIA WYGRYWA</div>
    <div v-if="!haveMafiaWon" class="gameOver-whoWon" :style="{ color: PlayerTypesColors.CITIZEN}">MIASTO WYGRYWA</div>
  </div>
</template>

<script>

import { PlayerTypesColors } from './../../enums/PlayerTypesColors'
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
    }
  }
  
}
</script>

<style scoped>
  .gameOver{
    z-index: 100000;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .gameOver-whoWon {
    font-size: 4rem;
  }

</style>
