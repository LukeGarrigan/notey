<template>


  <div id="cm" class="context-menu" v-show="show" v-on-clickaway="close">
    <slot></slot>
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import {Getter} from 'vuex-class';
import {mixin as clickaway} from 'vue-clickaway2';

@Component({
  mixins: [clickaway]
})
export default class ContextMenu extends Vue {
  public show: boolean = false;


  public open(event: any) {
    this.show = true;
    this.setPosition(event);
  }

  public setPosition(event: any) {
    const left = event.clientX;
    const top = event.clientY;

    const element: HTMLElement | null = document.getElementById('cm');
    if (element) {
      element.style.left = `${left}px`;
      element.style.top = `${top}px`;
    }
  }


  public close() {
    this.show = false;
  }



}
</script>

<style scoped lang="scss">

  .context-menu {
    position:fixed;
    z-index: 1001;
    background: white;
    min-height: 20px;
    width: 200px;
    border-radius: 10px;
    border: 2px solid black;
  }


  li {
    margin: 2px;
    padding: 10px;
    list-style: none;
    position: relative;

    &:hover {
      background: #f0f0eb;

      cursor: pointer;
    }
    text-align: left;
    border-radius: 5px;

    svg {
      position: absolute;
      right: 10px;
      top: 7px;
    }


  }

  .title {
    font-weight: bold;
    padding-bottom: 0px;
    &:hover {
      background: white;
      cursor: default;
    }
  }

  a {
    color: black;
    text-decoration: none;
  }




</style>

