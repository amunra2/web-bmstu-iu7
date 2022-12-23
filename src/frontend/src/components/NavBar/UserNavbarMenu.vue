<template>
    <div class="nav-menu">
      <UserPatch>User Name</UserPatch>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import Button from "@/components/Button.vue"
import UserPatch from "@/components/UserPatch.vue"

export default defineComponent({
  name: "AdminNavbarMenu",
  components: {
    Button,
    UserPatch
  }
})
</script>
