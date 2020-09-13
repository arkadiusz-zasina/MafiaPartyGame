<template>
  <div class="baseMobile">
    <div class="column">
      <img class="logo" src="@/assets/logo.png"/>
      <div class="viewHolder">
        <Eliminated v-if="isDead && this.actualState != MobileStatesEnum.GAME_OVER_STATE"/>
        <JoinRoom v-if="this.actualState == MobileStatesEnum.NOT_JOINED_STATE && !isDead" />
        <MessageScreen v-if="this.actualState == MobileStatesEnum.WAITING_FOR_START_STATE && !isDead" />
        <Description v-if="this.actualState == MobileStatesEnum.MAFIA_INFO_STATE && !isDead" :type="PlayerTypesEnum.MAFIA"/>
        <Description v-if="this.actualState == MobileStatesEnum.CITIZEN_INFO_STATE && !isDead" :type="PlayerTypesEnum.CITIZEN"/>
        <Description v-if="this.actualState == MobileStatesEnum.AGENT_INFO_STATE && !isDead" :type="PlayerTypesEnum.AGENT"/>
        <SleepScreen v-if="this.actualState == MobileStatesEnum.SLEEP_STATE && !isDead"/>
        <AgentChecks v-if="this.actualState == MobileStatesEnum.AGENT_CHECKS_STATE && !isDead"/>
        <AgentCheckResult v-if="this.actualState == MobileStatesEnum.AGENT_CHECKED_STATE && !isDead"/>
        <AgentProtects v-if="this.actualState == MobileStatesEnum.AGENT_PROTECTS_STATE && !isDead"/>
        <AgentProtectResult v-if="this.actualState == MobileStatesEnum.AGENT_PROTECTED_STATE && !isDead"/>
        <MafiaEliminates v-if="this.actualState == MobileStatesEnum.MAFIA_KILLS_STATE && !isDead"/>
        <MafiaEliminateResult v-if="this.actualState == MobileStatesEnum.MAFIA_KILLED_STATE && !isDead"/>
        <Discussion v-if="this.actualState == MobileStatesEnum.DISCUSSION_STATE && !isDead"/>
        <Voting v-if="this.actualState == MobileStatesEnum.VOTING_STATE && !isDead"/>
        <GameOver v-if="this.actualState == MobileStatesEnum.GAME_OVER_STATE"/>
        
      </div>
    </div>
  </div>
</template>

<script>
import { MobileStatesEnum } from "../../enums/MobileStatesEnum";
import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";
import * as MobileSignalService from './/../../services/MobileSignalService'

import JoinRoom from "../Mobile/JoinRoom.vue"
import MessageScreen from "../Mobile/MessageScreen.vue"
import Description from "../Mobile/Description.vue"
import Eliminated from "../Mobile/Eliminated.vue"
import Discussion from "../Mobile/Discussion.vue"
import Voting from "../Mobile/Voting.vue"

/*import VotingResult from "../Mobile/VotingResult.vue"*/
import SleepScreen from "../Mobile/SleepScreen.vue" 
import AgentChecks from "../Mobile/Agent/AgentChecks.vue" 
import AgentCheckResult from "../Mobile/Agent/AgentCheckResult.vue" 
import AgentProtects from "../Mobile/Agent/AgentProtects.vue" 
import AgentProtectResult from "../Mobile/Agent/AgentProtectResult.vue" 
import MafiaEliminates from "../Mobile/Mafia/MafiaEliminates.vue" 
import MafiaEliminateResult from "../Mobile/Mafia/MafiaEliminateResult.vue" 
import GameOver from "../Mobile/GameOver.vue" 

export default {
  name: 'BaseMobile',
  components: {
    Description,
    Discussion,
    Eliminated, 
    JoinRoom,
    MessageScreen,
    SleepScreen,
    AgentChecks,
    AgentCheckResult,
    AgentProtects,
    AgentProtectResult,
    MafiaEliminates,
    MafiaEliminateResult,
    GameOver,
    Voting
    /*VotingResult*/
  },
  computed: {
    MobileStatesEnum() { return MobileStatesEnum },
    PlayerTypesEnum() { return PlayerTypesEnum },
    actualState() { return this.$store.state.States.actualMobileState },
    isDead() { return this.$store.state.Players.isDead }
  },
  created() {
    MobileSignalService.connect(this.$store);
  }
}
</script>

<style scoped>
  .baseMobile {
    background: #0d0738;

    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: 2;

    overflow: auto;
  }

  .column {
    display: flex;
    flex-direction: column;
    align-items: center;

    height: 100%;
  }

  .logo {
    width: 50%;
    margin-top: 1rem;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: 1rem;

  }

  .viewHolder {
    flex: 1;
    width: 100%;
  }
</style>
