import Vue from 'vue';
import Vuex from 'vuex';
import {noteService} from '@/service/noteService';
import {NoteViewModel} from '@/models/NoteViewModel';
Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    notes: Array<NoteViewModel>(),
  },
  mutations: {
    setNotes(state, notes: NoteViewModel[]) {
      state.notes = notes;
    }
  },
  actions: {
    loadNotes: async ({commit}) => {
      const noteys = await noteService.getNotes();
      if (noteys) {
        commit('setNotes', noteys);
      }
      return noteys;
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
    }
  },
});
