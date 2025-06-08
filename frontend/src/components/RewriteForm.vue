<template>
  <div class="editor-container">
    <div class="editor-left">
      <h2>Content to AI Optimise</h2>
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
        <option value="Urgent">Urgent</option>
        <option value="Basic">Basic</option>
        <option value="Verbose">Verbose</option>
        <option value="Pirate">Pirate</option>
      </select>

      <button @click="rewriteText"
              :disabled="isLoading"
              class="editor-button">
        {{ isLoading ? 'Rewriting…' : 'Rewrite' }}
      </button>
    </div>

    <div class="editor-output-container" v-if="outputText">
      <h2>AI-Optimised Content</h2>

     
      <div class="editor-right">

        <div class="editor-output-text">
          {{ outputText }}
        </div>
        <button @click="copyOutput" class="copy-button">Copy to clipboard</button>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { ref } from 'vue'
  import { useClipboard } from '@/composables/useClipboard'

  const inputText = ref('')
  const tone = ref('')
  const outputText = ref('')
  const isLoading = ref(false)
  const { copyToClipboard } = useClipboard()

  function copyOutput() {
    if (outputText.value.trim()) {
      copyToClipboard(outputText.value)
    } else {
      alert('Nothing to copy.')
    }
  }

  async function rewriteText() {
    if (!inputText.value.trim()) {
      alert("Please enter some text to rewrite.");
      return;
    }

    if (!tone.value) {
      alert("Please select a tone before rewriting.");
      return;
    }

    isLoading.value = true;
    outputText.value = '';

    try {
      const res = await fetch('http://localhost:5000/rewrite', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ text: inputText.value, tone: tone.value })
      });

      const data = await res.json();
      outputText.value = data.rewrittenText;
    } catch (err) {
      outputText.value = '❌ Error contacting API.';
      console.error(err);
    } finally {
      isLoading.value = false;
    }
  }
</script>

<style scoped>
  .editor-wrapper {
    padding: 2rem;
  }

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
    flex: 0 0 700px;
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
    margin-top: 16px;
    flex: 1;
    min-width: 300px;
    max-width: 400px;
    min-height: 200px;
    background: #ffffff;
    border: 1px solid #ccc;
    border-radius: 10px;
    padding: 1rem;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    box-sizing: border-box;
  }

  .editor-output-title {
    font-size: 1.25rem;
    font-weight: 600;
    color: #1e293b;
    margin-bottom: 0.75rem;
  }

  .editor-output-text {
    flex-grow: 1;
    white-space: pre-wrap;
    line-height: 1.5;
    color: #334155;
    overflow-y: auto;
    max-height: 100%;
  }

  @media (max-width: 900px) {
    .editor-container {
      flex-direction: column;
    }

    .editor-left,
    .editor-output-container,
    .editor-right {
      width: 100%;
    }

    .editor-left {
      flex: 0 0 auto;
    }
  }


</style>

