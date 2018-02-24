<template>
 <b-container fluid>
      <b-row style="margin:10px">
        <b-col cols="12" class="text-center" > <h3>Lista Mail</h3> </b-col>
      </b-row>
      <b-row>       
  <b-table striped hover :items="items" :fields="fields" @row-clicked="rowclicked" >
    
  </b-table>
  </b-row>
  </b-container>

</template>

<script>

import Vue from 'vue';
import EventBus from './event-bus';

export default {
  name:'MailList',
  data () {
    return {
      fields: {
        name: {
          label: 'Nome',
          sortable: true
        },
        from: {
          label: 'Mittente',
          sortable: true
        },
        subject: {
          label: 'Oggetto',
          sortable: true
        },
        body: {
          label: 'Testo',
          sortable: true
        }
      },
      items: []
    }
  },
  created() {
    this.loadMails();
    EventBus.$on('refresh-mail-list', this.loadMails)
  },  
  beforeDestroy(){ EventBus.$off('refresh-mail-list'); },
  methods: {
    loadMails : function () {
      this.$http.get('/api/Mails').then(response => {
      this.items = response.body;
      }, response => {
        // error callback
      })
    } ,
    rowclicked : function (item, index, event) {
      EventBus.$emit('selected-mail', item);
    }  
  }
}
</script>