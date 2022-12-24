<template>
  <div class="container">
    <ServerItem class="item" v-for="n in 4" v-bind:key="n">
    </ServerItem>
  </div>
  <PagingFirst class="for-list" style=""/>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import ServerItem from "@/components/Servers/ServerItem.vue"
import PagingFirst from "@/components/PagingFirst.vue"

export default defineComponent({
  name: "ServerList",
  components: {
    ServerItem,
    PagingFirst
  },
})
</script>

<style scoped>
.container {
  display: flex;
  flex-wrap: wrap;
  align-content: space-between;
  min-height: 550px;
}
.for-list {
  font-size: var(--tiny-text);
  width: 100%;
  justify-content: right;
}
</style>
