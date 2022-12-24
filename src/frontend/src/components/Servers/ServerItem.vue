<template>
  <UpperBackground class="server-item">
    <PinkText class="item" fontSize="var(--middle-text)">
      ServerNameLongLong
    </PinkText>
    <BlueText class="item" fontSize="var(--little-text)">
      Platform
    </BlueText>
    <BlueText class="item" fontSize="var(--little-text)">
      Rating
    </BlueText>
    <ServerMenu class="item" :mode="mode"/>
    <BlueText class="item" fontSize="var(--little-text)">
      GameName
    </BlueText>
    <BlueText class="item" fontSize="var(--middle-text)">
      XBox
    </BlueText>
    <BlueText class="item" fontSize="var(--middle-text)">
      1000
    </BlueText>
  </UpperBackground> 
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import UpperBackground from "@/components/UpperBackground.vue"
import PinkText from "@/components/PinkText.vue"
import BlueText from "@/components/BlueText.vue"
import ServerMenu from "@/components/Servers/ServerMenu.vue"

export default defineComponent({
  name: "ServerItem",
  components: {
    UpperBackground,
    PinkText,
    BlueText,
    ServerMenu
  },
  props: {
    mode: {
      type: String,
      default: 'guest'
    }
  },
  data() {
    return {
      server: {
        id: 1,
        name: 'ServerName',
        gameName: 'Skyrim',
        rating: 100,
        platform: 'PC'
      }
    }
  }
})
</script>

<style scoped>
.server-item {
  border-radius: 0px 0px 0px 50px;
  padding: 10px;
  padding-left: 37px;
  padding-right: 37px;
  display: grid;
  grid-template-columns: 2fr repeat(2, 1fr) 2fr;
  column-gap: 50px;
  width: 100%;
}
.item:nth-child(1) {
  grid-column: 1;
  grid-row: span 2;
}
.item:nth-child(5) {
  grid-column: 1;
  grid-row: 3;
}
.item:nth-child(2) {
  grid-column: 2;
  grid-row: span 1;
  justify-self: center;
}
.item:nth-child(6) {
  grid-column: 2;
  grid-row: span 2;
  justify-self: center;
}
.item:nth-child(3) {
  grid-column: 3;
  grid-row: span 1;
  justify-self: center;
}
.item:nth-child(7) {
  grid-column: 3;
  grid-row: span 2;
  justify-self: center;
}
.item:nth-child(4) {
  grid-column: 4;
  grid-row: span 3;
}
</style>