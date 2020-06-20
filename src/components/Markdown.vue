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

@Component({
  components: {
    MarkdownEditor,
    MarkdownRenderer,
  },
})
export default class Markdown extends Vue {

  @Prop() public noteId: string = '';
  public loadedMarkdown: string = '';
  public markdown: string = '';
  public note: any = {};

  @Watch('noteId', {immediate: true})
  public async onIdChanged(newNoteId: string) {
    this.noteId = newNoteId;

    const md = await this.loadMarkdown();
    this.markdown = md;
    this.loadedMarkdown = md;
  }

  public markdownUpdated(updatedMarkdown: string) {
    this.markdown = updatedMarkdown;
  }

  private async loadMarkdown() {
    // bit of a dummy loadout for testing purposes
    const note = this.$store.getters.noteById(this.noteId);

    if (note) {
      return note.note;
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
