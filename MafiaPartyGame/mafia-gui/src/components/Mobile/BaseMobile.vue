<template>
  <div class="baseMobile">
    <div class="column">
      <img class="logo" src="@/assets/logo.png"/>
      <div class="viewHolder">
        <!-- FIX! !-->
        <JoinRoom v-if="this.actualState == MobileStatesEnum.WAITING_FOR_START_STATE/*NOT_JOINED_STATE*/" />
        <MessageScreen v-if="this.actualState == MobileStatesEnum.WAITING_FOR_START_STATE" />
        <Description v-if="true/*this.actualState == MobileStatesEnum.MAFIA_INFO_STATE*/" :type="PlayerTypesEnum.MAFIA"/>
        <Description v-if="this.actualState == MobileStatesEnum.CITIZEN_INFO_STATE" :type="PlayerTypesEnum.CITIZEN"/>
        <Description v-if="this.actualState == MobileStatesEnum.AGENT_INFO_STATE" :type="PlayerTypesEnum.AGENT"/>
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
/*import Voting from "../Mobile/Voting.vue"
import Discussion from "../Mobile/Discussion.vue"
import Eliminated from "../Mobile/Eliminated.vue"
import VotingResult from "../Mobile/VotingResult.vue"
import SleepScreen from "../Mobile/SleepScreen.vue" */

export default {
  name: 'BaseMobile',
  components: {
    Description,
  /*  Discussion,
    Eliminated, */
    JoinRoom,
    MessageScreen,
 /*   SleepScreen,
    Voting,
    VotingResult*/
  },
  computed: {
    MobileStatesEnum() { return MobileStatesEnum },
    PlayerTypesEnum() { return PlayerTypesEnum },
    actualState() { return this.$store.state.States.actualMobileState }
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
