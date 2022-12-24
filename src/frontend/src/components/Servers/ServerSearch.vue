<template>
  <UpperBackground class="search">
    <FormFieldLine>Search by Name:</FormFieldLine>
    <FormFieldLine>Search by Game:</FormFieldLine>
    <FormFieldLineDropDown> </FormFieldLineDropDown>
    <FormFieldLineDropDown> </FormFieldLineDropDown>
  </UpperBackground>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import UpperBackground from "@/components/UpperBackground.vue"
import FormFieldLine from "@/components/FormFieldLine.vue"
import FormFieldLineDropDown from "@/components/FormFieldLineDropDown.vue"

export default defineComponent({
  name: "ServerSearch",
  components: {
    UpperBackground,
    FormFieldLine,
    FormFieldLineDropDown
  }
})
</script>

<style scoped>
.search {
  display: flex;
  justify-content: space-between;
  width: calc(100% - 74px);
  padding: 10px;
  padding-left: 37px;
  padding-right: 37px;
}
</style>