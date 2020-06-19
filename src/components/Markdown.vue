<template>

  <div class="markdown-view">
    <MarkdownEditor @markdown-updated="markdownUpdated"></MarkdownEditor>
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
  public markdown: string = '';
  public note: any = {};

  @Watch('noteId', {immediate: true})
  public onIdChanged(newNoteId: string) {
    this.noteId = newNoteId;
    this.markdown = this.loadMarkdown();
  }

  public markdownUpdated(updatedMarkdown: string) {
    this.markdown = updatedMarkdown;
  }

  private loadMarkdown() {
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
