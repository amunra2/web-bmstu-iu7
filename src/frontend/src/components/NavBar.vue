<template>
  <nav class="nav-container">
  </nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: "NavBar",
})
</script>


<style scoped>
.nav-container {
  position: fixed;
  height: var(--navbar-height);
  background-color: var(--violet);
  left: 0;
  right: 0;
  padding-left: 1.4rem;
  padding-right: 1.4rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>