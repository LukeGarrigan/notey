<template>
  <div class="markdown-renderer" v-html="convertToHtml()">
    {{displayMarkdown}}
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import showdown from 'showdown';

@Component
export default class MarkdownRenderer extends Vue {
  public displayMarkdown: string = '';
  public converter: any = {};
  @Prop() public value!: string;

  public created() {
    this.converter = new showdown.Converter();
  }

  @Watch('value', { immediate: true})
  public onMarkdownChange(newMarkdown: string) {
    this.displayMarkdown = newMarkdown;
  }

  public convertToHtml() {
    return this.converter.makeHtml(this.displayMarkdown);
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
    font-size: 1.5em;
    height: 100%;
    text-align: left;
  }


</style>
