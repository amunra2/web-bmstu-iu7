<template>
  <NavBarView>
    <div class="container">
      <BlueText class="text" fontSize="var(--huge-text)">
        Suggest Server
      </BlueText>

      <UpperBackground class="center">
        <div class="container-row">
          <div class="container-column">
            <PinkText style="text-align: center" fontSize="var(--large-text)">Base</PinkText>
            <FormField>Ip</FormField>
            <FormField>Game</FormField>
            <FormField>Rating</FormField>
          </div>

          <div class="container-column">
            <PinkText style="text-align: center" fontSize="var(--large-text)">Additional</PinkText>
            <FormFieldSelect>Hosting</FormFieldSelect>
            <FormFieldSelect>Platform</FormFieldSelect>
            <FormFieldSelect>Country</FormFieldSelect>
          </div>
        </div>

        <Button>Suggest</Button>
      </UpperBackground>
    </div>
  </NavBarView>
</template>

<script>
import {defineComponent} from 'vue';
import NavBarView from "@/views/NavBarView.vue";
import BlueText from "@/components/BlueText.vue";
import PinkText from "@/components/PinkText.vue";
import UpperBackground from "@/components/UpperBackground.vue";
import FormField from "@/components/FormField.vue"
import FormFieldDropDown from "@/components/FormFieldDropDown.vue"
import FormFieldSelect from "@/components/FormFieldSelect.vue"
import Button from "@/components/Button.vue"

export default defineComponent({
  name: "SuggestServer",
  components: {
    NavBarView,
    BlueText,
    PinkText,
    UpperBackground,
    FormField,
    FormFieldDropDown,
    Button,
    FormFieldSelect,
  }
})
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
  gap: 30px;
}

.center {
  border-radius: 0px 50px 0px 50px;
  /* height: %; */
  width: 90%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 30px;
  gap: 50px;
}

.text {
  display: flex;
  flex-direction: row;
  justify-content: left;
  align-items: left;
  width: 90%
}

.container-row {
  /* display: grid;
  grid-template-columns: 2fr 2fr 2fr 1fr; */
  display: flex;
  flex-direction: row;
  gap: 100px;
}

.container-column {
  display: flex;
  flex-direction: column;
  width: 700px;
  gap: 30px;
}
</style>
