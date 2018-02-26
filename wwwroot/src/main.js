import Vue from 'vue'
import BootstrapVue from "bootstrap-vue"
import VueResource from "vue-resource"
import App from './App.vue'
import router from './router'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"
import bModal from 'bootstrap-vue/es/components/modal/modal'
import bModalDirective from 'bootstrap-vue/es/directives/modal/modal'

import 'vue-awesome/icons'
import Icon from 'vue-awesome/components/Icon.vue'
import NavBar from './components/NavBar.vue'
import lodash from 'lodash'
import VueLodash from 'vue-lodash'

Vue.component('icon', Icon);
Vue.component('navbar', NavBar);

Vue.use(BootstrapVue);
Vue.use(VueResource);
Vue.use(VueLodash, lodash)

Vue.directive('b-modal', bModalDirective);
Vue.component('b-modal', bModal);

Vue.config.productionTip = false

new Vue({
  el: '#app',
  router,
  components : { Icon, NavBar },
  render: h => h(App)
})
