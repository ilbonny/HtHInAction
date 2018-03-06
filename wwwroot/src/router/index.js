import Vue from 'vue'
import Router from 'vue-router'
import Customers from '../components/Customers.vue'
import MailToCustomers from '../components/MailToCustomers.vue'
import Mails from '../components/Mails.vue'
import MailSentAndReceived from '../components/MailSentAndReceived.vue'
import EventBus from '../components/event-bus';

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'MailToCustomers',
      component: MailToCustomers
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
    },
    {
      path: '/mailsmanage',
      name: 'MailSentAndReceived',
      component: MailSentAndReceived
    }
  ]
})
