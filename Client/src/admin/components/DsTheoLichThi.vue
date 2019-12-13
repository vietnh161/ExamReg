<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="12">
          <h4
            class="mb-4 mt-2"
          >{{title}}</h4>
        </b-col>
        <b-col lg="3" md="4">
          <b-form-group
            label="Hiển thị"
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
            label="Tìm kiếm"
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
                placeholder="Type to Search"
              ></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-col>

        <b-col xs="6">
          <b-spinner label="loading..." v-show="isBusy"></b-spinner>
        </b-col>
      </b-row>
      <b-col lg="12" v-show="items==null ? true: false">Chưa có ai đăng ký</b-col>
      <b-col lg="12" v-if="items==null ? false: true">
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
        ></b-table>
        <b-row>
          <b-col lg="4">
            <p>Đang hiển thị {{perPage > totalRows? totalRows : perPage}} / {{perPage > totalRows? perPage:totalRows}} item</p>
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
      </b-col>
    </b-container>
  </div>
</template>
<script>
import axios from "axios";
export default {
  components: {},
  data() {
    return {
      fields: [
        { key: "mssv", label: "Mssv", sortable: true },
        { key: "fullName", label: "Họ và tên", sortable: true },
        { key: "birthDay", label: "Ngày sinh" }
      ],
      items: [],

      id: "tableSv",
      perPage: 15,
      pageOptions: [15, 30, 45],
      filter: null,
      totalRows: 1,
      currentPage: 1,
      sortBy: "mssv",
      sortDesc: false,
      isBusy: false,
      mode: "single",

      title: ""
    };
  },
  created() {
    axios
      .get(
        "http://localhost:63834/api/lichthi/getbyid/" + this.$route.params.id
      )
      .then(rsp => {
        this.title =
          rsp.data.lopHocPhan.name + ' - '+
          rsp.data.lopHocPhan.title + ' - '+
          rsp.data.ngayThi.substr(0,10) +' - '+
          rsp.data.caThi.name +' ( '+
          rsp.data.gioBatDau.substr(0,5) + '-'+
          rsp.data.gioKetThuc.substr(0,5) +' ) '+
          rsp.data.phongThi.name;
      });
  },
  mounted() {},
  methods: {
    myProvider(ctx) {
      this.isBusy = true;
      var params =
        "?currentPage=" +
        ctx.currentPage +
        "&pageSize=" +
        ctx.perPage +
        "&lichThiId=" +
        this.$route.params.id;
      params += "&kiThiId=4";
      if (ctx.sortBy) {
        params += "&sort=" + ctx.sortBy;
      } else {
        params += "&sort=null";
      }
      if (ctx.sortDesc !== undefined) {
        let direction = ctx.sortDesc ? "DESC" : "ASC";
        params += "&sortBy=" + direction;
      }
      if (ctx.filter) {
        params += "&keyword=" + ctx.filter;
      } else {
        params += "&keyword=null";
      }
      const promise = axios.get(
        "http://localhost:63834/api/sinhvien/getmultipagingbylichthi" + params
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
      for (var i in this.items) {
        if (this.items[i].id == obj.id) {
          this.items[i].age = obj.age;
          this.items[i].first_name = obj.first_name;
          this.items[i].last_name = obj.last_name;
          // console.log(this.items[i])
        }
      }
    }
  }
};
</script>