<template>
  <div class="editor-wrapper">
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
      <option value="child">Child</option>
    </select>

    <button @click="rewriteText"
            :disabled="isLoading"
            class="editor-button">
      {{ isLoading ? 'Rewriting…' : 'Rewrite' }}
    </button>

    <div v-if="outputText" class="editor-output">
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
  .editor-wrapper {
    max-width: 700px;
    margin: 0 auto;
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 2rem 1rem;
  }

  .editor-title {
    font-size: 1.5rem;
    text-align: center;
    margin-bottom: 1rem;
  }

  .editor-textarea {
    width: 100%;
    padding: 1rem;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 8px;
    resize: vertical;
  }

  .editor-select {
    padding: 0.5rem;
    font-size: 1rem;
    border-radius: 6px;
    border: 1px solid #ccc;
  }

  .editor-button {
    padding: 0.75rem;
    font-size: 1rem;
    background-color: #0d9488;
    color: white;
    border: none;
    border-radius: 6px;
    cursor: pointer;
  }

    .editor-button:disabled {
      background-color: #94a3b8;
      cursor: not-allowed;
    }

  .editor-output {
    margin-top: 2rem;
    background: #f1f5f9;
    padding: 1rem;
    border-left: 4px solid #0d9488;
    border-radius: 6px;
  }
</style>


<!--<script setup>
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
      const response = await fetch('http://localhost:5218/rewrite', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          text: inputText.value,
          tone: tone.value
        })
      })

      const data = await response.json()
      outputText.value = data.rewrittenText
    } catch (err) {
      outputText.value = '❌ Error contacting rewrite service.'
      console.error(err)
    } finally {
      isLoading.value = false
    }
  }
</script>
<template>
  <div class="editor-wrapper">
    <h2 class="editor-title">Rewrite Your Content</h2>

    <textarea
      v-model="inputText"
      class="editor-textarea"
      rows="10"
      placeholder="Paste your text here..."
    ></textarea>

    <select v-model="tone" class="editor-select">
      <option disabled value="">Select tone</option>
      <option value="formal">Formal</option>
      <option value="casual">Casual</option>
      <option value="seo">SEO</option>
    </select>

    <button
      @click="rewriteText"
      :disabled="isLoading"
      class="editor-button"
    >
      {{ isLoading ? 'Rewriting…' : 'Rewrite' }}
    </button>

    <div v-if="outputText" class="editor-output">
      <h3>Output</h3>
      <p>{{ outputText }}</p>
    </div>
  </div>
</template>-->
