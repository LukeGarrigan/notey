<template>
  <div contenteditable="true" class="markdown-editor" @keydown="textUpdated">

  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class MarkdownEditor extends Vue {

  public textUpdated(event: any) {
    this.$emit('markdown-updated', event.target.innerText);
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-editor {
    position:relative;
    height: 100%;
    width:45%;
    border: #2c3e50 solid 2px;
    font-size:3em;
    text-align: left;
    padding: 25px;
  }

</style>
