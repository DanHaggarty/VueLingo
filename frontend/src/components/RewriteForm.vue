<template>
  <!-- Editor fields -->
  <div class="editor-container">
    <!-- Text input -->
    <div class="editor-left">
      <h2>Content to AI Optimise</h2>
      <textarea v-model="inputText"
                class="editor-textarea"
                rows="10"
                placeholder="Paste your text here..."></textarea>

      <!-- Tone selection -->
      <select v-model="tone" class="editor-select">
        <option disabled value="">Select tone</option>
        <option v-for="t in tones" :key="t" :value="t">{{ t }}</option>
      </select>

      <!-- Custom tone input -->
      <input v-model="customTone"
             class="editor-custom-tone"
             type="text"
             placeholder="Type your own tone (e.g. Friendly, Legal, Dramatic)"
             title="Entering a custom tone will override the preset tone selected"
             @input="clearPresetTone" />

      <!-- Optional language selection -->
      <select v-model="selectedLanguage" class="editor-select">
        <option value="">No translation</option>
        <option v-for="lang in languages" :key="lang" :value="lang">{{ lang }}</option>
      </select>

      <!-- Submit button -->
      <button @click="rewriteText"
              :disabled="isLoading"
              class="editor-button">
        {{ isLoading ? 'Rewriting…' : 'Rewrite' }}
      </button>
    </div>

    <!-- Text output -->
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
  import toneList from '@/data/tones.json'
  import languageList from '@/data/languages.json'

  const inputText = ref('')
  const tones = ref(toneList)
  const tone = ref('')
  const customTone = ref('')
  const languages = ref(languageList)
  const selectedLanguage = ref('')
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

  function clearPresetTone() {
    if (customTone.value) {
      tone.value = ''  // Deselect the preset tone if custom tone is provided
    }
  }

  async function rewriteText() {
    const selectedTone = customTone.value.trim() || tone.value;

    if (!inputText.value.trim()) {
      alert("Please enter some text to rewrite.");
      return;
    }

    if (!selectedTone) {
      alert("Please select or enter a tone before rewriting.");
      return;
    }

    isLoading.value = true;
    outputText.value = '';

    try {
      const apiUrl = 'https://vueling-api-demo-dygwfjfgcjemgtg8.westeurope-01.azurewebsites.net';
      const displayedApiUrl = apiUrl; //

      const res = await fetch(`${apiUrl}/rewrite`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          text: inputText.value,
          tone: selectedTone,
          translateTo: selectedLanguage.value || null
        })
      });

      const data = await res.json();
      outputText.value = data.rewrittenText;

      saveToHistory({
        input: inputText.value,
        output: data.rewrittenText,
        tone: selectedTone,
        language: selectedLanguage.value || null,
        timestamp: new Date().toISOString()
      });

    } catch (err) {
      outputText.value = '❌ Error contacting API.';
      console.error(err);
    } finally {
      isLoading.value = false;
    }
  }

  import { useHistory } from '@/composables/useHistory'
  const { saveToHistory } = useHistory()

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

  .editor-custom-tone {
    padding: 0.65rem;
    font-size: 1rem;
    border-radius: 10px;
    border: 1px solid #ccc;
    margin-top: 0.5rem;
    width: 100%;
    box-sizing: border-box;
    transition: border-color 0.2s, box-shadow 0.2s;
  }

    .editor-custom-tone:focus {
      border-color: #0d9488;
      box-shadow: 0 0 0 2px rgba(13, 148, 136, 0.2);
      outline: none;
    }

  .copy-button {
    margin-top: 1rem;
    padding: 0.65rem 1rem;
    font-size: 1rem;
    border-radius: 10px;
    border: none;
    background-color: #64748b; /* slate-500 */
    color: white;
    cursor: pointer;
    transition: background-color 0.2s ease;
  }

    .copy-button:hover {
      background-color: #475569; /* slate-600 */
    }

    .copy-button:active {
      background-color: #334155; /* slate-700 */
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

