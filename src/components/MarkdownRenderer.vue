<template>
  <div class="markdown-renderer">
    {{displayMarkdown}}
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';

@Component
export default class MarkdownRenderer extends Vue {
  public displayMarkdown: string = '';

  @Prop() public value!: string;

  @Watch('value', { immediate: true})
  public onMarkdownChange(newMarkdown: string) {
    this.convertToHtml(newMarkdown);
  }

  private convertToHtml(markdown: string) {
    // todo
    this.displayMarkdown = markdown;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-renderer {
    margin-left: 25px;
    width: 45%;
    border: #2c3e50 solid 2px;
    padding: 25px;
    font-size: 3em;
    height: 100%;
    text-align: left;
  }


</style>
