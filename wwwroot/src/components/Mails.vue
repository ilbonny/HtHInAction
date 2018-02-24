<template>
  <b-container fluid>
    <b-row>
        <b-col cols="8"><maillist></maillist></b-col>
        <b-col cols="4"><maildetails></maildetails></b-col>        
    </b-row>
</b-container>

</template>

<script>

import Vue from 'vue'

import MailList from './MailList.vue'
import MailDetails from './MailDetails.vue'

Vue.component('maillist', MailList);
Vue.component('maildetails', MailDetails);


export default{
  name: 'Mails',  
}
</script>
