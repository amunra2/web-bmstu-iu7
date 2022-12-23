<template>
  <div class="form-field">
    <BlueText><slot></slot></BlueText>
    <InputLine />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BlueText from "@/components/BlueText.vue"
import InputLine from "@/components/InputLine.vue"

export default defineComponent({
  name: "FormField",
  components: {
    BlueText,
    InputLine
  },
  props: {}
});
</script>

<style scoped>
  /* .form-field {
    
  } */
</style>
