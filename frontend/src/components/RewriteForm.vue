<template>
  <div class="editor-container">
    <div class="editor-left">
      <h2 class="editor-title">Rewrite Your Content</h2>

      <textarea v-model="inputText"
                class="editor-textarea"
                rows="10"
                placeholder="Paste your text here..."></textarea>

      <select v-model="tone" class="editor-select">
        <option disabled value="">Select tone</option>
        <option value="formal">Formal</option>
        <option value="casual">Casual</option>
        <option value="seo">SEO</option>
      </select>

      <button @click="rewriteText"
              :disabled="isLoading"
              class="editor-button">
        {{ isLoading ? 'Rewriting…' : 'Rewrite' }}
      </button>
    </div>

    <div class="editor-right" v-if="outputText">
      <h3>Output</h3>
      <p>{{ outputText }}</p>
    </div>
  </div>
</template>

<script setup>
  import { ref } from 'vue'

  const inputText = ref('')
  const tone = ref('')
  const outputText = ref('')
  const isLoading = ref(false)

  async function rewriteText() {
    if (!inputText.value || !tone.value) return
    isLoading.value = true
    outputText.value = ''

    try {
      const res = await fetch('http://localhost:5000/rewrite', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ text: inputText.value, tone: tone.value })
      })

      const data = await res.json()
      outputText.value = data.rewrittenText
    } catch (err) {
      outputText.value = '❌ Error contacting API.'
      console.error(err)
    } finally {
      isLoading.value = false
    }
  }
</script>

<style scoped>
/*  .editor-container {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: center;
    gap: 2rem;
    padding: 2rem;
    box-sizing: border-box;
    flex-wrap: nowrap;
  }

  .editor-left {
    width: 60%;
    max-width: 700px;
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }*/
  .editor-container {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: center;
    gap: 2rem;
    padding: 2rem;
    flex-wrap: nowrap;
  }

  .editor-left {
    flex: 0 0 700px; /* ✅ FIXED WIDTH, won't shrink or grow */
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }

  .editor-title {
    font-size: 1.75rem;
    margin-bottom: 0.5rem;
    color: #1e293b;
    text-align: center;
  }

  .editor-textarea {
    width: 100%;
    min-height: 200px;
    padding: 1rem;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 10px;
  }

  .editor-select,
  .editor-button {
    padding: 0.65rem;
    font-size: 1rem;
    border-radius: 10px;
    border: 1px solid #ccc;
  }

  .editor-button {
    background-color: #0d9488;
    color: white;
    border: none;
    cursor: pointer;
  }

    .editor-button:disabled {
      background-color: #94a3b8;
    }

  .editor-right {
    width: 35%;
    min-width: 280px;
    background: #f1f5f9;
    border-left: 4px solid #0d9488;
    padding: 1rem;
    border-radius: 10px;
    word-break: break-word;
  }

  /* 📱 Stack on small screens */
  @media (max-width: 900px) {
    .editor-container {
      flex-direction: column;
    }

    .editor-left,
    .editor-right {
      width: 100%;
    }
  }


</style>

