<template>
  <div class="description">
    <div class="headerRow">
      <div class="headerFirst">Jesteś</div>
      <div class="headerSecond">
        <div v-if="type == PlayerTypesEnum.AGENT" :style="{ color: PlayerTypesColors.AGENT }">AGENTEM</div>
        <div v-if="type == PlayerTypesEnum.CITIZEN" :style="{ color: PlayerTypesColors.CITIZEN }">MIESZKAŃCEM</div>
        <div v-if="type == PlayerTypesEnum.MAFIA" :style="{ color: PlayerTypesColors.MAFIA }">MAFIĄ</div>
      </div>
    </div>
  </div>
</template>

<script>
import { PlayerTypesColors } from "../../enums/PlayerTypesColors";
import { PlayerTypesEnum } from "../../enums/PlayerTypesEnum";

export default {
  name: 'Description',
  props: {
    type: String
  },
  computed: {
    PlayerTypesEnum() {
      return PlayerTypesEnum;
    },
    PlayerTypesColors() {
      return PlayerTypesColors;
    }
  },
  created() {
  }
}
</script>

<style scoped>
  .headerRow {
    display: flex;
    justify-content: center;
    align-items: baseline;
    margin-top: 1rem;
  }

  .headerFirst {
    margin-right: .6rem;
  }

  .headerSecond {
    font-size: 2.4rem;
  }
</style>
