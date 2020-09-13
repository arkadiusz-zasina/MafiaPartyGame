<template>
  <div class="eliminated">
    <div class="spacer"/>
    <div class="youAreLabel">Jesteś</div>
    <div class="eliminatedLabel">WYELIMINOWANY</div>
    <div class="spacer"/>
    <div class="instructionsLabel">
      <div class="instructionsLabel-first">NIE MOŻESZ </div>
        porozumiewać się z żywymi graczami w żaden sposób.
    </div>
    <div class="spacer"/>
  </div>
</template>

<script>
export default {
  name: 'Eliminated',
  props: {
    msg: String
  }
}
</script>

<style scoped>
  .eliminated {
    height: 100%;
    width: 100%;
    display: flex;
    align-items: center;
    flex-direction: column;
  }
  .eliminatedLabel {
    font-size: 3rem;
    color: #e05050;
  }

  .spacer {
    flex: 1;
  }

  .instructionsLabel-first {
    color: #e05050;
    display: inline;
  }

  .instructionsLabel {
    width: 70%;
    text-align: center;
  }
</style>
