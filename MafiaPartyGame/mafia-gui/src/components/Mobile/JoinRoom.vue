<template>
  <div class="joinRoom">
    <div class="inputLabel">Wpisz kod pokoju:</div>
    <input class="codeInput" @input="codeInputHandle" :value="state.gameCode" type="number"/>
    <div class="inputLabel">Wpisz imię:</div>
    <input class="nameInput" @input="nameInputHandle" :value="state.name" type="text"/>
    <CommonButton class="button" text="Wejdź" :click="this.onJoin"/>
  </div>
</template>

<script>
import CommonButton from './Elements/CommonButton'
import * as MobileSignalService from './../../services/MobileSignalService'

export default {
  name: 'JoinRoom',
  components: {
    CommonButton
  },
  data: function () {
      return {
          state: {
            gameCode: null,
            name: null
          }
      }
  },
  methods: {
    onJoin() {
      MobileSignalService.ConnectToGame( this.$store, this.state.gameCode, this.state.name);
    },
    codeInputHandle: function(event) {
      const value = event.target.value
      if (String(value).length <= 4) {
          this.state.gameCode = value
      }
      this.$forceUpdate()
    },
    nameInputHandle: function(event) {
      const value = event.target.value
      if (value.length <= 10) {
          this.state.name = value
      }
      this.$forceUpdate()
    }
  }

}
</script>

<style scoped>
  .joinRoom {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    width: 80%;
    height: 100%;
    margin-left: auto;
    margin-right: auto;
    
  }

  .codeInput, .nameInput {
    width: 90%;
    background-color: #dcd8f3;
    border: none;
    border-radius: 20px;

    outline: none;
    padding-left: 1rem;
    padding-right: 1rem;
    text-align: center;
  }

  .codeInput {
    height: 100px;
    font-size: 4rem;
    color: rgb(161, 161, 0);
  }

  .nameInput {
    height: 60px;

    font-size: 2rem;
    color: rgb(184, 32, 32);
  }

  .inputLabel {
    align-self:flex-start;
    margin-left: 10%;
    margin-bottom: .3rem;
    margin-top: 2rem;
    font-size: 1.5rem;
  }

  .button {
    margin-top: 2rem;
    margin-bottom: 2rem;
  }

  input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

/* Firefox */
input[type=number] {
  -moz-appearance: textfield;
}
</style>

