<template>
<b-navbar toggleable="md" type="dark" variant="dark">
  <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
  <b-navbar-brand href="#"><img src="../assets/logo-housetohouse.png" width="226" height="55"/></b-navbar-brand>
  <b-collapse is-nav id="nav_collapse">

    <!-- Right aligned nav items -->
    <b-navbar-nav class="ml-auto">
    <b-nav pills>      
        <b-nav-item href="#">
          <router-link :to="'/'">
            <b-button variant="primary">
              <icon name="home" scale="2"/>
            </b-button>
          </router-link>
        </b-nav-item>
        <b-nav-item href="#">
          <router-link :to="'/customers'">
            <b-button variant="primary">
              <icon name="user-secret" scale="2"/>
            </b-button>
          </router-link>
        </b-nav-item>
         <b-nav-item href="#">
          <router-link :to="'/mails'">
            <b-button variant="primary">
              <icon name="envelope-square" scale="2"/>
            </b-button>
          </router-link>
        </b-nav-item>
         <b-nav-item href="#">
          <router-link :to="'/mails'">
            <b-button variant="primary">
              <icon name="envelope-open" scale="2"/>
              <b-badge variant="light">{{notificationsEmail}}</b-badge>
            </b-button>
          </router-link>
        </b-nav-item>
    </b-nav>
      <b-nav-form>
        <b-form-input size="sm" class="mr-sm-2" type="text" placeholder="Search"/>
        <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
      </b-nav-form>
      
    </b-navbar-nav>
  </b-collapse>
</b-navbar>
</template>
<script>
import Vue from "vue";

export default {
  name: "NavBar",
  data () {
    return {
      notificationsEmail: 0
    }
  },
  created() {
    this.connection = new this.$signalR.HubConnection('/emailNotifications');
  },
  mounted: function () {
    this.connection.start();
    this.connection.on('emailNotifications', data => {
         this.notificationsEmail = data;
    });
  },
};
</script>