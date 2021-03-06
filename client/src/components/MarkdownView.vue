<template>

  <div class="note">

    <div v-if="note" contenteditable="true" class="note-title" v-text="note.title"  @keypress.enter.prevent="titleUpdated">

    </div>


    <div class="markdown-view">
      <MarkdownEditor :value="loadedMarkdown" @markdown-updated="markdownUpdated" @save-note="saveNote"></MarkdownEditor>
      <MarkdownRenderer :value="markdown"></MarkdownRenderer>
    </div>


  </div>

</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import MarkdownEditor from '@/components/MarkdownEditor.vue';
import MarkdownRenderer from '@/components/MarkdownRenderer.vue';
import {Action, Getter} from 'vuex-class';
import {NoteViewModel} from '@/models/NoteViewModel';
import {noteService} from '@/service/noteService';
import {AddNoteRequest} from '@/models/requests/AddNoteRequest';

@Component({
  components: {
    MarkdownEditor,
    MarkdownRenderer,
  },
})
export default class MarkdownView extends Vue {
  public noteId: string = '';
  @Action('updateCurrentNote') public updateCurrentNote: any;
  @Getter('noteById') public noteById: any;
  @Getter('currentNote') public currentNote!: NoteViewModel;

  public loadedMarkdown: string = '';
  public markdown: string = '';
  public note!: NoteViewModel;

  @Watch('$route', {immediate: true, deep: true})
  public async onUrlChange(newVal: any) {
    this.noteId = this.$route.params.noteId;
    await this.loadMarkdown();
    this.markdown = this.note.markdown;
    this.loadedMarkdown = this.note.markdown;
  }

  public markdownUpdated(updatedMarkdown: string) {
    this.markdown = updatedMarkdown;
    this.note.markdown = updatedMarkdown;
    this.updateCurrentNote(this.note);
  }

  public async titleUpdated(event: any) {
    const title = event.target.innerText;
    this.note.title = title;
    this.updateCurrentNote(this.note);
    this.saveNote();
  }

  public async saveNote() {
    const saveNote: AddNoteRequest = {
      id: this.currentNote.id,
      markdown: this.currentNote.markdown,
      title: this.currentNote.title
    };
    await noteService.saveNote(saveNote);
  }

  private async loadMarkdown() {
    const note = this.noteById(this.noteId);
    this.note = note;
  }



}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .markdown-view {
    width: 100%;
    height: 100%;
    display:flex;
    justify-content: left;
  }

  .note {
    background:  #fbfbfb;
    width: 100%;
    text-align: left;
  }

  .note-title {
    font-size: 2.5em;
    padding: 15px;

    border-left: #d7d7d7 solid 2px;
    border-top: #d7d7d7 solid 2px;
    border-right: #d7d7d7 solid 2px;

  }

</style>
