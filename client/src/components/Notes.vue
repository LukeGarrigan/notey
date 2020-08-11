<template>

  <div class="left-panel">
    <input placeholder="Search" @keyup="doSearch" class="search-box">

    <div class="top-panel">
      <div class="note-count">
        {{notes.length}} {{notes.length == 1 ? 'note' : 'notes'}}
      </div>

      <div class="plus-icon" @click="addUntitledNote">
        <PlusIcon></PlusIcon>
      </div>
    </div>

    <div class="notes-container" >
      <Article :ref="note.id" v-for="note in notes" :id="note.id" class="note-preview" @click="chooseNote(note)" tabindex="1"  @contextmenu.prevent="rightClickedNote(note, $event)">
        <div class="tags">
          <div class="tag">
            Coding
          </div>

        </div>


        <div class="title">
          {{note.title}}
        </div>


        <div class="update-time">
          {{formatDate(note)}}
        </div>
      </Article>

    </div>



    <Modal v-if="showModal" @clickedAway="showModal = false">
      <h1>Enter a title!</h1>
      <input id="titleInput" v-model="title" name="value" onsubmit="addNote" @keypress.enter="addNote"/>
    </Modal>

    <Modal v-if="showEditModal" @clickedAway="showEditModal = false">
      <h1>Enter a title!</h1>
      <input id="titleEdit" v-model="contextNote.title" name="value" onsubmit="editNoteTitle" @keypress.enter="editNoteTitle"/>
    </Modal>



    <ContextMenu ref="context" v-show="contextNote && showContext">
      <li class="title">
        {{contextNote.title}}
      </li>
      <hr>
      <li @click="enableEditModal">
        <a href="#">Edit title</a> <EditIcon></EditIcon>
      </li>
      <li @click="deleteNote(contextNote.id)">
        <a href="#">Delete</a> <TrashIcon></TrashIcon>
      </li>
    </ContextMenu>

  </div>

</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import {Action, Getter, Mutation} from 'vuex-class';
import {NoteViewModel} from '@/models/NoteViewModel';
import PlusIcon from 'vue-material-design-icons/Plus.vue';
import EditIcon from 'vue-material-design-icons/SquareEditOutline.vue';
import TrashIcon from 'vue-material-design-icons/TrashCanOutline.vue';
import {noteService} from '@/service/noteService';
import {AddNoteRequest} from '@/models/requests/AddNoteRequest';
import Modal from '@/components/Modal.vue';
import moment from 'moment';
import ContextMenu from '@/components/ContextMenu.vue';
@Component({
  components: {
    PlusIcon,
    Modal,
    TrashIcon,
    ContextMenu,
    EditIcon
  },
})
export default class Notes extends Vue {
  @Action('loadNotes') public loadNotes: any;
  @Action('searchNotes') public searchNotes: any;
  @Action('setCurrentNote') public setCurrentNote: any;
  @Mutation('setSearchValue') public setSearchValue: any;

  public showModal: boolean = false;
  public showEditModal: boolean = false;
  public showContext: boolean = false;

  public notes: NoteViewModel[] = [];
  public title: string = '';
  public selected: string = '';

  public contextNote: NoteViewModel = {
    title: '',
    markdown: '',
    id: '',
    lastUpdateTime: ''
  };

  public async mounted() {
    this.notes = await this.loadNotes();
    this.openFirstNote();
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

  public async addUntitledNote() {
    const addNoteRequest: AddNoteRequest = {
      id: '',
      markdown: '',
      title: 'Untitled'
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

  public enableEditModal() {
    this.showModal = true;
    this.showContext = false;
  }

  public async editNoteTitle() {
    const saveNote: AddNoteRequest = {
      id: this.contextNote.id,
      markdown: this.contextNote.markdown,
      title: this.contextNote.title
    };
    await noteService.saveNote(saveNote);
  }

  public formatDate(note: NoteViewModel) {
    return moment(note.lastUpdateTime).fromNow();
  }

  public async deleteNote(noteId: string) {
    this.showContext = false;
    await noteService.deleteNote(noteId);
    this.notes = await this.loadNotes();
    this.openFirstNote();

  }

  public async doSearch(event: any) {
    const value = event.target.value;
    this.setSearchValue(value);
    if (value) {
      this.notes = await this.searchNotes(value.toLowerCase());
    } else {
      this.notes = await this.loadNotes();
    }
  }

  public rightClickedNote(note: NoteViewModel, event: any) {
    this.showContext = true;
    this.contextNote = note;

    const contextMenu: any = this.$refs.context;
    if (contextMenu) {
      contextMenu.open(event);
    }
  }

  private openFirstNote() {
    this.$router.push({
      name: 'note-page',
      params: {noteId: this.notes[0].id}
    }).catch(err => {});
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

  .search-box {
    position: relative;
    display: flex;
    width: 95%;
    border:0;
    border-radius:7px;
    margin-top: 2px;

    &::placeholder {
      padding-left: 10px;
      font-size: 23px;
    }
  }

  input {
    width: 400px;
    height: 40px;
    font-size: 2em;
  }

  .notes-container {
    height: 91%;
    overflow: auto;

    &::-webkit-scrollbar {
      width: 0.8em;
    }

    &::-webkit-scrollbar-thumb {
      background-color: #c9ced0;
      border-radius: 10px;
      &:hover {
        background-color: #babfc1;
      }
    }

  }

  .top-panel {
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
    height:90%;
  }

  .note-preview {
    position: relative;
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


    .trash-icon {
      display:none;
    }

    &:hover {
      border: #8bc6d7 solid 2px;
      cursor: pointer;

      .trash-icon {
        position: absolute;
        display:inline;
        left: 10px;

        &:hover {
          color: grey;
        }
      }
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


  .tags {
    position: absolute;
    right:10px;
    top: 10px;
    display: flex;
    flex-wrap: wrap;

  }
  .tag {
    background: teal;
    border-radius: 10px;
    font-size: 12px;
    color: white;
    padding: 6px;
    margin-left: 5px;
  }


</style>
