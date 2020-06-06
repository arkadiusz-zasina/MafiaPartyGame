<template>
  <div class="playerPickerElement" :style="{ flexBasis: flexBasis + '%' }" @click="onPlayerClicked(player)">
      <div class="nameContainer" :style="{ backgroundColor: '#' + player.color }">
        {{ player.name }}
    </div>
  </div>
</template>

<script>
export default {
  name: 'PlayerPickerElement',
  props: {
    player: Object,
    flexBasis: Number,
    onPlayerClicked: Function
  }
}
</script>

<style scoped>

    .nameContainer {

        text-align: center;
        font-size: 1.7rem;
        margin-top: .3rem;
        margin-bottom: .3rem;
        

        margin-left: .4rem;
        margin-right: .4rem;

        padding-top: .3rem;
        padding-bottom: .3rem;

        border-radius: 15px;
        cursor: pointer;
    }

</style>
