<template>

  <div class="left-panel">

    <div v-for="note in notes" class="note-preview" @click="chooseNote(note)">
      {{note.title}}
    </div>

  </div>

</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { Action} from 'vuex-class';
import {NoteViewModel} from '@/models/NoteViewModel';
@Component({
  components: {

  },
})
export default class Notes extends Vue {
  @Action('loadNotes') public loadNotes: any;
  @Action('setCurrentNote') public setCurrentNote: any;


  public notes: NoteViewModel[] = [];
  public async mounted() {
    this.notes = await this.loadNotes();

  }

  public chooseNote(note: NoteViewModel) {
    this.setCurrentNote(note);
    this.$router.push({
      name: 'note-page',
      params: {noteId: note.id}
    });
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">


  .left-panel {
    width:20%;
    height:1080px;
  }

  .note-preview {
    display:flex;
    align-items: center;
    justify-content: center;
    
    height:150px;
    border-left: #d7d7d7 solid 2px;
    border-bottom: #d7d7d7 solid 2px;
    font-size:1.5em;


    &:hover {
      border: #8bc6d7 solid 2px;
      cursor: pointer;
    }
  }

</style>
