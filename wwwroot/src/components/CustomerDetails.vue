<template>
  <div id="app">
    <b-container fluid>
      <b-row>
        <b-col cols="12" class="text-center" > <h3>Anagrafica Cliente</h3> </b-col>
      </b-row>
      <b-row>
        <b-col cols="2">
          
        </b-col>
        <b-col cols="8" >
        <b-form @submit="onSubmit" @reset="onReset" v-if="show">
          
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
            <b-form-group id="inputGroup6" label="Tipo:" label-for="inputType">
             <b-form-select id="inputType" :options="types" required v-model="form.type"></b-form-select>
           </b-form-group>
           <b-button type="submit" variant="primary">Salva</b-button>
           <b-button type="reset" variant="danger">Azzera</b-button>          
          </b-form>
        
        </b-col>        
        <b-col cols="2"></b-col>
      </b-row>
    </b-container>    
  </div>
</template>

<script>
export default { 
  name: "app",
  data() {
    return {
      form: {
        email: "",
        name: "",
        surname: "",
        city: "",
        address :"",
        phone :"",
        type: null        
      },
      types: [
        { text: "Seleziona il tipo", value: null },
        "Nuovo",
        "Cliente"
      ],
      show: true
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      this.$http.post('/api/Customers', JSON.stringify(this.form)).then(response => {
          alert(response.status);
          //response.statusText;
          //response.headers.get('Expires');
          //this.someData = response.body;
      }, response => {
        // error callback
      });
    },
    onReset(evt) {
      evt.preventDefault();
      this.form.email = "";
      this.form.name = "";
      this.form.surname = "";
      this.form.city = ""; 
      this.form.address = "";
      this.form.phone ="";
      this.form.type = null;      
      this.show = false;

      this.$nextTick(() => {
        this.show = true;
      });
    }
}
};
</script>
