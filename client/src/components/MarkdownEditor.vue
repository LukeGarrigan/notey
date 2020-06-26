<template>
  <div contenteditable="true" class="markdown-editor" @keyup="textUpdated" v-html="actualMarkdown" @keydown="clearTimer">

  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';

@Component
export default class MarkdownEditor extends Vue {

  @Prop() public value!: string;
  public actualMarkdown: string = '';

  public doneTypingInterval: number = 2000;
  public typingTimer: any;

  @Watch('value', { immediate: true})
  public onMarkdownChange(newMarkdown: string) {
    this.actualMarkdown = newMarkdown;
  }

  public textUpdated(event: any) {
    this.$emit('markdown-updated', event.target.innerText);
    this.clearTimer();
    this.typingTimer = setTimeout(() => this.saveNote(), this.doneTypingInterval);
  }

  public clearTimer() {
    clearTimeout(this.typingTimer);
  }

  public saveNote() {
    this.$emit('save-note'); // maybe do this
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-editor {
    position:relative;
    height: 100%;
    width:40%;
    border-left: #d7d7d7 solid 2px;
    border-top: #d7d7d7 solid 2px;
    border-bottom: #d7d7d7 solid 2px;
    font-size:1.5rem;
    text-align: left;
    color:grey;
    font-family: Consolas;
    padding: 40px;
    background-color: #fbfbfb;
    overflow: auto;
    word-break: break-all;
  }

</style>
