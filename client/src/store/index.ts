import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    notes: [
      {
        id : 'sdflksd-dfskjfdskf-cxxc-fdskdfs',
        title : 'First note',
        note: '# this is the actual markdown 1',
      },
      {
        id: 'sfdjksdfkjsdf-xcvcxvvxc-sadsdsfd',
        title : 'Second note',
        note: '# this is the actual markdown 2',
      },
      {
        id: 'sdcsfdjksdfkjsdf-xcvcxvvxc-sadsdsfd',
        title : 'Third note',
        note: '# this is the actual markdown 3',
      }
    ],
  },
  mutations: {
  },
  actions: {
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
