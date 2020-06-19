<template>

  <div class="left-panel">

    <div v-for="note in getNotes()" class="note" @click="chooseNote(note)">
      {{note.title}}
    </div>

  </div>

</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  components: {

  },
})
export default class Notes extends Vue {


  public getNotes() {
    return this.$store.getters.notes;
  }

  public chooseNote(note: any) {
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

  .note {
    height:150px;
    border-left: #d7d7d7 solid 2px;
    border-bottom: #d7d7d7 solid 2px;

    &:hover {
      border: #8bc6d7 solid 2px;
      cursor: pointer;
    }
  }

</style>
