<template>
  <div>
    <h1>{{ msg }}</h1>
    <PinkText fontSize=96px />
  </div>
</template>

<script lang="ts">
import PinkText from '@/components/PinkText.vue'

export default {
  props: ['msg'],
  components: {
    PinkText
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
