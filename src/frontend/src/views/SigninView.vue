<template>
    <body class="container">
        <UpperBackground class="center">
            <UpperBackground class="login-header">
                <BlueText fontSize="var(--middle-text)">ServerING</BlueText>
                <PinkText fontSize="var(--large-text)">Sign In</PinkText>
            </UpperBackground>
            <FormField>Login</FormField>
            <FormField>Password</FormField>
            <Button>Sign In</Button>
            <BlueText class="text" fontSize="var(--tiny-text)">
                Don't have an account?
            </BlueText>
            <PinkText class="text" fontSize="var(--tiny-text)">
              <router-link style="color: var(--magenta)" to="/signup">Sign Up</router-link>
            </PinkText>
        </UpperBackground>
    </body>
</template>


<script lang="ts">
import { defineComponent } from "vue";
import UpperBackground from "@/components/UpperBackground.vue"
import BlueText from "@/components/BlueText.vue"
import PinkText from "@/components/PinkText.vue"
import FormField from "@/components/FormField.vue"
import Button from "@/components/Button.vue"

export default defineComponent({
    name: "SignupView",
    components: {
        UpperBackground,
        BlueText,
        PinkText,
        FormField,
        Button,
    }
})
</script>


<style scoped>
.container {
  display: flex;
  margin: 0;
  width: 100%;
  height: 100%;
  justify-content: center;
  align-items: center;
}
.center {
  /* position: relative; */
  border-radius: 0px 50px 0px 50px;
  height: 75%;
  width: 25%;
  display: grid;
  justify-content: center;
  align-items: center;
  padding: 15px;
}

.login-header {
    align-items: center;
    border-radius: 0px 0px 0px 30px;
    display: flex;
    flex-direction: column;
    overflow: hidden;
    padding: 15px 20px;
    width: fit-content;
    box-shadow: 0px 0px 60px var(--magenta);
}

.text {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
</style>
