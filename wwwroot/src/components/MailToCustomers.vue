<template>
 <b-container fluid>
    <b-row style="margin:10px">
        <b-col cols="12" class="text-center" > <h3>Invia Mail a Clienti</h3> </b-col>
    </b-row>
    <b-row style="margin:10px">
        <b-col cols="6" class="text-center" > 
            <b-form-fieldset horizontal label-text-align="right" label="Seleziona la Mail:" :label-cols="6">
                <b-form-select :disabled="isBusy" class="form-control" v-model="mails">
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
        <!--<b-table striped hover :items="items" :fields="fields" @row-clicked="rowclicked"/>-->
    </b-row>
  </b-container>

</template>
<script>
export default {
  name: 'MailToCustomers'
}
</script>

<style>
</style>