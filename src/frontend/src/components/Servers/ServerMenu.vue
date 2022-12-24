<template>
    <div v-if="this.mode === 'guest'" class="guest">
      <Button style="height: 70%; width: 200px">Info</Button>
    </div>
    <div v-else-if="this.mode === 'user-star'" class="user-star">
      <Button style="height: 50px; width: 50px">Star</Button>
      <Button style="height: 70%; width: 200px">Info</Button>
    </div>
    <div v-else-if="this.mode === 'user-status'" class="user-star">
      <Button style="height: 50px; width: 50px">State</Button>
      <Button style="height: 70%; width: 200px">Info</Button>
    </div>
    <div v-else-if="this.mode === 'admin'" class="guest">
      <Button style="height: 70%; width: 200px">Update</Button>
      <Button style="height: 70%; width: 200px">Delete</Button>
    </div>
    <div v-else-if="this.mode === 'admin-suggest'" class="guest">
      <Button style="height: 50px; width: 50px">OK</Button>
      <Button style="height: 50px; width: 50px">NOT</Button>
      <Button style="height: 70%; width: 200px">Info</Button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import Button from "@/components/Button.vue"

export default defineComponent({
  name: "ServerMenu",
  components: {
    Button
  },
  props: {
    mode: {
      type: String,
      default: 'guest'
    }
  }
})
</script>

<style scoped>
.guest {
  display: flex;
  justify-content: right;
  align-items: center;
  gap: 70px
}
.user-star {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>