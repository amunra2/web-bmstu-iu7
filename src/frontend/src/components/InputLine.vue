<template>
    <input class="input-line">
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "InputLine",
  components: {},
  props: {}
});
</script>

<style scoped>
  .input-line {
    background: transparent;
    border: none;
    border-bottom: 2px solid var(--cyan);;
    width: 231px;
    height: 25px;
    filter: drop-shadow(0px 0px 10px var(--magenta));
    font: 400 23px "Chakra Petch", sans-serif;
    color: var(--cyan);
  }
</style>

