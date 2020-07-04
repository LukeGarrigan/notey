<template>
  <div class="markdown-renderer" v-html="convertToHtml()">

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
    this.converter = new showdown.Converter({
      strikethrough: true,
      literalMidWordUnderscores: true,
      smoothLivePreview: true,
      headerLevelStart: 2,
    });
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
    width: 45%;
    border: #d7d7d7 solid 2px;
    font-size: 1.5rem;
    height: 100%;
    text-align: left;
    background-color: #ffffff;
    padding: 40px;
    overflow: auto;
    box-sizing: border-box;

    &::-webkit-scrollbar {
      width: 15px;
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
