<template>
  <div class="baseHost">
    <div class="background"/>
    <video class="background-smoke" autoplay loop muted src="@/assets/smoke_background.mp4"/>
    <div class="column">
      <div class="baseSpacer"/>
      <img :class="logoClass" src="@/assets/logo.png"/>
      <WaitingForPlayers class="smallerComponent" v-if="this.actualState == StatesEnum.AWAITING_PLAYERS_STATE" />
      <WaitingForPlayersReady class="biggerComponent" v-if="this.actualState == StatesEnum.AWAITING_PLAYERS_READY_STATE" />
      <Discussion class="biggerComponent" v-if="this.actualState == StatesEnum.DISCUSSION_STATE" />
      <Draw class="biggerComponent" v-if="this.actualState == StatesEnum.DRAW_STATE" />
      <Eliminated class="biggerComponent" v-if="this.actualState == StatesEnum.DISCUSSION_STATE" />
      <LeftInGame class="biggerComponent" v-if="this.actualState == StatesEnum.GAME_OVER_STATE" />
      <Night class="biggerComponent" v-if="this.actualState == StatesEnum.AGENT_CHECKS_STATE" />
      <Voting class="biggerComponent" v-if="this.actualState == StatesEnum.VOTING_STATE" />
    </div>
  </div>
</template>

<script>
import { StatesEnum } from "../../enums/StatesEnum";

import WaitingForPlayers from "../Host/WaitingForPlayers.vue"
import WaitingForPlayersReady from "../Host/WaitingForPlayersReady.vue"
import Discussion from "../Host/Discussion.vue"
import Draw from "../Host/Draw.vue"
import Eliminated from "../Host/Eliminated.vue"
import LeftInGame from "../Host/LeftInGame.vue"
import Night from "../Host/Night.vue"
import Voting from "../Host/Voting.vue"

export default {
  name: 'BaseHost',
  components: {
    WaitingForPlayers,
    WaitingForPlayersReady,
    Discussion,
    Draw ,
    Eliminated,
    LeftInGame,
    Night,
    Voting
  },
  props: {
    msg: String
  },
  computed: {
    logoClass() {
      return this.$store.state.HostUI.isLogoLeft ? 'logo-left' : 'logo';
    },
    actualState() {
      return this.$store.state.States.actualState;
    },
    StatesEnum() {
      return StatesEnum;
    }
  }
}

</script>

<style scoped>
  .baseHost {
    width: 100%;
    height: 100%;
    position: relative;
  }

  .column {
    height: 100%;

    display: flex;
    flex-direction: column;
  }

  .baseSpacer {
    flex: 1;
  }


  .smallerComponent {
    flex: 3;
  }

  .biggerComponent {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
  }

  .background {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: -2;
    background: radial-gradient(circle, rgba(39,24,143,1) 0%, rgb(14, 8, 56) 60%);
    
  }
  .background-smoke {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: -1;
    opacity: 0.2;
    object-fit: cover;
    
    animation: fade 121s;
    animation-iteration-count: infinite;
  }
  .logo, .logo-left {
    position: absolute;
    padding-top: 40px;

    transition: right 1s cubic-bezier(.7,0,.3,1), left 1s cubic-bezier(.7,0,.3,1), width .7s ease, margin-left 1s cubic-bezier(.7,0,.3,1);
  }


  .logo {
    width: 400px;

    text-align: center;
    left: 50%;
    right:  50%;
    margin-left: -200px;
  }
  .logo-left {
    width: 200px;
    text-align: left;
    left: 80px;
    right:20px;
    margin-left: 0px;
  }

  @keyframes fade {
    0% {
      opacity: 0;
    }

    5% {
      opacity: 0.2;
    }

    95% {
      opacity: 0.2;
    }

    100% {
      opacity: 0;
    }
    
  }
</style>

<style>
  @keyframes slideIn {
    from {
      opacity: 0;
      transform: translateY(-30px)
    }
    to {
      opacity: 1;
      transform: translateY(0px)
    }
    
  }

  @keyframes slideOut {
    from {
      opacity: 1;
      transform: translateY(0px)
    }
    to {
      opacity: 0;
      transform: translateY(30px)
    }
    
  }
</style>
