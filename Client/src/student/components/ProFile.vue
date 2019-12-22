<template>
<div>
    <b-card bg-variant="light">
        <b-form-group
        label-cols-sm="1"
        label="Họ và tên:"
        label-align-sm="right"
        label-for="fullname"
      >
        <b-form-input id="fullname" v-model="sinhVien.fullName"></b-form-input>
      </b-form-group>
       <b-form-group
        label-cols-sm="1"
        label="Ngày sinh:"
        label-align-sm="right"
        label-for="birthDay"
      >
        <b-form-input type="date"  id="birthDay" v-model="sinhVien.birthDay"></b-form-input>
      </b-form-group>
       <b-form-group
        label-cols-sm="1"
        label="Địa chỉ:"
        label-align-sm="right"
        label-for="address"
      >
        <b-form-input id="address" v-model="sinhVien.address"></b-form-input>
      </b-form-group>
       <b-form-group
        label-cols-sm="1"
        label="Điện thoại:"
        label-align-sm="right"
        label-for="phone"
      >
        <b-form-input id="phone" v-model="sinhVien.phone"></b-form-input>
      </b-form-group>
       <b-form-group
        label-cols-sm="1"
        label="Email:"
        label-align-sm="right"
        label-for="email"
      >
        <b-form-input id="email" v-model="sinhVien.email"></b-form-input>
      </b-form-group>
        <b-form-group
        label-cols-sm="1"
        label-align-sm="right"
        label-for="submit"
      >
        <b-button id="submit" @click="submit">Ok</b-button>
      </b-form-group>

  </b-card>
</div>
</template>
<script>
import Axios from 'axios'
import print from 'printd'
export default {
    data(){
        return{
            sinhVien: {}
        }
    },
    created(){
        Axios.get("http://localhost:63834/api/account/getProfile")
        .then(rsp => {
            this.sinhVien = rsp.data
            this.sinhVien.birthDay = this.sinhVien.birthDay.slice(0,10);
        })
    },
    methods:{
        submit(){
            
        }
    }
    
}
</script>