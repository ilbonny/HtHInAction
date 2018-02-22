import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '../components/HelloWorld.vue'
import CustomerDetails from '../components/CustomerDetails.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/customerDetails',
      name: 'CustomerDetails',
      component: CustomerDetails
    }
  ]
})
