<template>
  <div class="playerTagShort" :style="{ backgroundColor: color }">
      {{ playerName }}
  </div>
</template>

<script>
export default {
  name: 'PlayerTagShort',
  props: {
    playerName: String,
    color: String
  }
}
</script>

<style scoped>
    .playerTagShort {
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
</style>
