VueLingo – AI-Powered Content Optimiser 🌐🧠
VueLingo is a full-stack AI application that rewrites and optionally translates user-provided text in various tones.

✨ Features
🔁 AI Rewrite – Rewrites text in tones like Formal, Casual, SEO, Child, Urgent, etc - alternatively you can enter your own freeform tone.

🌍 Optional Translation – Translate output into languages like Spanish, German, French, Portuguese, etc.

🕓 Session History – Stores recent rewrites with input, output, tone, and language.

📄 CSV Export – Easily export session history for reuse or analysis.

🔗 Routing – Navigate between Editor, History, and About pages with Vue Router.

💾 Local/Cloud Hosting – Easily runs locally or deploys to Azure Static Web Apps + App Service.

🧰 Tech Stack
Frontend
Vue 3 + Composition API + <script setup>

Vue Router, scoped CSS

Client-side sessionStorage for history

Hosted on Azure Static Web Apps

Backend
.NET 8 Web API

OpenAI GPT-3.5 API integration

CORS support for local and cloud environments

Hosted on Azure App Service
	- frontend: https://vuelingo.azurestaticapps.net
	- api: https://vueling-api-demo-dygwfjfgcjemgtg8.westeurope-01.azurewebsites.net/swagger/index.html

CI/CD with GitHub Actions

Debugging locally
When running the solution locally please follow these steps
	- VueLingo\frontend\src\components\RewriteForm.vue - amend 'const apiUrl = 'https://vueling-api-demo-dygwfjfgcjemgtg8.westeurope-01.azurewebsites.net'
	  value to 'const apiUrl = 'http://localhost:5000'
	- Start the API locally:
		  - Open the VueLingo solution in Visual Studio
		  - Open Developer Command Prompt
		  - cd VueLingo.WebApi
	      - Run `dotnet run`
	- Start the Vue frontend locally:
	- Open developer commanlin in the VueLingo solution
		  - Open the VueLingo\frontend folder in visual studio
		  - Open Developer Command Prompt
		  - Run `npm run dev`
