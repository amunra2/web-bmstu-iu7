<template>
  <PinkText fontSize="var(--tiny-text)">Good</PinkText>
  <PinkText fontSize="var(--little-text)">Info</PinkText>
  <PinkText fontSize="var(--middle-text)">Good</PinkText>
  <PinkText fontSize="var(--large-text)">Good</PinkText>
  <PinkText fontSize="var(--huge-text)">Good</PinkText>
  <Button v-on:click="consoleInfo">LongLongLongLongLongLong</Button>
  <UserPatch v-on:click="consoleInfo">User Name</UserPatch>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import PinkText from "@/components/PinkText.vue"
import Button from "@/components/Button.vue"
import UserPatch from "@/components/UserPatch.vue"

export default defineComponent({
  name: "HomeView",
  components: {
    PinkText,
    Button,
    UserPatch
  },
  methods: {
    consoleInfo: function(event: Event){
      console.log(event.timeStamp)
    },
  },
});
</script>