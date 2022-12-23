<template>
  <body class="container">
    <UpperBackground class="center">
      <PinkText fontSize="var(--tiny-text)">Good</PinkText>
      <PinkText fontSize="var(--little-text)">Info</PinkText>
      <PinkText fontSize="var(--middle-text)">Good</PinkText>
      <PinkText fontSize="var(--large-text)">Good</PinkText>
      <PinkText fontSize="var(--huge-text)">Good</PinkText>
      <Button v-on:click="consoleInfo">LongLongLongLongLongLong</Button>
      <UserPatch v-on:click="consoleInfo">User Name</UserPatch>
    </UpperBackground>
  </body>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import PinkText from "@/components/PinkText.vue"
import Button from "@/components/Button.vue"
import UserPatch from "@/components/UserPatch.vue"
import UpperBackground from "@/components/UpperBackground.vue";

export default defineComponent({
  name: "HomeView",
  components: {
    PinkText,
    Button,
    UserPatch,
    UpperBackground
  },
  methods: {
    consoleInfo: function(event: Event){
      console.log(event.timeStamp)
    },
  },
});
</script>

<style scoped>
.container {
  display: flex;
  margin: 0;
  width: 100%;
  height: 100%;
  justify-content: center;
  align-items: center;
}
.center {
  position: relative;
  width: 75%;
  display: grid;
  justify-content: center;
  align-items: center;
}
</style>