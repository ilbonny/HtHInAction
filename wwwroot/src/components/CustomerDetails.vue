<template> 
    <b-container fluid>
      <b-row>
        <b-col cols="12" class="text-center" > <h3>Anagrafica Cliente</h3> </b-col>
      </b-row>
      <b-row>        
          <b-col cols="12" >
          <b-form @submit="onSubmit" @reset="onReset">       
           <b-form-group id="inputGroup6" label="Codice Incaricato:" label-for="inputDelegate">
             <b-form-input id="inputDelegate" type="text" v-model="form.delegate" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup0" label="Email:" label-for="inputEmail">
             <b-form-input id="inputEmail" type="text" v-model="form.email" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup1" label="Nome:" label-for="inputName">
             <b-form-input id="inputName" type="text" v-model="form.name" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup2" label="Cognome:" label-for="inputSurname">
             <b-form-input id="inputSurname" type="text" v-model="form.surname" required > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup3" label="Città:" label-for="inputCity">
             <b-form-input id="inputCity" type="text" v-model="form.city"  > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup4" label="Indirizzo:" label-for="inputAddress">
             <b-form-input id="inputAddress" type="text" v-model="form.address"  > </b-form-input>
           </b-form-group>
           <b-form-group id="inputGroup5" label="Telefono:" label-for="inputPhone">
             <b-form-input id="inputPhone" type="text" v-model="form.phone"  > </b-form-input>
           </b-form-group>            
          <b-button type="reset" variant="secondary"  >Nuovo</b-button>   
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
  name: "CustomerDetails",
  created() { EventBus.$on('selected-customer', this.selectedCustomer) },
  beforeDestroy(){ EventBus.$off('selected-customer'); },
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
        this.$http.post('/api/Customers', JSON.stringify(this.form)).then(response => {
           EventBus.$emit('refresh-customer-list');        
        }, response => {
         // error callback
        });  
      }
      else{
        this.$http.put('/api/Customers/' + this.form.id, JSON.stringify(this.form)).then(response => {
           EventBus.$emit('refresh-customer-list');        
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
      this.$http.delete('/api/Customers/' + this.form.id).then(response => {
          EventBus.$emit('refresh-customer-list');  
          this.form = {}      
      }, response => {
        // error callback
      });
    },
    selectedCustomer(mail){
      this.form = mail;
      this.isNew = false;
    }
}
};
</script>
