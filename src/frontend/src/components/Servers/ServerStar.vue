<template>
    <button class="star" v-on:click="changeState">
        <img v-if="state" src="@/assets/img/star-on.svg"/>
        <img v-else src="@/assets/img/star-off.svg"/>
    </button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: "ServerStar",
  data() {
    return {
      state: false
    }
  },
  methods: {
    changeState: function() {
      this.state = !this.state
    }
  }
})
</script>

<style scoped>
.star {
  background: none;
  color: inherit;
  border: none;
  padding: 0;
  font: inherit;
  cursor: pointer;
  outline: inherit;
}
</style>
