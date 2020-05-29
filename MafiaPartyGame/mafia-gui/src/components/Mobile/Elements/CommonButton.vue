<template>
  <button class="commonButton" @click="this.click">
      {{ text }}
  </button>
</template>

<script>
export default {
  name: 'CommonButton',
  props: {
    text: String,
    click: Function
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Open+Sans+Condensed:wght@700&display=swap');

    .commonButton {
        width: 100%;
        font-family: 'Open Sans Condensed', sans-serif;
        cursor: pointer;

        padding: 0;
        border: none;
        background-color: #512b81;
        
        font-size: 2rem;

        color: #e4d9f2;
        border-radius: 10px;
        padding-top: .3rem;
        padding-bottom: .3rem;

        transition: background-color .3s ease-in-out;
        outline:none;
    }

    .commonButton:hover {
        background-color: #763ac0;
    }
</style>
