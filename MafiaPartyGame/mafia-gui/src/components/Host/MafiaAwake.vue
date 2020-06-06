<template>
  <div class="mafiaAwake">
    <div class="firstLine">Budzi się </div>
    <div class="secondLine">Mafia</div>
  </div>
</template>

<script>
export default {
  name: 'MafiaAwake',
  props: {
    msg: String
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
