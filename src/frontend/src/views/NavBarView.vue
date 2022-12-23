<template>
    <div style="height: var(--navbar-height)">
      <NavBar />
    </div>
    <div style="height: calc(100% - var(--navbar-height)">
      <slot></slot>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import NavBar from "@/components/NavBar/NavBar.vue"

export default defineComponent({
    name: "NavBarView",
    components: {
        NavBar
    }
})
</script>
