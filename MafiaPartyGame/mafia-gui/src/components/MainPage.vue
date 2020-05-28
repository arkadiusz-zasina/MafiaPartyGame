<template>
  <div class="mainPage">
    <BaseHost v-if="!this.isMobile"/>
    <BaseMobile v-if="this.isMobile"/>
  </div>
</template>

<script>
import BaseHost from '@/components/Host/BaseHost.vue'
import BaseMobile from '@/components/Mobile/BaseMobile.vue'
export default {
  name: 'MainPage',
  props: {
    msg: String
  },
  components: {
    BaseHost,
    BaseMobile
  },
  computed: {
    isMobile() {
      console.log(window.innerWidth);
        return window.innerWidth < 600;
    }
  }
}
</script>

<style scoped>
  .mainPage {
    height: 100%;
    width: 100%;
    position: relative;
  }
</style>

<style>

</style>
