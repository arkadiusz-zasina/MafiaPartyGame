<template>
  <div class="finalState">
    <div class="spacer"/>
    <div class="finalState-townVoted">Miasto zagłosowało!</div>
    <div class="finalState-eliminated">Osoba wyeliminowana to <div :style="{ color: '#' + selectedPlayer.color, display: 'inline' }">{{selectedPlayer.name}}</div>.</div>
    <div class="finalState-resultLabel"><div :style="{ color: '#' + selectedPlayer.color, display: 'inline' }">{{selectedPlayer.name}}</div> to...</div>
    <div class="finalState-result">
        <div v-if="selectedPlayer.type == 0" :style="{ color: PlayerTypesColors.MAFIA, display: 'inline' }">MAFIA</div>
        <div v-if="selectedPlayer.type == 1" :style="{ color: PlayerTypesColors.CITIZEN, display: 'inline' }">MIESZKANIEC</div>
        <div v-if="selectedPlayer.type == 2" :style="{ color: PlayerTypesColors.AGENT, display: 'inline' }">AGENT</div>
    </div>
    <div class="spacer"/>
    <div v-if="!isGameOver" class="finalState-playersReady">
      <div class="finalState-playersReady-label">Osoby gotowe do następnej rundy:</div>
      <div class="playerTags">
          <PlayerTagReady v-for="p in playersList" :playerName="p.name" :color="'#' + p.color" :key="p.name" :ready="isReady(p.name)"/>
      </div>
    </div>
    <div class="spacer"/>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";
import { StatesEnum } from "../../enums/StatesEnum";
import PlayerTagReady from "./Elements/PlayerTagReady";
export default {
  name: 'FinalState',
  components: {
    PlayerTagReady
  },
  props: {
      isGameOver: Boolean
  },
  computed: {
    selectedPlayer() {
      return this.$store.state.Voting.votingMainResult[0];
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    },
    PlayerTypesEnum() {
      return PlayerTypesEnum;
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
  },
  created() {
    if (!this.isGameOver) return;

    setTimeout(() => {
        this.$store.commit('HostUI/setIsOverlayVisible', true);
    }, 10000);
    setTimeout(() => {
        this.$store.commit('States/changeCurrentState', StatesEnum.GAME_OVER_STATE);
    }, 13000);
  }
}
</script>

<style scoped>
  .finalState {
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

  .finalState-townVoted {
    font-size: 2rem;
  }

  .finalState-eliminated {
    font-size: 3rem;
  }

  .finalState-playersReady-label {
    font-size: 2rem;
    text-align: center;
  }

  .finalState-result {
    animation: reveal-long 6s;
  }

  .finalState-resultLabel {
      margin-top: 3rem;
      font-size: 1.5rem;
  }

  .finalState-result {
      font-size: 6rem;
  }
</style>
