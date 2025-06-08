<template>
  <div class="history-page">
    <h2>Rewrite History</h2>
    <button @click="handleClearHistory" class="clear-button">Clear History</button>

    <div v-if="history.length">
      <div v-for="(item, index) in history" :key="index" class="history-item">
        <p><strong>Time:</strong> {{ new Date(item.timestamp).toLocaleString() }}</p>
        <p><strong>Tone:</strong> {{ item.tone }}</p>
        <p><strong>Language:</strong> {{ item.language || 'None' }}</p>
        <p><strong>Input:</strong> {{ item.input }}</p>
        <p><strong>Output:</strong> {{ item.output }}</p>
      </div>
    </div>
    <p v-else>No history yet.</p>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import { useHistory } from '@/composables/useHistory'

  const { getHistory, clearHistory } = useHistory()
  const history = ref([])

  onMounted(() => {
    history.value = getHistory()
  })

  function handleClearHistory() {
    clearHistory()
    history.value = [] // 🧹 update view immediately
  }
</script>

<style scoped>
  .history-page {
    max-width: 800px;
    margin: auto;
    padding: 2rem;
  }

  .history-item {
    border: 1px solid #ccc;
    padding: 1rem;
    border-radius: 10px;
    margin-bottom: 1rem;
  }

  .clear-button {
    background-color: #ef4444;
    color: white;
    padding: 0.5rem 1rem;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    margin-bottom: 1rem;
  }
</style>
