<template>
  <div class="playerPicker">
    <transition-group class="pickerContainer" name="trans-group">
        <PlayerPickerElement :flexBasis="flexBasisValue" class="trans-group-item"  v-for="p in players" :key="p.connID"  :onPlayerClicked="onPlayerClicked" :player="p"/> 
    </transition-group>
  </div>
</template>

<script>
import PlayerPickerElement from './PlayerPickerElement'
export default {
  name: 'PlayerPicker',
  components: {
      PlayerPickerElement
  },
  props: {
    players: Array,
    onPlayerClicked: Function
  },
  computed: {
      flexBasisValue() {
          var val = Math.floor(this.players.length / 10) + 1;
          var basisValue = 100;
          basisValue /= val;
          return basisValue;
      }
  }
}
</script>

<style scoped>
    .playerPicker {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }

    .pickerContainer {
        width: 90%;
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        overflow: hidden;

        justify-content:space-evenly;
    }

    .trans-group-enter, .trans-group-leave-to, .trans-group-leave-active {
        opacity: 0;
    }

    .playerPickerElement {
        transition: all 1s;
    }

</style>
