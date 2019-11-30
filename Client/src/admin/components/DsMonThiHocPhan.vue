<template>
  <div id="DsMthp">
    <div class="row">
      <!-- <ThemMoi v-on:addMonHoc="addCourse"></ThemMoi> -->
      <div class="col-lg-6 col-xl-3">
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

      <div class="col-lg-6 col-xl-3" v-for="item in items" :key="item.id">
        <div v-if="editId == item.id && isEditting">
          <FormNhap
            v-bind:isEdit="true"
            v-bind:item="item"
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
                <b-link href="#/hocphan/" class="col-md-12">
                  <font-awesome-icon icon="angle-right" />
                </b-link>
              </div>
            </b-card-footer>
          </b-card>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import FormNhap from "./utils/FormNhap";

export default {
  components: {
    FormNhap
  },
  data() {
    return {
      //  items: [],
      items: [
        {
          id: 6,
          title: "INT 3306",
          name: "Phat trien ung dung web"
        },
        {
          id: 7,
          title: "INT 3306",
          name: "Phat trien ung dung web"
        },
        {
          id: 8,
          title: "INT 3306",
          name: "Phat trien ung dung web"
        },
        {
          id: 9,
          title: "INT 3306",
          name: "Phat trien ung dung web"
        },
        {
          id: 10,
          title: "INT 3306",
          name: "Phat trien ung dung web"
        }
      ],
      isCreating: false,
      isEditting: false,
      editId: null,
      loading: false
    };
  },
  // created() {
  //   api
  //     .getAll()
  //     .then(response => {
  //       this.items = response.data;
  //     })
  //     .catch(error => {
  //       console.log(error.response.data);
  //     });
  // },
  methods: {
    close() {
      this.isCreating = false;
      this.isEditting = false;
    },
    openEdit(obj) {
      this.isEditting = true;
      this.isCreating = false;
      this.editId = obj.id;
    },

    addCourse(obj) {
      console.log(obj);
    },
    deleteCourse(id) {
      console.log(id);
    },
    updateCourse(obj) {
      console.log(obj);
      var a = this.items.find(x => (x.id = obj.id));
      a.name = obj.name;
      a.title = obj.title;
      this.isEditting = false;
    },
    addHocPhan(item, obj) {}
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
</style>
