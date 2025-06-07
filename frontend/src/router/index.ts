import { createRouter, createWebHistory } from 'vue-router';
import EditorPage from '../pages/EditorPage.vue';
import HistoryPage from '../pages/HistoryPage.vue';

const routes = [
  { path: '/', name: 'Editor', component: EditorPage },
  { path: '/history', name: 'History', component: HistoryPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
