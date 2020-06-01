<template>
  <div class="sleepScreen">
    <div class="firstLine">Śpij!</div>
    <div class="secondLine">Co Ty wyprawiasz?!</div>
    <div class="secondLine">Nie powinieneś tego widzieć!</div>
  </div>
</template>

<script>
export default {
  name: 'SleepScreen',
  props: {
    msg: String
  }
}
</script>

<style scoped>
  .sleepScreen {
    display: flex;
    flex-direction: column;

    height: 100%;

    align-items: center;
    justify-content: center;
  }

  .firstLine {
    font-size: 2rem;
    color: #e05050;
    margin-bottom: 2rem;
  }
</style>
