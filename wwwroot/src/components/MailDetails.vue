<template>  
    <b-container fluid>
      <b-row style="margin:10px">
        <b-col cols="12" class="text-center" > <h3>Anagrafica Mail</h3> </b-col>
      </b-row>
      <b-row>       
        <b-col cols="12" >
        <b-form @submit="onSubmit" @reset="onReset">          
           <b-form-group id="inputGroup0" label="Nome:" label-for="inputName">
             <b-form-input id="inputName" type="text" v-model="form.name" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup1" label="Mittente:" label-for="inputFrom">
             <b-form-input id="inputFrom" type="text" v-model="form.from" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup2" label="Soggetto:" label-for="inputSubject">
             <b-form-input id="inputSubject" type="text" v-model="form.subject" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup3" label="Testo:" label-for="inputBody">
             <b-form-textarea id="inputBody" v-model="form.body" :rows="8" />
           </b-form-group>
           <b-button type="reset" variant="secondary"  >Nuova</b-button>   
           <b-button type="submit" variant="primary" :disabled="!form" >Salva</b-button>
           <b-button type="delete" variant="danger" @click="onDelete" :disabled="!form">Cancella</b-button>       
          </b-form>        
        </b-col>             
      </b-row>
    </b-container>    
    
</template>

<script>

import Vue from 'vue';
import EventBus from './event-bus';

export default { 
  name: "MailDetails",
  created() { EventBus.$on('selected-mail', this.selectedMail) },
  beforeDestroy(){ EventBus.$off('selected-mail'); },
  data() {
    return {
      form: {},
      isNew: true
    };
  },
  methods: {    
    onSubmit(evt) {
      evt.preventDefault();

      if(this.isNew)
      {
        this.$http.post('/api/Mails', JSON.stringify(this.form)).then(response => {
           EventBus.$emit('refresh-mail-list');        
        }, response => {
         // error callback
        });  
      }
      else{
        this.$http.put('/api/Mails/' + this.form.id, JSON.stringify(this.form)).then(response => {
           EventBus.$emit('refresh-mail-list');        
        }, response => {
         // error callback
        });
      }
      
    },
    onReset(evt) {
      evt.preventDefault();
      this.form = {}    
      this.isNew = true;
    },
    onDelete(evt){
      evt.preventDefault();
      this.$http.delete('/api/Mails/' + this.form.id).then(response => {
          EventBus.$emit('refresh-mail-list');  
          this.form = {}      
      }, response => {
        // error callback
      });
    },
    selectedMail(mail){
      this.form = mail;
      this.isNew = false;
    }
}
};
</script>
