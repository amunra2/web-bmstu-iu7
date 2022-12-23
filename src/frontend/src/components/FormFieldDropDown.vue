<template>
  <div class="form-field-drop-down">
    <BlueText><slot></slot></BlueText>
    <div class="form-field-drop-down" style="display: flex; gap: 5px;">
      <InputLine />
      <DropDownButton size=15px />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BlueText from "@/components/BlueText.vue"
import InputLine from "@/components/InputLine.vue"
import DropDownButton from "@/components/DropDownButton.vue"

export default defineComponent({
  name: "FormField",
  components: {
    BlueText,
    InputLine,
    DropDownButton
  },
  props: {}
});
</script>

<style scoped>
  .form-field-drop-down {
    /* justify-content: center;
    align-items: center;
    text-align: center; */
  }
</style>
