<template>

  <div class="markdown-view">
    <MarkdownEditor @markdown-updated="markdownUpdated"></MarkdownEditor>
    <MarkdownRenderer :value="markdown"></MarkdownRenderer>
  </div>

</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import MarkdownEditor from '@/components/MarkdownEditor.vue';
import MarkdownRenderer from '@/components/MarkdownRenderer.vue';

@Component({
  components: {
    MarkdownEditor,
    MarkdownRenderer,
  },
})
export default class Markdown extends Vue {

  public markdown: string = '';

  public markdownUpdated(updatedMarkdown: string) {
    this.markdown = updatedMarkdown;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-view {
    width: 100%;
    height:1000px;
    display:flex;
    justify-content: center;
  }

</style>
