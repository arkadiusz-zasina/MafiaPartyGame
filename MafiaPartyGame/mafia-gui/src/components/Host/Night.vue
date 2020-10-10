<template>
  <div class="night">
    <div class="firstLine">Miasto idzie spać</div>
    <div class="secondLine">Wszyscy zamykają oczy!</div>
  </div>
</template>

<script>

import {Howl} from 'howler';
let audioUrl = require('../../assets/night.wav');
let sound = new Howl({
      src: [audioUrl],
      autoplay: false,
      loop: true,
      volume: 1,
    });

export default {
  name: 'Night',
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
  .night {
    display: flex;
    flex-direction: column;

    align-items: center;
    justify-content: center;

    animation: slideIn .3s;
  }

  .firstLine, .secondLine {
    font-size: 2rem;
  }

  .secondLine {
    color: #e05050;
    font-weight: 300;
  }
</style>
