# VueLingo ✨

**VueLingo** is a lightweight Vue 3 + TypeScript web application that uses AI to rewrite and optionally translate user-provided content. The project showcases frontend integration with a .NET 8 backend API and OpenAI GPT models, built to explore language optimization, tone adaptation, and seamless user experience.

---

## 🚀 Features

- 🔄 Rewrites content with selectable tones (Formal, Casual, SEO, etc.)
- 🌍 Optional translation into multiple languages
- 🧠 Intelligent tone override using custom input
- 🗃️ Session-based rewrite history (stored in-browser)
- 📤 Export history to CSV
- 🧩 Clean, responsive UI with Vue 3 Composition API
- ⚙️ .NET 8 Minimal API backend
- ☁️ Deployed with Azure App Services and Static Web Apps

---

## 📷 Live Demo

**Frontend:** [https://happy-dune-0e302d803.6.azurestaticapps.net](https://happy-dune-0e302d803.6.azurestaticapps.net)  
**API:** Secured via Azure; deployed separately

---

## 🧠 Project Purpose

This project was developed to:

- Demonstrate modern Vue 3 and TypeScript proficiency
- Explore practical AI integration with frontend/backend separation
- Experiment with tone control and multilingual content generation
- Provide a simple, shareable UI for testing content variations

---

## 🛠️ Tech Stack

| Layer       | Technology                      |
|-------------|----------------------------------|
| Frontend    | Vue 3, Vite, TypeScript, SCSS    |
| Backend     | .NET 8 Minimal API, OpenAI       |
| State Mgmt  | `sessionStorage`, composables    |
| Deployment  | Azure Static Web Apps + App Service |
| CI/CD       | GitHub Actions                   |

---

## 🧪 Project Structure

```
/frontend        → Vue 3 app
/backend         → .NET Core Minimal API
/composables     → Clipboard, History, CSV helpers
/data            → Tone and language config JSON
```

---

## ⚙️ Running Locally

1. **Clone the repo**

```bash
git clone https://github.com/your-username/vueling-app.git
cd vuelingo
```

2. **Backend setup**

- Set your OpenAI API key in `appsettings.json`

```json
{
  "OpenAI": {
    "ApiKey": "your-openai-key"
  }
}
```

- Run the API:

```bash
cd VueLingo.WebApi
dotnet run
```

3. **Frontend setup**

```bash
cd frontend
npm install
```

4. **Important:** Edit `RewriteForm.vue` to use local API endpoint:

```ts
const res = await fetch('http://localhost:5000/rewrite', { ... })
```

5. **Run frontend**

Open a separate Visual Studio or suitable IDE.
Open folder VueLingo/frontend in IDE
```bash
npm run dev
```

Then open: [http://localhost:5173](http://localhost:5173)

---

## 📤 Export Feature

- View history from the navigation bar
- Export your full rewrite history as CSV
- Separates native vs translated outputs cleanly in export

---

## 📈 Future Enhancements

- Persistent user sessions via login
- Previews with Markdown/HTML support
- Multi-step content pipelines (e.g. rewrite → translate → format)

---

## 📄 License

MIT License – free for personal and commercial use.
