<template>
  <nav class="navbar">
    <RouterLink to="/">
      <div class="logo">VueLingo</div>
    </RouterLink>
    <div class="nav-links">
      <RouterLink to="/">Editor</RouterLink>
      <RouterLink to="/history">History</RouterLink>
      <RouterLink to="/about">About</RouterLink>
    </div>
  </nav>
</template>

<style scoped>
  .navbar {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    height: 60px;
    background-color: #f5f5f5;
    border-bottom: 1px solid #ddd;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 2rem;
    z-index: 1000;
  }

  .logo {
    font-size: 1.25rem;
    font-weight: bold;
  }

  .nav-links a {
    margin-left: 1.5rem;
    text-decoration: none;
    color: #007b7f;
  }
</style>
