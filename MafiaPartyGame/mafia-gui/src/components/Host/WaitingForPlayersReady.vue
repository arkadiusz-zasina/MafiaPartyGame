<template>
  <div class="waitingForPlayersReady">
      <div class="content">
        <div class="awaitingReadyText">Oczekiwanie na gotowość graczy</div>
      </div>
  </div>
</template>

<script>
export default {
  name: 'WaitingForPlayersReady',
  props: {
    msg: String
  }
}
</script>

<style scoped>
  .waitingForPlayersReady {
    display: flex;
    justify-content: center;
    align-items: center;

    animation: slideIn .3s;
  }

  .awaitingReadyText {
    font-size: 2.2rem;
  }
</style>
