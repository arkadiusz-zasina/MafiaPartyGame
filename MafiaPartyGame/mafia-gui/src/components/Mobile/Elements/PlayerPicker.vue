<template>
  <div class="playerPicker">
    <div class="pickerContainer">
        <PlayerPickerElement :flexBasis="flexBasisValue" v-for="p in players" :key="p.connID"  :onPlayerClicked="onPlayerClicked2" :player="p" :class="getPlayerClass(p)"/> 
    </div>
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
      data: function () {
      return {
          state: {
            selectedPlayer: null
          }
      }
  },
  methods: {
      onPlayerClicked2(player) {
          this.state.selectedPlayer = player;
          setTimeout(() => {
              this.state.selectedPlayer = 'all';
          }, 400);
          setTimeout(() => {
              this.onPlayerClicked(player);
          }, 1000);
      },
      getPlayerClass(player) {
        if (player == this.state.selectedPlayer || this.state.selectedPlayer == 'all') return 'trans-group-item-leaving';
        return 'trans-group-item';
      }
  },
  computed: {
      flexBasisValue() {
          var val = Math.floor(this.players.length / 10) + 1;
          var basisValue = 100;
          basisValue /= val;
          return basisValue;
      },
      
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

    .playerPickerElement {
        transition: all .6s;
    }

    .trans-group-item-leaving {
        opacity: 0;
        transform: translateX(10px);
    }

    .trans-group-item {
        opacity: 1;
    }

</style>
