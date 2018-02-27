<template>
 <b-container fluid>
      <b-row style="margin:10px">
        <b-col cols="12" class="text-center" > <h3>Lista Clienti</h3> </b-col>
      </b-row>
      <b-row>       
  <b-table striped hover :items="items" :fields="fields" @row-clicked="rowclicked" >
     <template slot="actions" slot-scope="item">
         <input type="checkbox" name="checked" :key="item.index" :value="item.item" @click.stop v-model="checkedItems" @change="updateCheck" >
      </template>
  </b-table>
  </b-row>
  </b-container>
</template>
<script>

import Vue from 'vue';
import EventBus from './event-bus';

export default {
  name:'CustomerList',
  data () {
    return {
      fields: {        
        actions: {
          label: 'Sel.','class': 'text-right'
        },
        email: {
          label: 'Email',
          sortable: true
        },
        name: {
          label: 'Nome',
          sortable: true
        },
        surname: {
          label: 'Cognome',
          sortable: true
        },
        city: {
          label: 'Città',
          sortable: true
        },
        address: {
          label: 'Indirizzo',
          sortable: true
        },
        phone: {
          label: 'Telefono',
          sortable: true
        }
      },
      items: [],
      checkedItems : []
     
    }
  },
  created() {
    this.loadCustomers();
    EventBus.$on('refresh-customer-list', this.loadCustomers)
    EventBus.$on('find-customers', this.findCustomers)
  },  
  beforeDestroy(){ 
    EventBus.$off('refresh-customer-list'); 
    EventBus.$off('find-customers'); 
  },
  methods: {
    loadCustomers : function () {
      this.$http.get('/api/Customers').then(response => {
      this.items = response.body;
      }, response => {
        // error callback
      })
    } ,
    rowclicked : function (item, index, event) {
      EventBus.$emit('selected-customer', item);
    },
    updateCheck: function () {
      EventBus.$emit('check-customer', this.checkedItems);
    },
    findCustomers : function (filter) {
      this.$http.get('/api/Customers/find/' + filter).then(response => {
      this.items = response.body;
      }, response => {
        // error callback
      })
    }
  }
}
</script>