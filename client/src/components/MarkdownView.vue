<template>

  <div class="markdown-view">
    <MarkdownEditor :value="loadedMarkdown" @markdown-updated="markdownUpdated"></MarkdownEditor>
    <MarkdownRenderer :value="markdown"></MarkdownRenderer>
  </div>

</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import MarkdownEditor from '@/components/MarkdownEditor.vue';
import MarkdownRenderer from '@/components/MarkdownRenderer.vue';
import {Getter} from 'vuex-class';

@Component({
  components: {
    MarkdownEditor,
    MarkdownRenderer,
  },
})
export default class MarkdownView extends Vue {
  @Prop() public noteId: string = '';
  @Getter('noteById') public noteById: any;

  public loadedMarkdown: string = '';
  public markdown: string = '';
  public note: any = {};


  @Watch('$route', {immediate: true, deep: true})
  public async onUrlChange(newVal: any) {
    this.noteId = this.$route.params.noteId;
    const md = await this.loadMarkdown();
    this.markdown = md;
    this.loadedMarkdown = md;
  }

  public markdownUpdated(updatedMarkdown: string) {
    this.markdown = updatedMarkdown;
  }

  private async loadMarkdown() {
    const note = this.noteById(this.noteId);

    if (note) {
      return note.markdown;
    }

    return '';
  }



}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-view {
    width: 100%;
    height:1000px;
    display:flex;
    justify-content: left;
  }

</style>
