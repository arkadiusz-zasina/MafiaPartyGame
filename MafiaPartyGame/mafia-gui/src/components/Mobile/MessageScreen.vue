<template>
  <div class="messageScreen">
    Oczekiwanie na rozpoczęcie
  </div>
</template>

<script>
export default {
  name: 'MessageScreen',
}
</script>

<style scoped>
  .messageScreen {
    display: flex;
    flex-direction: column;

    height: 100%;

    justify-content: center;
    align-items: center;

    animation: slideIn .3s;
  }
</style>
