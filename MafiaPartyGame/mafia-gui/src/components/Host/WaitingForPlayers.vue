<template>
  <div class="waitingForPlayers">
    <div class="column">
      <div class="row-code">
        <div class="codeText">Kod pokoju:</div>
        <div class="codeNumber">{{ gameCode}}</div>
      </div>
      <div class="participants">
        <div class="participants-text">Uczestnicy:</div>
        <transition-group class="participants-list" name="flip-list" tag="div">
          <PlayerTagShort class="flip-list-item" v-for="p in playersList" :playerName="p.name" :color="'#' + p.color" :key="p.name"/>
        </transition-group>
      </div>
      <div class="startButton" @click="begin">Rozpocznij!</div>
    </div>
  </div>
</template>

<script>

import PlayerTagShort from '../PlayerTagShort.vue';
import * as HostSignalService from './../../services/HostSignalService'

export default {
  name: 'WaitingForPlayers',
  components: {
    PlayerTagShort
  },
  props: {
    msg: String
  },
  methods: {
    begin() {
      HostSignalService.BeginGame(this.$store);
    }
  },
  computed: {
    gameCode() {
      return this.$store.state.Connection.gameCode;
    },
    playersList() {
      return this.$store.state.Players.playersList;
    }
  }
}
</script>

<style scoped>
  .waitingForPlayers {
    height: 100%;
    width: 100%;
  }

  .column {
    height: 100%;
    width: 100%;

    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .codeText {
    font-size: 2.2rem;
    margin-right: 1rem;
    height: fit-content;
  }

  .codeNumber {
    font-size: 2.2rem;
    background-color: #a71b1b;
    color: white;
    padding-left: 1.4rem;
    padding-right: 1.4rem;
    border-radius: 20px;

    height: fit-content;
  }

  .participants {
    display: flex;
    flex-direction: column;
    align-items: center;
    flex: 2;
    text-align: center;
  }

  .participants-text {
    font-size: 2.2rem;
  }

  .participants-list {
    display: flex;
    flex-wrap: wrap;
    max-width: 70%;
    justify-content:center;
  }

  .startButton {
    flex: 1;
    flex-grow: 0;
    height: 500px;

    background-color: #98142b;
    color: white;
    font-size: 3rem;
    padding-left: 4rem;
    padding-right: 4rem;
    padding-top: .7rem;
    padding-bottom: .7rem;

    border-radius: 30px;

    margin-bottom: 5%;

    cursor: pointer;
    transition: background-color .2s ease-in-out;

  }

  .startButton:hover {
    background-color: #bd1833;
  }

  .row-code {
    display: flex;
    flex: 1;
  }

.flip-list-enter, .flip-list-leave-to {
  opacity: 0;
  transform: translateY(30px);
}

.flip-list-item {
   transition: all 1s;
}

</style>
