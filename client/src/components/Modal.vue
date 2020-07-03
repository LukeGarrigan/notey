<template>

  <div class="modal-container">
    <div class="modal" v-on-clickaway="clickedAway">
      <slot></slot>
    </div>
  </div>

</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import {mixin as clickaway} from 'vue-clickaway2';


@Component({
  mixins: [clickaway]
})
export default class Modal extends Vue {


  public clickedAway() {
    this.$emit('clickedAway');
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

  .modal-container {
    width: 100%;
    height: 100%;
    background-color: rgba(211,211,211, 0.7);
    position: fixed;
    top: 50%;
    left: 50%;
    -webkit-transform: translate(-50%, -50%);
    transform: translate(-50%, -50%);
    z-index: 1000;
    .modal {
      width: 600px;
      height: 200px;
      background-color: white;
      position: fixed;

      top: 40%;
      left: 50%;
      -webkit-transform: translate(-50%, -50%);
      transform: translate(-50%, -50%);

      -webkit-box-shadow: 3px 3px 5px 6px #ccc;
      -moz-box-shadow:    3px 3px 5px 6px #ccc;
    }

  }



</style>
