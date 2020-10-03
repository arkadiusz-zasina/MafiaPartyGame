<template>
  <div class="stayTuned">
    <div class="firstLine">Poczekaj sobie chwilkę.</div>
  </div>
</template>

<script>
export default {
  name: 'StayTuned',
  props: {
    isGameOver: Boolean
  }
}
</script>

<style scoped>
  .stayTuned {
    display: flex;
    flex-direction: column;

    height: 100%;

    align-items: center;
    justify-content: center;

    animation: slideIn .3s;
  }
</style>
