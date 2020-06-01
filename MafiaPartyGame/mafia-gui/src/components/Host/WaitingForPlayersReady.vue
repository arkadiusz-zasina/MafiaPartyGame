<template>
  <div class="waitingForPlayersReady">
      <div class="content">
        <div class="awaitingReadyText">Oczekiwanie na gotowość graczy</div>
        <div class="playerTags">
          <PlayerTagReady v-for="p in playersList" :playerName="p.name" :color="'#' + p.color" :key="p.name" :ready="isReady(p.name)"/>
        </div>
      </div>
  </div>
</template>

<script>
import PlayerTagReady from "./Elements/PlayerTagReady";

export default {
  name: 'WaitingForPlayersReady',
  props: {
    msg: String
  },
  components: {
    PlayerTagReady
  },
  computed: {
    playersList() {
      return this.$store.state.Players.playersList;
    },
    votingReady() {
      return this.$store.state.Voting.votingReady;
    }
  },
  methods: {
    isReady(name) {
      return this.votingReady.includes(name);
    }
  }
}
</script>

<style scoped>
  .waitingForPlayersReady {
    display: flex;
    justify-content: center;
    align-items: center;

    animation: slideIn .3s;
  }

  .awaitingReadyText {
    font-size: 2.2rem;
    text-align: center;
  }

  .playerTags {
    display: flex;
    flex-wrap: wrap;
    margin-left: auto;
    margin-right: auto;

    max-width: 70%;
    justify-content:center;
  }
</style>
