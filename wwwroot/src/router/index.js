import Vue from 'vue'
import Router from 'vue-router'
import Customers from '../components/Customers.vue'
import Mails from '../components/Mails.vue'
import EventBus from '../components/event-bus';

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Customers',
      component: Customers
    },
    {
      path: '/customers',
      name: 'Customers',
      component: Customers
    },
    {
      path: '/mails',
      name: 'Mails',
      component: Mails
    }
  ]
})
