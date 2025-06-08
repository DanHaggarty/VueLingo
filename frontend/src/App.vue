<!--<template>
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
</style>-->

<template>
  <div class="app-wrapper">
    <Navbar />
    <main class="main-content">
      <RouterView />
    </main>
  </div>
</template>

<script setup>
  import Navbar from './components/Navbar.vue'
</script>

<style scoped>
  .app-wrapper {
    width: 100%;
    margin: 0;
    padding: 0;
  }

  .main-content {
    width: 100%;
    padding-top: 70px; /* account for fixed nav */
  }
</style>
