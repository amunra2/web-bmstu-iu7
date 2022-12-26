<template>
    <div v-if="this.mode === 'guest'" class="guest">
      <router-link style="text-decoration: none" :to="redirectLink">
        <Button style="height: 70%; width: 200px">Info</Button>
      </router-link>
    </div>
    <div v-else-if="this.mode === 'user-star'" class="user-star">
      <div class="one-center">
        <ServerStar style="height: 70%"/>
      </div>
      <router-link style="text-decoration: none" :to="redirectLink">
        <Button style="height: 70%; width: 200px">Info</Button>
      </router-link>
    </div>
    <div v-else-if="this.mode === 'user-status'" class="user-star">
      <div class="one-center">
        <ServerStatus state="rejected"/>
      </div>
      <router-link style="text-decoration: none" :to="redirectLink">
        <Button style="height: 70%; width: 200px">Info</Button>
      </router-link>
    </div>
    <div v-else-if="this.mode === 'admin'" class="guest">
      <Button style="height: 70%; width: 200px">Update</Button>
      <Button style="height: 70%; width: 200px">Delete</Button>
    </div>
    <div v-else-if="this.mode === 'admin-suggest'" class="guest">
      <div class="two-center">
        <ServerButton />
        <ServerButton :accept="false" />
      </div>
      <router-link style="text-decoration: none" :to="redirectLink">
        <Button style="height: 70%; width: 200px">Info</Button>
      </router-link>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import Button from "@/components/Button.vue"
import ServerStatus from "@/components/Servers/ServerStatus.vue"
import ServerStar from "@/components/Servers/ServerStar.vue"
import ServerButton from "@/components/Servers/ServerButton.vue"


export default defineComponent({
  name: "ServerMenu",
  components: {
    Button,
    ServerStatus,
    ServerStar,
    ServerButton
  },
  props: {
    mode: {
      type: String,
      default: 'guest'
    },
    serverId: {
      type: Number,
      default: 0
    }
  },
  computed: {
    redirectLink() {
      return `/server-info/${this.serverId}`;
    }
  },
  mounted() {
    console.log("ServerMenu", this.serverId, this.redirectLink);
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
.one-center {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 75%;
}
.two-center {
  display: flex;
  justify-content: space-around;
  align-items: center;
  width: 75%;
}
.user-star {
  display: flex;
  justify-content: right;
  align-items: center;
  gap: 20%;
}
</style>