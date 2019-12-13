<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="12">
          <h4 class="mb-4 mt-2">{{lopHp.name}} - {{lopHp.title}}</h4>
        </b-col>
        <b-col lg="1">
        
        </b-col>
        <b-col lg="2" md="4">
          <b-form-group
            label="Hien thi"
            label-align-sm="right"
            label-cols-sm="6"
            label-cols-md="4"
            label-cols-lg="3"
            label-size="sm"
            label-for="perPageSelect"
            class="mb-0"
          >
            <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
          </b-form-group>
        </b-col>
        <b-col lg="6"  md="8" class="mb-2">
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
          <ActionDsDuThi
            :items="items"
            @changeMode="changeMode"
            @update="update"
            @add="add"
            :itemEdit="itemEdit"
            :idDel="idDel"
            :idEdit="idEdit"
            :acceptDel="acceptDel"
            :acceptEdit="acceptEdit"
          />
      
           <b-col xs="6">
          <b-spinner label="loading..." v-show="isBusy"></b-spinner>
          </b-col>
          </b-row>
            </b-col>
      </b-row>
      <b-table
       sticky-header="750px"
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
      ></b-table>
      <b-row>
        <b-col lg="4">
          <p
          >Đang hiển thị {{perPage > totalRows? totalRows : perPage}} / {{perPage > totalRows? perPage:totalRows}} item</p>
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
import ActionDsDuThi from "./utils/ActionDsDuThi";
import axios from 'axios'
export default {
  components: {
    ActionDsDuThi
  },
  data() {
    return {
      fields: [
        { key: "sinhVien.mssv", label:"MSSV",  },
        { key: "sinhVien.fullName",label:"Họ và tên",  },
        {
          key: "duDieuKien",
          label: "Qua môn",
          formatter: value => {
            return value ? "Có" : "Không";
          }
        }
      ],
      items: [],

      id: "tableSv",
      perPage: 15,
      pageOptions: [15, 30, 45],
      filter: null,
      totalRows: 1,
      currentPage: 1,
      sortBy: "id",
      sortDesc: false,
      mode: "single",
      isBusy: false,

      searchCount: null,

      selected: "edit",
      itemsSelected: Array,
      idEdit: null,
      itemEdit: Object,
      idDel: Array,
      acceptDel: null,
      acceptEdit: null,
      lopHp:{
        lopHpId:null,
        title:"",
        name:""
      }
    };
  },
  created(){
    this.lopHp.lopHpId = this.$route.params.id;
       axios.get('http://localhost:63834/api/hocphan/getbyid/' + this.lopHp.lopHpId)
      .then(response => {
        this.lopHp = response.data;
      })
      .catch(error => {
        console.log(error.response.data);
      });
  },
  mounted() {
    // Set the initial number of items
    this.totalRows = this.items.length;
  },
  methods: {
     myProvider(ctx) {
       var a = this;
      this.isBusy = true;
      var params =
        "?currentPage=" + ctx.currentPage + "&pageSize=" + ctx.perPage +"&lopHpId=" +  this.lopHp.lopHpId;
      if(ctx.filter){
         params += "&keyword=" + ctx.filter;
      }else{
         params += "&keyword=null";
      }
      const promise = axios.get(
        "http://localhost:63834/api/sinhvienlophp/getmultipaging" + params
      );
      return promise.then(response => {
        const items = response.data;
        this.totalRows = items.totalRow;
        this.isBusy = false;
        return items.result || [];
      })
      .catch(function (error) {
       if (error.response) {
         if(error.response.status == 400){
           // a.$bvModal.msgBoxOk("Vui lòng nhập đúng đường dẫn")
           a.$router.push("/notfound")
         }
    }
  });
      
    },
  
    update(obj) {
      console.log(obj);
      for (var i in this.items) {
        if (this.items[i].id == obj.id) {
          this.items[i].age = obj.age;
          this.items[i].first_name = obj.first_name;
          this.items[i].last_name = obj.last_name;
          // console.log(this.items[i])
        }
      }
    },
    add(obj) {
      obj.id = null;
      this.items.push(obj);
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
      for (const item in this.itemEdit) {
        this.idEdit = this.itemEdit[item];
        delete this.itemEdit[item];
        break;
      }
    },
    getItemDel(arr) {
      this.idDel = new Array();
      this.acceptEdit = false;
      for (const item in arr) {
        this.idDel.push(arr[item].id);
      }
      if (this.idDel.length > 0) {
        this.acceptDel = true;
      } else this.acceptDel = false;
    }
  }
};
</script>