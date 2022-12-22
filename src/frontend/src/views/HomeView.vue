<template>
  <PinkText fontSize="var(--tiny-text)">Good</PinkText>
  <PinkText fontSize="var(--little-text)">Good</PinkText>
  <PinkText fontSize="var(--middle-text)">Good</PinkText>
  <PinkText fontSize="var(--large-text)">Good</PinkText>
  <PinkText fontSize="var(--huge-text)">Good</PinkText>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import PinkText from "@/components/PinkText.vue"

export default defineComponent({
  name: "HomeView",
  components: {
    PinkText
  },
});
</script>
