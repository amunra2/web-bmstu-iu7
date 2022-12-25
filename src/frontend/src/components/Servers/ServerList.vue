<template>
  <div class="container">
    <ServerItem
      class="item"
      :mode="mode"
      v-for="server in servers"
      v-bind:server="server"
      v-bind:key="server.id">
    </ServerItem>
  </div>
  <Paging v-bind:page="currentPage"
          v-on:next-page="nextPage"
          v-on:prev-page="prevPage"
          class="for-list"/>
  {{ currentPage}}
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import ServerItem from "@/components/Servers/ServerItem.vue"
import Paging from "@/components/Paging/Paging.vue"

import ServerInterface from "@/Interfaces/ServerInterface"

export default defineComponent({
  name: "ServerList",
  components: {
    ServerItem,
    Paging
  },
  props: {
    mode: {
      type: String,
      default: 'guest'
    },
    initPage: {
      type: Object,
      default: { num: 1, isLast: false}
    }
  },
  data() {
    return {
      servers: [],
      currentPage: this.initPage
    }
  },
  mounted() {
    ServerInterface.getAll(this.currentPage.num, 4).then(json => {this.servers = json})
    ServerInterface.getAll(this.currentPage.num + 1, 4).then(json => {
      this.currentPage.isLast = (json.length == 0)
    })
  },
  methods: {
    nextPage() {
      console.log("next page")
      this.currentPage.num += 1
      ServerInterface.getAll(this.currentPage.num, 4).then(json => {this.servers = json})
      ServerInterface.getAll(this.currentPage.num + 1, 4).then(json => {
        this.currentPage.isLast = (json.length == 0)
      })
      this.$emit('new-page', this.currentPage.num)
    },

    prevPage() {
      console.log("prev page")
      this.currentPage.num -=1
      this.currentPage.isLast = false
      ServerInterface.getAll(this.currentPage.num, 4).then(json => {this.servers = json})
      this.$emit('new-page', this.currentPage.num )
    }
  }
})
</script>

<style scoped>
.container {
  display: flex;
  flex-wrap: wrap;
  align-content: space-between;
  min-height: 550px;
  width: 100%
}
.for-list {
  font-size: var(--tiny-text);
  width: 100%;
  justify-content: right;
}
</style>
