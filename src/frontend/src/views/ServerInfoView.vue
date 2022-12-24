<template>
  <NavBarView>
    <div class="container">
      <BlueText class="text" fontSize="var(--huge-text)">
        ServerNameLongLong
      </BlueText>

      <UpperBackground class="center">
        <div class="container-row">
          <div class="container-column">
            <PinkText fontSize="var(--middle-text)">
              Base
            </PinkText>
            <BlueText fontSize="var(--tiny-text)">
              Ip
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              127.0.0.1
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Game
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              Skyrim
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Rating
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              1000
            </BlueText>
          </div>

          <div class="container-column">
            <PinkText fontSize="var(--middle-text)">
              Hosting
            </PinkText>
            <BlueText fontSize="var(--tiny-text)">
              Name
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              Heroku
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Price per Month
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              30$
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Month Number
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              50
            </BlueText>
          </div>

          <div class="container-column">
            <PinkText fontSize="var(--middle-text)">
              Platform
            </PinkText>
            <BlueText fontSize="var(--tiny-text)">
              Name
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              PC
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Popularity
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              100
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Average Cost
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              500$
            </BlueText>
          </div>

          <div class="container-column">
            <PinkText fontSize="var(--middle-text)">
              Country
            </PinkText>
            <BlueText fontSize="var(--tiny-text)">
              Name
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              Russia
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Level of Interest
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              1000
            </BlueText>
            <BlueText fontSize="var(--tiny-text)">
              Players Number
            </BlueText>
            <BlueText fontSize="var(--middle-text)">
              100
            </BlueText>
          </div>
        </div>
      </UpperBackground>

      <BlueText class="text" fontSize="var(--large-text)">
          Players
      </BlueText>

      <div class="player-column">
        <div class="player-row">
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
        </div>

        <div class="player-row">
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
        </div>

        <div class="player-row">
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
          <PlayerItem></PlayerItem>
        </div>
      </div>
    </div>
  </NavBarView>
</template>


<script lang="ts">
import { defineComponent } from 'vue';
import NavBarView from "@/views/NavBarView.vue";
import BlueText from "@/components/BlueText.vue";
import PinkText from "@/components/PinkText.vue";
import UpperBackground from "@/components/UpperBackground.vue";
import PlayerItem from "@/components/PlayerItem.vue";

export default defineComponent({
  name: "ServerInfoView",
  components: {
    NavBarView,
    PinkText,
    BlueText,
    UpperBackground,
    PlayerItem,
  }
});
</script>


<style scoped>
.container {
  display: flex;
  flex-direction: column;
  margin: 0;
  width: 100%;
  height: 100%;
  justify-content: center;
  align-items: center;
  gap: 15px;
}

.container-row {
  display: grid;
  grid-template-columns: 2fr 2fr 2fr 1fr;
  justify-content: space-between;
  width: 100%;
  gap: 10px;
}

.container-column {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.center {
  border-radius: 0px 50px 0px 50px;
  width: 90%;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 30px;
}

.text {
  display: flex;
  flex-direction: row;
  justify-content: left;
  align-items: left;
  width: 90%
}

.player-row {
  display: flex;
  flex-direction: row;
  gap: 30px;
  padding: 10px;
}

.player-column {
  display: flex;
  flex-direction: column;
  gap: 10px;
  overflow: auto;
  max-height: 330px;
}
</style>
