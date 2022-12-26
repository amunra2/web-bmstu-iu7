<template>
  <UpperBackground class="search">
    <BlueText fontSize="var(--little-text)">Search by Name:</BlueText>
    <InputLine @serverName="setName" name="serverName"/>
    <BlueText fontSize="var(--little-text)">Search by Game:</BlueText>
    <InputLine @gameName="setGame" name="gameName"/>
  </UpperBackground>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import UpperBackground from "@/components/UpperBackground.vue"
import BlueText from "@/components/BlueText.vue"
import InputLine from "@/components/InputLine.vue"

export default defineComponent({
  name: "ServerSearch",
  components: {
    UpperBackground,
    BlueText,
    InputLine
  },
  data() {
    return {
      serverName: '',
      gameName: ''
    }
  },
  methods: {
    setName(name: string) {
      console.log('new name')
      this.serverName = name
      this.$emit('name-input', this.serverName)
    },

    setGame(game: string) {
      console.log('new name')
      this.gameName = game
      this.$emit('game-input', this.gameName)
    },
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