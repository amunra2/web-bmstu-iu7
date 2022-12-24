<template>
  <UpperBackground>
    <PinkText>Server</PinkText>
  </UpperBackground> 
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import UpperBackground from "@/components/UpperBackground.vue"
import PinkText from "@/components/PinkText.vue"

export default defineComponent({
  name: "ServerItem",
  components: {
    UpperBackground,
    PinkText
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
