<template>
  <div>
    <b-container fluid>
      <b-row class="mt-2">
        <b-col lg="6">
          <h4>Danh sách lịch thi hiện có</h4>
        </b-col>
        <b-col lg="12" class="mt-2">
          <b-table
            :sticky-header="'400px'"
            bordered
            outlined
            hover
            :fields="fields1"
            :busy.sync="isBusy"
            id="tb-lichthi"
            :items="items1"
          >
            <template v-slot:cell(action)="row">
              <b-button size="sm" @click="add(row.item)">đăng ký</b-button>
            </template>
          </b-table>
        </b-col>
      </b-row>
      <b-row class="mt-2">
        <b-col lg="6">
          <h4>Danh sách lịch thi đã đăng kí</h4>
        </b-col>
        <b-col lg="12" class="mt-2">
          <b-table
            :sticky-header="'400px'"
            bordered
            outlined
            hover
            :fields="fields2"
            :items="items2"
            :busy.sync="isBusy"
            id="tb-dadky"
          >
            <template v-slot:cell(action)="row">
              <b-button size="sm" @click="del(row.item)" class="btn-danger">Xóa</b-button>
            </template>
          </b-table>
        </b-col>
        <b-col>
          <b-button size="sm" @click="commit">Ghi nhận</b-button>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      fields1: [
        { key: "lopHocPhan.name", label: "Tên học phần" },
        { key: "lopHocPhan.title", label: "Mã học phần" },
        { key: "caThi.name", label: "Ca thi" },
        { key: "phongThi.name", label: "Phòng thi", sortable: true },
        { key: "ngayThi", label: "Ngày Thi", sortable: true },
        { key: "gioBatDau", label: "Giờ bắt đầu" },
        { key: "gioKetThuc", label: "Giờ kết thúc" },
        { key: "count", label: "Đã đăng ký", sortable: true },
        { key: "action", label: "" }
      ],

      fields2: [
        { key: "lopHocPhan.name", label: "Tên học phần" },
        { key: "lopHocPhan.title", label: "Mã học phần" },
        { key: "caThi.name", label: "Ca thi" },
        { key: "phongThi.name", label: "Phòng thi", sortable: true },
        { key: "ngayThi", label: "Ngày Thi", sortable: true },
        { key: "gioBatDau", label: "Giờ bắt đầu" },
        { key: "gioKetThuc", label: "Giờ kết thúc" },
        { key: "action", label: "" }
      ],

      items1: [],
      items2: [],

      isBusy: false
    };
  },
  created() {
      this.myProvider1();
      this.myProvider2();
  },
  methods: {
    myProvider1() {
      this.isBusy = true;
      axios.get("http://localhost:63834/api/sinhvien/getlichthi").then(data => {
        this.items1 = data.data.result;
       this.isBusy=false;
      });
    },
    myProvider2() {
       axios.get("http://localhost:63834/api/sinhvien/getlichthidadky").then(data => {
        this.items2 = data.data.result;
      });
    },

    add(item) {
     this.items2.push(item);
    },
    del(item){
     this.items2.splice(this.items2.indexOf(item),1);
    },
    commit(){

    }
  }
};
</script>