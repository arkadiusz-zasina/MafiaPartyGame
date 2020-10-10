<template>
  <div class="mafiaAwake">
    <div class="firstLine">Budzi się </div>
    <div class="secondLine">Mafia</div>
  </div>
</template>

<script>

import {Howl} from 'howler';
let audioUrl = require('../../assets/mafia.wav');
let sound = new Howl({
      src: [audioUrl],
      autoplay: false,
      loop: false,
      volume: 1,
    });

export default {
  name: 'MafiaAwake',
  data: function () {
      return {
          state: {
            s: null
          }
      }
  },
  created() {
    this.state.s = sound.play();
    sound.fade(0, 1, 1000, this.state.s);
  },
  destroyed() {
    sound.fade(1, 0, 1000, this.state.s);
    setTimeout(() => {
      sound.stop(this.state.s);
    }, 1000)
  }
}
</script>

<style scoped>
  .mafiaAwake {
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
    color: #e05050;
  }
</style>
