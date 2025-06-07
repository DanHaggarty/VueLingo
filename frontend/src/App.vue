<template>
  <div id="app">
    <Navbar />
    <main class="p-4">
      <RouterView />
    </main>
  </div>
</template>

<script setup>
  import Navbar from './components/Navbar.vue';
</script>

<style scoped>
  #app {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    color: #333;
  }
</style>
