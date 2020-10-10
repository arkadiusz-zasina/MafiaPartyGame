<template>
  <div id="app">
    <MainPage msg="Welcome to Your Vue.js App"/>
  </div>
</template>

<script>
import MainPage from './components/MainPage.vue'

export default {
  name: 'App',
  components: {
    MainPage
  },
  created() {
    document.title = 'Mafia Party';
  }
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Open+Sans+Condensed:wght@300;700&display=swap');

#app {
  width: 100%;
  height: 100%;
  position: relative;
  font-family: 'Open Sans Condensed', sans-serif;
  font-weight: 700;
  color: #c8c1f4;

}

html, body {
    margin: 0;
    height: 100%;
}
</style>
