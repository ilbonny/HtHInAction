import Vue from 'vue'
import BootstrapVue from "bootstrap-vue"
import App from './App.vue'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"
import bModal from 'bootstrap-vue/es/components/modal/modal'
import bModalDirective from 'bootstrap-vue/es/directives/modal/modal'

Vue.use(BootstrapVue);
Vue.component('b-modal', bModal);
Vue.directive('b-modal', bModalDirective);

new Vue({
  el: '#app',
  render: h => h(App)
})
