<template>
 <b-container fluid>
      <b-row style="margin:10px">
        <b-col cols="12" class="text-center" > <h3>Gestione Mail Clienti</h3></b-col>
      </b-row>
      <b-row>       
  <b-table striped hover :items="items" :fields="fields" @row-clicked="rowclicked">
  </b-table>
  </b-row>
  </b-container>
</template>
<script>

import Vue from 'vue';
import EventBus from './event-bus';

export default {
  name:'MailSentAndReceived',
  data () {
    return {
      fields: {        
        to: {
          label: 'Spedita A',
          sortable: true
        },
        from: {
          label: 'Spedita Da',
          sortable: true
        },
        body: {
          label: 'Messaggio',
          sortable: true
        },
        date: {
          label: 'Data',
          sortable: true
        }
      },
      items: []
    }
  },
  created() {
    this.loadMailsSent();    
  },  
  methods: {
    loadMailsSent : function () {
      this.$http.get('/api/MailSentAndReceived/sent').then(response => {
      this.items = response.body;
      }, response => {
        // error callback
      })
    } 
  }
}
</script>