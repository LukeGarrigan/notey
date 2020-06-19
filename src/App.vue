<template>
  <div id="app">
    <Markdown></Markdown>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Markdown from '@/components/Markdown.vue';

@Component({
  components: {
    Markdown,
  },
})
export default class App extends Vue {}
</script>

<style lang="scss">
  body {
    background-color: #e2e2e2;


    h2 {
        border-bottom: 1px solid #ddd;
    }

    h1, h2, h3, h4 {
      line-height: 1.4;
    }

  }
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
</style>
