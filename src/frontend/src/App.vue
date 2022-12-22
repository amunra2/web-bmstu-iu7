<template>
  <Background>
    <router-view/>
  </Background>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Background from "@/components/Background.vue"

export default defineComponent({
  data() {
    return { name: "Marina" };
  },
  components: {
    Background
  }
});
</script>

<style scoped>
h1 {
  color: blue;
}
</style>
