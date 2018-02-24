<template>
<div>
  <b-btn v-b-modal.modal1>Launch demo modal</b-btn>
  <b-modal id="modal1" title="Bootstrap-Vue">
    <p class="my-4">Hello from modal!</p>
  </b-modal>
</div>
</template>

<script>

import Vue from 'vue'

export default{
  name: 'ModalConfim',  
}
</script>