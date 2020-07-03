<template>

  <div class="left-panel">

    <div class="all-notes">

      <div class="note-count">
        {{notes.length}} {{notes.length == 1 ? 'note' : 'notes'}}
      </div>

      <div class="plus-icon" @click="enableModal()">
        <PlusIcon></PlusIcon>
      </div>
    </div>

    <Article :ref="note.id" v-for="note in notes" :id="note.id" class="note-preview" @click="chooseNote(note)" tabindex="1">
      <div class="title">
        {{note.title}}
      </div>


      <div class="update-time">
        {{formatDate(note)}}
      </div>

    </Article>


    <Modal v-if="showModal" @clickedAway="showModal = false">
      <h1>Enter a title!</h1>
      <input id="titleInput" v-model="title" name="value" onsubmit="addNote" @keypress.enter="addNote"/>
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
import moment from 'moment';

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
  public selected: string = '';

  public async mounted() {
    this.notes = await this.loadNotes();
  }

  public chooseNote(note: NoteViewModel) {
    this.setStyleForNote(note.id);
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

    this.title = '';

    const id = await noteService.saveNote(addNoteRequest);
    this.notes = await this.loadNotes(); // should be reconsidered
    await this.$router.push({
      name: 'note-page',
      params: {noteId: id}
    });
  }

  public enableModal() {
    this.showModal = true;
    this.$nextTick(() => {// time for it to be added to the dom
      const input: HTMLElement | null = document.getElementById('titleInput');
      if (input) {
        input.focus();
      }
    });
  }

  public formatDate(note: NoteViewModel) {
    return moment(note.lastUpdateTime).fromNow();
  }

  private setStyleForNote(id: string) {
    const element: HTMLElement | null = document.getElementById(this.selected);
    if (element) {
      element.style.border = '';
    }
    this.selected = id;

    const selectedElement: HTMLElement | null = document.getElementById(this.selected);
    if (selectedElement) {
      selectedElement.style.border = '#8bc6d7 solid 2px';
    }


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
    box-sizing: border-box;
    outline: none;
    display:flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height:150px;
    border-left: #d7d7d7 solid 2px;
    border-bottom: #d7d7d7 solid 2px;
    border-right: #d7d7d7 solid 2px;
    border-top: #d7d7d7 solid 2px;
    font-size:1.5em;


    &:hover {
      border: #8bc6d7 solid 2px;
      cursor: pointer;
    }

    .title {
      display: flex;
      font-size: 1.2em;
    }

    .update-time {
      display: flex;
      font-size: 0.6em;
      color: grey;
    }

  }


</style>
