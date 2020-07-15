<template>
  <div contenteditable="true" class="markdown-editor" @keyup="textUpdated" v-html="actualMarkdown" @keydown="clearTimer">

  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import {Getter} from 'vuex-class';

@Component
export default class MarkdownEditor extends Vue {
  @Getter('searchValue') public getSearchValue: any;

  @Prop() public value!: string;
  public actualMarkdown: string = '';

  public doneTypingInterval: number = 2000;
  public typingTimer: any;

  @Watch('value', { immediate: true})
  public onMarkdownChange(newMarkdown: string) {
    this.actualMarkdown = newMarkdown;
    this.highlightSearchedForWords();
  }

  @Watch('getSearchValue', {immediate: true})
  public onSearchValueChange() {
    this.highlightSearchedForWords();
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
    this.$emit('save-note');
  }

  private highlightSearchedForWords() {
    this.stripHighlightingHtml();
    if (this.getSearchValue.length > 1) {
      const re = new RegExp(this.getSearchValue, 'g');
      this.actualMarkdown = this.actualMarkdown.replace(re, `<span class="highlighted">${this.getSearchValue}</span>`);
    }
  }

  private stripHighlightingHtml() {
    this.actualMarkdown = this.actualMarkdown.replace(/<span class="highlighted">/g, '');
    this.actualMarkdown = this.actualMarkdown.replace(/<\/span>/g, '');
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-editor {
    position:relative;
    height: 100%;
    width:45%;
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
    word-break: break-word;
    white-space: pre-wrap;
    box-sizing:border-box;

    &::-webkit-scrollbar {
      width: 12px;
    }

    &::-webkit-scrollbar-thumb {
      background-color: #c9ced0;
      border-radius: 10px;
      &:hover {
        background-color: #babfc1;
      }
    }
  }



</style>

<style>
  .highlighted {
    background-color: #1b8080;
    color: white;
    border-radius: 5px;
    padding: 5px;
  }
</style>
