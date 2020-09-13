<template>
  <div class="discussion">
    <div class="spacer"/>
    <div class="discussion-wakesWithout" v-if="this.killed == null">Budzą się wszyscy mieszkańcy!</div>
    <div class="discussion-wakesWithout" v-if="this.killed != null">Całe miasto budzi się bez <div :style="{ color: '#' + killed.color, display: 'inline' }">{{killed.name}}</div>.</div>
    <div class="discussion-goal" v-if="this.killed == null">Waszym zadaniem jest ustalić komu <div :style="{ color: PlayerTypesColors.AGENT, display: 'inline' }">Agent</div> udaremnił próbę <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }">eliminacji</div>.</div>
    <div class="discussion-goal" v-if="this.killed != null">Waszym zadaniem jest ustalić kto mógł go <div :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }">wyeliminować</div>.</div>
    <div class="spacer"/>
    <div class="discussion-playersReady">
      <div class="discussion-playersReady-label">Osoby gotowe do głosowania:</div>
      <div class="playerTags">
          <PlayerTagReady v-for="p in playersList" :playerName="p.name" :color="'#' + p.color" :key="p.name" :ready="isReady(p.name)"/>
      </div>
    </div>
    <div class="spacer"/>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import PlayerTagReady from "./Elements/PlayerTagReady";
export default {
  name: 'Discussion',
  components: {
    PlayerTagReady
  },
  computed: {
    killed() {
      return this.$store.state.Players.lastlyKilled;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
    playersList() {
      return this.$store.state.Players.playersList.filter(x => x.isAlive);
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
  .discussion {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;

    animation: slideIn .3s;
  }
  .playerTags {
    display: flex;
    flex-wrap: wrap;
    margin-left: auto;
    margin-right: auto;

    width: 90%;
    justify-content:center;
  }
  .spacer {
    flex: 1;
  }

  .discussion-wakesWithout {
    font-size: 3rem;
  }

  .discussion-goal {
    font-size: 2rem;
  }

  .discussion-playersReady-label {
    font-size: 2rem;
    text-align: center;
  }
</style>
