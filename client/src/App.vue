<template>
  <div id="app">
    <Notes></Notes>
    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Markdown from '@/components/MarkdownView.vue';
import Notes from '@/components/Notes.vue';

@Component({
  components: {
    Markdown,
    Notes,
  },
})
export default class App extends Vue {}
</script>

<style lang="scss">
  body {
    background-color: #e2e2e2;


    img {
      max-width:600px;
    }

    h2 {
        border-bottom: 1px solid #ddd;
    }

    h1, h2, h3, h4 {
      line-height: 1.4;
    }


    pre {
      white-space: pre-wrap;
      background-color: #f8f8f8;
      border: 1px solid #dfdfdf;
      margin-top: 1.5em;
      margin-bottom: 1.5em;
      padding: 0.4em 0.5em 0.4em;
    }




  }
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
    display:flex;
  }
</style>
