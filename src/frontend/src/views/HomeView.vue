<template>
  <NavBarView>
    <div class="container">
      <BlueText class="text" fontSize="var(--huge-text)">
        Server List
      </BlueText>
      <ServerSearch style="margin-bottom: 20px; width: calc(100% - 74px);"/>
      <ServerList mode='guest' />
    </div>
  </NavBarView>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NavBarView from "@/views/NavBarView.vue"
import BlueText from "@/components/BlueText.vue"
import ServerList from "@/components/Servers/ServerList.vue"
import ServerSearch from "@/components/Servers/ServerSearch.vue"

export default defineComponent({
  name: "HomeView",
  components: {
    NavBarView,
    BlueText,
    ServerList,
    ServerSearch
  },
  methods: {
    consoleInfo: function(event: Event){
      console.log(event.timeStamp)
    },
  },
});
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  margin: 0;
  width: 90%;
  height: 100%;
  justify-content: center;
  align-items: center;
  gap: 10px;
}
.text {
  display: flex;
  flex-direction: row;
  justify-content: left;
  align-items: left;
  width: 100%;
}
</style>