<template>

  <div class="left-panel">

    <div class="all-notes">

      <div class="note-count">
        {{notes.length}} {{notes.length == 1 ? 'note' : 'notes'}}
      </div>

      <div class="plus-icon" @click="showModal = true">
        <PlusIcon></PlusIcon>
      </div>
    </div>

    <div v-for="note in notes" class="note-preview" @click="chooseNote(note)">
      {{note.title}}
    </div>


    <Modal v-if="showModal">
      <h1>Enter a title!</h1>
      <input v-model="title" name="value" onsubmit="addNote" @keypress.enter="addNote"/>
    </Modal>

  </div>

</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { Action} from 'vuex-class';
import {NoteViewModel} from '@/models/NoteViewModel';
import PlusIcon from 'vue-material-design-icons/Plus.vue';
import {noteService} from '@/service/noteService';
import {AddNoteRequest} from '@/models/requests/AddNoteRequest';
import Modal from '@/components/Modal.vue';

@Component({
  components: {
    PlusIcon,
    Modal
  },
})
export default class Notes extends Vue {
  @Action('loadNotes') public loadNotes: any;
  @Action('setCurrentNote') public setCurrentNote: any;
  public showModal: boolean = false;

  public notes: NoteViewModel[] = [];
  public title: string = '';
  public async mounted() {
    this.notes = await this.loadNotes();
  }

  public chooseNote(note: NoteViewModel) {
    this.setCurrentNote(note);
    this.$router.push({
      name: 'note-page',
      params: {noteId: note.id}
    }).catch(err => {});
  }

  public async addNote() {
    this.showModal = false;
    const addNoteRequest: AddNoteRequest = {
      id: '',
      markdown: '',
      title: this.title
    };

    const id = await noteService.saveNote(addNoteRequest);
    this.notes = await this.loadNotes(); // should be reconsidered
    await this.$router.push({
      name: 'note-page',
      params: {noteId: id}
    });
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  input {
    width: 400px;
    height: 40px;
    font-size: 2em;
  }

  .all-notes {
    box-sizing: border-box;
    padding:2em;
    height:100px;
    display: flex;
    align-items: center;

    color:grey;

    .note-count {
      text-align: left;
      width:90%
    }

    .plus-icon {
      width: 40px;
      height: 40px;
      border-radius: 50%;
      background-color: white;
      display: flex;
      justify-content: center;
      align-items: center;


      &:hover {
        color: white;
        background-color: grey;
        cursor: pointer;
      }
    }
  }

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
