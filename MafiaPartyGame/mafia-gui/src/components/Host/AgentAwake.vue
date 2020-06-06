<template>
  <div class="agentAwake">
    <div class="firstLine">Budzi się </div>
    <div class="secondLine">Agent</div>
  </div>
</template>

<script>
export default {
  name: 'AgentAwake',
  props: {
    msg: String
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
