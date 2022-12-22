<template>
  <button class="button">
    <PinkText fontSize="var(--little-text)">
        <slot></slot>
    </PinkText>
  </button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import PinkText from '@/components/PinkText.vue'

export default defineComponent({
  data() {
    return {
      hover: false,
    }
  },
  name: "Button",
  components: {
    PinkText
  }
})
</script>

<style scoped>
.button{
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  background-color: transparent; 
  border: 4px solid var(--magenta);
  filter: drop-shadow(0px 0px 15px var(--magenta));
  border-radius: 0px 0px 0px 15px;
  font-family: "Chakra Petch";
}
.button:hover{
  border: 4px solid var(--magenta);
  border-radius: 0px 0px 0px 15px;
  filter: drop-shadow(0px 0px 15px var(--cyan));
}
</style>
