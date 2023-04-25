import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
Vue.config.devtools = true
Vue.config.performance = true


Vue.config.productionTip = false
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')