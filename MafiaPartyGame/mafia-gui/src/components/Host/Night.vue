<template>
  <div class="night">
    <div class="firstLine">Miasto idzie spać</div>
    <div class="secondLine">Wszyscy zamykają oczy!</div>
  </div>
</template>

<script>
export default {
  name: 'Night',
  props: {
    msg: String
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
