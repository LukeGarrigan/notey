import Vue from 'vue';
import VueRouter, {RouteConfig} from 'vue-router';
import Markdown from '@/components/Markdown.vue';

Vue.use(VueRouter);

const routes: RouteConfig[] = [
    {
        path: '/note/:noteId',
        name: 'note-page',
        component: Markdown,
        props: true,
    }

];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
});

export default router;
