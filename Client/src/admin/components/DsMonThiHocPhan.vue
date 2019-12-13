<template>
  <div id="DsMthp">
  
    <b-container fluid class="mt-2">
      <b-row>
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
                placeholder="Mã học phần"
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
    </b-container>
    <div class="row custom">
      <!-- <ThemMoi v-on:addMonHoc="addCourse"></ThemMoi> -->
      <div class="col-lg-6 col-xl-4">
        <div v-show="!isCreating">
          <b-card class="text-center">
            <b-card-header>
              <h5>Them moi</h5>
            </b-card-header>
            <b-card-text>...</b-card-text>
            <b-card-footer>
              <b-button class="btn btn-sm" @click="isCreating=true; isEditting=false">
                <font-awesome-icon icon="plus" />
              </b-button>
            </b-card-footer>
          </b-card>
        </div>
        <div v-show="isCreating">
          <FormNhap v-bind:isEdit="false" @closeForm="close" @add="addCourse"></FormNhap>
        </div>
      </div>

      <div class="col-lg-6 col-xl-4" v-for="item in items" :key="item.monThiId">
        <div v-if="editId == item.monThiId && isEditting">
          <FormNhap
            v-bind:isEdit="true"
            v-bind:itemEdit="item"
            @closeForm="close"
            @update="updateCourse"
          ></FormNhap>
        </div>
        <div v-else>
          <b-card class="text-center">
            <b-card-header>
              <div class="row">
                <div class="col-md-3 col-3"></div>
                <div class="col-md-6 col-6">
                  <h5>{{item.title}}</h5>
                </div>
                <div class="col-md-3 col-3">
                  <div class="row">
                    <div class="col-md-6 col-1">
                      <b-button class="btn btn-sm mr-2" @click="openEdit(item)">
                        <font-awesome-icon icon="edit" />
                      </b-button>
                    </div>
                    <div class="col-md-6 col-1">
                      <b-button class="btn btn-sm" @click="deleteCourse(item.monThiId)">
                        <font-awesome-icon icon="trash" />
                      </b-button>
                    </div>
                  </div>
                </div>
              </div>
            </b-card-header>
            <b-card-text>{{item.name}}</b-card-text>
            <b-card-footer>
              <div class="row">
                <b-link :href="'/admin/monthi/'+ item.title +'/' + item.monThiId" class="col-md-12">
                  <font-awesome-icon icon="angle-right" />
                </b-link>
              </div>
            </b-card-footer>
          </b-card>
        </div>
      </div>
    </div>
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
  </div>
</template>
<script>
import FormNhap from "./utils/FormNhap";
import axios from "axios";
export default {
  components: {
    FormNhap
  },
  data() {
    return {
      items: [],

      filter: "",
      itemsSearch: [{}],
      searchCount: null,
      itemsPerPage: [{}],
      perPage: 15,
      pageOptions: [15, 30, 45],
      totalRows: 1,
      currentPage: 1,
      isBusy: false,

      isCreating: false,
      isEditting: false,
      editId: null,
      loading: false,

      message: ""
    };
  },

  created() {
    this.isBusy = true;
    axios
      .get(
        "http://localhost:63834/api/monthi/getmultipaging?currentPage=1&pageSize=15&kithi=4&keyword=null"
      )
      .then(response => {
        this.items = response.data.result;
        this.totalRows = response.data.totalRow;
        this.isBusy = false;
      })

      .catch(error => {
       // console.log(error.response.status);
        if(error.response.status == "401"){
          this.$router.push('/')
        }
      });
  },
  mounted() {
    // Set the initial number of items
    //this.test();
  },
  methods: {
    nextPage(page) {
      this.isBusy = true;
      var params =
        "?currentPage=" + page + "&pageSize=" + this.perPage + "&kithi=4";
      if (this.filter != "") {
        params += "&keyword=" + this.filter;
      } else {
        params += "&keyword=null";
      }
      axios
        .get("http://localhost:63834/api/monthi/getmultipaging" + params)
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
        "&kithi=4" +
        "&keyword=null";
      axios
        .get("http://localhost:63834/api/monthi/getmultipaging" + params)
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
        this.perPage ;
      
      if (keyword != "") {
        params += "&keyword=" + keyword;
      } else {
        params += "&keyword=null";
      }
      this.isBusy = true;
      axios
        .get("http://localhost:63834/api/monthi/getmultipaging" + params)
        .then(response => {
          this.items = response.data.result;
          this.totalRows = response.data.totalRow;
          this.isBusy = false;
        });
    },

    close() {
      this.isCreating = false;
      this.isEditting = false;
    },
    openEdit(obj) {
      this.isEditting = true;
      this.isCreating = false;
      this.editId = obj.monThiId;
    },

    addCourse(obj) {
      axios
        .post("http://localhost:63834/api/monthi/create", obj)
        .then(rsp => {
          this.$bvModal.msgBoxOk("Thêm thành công").then(value => {
           this.isCreating = false;
           this.isEditting = false;
            this.$router.go("/admin/monthi");
          });
        })
        .catch(err => {
          this.$bvModal.msgBoxOk(err.response.data).then(value => {
            // this.$router.go("/admin/monthi");
          });
        });
    },
    deleteCourse(id) {
      this.$bvModal
        .msgBoxConfirm("Thao tác sẽ xóa toàn bộ dữ liệu liên quan, bạn chắc chứ?")
        .then(value => {
          if (value) {
            axios
              .delete("http://localhost:63834/api/monthi/delete?id=" + id)
              .then(rsp => {
                this.$bvModal.msgBoxOk("Xóa thành công").then(value => {
                  this.$router.go("/admin/monthi");
                });
              })
              .catch(err => {
                this.$bvModal
                  .msgBoxOk("Xóa khong thành công")
                  .then(value => {});
              });
          }
        });
    },
    updateCourse(obj) {
      axios
        .put("http://localhost:63834/api/monthi/update", obj)
        .then(rsp => {
          this.$bvModal.msgBoxOk("Sửa thành công").then(value => {
            this.$router.go("/admin/monthi");
          });
        })
        .catch(err => {
          //  console.log(err)
          this.$bvModal.msgBoxOk(err.response.data).then(value => {
             this.$router.go("/admin/monthi");
          });
        });
      this.isEditting = false;
    }
  }
};
</script>
<style scoped>
.pointer {
  cursor: pointer;
}
#DsMthp {
  padding: 10px;
}
.card {
  margin: 10px 0px 10px 0px;
  border: solid 1px grey;
}
.card-text {
  margin: 20px 0 20px 0;
}
.card-body {
  padding-bottom: 0;
}
.link {
  padding: 5px;
}
.link:hover {
  color: red;
}
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
