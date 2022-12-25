<template>
  <Background>
    <router-view/>
  </Background>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Background from "@/components/Background.vue"
import NavBar from "@/components/NavBar/NavBar.vue"

import auth from "@/authentificationService"

export default defineComponent({
  setup() {
    auth.logout()
  },
  components: {
    Background,
    NavBar
  }
});
</script>


<style>
@font-face {
  font-family: "Chakra Petch";
  src: local("ChakraPetch"),
   url(./assets/fonts/ChakraPetch-Regular.ttf) format("truetype");
}
#app {
  font-family: "Chakra Petch";
}
:root{
  --tiny-text: 24px;
  --little-text: 32px;
  --middle-text: 48px;
  --large-text: 60px;
  --huge-text: 96px;
  --black: rgb(0, 0, 0);
  --cyan: rgb(30, 228, 216);
  --magenta: rgb(213, 30, 228);
  --violet: rgb(58, 13, 105);
  --navbar-height: 64px;
}
</style>