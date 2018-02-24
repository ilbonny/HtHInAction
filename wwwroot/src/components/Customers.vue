<template>
  <b-container fluid>
    <b-row>
        <b-col cols="8"><customerlist></customerlist></b-col>
        <b-col cols="4"><customerdetails></customerdetails></b-col>        
    </b-row>
</b-container>

</template>

<script>

import Vue from 'vue'

import CustomerList from './CustomerList.vue'
import CustomerDetails from './CustomerDetails.vue'

Vue.component('customerlist', CustomerList);
Vue.component('customerdetails', CustomerDetails);

export default{
  name: 'Customers',  
}
</script>
