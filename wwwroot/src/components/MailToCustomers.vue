<template>
 <b-container fluid>
    <b-row style="margin:10px">
        <b-col cols="12" class="text-center" > <h3>Invia Mail a Clienti</h3> </b-col>
    </b-row>
    <b-row style="margin:10px">
        <b-col cols="6" class="text-center" > 
            <b-form-fieldset horizontal label-text-align="right" label="Seleziona la Mail:" :label-cols="6">
                <b-form-select :disabled="isBusy" class="form-control" :options="mails" v-model="selectmail">
            </b-form-select>
            </b-form-fieldset>
        </b-col>
        <b-col cols="6" class="text-center" > 
            <b-form-fieldset label-text-align="right" horizontal label="Filtro Clienti:" :label-cols="3">
                <b-form-input :disabled="isBusy" v-model="filter" ></b-form-input>
            </b-form-fieldset>
        </b-col>        
    </b-row>
    <b-row>       
        <b-col cols="12"><customerlist></customerlist></b-col>
    </b-row>
  </b-container>

</template>
<script>
import Vue from 'vue';
import EventBus from './event-bus';
import CustomerList from './CustomerList.vue'

export default {
  name: 'MailToCustomers',
  data () {
    return {
        mails: [],
        selectmail : {},
        isBusy:false,
        filter:''
    }
  },
  created() {
    EventBus.$on('send-mail-ids', this.sendMails)
    this.loadMails();
  },
  methods: {
    loadMails : function () {
      this.$http.get('/api/Mails').then(response => {
      this.mails = this._.map(response.body, function(a){ return {value:a.id, text:a.name}});
      }, response => {
        // error callback
      })
    },
    sendMails : function (checkedItems) {
      this.$http.post('/api/Mails/sendmail/' + selectmail , checkedItems ).then(response => {
      
      }, response => {
        // error callback
      })
    } 
  }
}
</script>

<style>
</style>