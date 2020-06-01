<template>
  <div class="playerTagReady" :style="{ backgroundColor: color }">
      <img v-if="ready" class="tick" src="@/assets/tick-marked.png"/>
      <img v-if="!ready" class="tick" src="@/assets/tick-unmarked.png"/>
      <div class="playerName">
      {{ playerName }}
      </div>
  </div>
</template>

<script>
export default {
  name: 'PlayerTagReady',
  props: {
    playerName: String,
    color: String,
    ready: Boolean
  },
}
</script>

<style scoped>
    .playerTagReady {
        position: relative;

        display: flex;
        flex-direction: row;
        align-items: center;

        color: white;
        text-align: center;
        width:fit-content;
        font-size: 1.4rem;
        border-radius: 15px;
        padding-top: .3rem;
        padding-bottom: .3rem;
        padding-right: 1rem;
        padding-left: 1rem;
        margin-left: 1rem;
        margin-right: 1rem;
        margin-top: 1rem;
    }
    .tick {
      height: 25px;
      object-fit: cover;
    }
    .playerName {
      margin-left: .7rem;
    }
</style>
