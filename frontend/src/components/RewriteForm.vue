<template>
  <div class="flex flex-col gap-4 max-w-2xl mx-auto">
    <textarea v-model="inputText"
              placeholder="Enter your text here..."
              rows="6"
              class="p-2 border border-gray-300 rounded"></textarea>

    <select v-model="tone" class="p-2 border border-gray-300 rounded w-48">
      <option disabled value="">Select tone</option>
      <option value="formal">Formal</option>
      <option value="casual">Casual</option>
      <option value="seo">SEO</option>
    </select>

    <button @click="rewriteText"
            class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 w-32">
      Rewrite
    </button>

    <div v-if="outputText" class="mt-4 p-4 bg-gray-100 border rounded">
      <h3 class="font-bold mb-2">Rewritten Output:</h3>
      <p>{{ outputText }}</p>
    </div>
  </div>
</template>

<script setup>
  import { ref } from 'vue';

  const inputText = ref('');
  const tone = ref('');
  const outputText = ref('');

  function rewriteText() {
    if (!inputText.value || !tone.value) {
      alert('Please enter text and select a tone.');
      return;
    }

    // Simulate AI response
    outputText.value = `🔁 [${tone.value.toUpperCase()} rewrite]: ${inputText.value}`;
  }
</script>
