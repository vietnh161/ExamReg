<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="3" md="4">
          <b-form-group
            label="Hien thi"
            label-align-sm="right"
            label-cols-sm="4"
            label-cols-md="4"
            label-cols-lg="4"
            label-size="sm"
            label-for="perPageSelect"
            class="mb-0"
          >
            <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
          </b-form-group>
        </b-col>
        <b-col lg="6" md="8" class="mb-2">
          <b-form-group
            label="Tim kiem"
            label-cols-sm="2"
            label-align-sm="right"
            label-size="sm"
            label-for="filterInput"
            class="mb-0"
          >
            <b-input-group size="sm">
              <b-form-input
                debounce="1000"
                v-model="filter"
                type="search"
                id="filterInput"
                placeholder="Mssv hoặc Họ và tên"
              ></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-col>

        <b-col lg="3">
          <b-row>
          <ActionDsSv
            @changeMode="changeMode"
            @update="update"
            @add="add"
            @del="del"
            :itemEdit="itemEdit"
            :idDel="idDel"
            :acceptDel="acceptDel"
            :acceptEdit="acceptEdit"
            :message="message"
          />
          <b-col xs="6"> 
          <b-spinner label="loading..." v-show="isBusy"></b-spinner>
          </b-col>
          </b-row>
        </b-col>
        
      </b-row>
      <b-table
       :sticky-header="'760px'"
        bordered
        outlined
        hover
        
        :busy.sync="isBusy"
        :id="id"
        :selectable="true"
        :select-mode="mode"
        :items="myProvider"
        :fields="fields"
        :current-page="currentPage"
        :per-page="perPage"
        :filter="filter"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        @row-selected="getItemSelected"
      >
       
           
      </b-table>
      <b-row>
        <b-col lg="4">
         <p>Đang hiển thị {{totalRows > perPage ? perPage: totalRows}} / {{totalRows > perPage ? totalRows: perPage}} item</p>
        </b-col>
        <b-col lg="4">
          <b-pagination
            v-model="currentPage"
            pills
            :total-rows="totalRows"
            :per-page="perPage"
            align="fill"
            size="sm"
            class="my-0"
          ></b-pagination>
        </b-col>
        <b-col lg="4"></b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import ActionDsSv from "./utils/ActionDsSv";
import axios from "axios";
export default {
  components: {
    ActionDsSv
  },
  data() {
    return {
      fields: [
        { key: "mssv", label: "Mssv", sortable: true },
        { key: "fullName", label: "Họ và tên", sortable: true },
        { key: "address", label: "Địa Chỉ" },
        { key: "birthDay", label: "Ngày sinh" },
        { key: "phone", label: "Điện thoại" },
        { key: "email", label: "Email" },
        { key: "user.userName", label: "username" },
      ],
      items: [],

      id: "my-table",
      perPage: 15,
      pageOptions: [15, 30, 45],
      filter: null,
      totalRows: 0,
      currentPage: 1,
      sortDesc: false,
      sortBy: "mssv",
      isBusy: false,
      mode: "single",
      searchCount: null,

      selected: "edit",
      
      itemsSelected: Array,
      itemEdit: Object,
      idDel: Array,
      acceptDel: null,
      acceptEdit: null,
       message:""
    };
  },
  computed: {},
  created() {},

  mounted() {},
  methods: {
    myProvider(ctx) {
      this.isBusy = true;
      var params =
        "?currentPage=" + ctx.currentPage + "&pageSize=" + ctx.perPage;
      if (ctx.sortBy) {
        params += "&sort=" + ctx.sortBy;
      }else{
        params += "&sort=null";
      }
      if(ctx.filter){
         params += "&keyword=" + ctx.filter;
      }else{
         params += "&keyword=null";
      }
      if (ctx.sortDesc !== undefined) {
        let direction = ctx.sortDesc ? "DESC" : "ASC";
        params += "&sortBy=" + direction;
      }
      const promise = axios.get(
        "http://localhost:63834/api/sinhvien/getmultipaging" + params
      );
      return promise.then(data => {
        const items = data.data;
        this.totalRows = items.totalRow;
        this.isBusy = false;
        return items.result || [];
      });
    },
    update(obj) {
      console.log(obj);
      axios.put("http://localhost:63834/api/sinhvien/update",obj)
      .then(rsp => {
         this.message = "Sửa thành công";
      }).catch(err => {
      //  console.log(err)
        this.message = err.response.data;
      })
    },
    add(obj) {
      this.message = "";
      axios.post("http://localhost:63834/api/sinhvien/create",obj)
      .then(rsp => {
        this.message = "Thêm thành công";
      }).catch(err => {
        this.message = err.response.data;
      })
    },
    del(a){
      this.message = "";
      axios.delete("http://localhost:63834/api/sinhvien/delete",{data: a})
      .then(rsp => {
         this.$bvModal.msgBoxOk("Xóa thành công " + rsp.data.successCount+" , thất bại "+rsp.data.notSuccessCount).then(value => {
            this.$router.go("/admin/sinhvien");
          });
      }).catch(err => {
        this.$bvModal.msgBoxOk("Xóa khong thành công").then(value => {
           
          });
      })
    },
    changeMode(mode, selected) {
      this.mode = mode;
      if (selected == "edit") {
        this.acceptDel = false;
      } else {
        this.acceptEdit = false;
      }
      this.selected = selected;
    },
    getItemSelected(items) {
      if (this.selected == "edit") {
        this.getItemEdit(items[0]);
      } else {
        this.getItemDel(items);
      }
    },
    getItemEdit(obj) {
      this.itemEdit = new Object();
      if (obj != null) this.acceptEdit = true;
      else this.acceptEdit = false;
      for (const item in obj) {
        this.itemEdit[item] = obj[item];
      }
    },
    getItemDel(arr) {
      this.idDel = new Array();
      this.acceptEdit = false;
      for (const item in arr) {
        this.idDel.push(arr[item].sinhVienId);
      }
      if (this.idDel.length > 0) {
        this.acceptDel = true;
      } else this.acceptDel = false;
    }
  }
};
</script>
<style scoped >

</style>