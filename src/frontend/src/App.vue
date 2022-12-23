<template>
  <h1>Hello, {{ name }}!</h1>
  <hr>
  <BlueText fontSize=100px>Hello World</BlueText>
  <hr>
  <DropDownButton size=100px rotateAngle=0deg></DropDownButton>
  <hr>
  <PagingMiddle>1</PagingMiddle>
  <hr>
  <PagingFirst />
  <hr>
  <PagingLast>23</PagingLast>
  <hr>
  <InputLine />
  <hr>
  <FormField>Some Text</FormField>
  <hr>
  <FormFieldLine>Something</FormFieldLine>
  <hr>
  <FormFieldDropDown>Text</FormFieldDropDown>
  <hr>
  <FormFieldLineDropDown>Test</FormFieldLineDropDown>
  <hr>
  <DropDownMenu>Hello</DropDownMenu>
  <hr>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BlueText from "@/components/BlueText.vue"
import DropDownButton from "@/components/DropDownButton.vue"
import PagingMiddle from "@/components/PagingMiddle.vue"
import PagingFirst from "@/components/PagingFirst.vue"
import PagingLast from "@/components/PagingLast.vue"
import InputLine from "@/components/InputLine.vue"
import FormField from "@/components/FormField.vue"
import FormFieldLine from "@/components/FormFieldLine.vue"
import FormFieldDropDown from "@/components/FormFieldDropDown.vue"
import FormFieldLineDropDown from "@/components/FormFieldLineDropDown.vue"
import DropDownMenu from "@/components/DropDownMenu.vue"

export default defineComponent({
  data() {
    return { name: "Marina" };
  },
  components: {
    BlueText,
    DropDownButton,
    PagingMiddle,
    PagingFirst,
    PagingLast,
    InputLine,
    FormField,
    FormFieldLine,
    FormFieldDropDown,
    FormFieldLineDropDown,
    DropDownMenu,
  }
});
</script>

<style scoped>
@font-face {
  font-family: "Chakra Petch";
  src: local("ChakraPetch"),
   url(./assets/fonts/ChakraPetch-Regular.ttf) format("truetype");
}

#app {
  font-family: "Chakra Petch";
}

h1 {
  color: blue;
}
</style>
