<template>
  <div class="history-page">
    <h2>Rewrite History</h2>


    <div class="history-actions">
      <button @click="handleClearHistory" class="clear-button">Clear History</button>
      <button @click="goToEditor" class="back-button">Back to Editor</button>
      <button @click="exportHistoryToCsv" class="export-button">Export to CSV</button>
    </div>

    <div v-if="history.length">
      <div v-for="(item, index) in history" :key="index" class="history-item">
        <p><strong>Time:</strong> {{ new Date(item.timestamp).toLocaleString() }}</p>
        <p><strong>Tone:</strong> {{ item.tone }}</p>
        <p><strong>Language:</strong> {{ item.language || 'None' }}</p>
        <p><strong>Input:</strong> {{ item.input }}</p>
        <p><strong>Output:</strong> {{ item.output.split('\n\n')[0] }}</p>
        <p v-if="item.language && item.output.includes('\n\n')">
          <strong>Translation:</strong>
          {{ item.output.split('\n\n')[1] }}
        </p>
      </div>
    </div>
    <p v-else>No history yet.</p>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import { useHistory } from '@/composables/useHistory'
  import { useRouter } from 'vue-router'
  import { useExportCsv } from '@/composables/useExportCsv'


  const { getHistory, clearHistory } = useHistory()
  const history = ref([])
  const router = useRouter()
  const { exportHistoryToCsv } = useExportCsv()

  onMounted(() => {
    history.value = getHistory()
  })

  function handleClearHistory() {
    clearHistory()
    history.value = [] // 🧹 update view immediately
  }

  function goToEditor() {
    router.push({ name: 'Editor' })
  }
</script>

<style scoped>
  .history-page {
    max-width: 800px;
    margin: auto;
    padding: 2rem;
  }

  .history-actions {
    display: flex;
    justify-content: flex-start;
    gap: 1rem;
    margin-bottom: 1.5rem;
  }

    .history-actions button {
      padding: 0.5rem 1rem;
      border-radius: 8px;
      border: none;
      color: white;
      font-size: 1rem;
      cursor: pointer;
      transition: background-color 0.2s ease, transform 0.1s ease;
    }

  .clear-button {
    background-color: #ef4444; /* red */
  }

  .back-button {
    background-color: #0d9488; /* teal */
  }

  .export-button {
    background-color: #10b981; /* green */
  }

  .history-actions button:hover {
    opacity: 0.9;
  }

  .history-actions button:active {
    transform: scale(0.98);
  }

  .history-item {
    border: 1px solid #ccc;
    padding: 1rem;
    border-radius: 10px;
    margin-bottom: 1rem;
  }
</style>
