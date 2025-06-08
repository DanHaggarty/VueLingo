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
</template>
