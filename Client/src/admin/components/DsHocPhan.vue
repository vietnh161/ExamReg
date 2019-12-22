<template>
  <!-- eslint-disable-next-line vue/max-attributes-per-line -->
  <div>
    <b-container class="mt-2">
      <b-row class="justify-content-md-center">
        <b-col lg="3">
          <b-form-group
            label="Hiển thị"
            label-align-sm="right"
            label-cols-sm="6"
            label-cols-md="4"
            label-cols-lg="3"
            label-size="sm"
            label-for="perPageSelect"
            class="mb-0"
          >
            <b-form-select
              v-model="perPage"
              id="perPageSelect"
              size="sm"
              :options="pageOptions"
              @input="changePerPage"
            ></b-form-select>
          </b-form-group>
        </b-col>
        <b-col lg="6" md="auto">
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
                @input="search"
                v-model="filter"
                type="search"
                id="filterInput"
                placeholder="Tên hoặc Mã học phần"
              ></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
          <b-col lg="3">
            <b-spinner label="loading..." v-show="isBusy"></b-spinner>
          </b-col>
        </b-col>
      </b-row>
      <b-list-group class="mt-2 custom" horizontal="y">
        <b-list-group-item
          :href="'/admin/duthi/'+item.title+'/' + item.lophpId"
          v-for="item in items"
          v-bind:key="item.lophpId"
        >{{item.name}} - {{item.title}}</b-list-group-item>
      </b-list-group>
      <b-row class="mt-2">
        <b-col lg="4">
          <p>Đang hiển thị {{perPage > totalRows ? totalRows:perPage}} / {{perPage > totalRows ? perPage:totalRows}} item</p>
        </b-col>

        <b-col lg="4">
          <b-pagination
            v-model="currentPage"
            :total-rows="totalRows"
            :per-page="perPage"
            align="fill"
            size="sm"
            class="my-0"
            @input="nextPage"
          ></b-pagination>
        </b-col>
        <b-col lg="4"></b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      filter: "",
      itemsSearch: [{}],
      searchCount: null,
      itemsPerPage: [{}],
      perPage: 15,
      pageOptions: [15, 30, 45],
      totalRows: 1,
      currentPage: 1,
      items: [],
      isBusy: false
    };
  },
  created() {
    // Set the initial number of items
    this.isBusy = true;
   var a = this.$route.params.id;
    if (a) {
      axios
        .get(
          "http://localhost:63834/api/hocphan/getmultipaging-sub?currentPage=1&pageSize=15&monThiId=" +
            this.$route.params.id +
            "&kithi=" +
            localStorage.kiThiId +
            "&keyword=null"
        )
        .then(response => {
          this.items = response.data.result;
          this.totalRows = response.data.totalRow;
          this.isBusy = false;
        })

        .catch(error => {
          console.log(error.response.data);
        });
    } else {
      axios
        .get(
          "http://localhost:63834/api/hocphan/getmultipaging?currentPage=1&pageSize=15&kithi=" +
            localStorage.kiThiId +
            "&keyword=null"
        )
        .then(response => {
          this.items = response.data.result;
          this.totalRows = response.data.totalRow;
          this.isBusy = false;
        })

        .catch(error => {
          console.log(error.response.data);
        });
    }
  },

  methods: {
    nextPage(page) {
      this.isBusy = true;
      var params =
        "?currentPage=" + page + "&pageSize=" + this.perPage + "&kithi="+localStorage.kiThiId;
      if (this.filter != "") {
        params += "&keyword=" + this.filter;
      } else {
        params += "&keyword=null";
      }
      axios
        .get("http://localhost:63834/api/hocphan/getmultipaging" + params)
        .then(response => {
          this.items = response.data.result;
          this.totalRows = response.data.totalRow;
          this.isBusy = false;
        });
    },
    changePerPage(perPage) {
      this.filter = "";
      this.isBusy = true;
      this.currentPage = 1;
      var params =
        "?currentPage=" +
        this.currentPage +
        "&pageSize=" +
        perPage +
        "&kithi="+localStorage.kiThiId +
        "&keyword=null";
      axios
        .get("http://localhost:63834/api/hocphan/getmultipaging" + params)
        .then(response => {
          this.items = response.data.result;
          this.totalRows = response.data.totalRow;
          this.isBusy = false;
        });
    },
    search(keyword) {
      this.currentPage = 1;
      var params =
        "?currentPage=" +
        this.currentPage +
        "&pageSize=" +
        this.perPage +
        "&kithi=" +
        localStorage.kiThiId;
      if (keyword != "") {
        params += "&keyword=" + keyword;
      } else {
        params += "&keyword=null";
      }
      this.isBusy = true;
      axios
        .get("http://localhost:63834/api/hocphan/getmultipaging" + params)
        .then(response => {
          this.items = response.data.result;
          this.totalRows = response.data.totalRow;
          this.isBusy = false;
        });
    }
  }
};
</script>
<style scoped>
.custom {
  max-height: 760px;
  overflow: auto;
}
@media only screen and (max-width: 990px) {
  .custom {
    max-height: 1460px;
    overflow: auto;
  }
}
</style>