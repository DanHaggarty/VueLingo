import { createRouter, createWebHistory } from 'vue-router';
import EditorPage from '../pages/EditorPage.vue';
import HistoryPage from '../pages/HistoryPage.vue';
import AboutPage from '../pages/AboutPage.vue';

const routes = [
  { path: '/', name: 'Editor', component: EditorPage },
  { path: '/history', name: 'History', component: HistoryPage },
  { path: '/about', name: 'About', component: AboutPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
