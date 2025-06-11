<template>
  <div class="about-page">
    <h1>🧠 About VueLingo</h1>

    <section>
      <h2>What is VueLingo?</h2>
      <p>
        <strong>VueLingo</strong> is a lightweight AI-powered content rewriting tool. It lets you paste any text, choose a tone (like formal, casual, SEO, or even “pirate”), and optionally translate the output into another language — all in a clean, fast, and responsive web interface.
      </p>
      <p>
        It’s designed to help content creators, marketers, and multilingual teams quickly optimize or adapt copy for different tones and audiences.
      </p>
    </section>

    <section>
      <h2>How does it work?</h2>
      <ul>
        <li>The frontend is built with <strong>Vue 3</strong>, using script setup, scoped styles, and composables.</li>
        <li>The backend is a <strong>.NET Core API</strong> that calls OpenAI’s GPT model.</li>
        <li>User input, selected tone, and optional translation language are sent to the backend.</li>
        <li>The backend returns AI-rewritten content, optionally translated.</li>
        <li>Rewrites are stored in <strong>session history</strong> (MVP-style, no login or DB), and can be exported to CSV for review or re-use.</li>
      </ul>
    </section>

    <section>
      <h2>Why I built it</h2>
      <p>
        I'm currently applying for a role at <strong>LanguageWire</strong> — a company focused on content localization and AI solutions. While I have strong full-stack experience, especially with <strong>.NET Core</strong>, I haven’t had the chance to use Vue commercially.
      </p>
      <p>
        So I built VueLingo as a way to:
      </p>
      <ul>
        <li>Prove I can pick up and effectively use <strong>Vue</strong> in a real project.</li>
        <li>Show how I approach building <strong>AI-powered, multilingual</strong> tools.</li>
        <li>Demonstrate thoughtful UX and clean code.</li>
      </ul>
      <p>
        This project was designed, built, and deployed independently — from frontend layout to backend integration and Azure deployment.
      </p>
    </section>
  </div>
</template>

<style scoped>
  .about-page {
    max-width: 800px;
    margin: auto;
    padding: 2rem;
    line-height: 1.6;
  }

    .about-page h1 {
      font-size: 2rem;
      margin-bottom: 1.5rem;
    }

    .about-page h2 {
      font-size: 1.4rem;
      margin-top: 2rem;
      margin-bottom: 1rem;
      color: #0f766e;
    }

    .about-page ul {
      padding-left: 1.25rem;
      list-style-type: disc;
    }

    .about-page p {
      margin-bottom: 1rem;
    }
</style>
