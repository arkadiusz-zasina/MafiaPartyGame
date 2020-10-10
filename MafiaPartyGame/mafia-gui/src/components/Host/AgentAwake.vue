<template>
  <div class="agentAwake">
    <div class="firstLine">Budzi się </div>
    <div class="secondLine">Agent</div>
  </div>
</template>

<script>
import {Howl} from 'howler';
let audioUrl1 = require('../../assets/keyboard.mp3');
let audioUrl2 = require('../../assets/agent_background.wav');
let sound1 = new Howl({
      src: [audioUrl1],
      autoplay: false,
      loop: true,
      volume: 1,
    });
let sound2 = new Howl({
      src: [audioUrl2],
      autoplay: false,
      loop: true,
      volume: 1,
    });

export default {
  name: 'AgentAwake',
  data: function () {
      return {
          state: {
            s1: null,
            s2: null
          }
      }
  },
  created() {
    this.state.s1 = sound1.play();
    this.state.s2 = sound2.play();
    sound1.fade(0, 1, 1000, this.state.s1);
    sound2.fade(0, 1, 1000, this.state.s2);
  },
  destroyed() {
    sound1.fade(1, 0, 1000, this.state.s1);
    sound2.fade(1, 0, 1000, this.state.s2);
    setTimeout(() => {
      sound1.stop();
      sound2.stop();
    }, 1000)
  }
}
</script>

<style scoped>
  .agentAwake {
    display: flex;
    flex-direction: row;

    align-items: center;
    justify-content: center;

    animation: slideIn .3s;
  }

  .firstLine, .secondLine {
    font-size: 2rem;
  }

  .secondLine {
    margin-left: .6rem;
    color: #fdff81;
  }
</style>
