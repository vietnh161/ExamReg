// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import store from './store/store'
import axios from 'axios'
import babel from '@babel/polyfill'
Vue.prototype.$http = axios;

const token = localStorage.getItem('token')
if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] ='Bearer '+ token
}


import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import { library } from '@fortawesome/fontawesome-svg-core'
import { faEdit, faTrash, faPlus,faAngleRight,faBars,faAngleDoubleLeft } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import Admin from "./layouts/Admin"
import Student from "./layouts/Student"
import Default from "./layouts/Default"

library.add(faEdit,faPlus,faTrash,faAngleRight,faBars,faAngleDoubleLeft)

Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.component('admin-layout',Admin)
Vue.component('student-layout', Student)
Vue.component('default-layout',Default )

Vue.use(BootstrapVue)
Vue.use(axios)
Vue.use(babel)
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
