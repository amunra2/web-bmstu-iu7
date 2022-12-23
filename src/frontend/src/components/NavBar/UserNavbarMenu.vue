<template>
    <div class="nav-menu">
      User 
      <Button>Sign Up</Button>
      <Button>Sign In</Button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import Button from "@/components/Button.vue"

export default defineComponent({
  name: "AdminNavbarMenu",
  components: {
    Button
  }
})
</script>
