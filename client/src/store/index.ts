import Vue from 'vue';
import Vuex from 'vuex';
import {noteService} from '@/service/noteService';
import {NoteViewModel} from '@/models/NoteViewModel';
Vue.use(Vuex);

export interface ModuleState {
  currentNote: NoteViewModel;
  notes: NoteViewModel[];
}

export default new Vuex.Store({
  state: {
    notes: Array<NoteViewModel>(),
    currentNote: {title: '', markdown: '', id: ''} as NoteViewModel
  } as ModuleState,
  mutations: {
    setNotes(state, notes: NoteViewModel[]) {
      state.notes = notes;
    },
    setCurrentNote(state, note: NoteViewModel) {
      state.currentNote = note;
    },
    updateCurrentNote(state, note: NoteViewModel) {
      state.currentNote = note;
    }
  },
  actions: {
    loadNotes: async ({commit}) => {
      const noteys = await noteService.getNotes();
      if (noteys) {
        commit('setNotes', noteys);
      }
      return noteys;
    },
    setCurrentNote: async ({commit}, note: NoteViewModel) => {
      commit('setCurrentNote', note);
    },
    updateCurrentNote: async ({commit}, note: NoteViewModel) => {
      commit('updateCurrentNote', note);
    }
  },
  modules: {
  },
  getters: {
    notes: (state) => {
      return state.notes;
    },

    noteById: (state) => (id: string) => {
      return state.notes.find(n => n.id === id);
    },

    currentNote: (state) => {
      return state.currentNote;
    }


  },
});
