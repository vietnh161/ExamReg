<template>
  <b-container fluid>
    <b-button v-b-modal.modal-add size="sm">Thêm kì thi</b-button>
    <b-button v-b-modal.modal-edit size="sm">Sửa kì thi</b-button>
    <b-button @click="submitDel" size="sm">Xóa kì thi</b-button>
    <b-row class="mt-2">
      <b-col md="12">
        <p>Vui lòng thực hiên theo các bước sau</p>
      </b-col>
      <b-col md="12">
        <p>Tạo kì thi -> Thêm môn thi học phần -> Thêm sinh viên / học phần -> Thêm sinh viên không đủ điều kiện -> Thêm lịch thi</p>
      </b-col>
    </b-row>
    <b-col class="custom">
      <b-link href="/admin/monthi-hocphan/addmulti" target="blank">Thêm môn thi học phần</b-link>
    </b-col>
    <b-col class="custom">
      <b-link href="/admin/sinhvien-hocphan/addmulti" target="blank">Thêm sinh viên / học phần</b-link>
    </b-col>
    <b-col class="custom">
      <b-link
        href="/admin/sinhvien-hocphan/addnotpass"
        target="blank"
      >Thêm sinh viên không đủ điều kiện</b-link>
    </b-col>
    <b-col class="custom">
      <b-link href="/admin/lichthi" target="blank">Thêm lịch thi</b-link>
    </b-col>
    <b-col md="12">
      <p>Danh sách các kì thi đã có</p>
    </b-col>
    <b-table
      bordered
      outlined
      hover
      :id="id"
      :selectable="true"
      :select-mode="mode"
      :items="items"
      :fields="fields"
      @row-selected="getItemSelected"
    ></b-table>

    <b-modal id="modal-add" ref="add" title="BootstrapVue" hide-header hide-footer>
      <b-form @submit.stop.prevent="submitAdd">
        <b-form-group label="Tên kì thi">
          <b-form-input v-model="name"></b-form-input>
        </b-form-group>
        <b-row align-h="end">
          <b-col lg="4">
            <b-button size="sm" @click="closeModala()">Cancel</b-button>
            <b-button type="submit" variant="primary" size="sm">OK</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-modal>
    <b-modal id="modal-edit" ref="edit" title="BootstrapVue" hide-header hide-footer>
      <b-form @submit.stop.prevent="submitUpdate">
        <b-form-group label="Tên kì thi">
          <b-form-input v-model="itemsSelected.name"></b-form-input>
        </b-form-group>
        <b-row align-h="end">
          <b-col lg="4">
            <b-button size="sm" @click="closeModale()">Cancel</b-button>
            <b-button type="submit" variant="primary" size="sm">OK</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-modal>
  </b-container>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      name: "",
      fields: [
        { key: "name", label: "Tên kì thi" },
        { key: "createdDate", label: "Ngày tạo" }
      ],
      items: [],

      id: "my-table",
      mode: "single",

      selected: "edit",
      itemsSelected: Object,
      itemEdit: Object,
      idDel: Array,
      acceptDel: null,
      acceptEdit: null
    };
  },
  created() {
    axios.get("http://localhost:63834/api/kithi/getall").then(rsp => {
      this.items = rsp.data;
    });
  },
  methods: {
    closeModala() {
      this.$nextTick(() => {
        this.$refs.add.hide();
      });
    },
    closeModale() {
      this.$nextTick(() => {
        this.$refs.edit.hide();
      });
    },
    submitAdd() {
      axios
        .post("http://localhost:63834/api/kithi/create", { name: this.name })
        .then(rsp => {
          this.$bvModal.msgBoxOk("Thêm thành công").then(value => {
            this.$router.go("/admin/kithi");
          });
        })
        .catch(err => {
          this.$bvModal.msgBoxOk("Thêm khong thành công");
        });
    },
    submitUpdate() {
      axios
        .put("http://localhost:63834/api/kithi/update", this.itemsSelected)
        .then(rsp => {
          this.$bvModal.msgBoxOk("Sửa thành công").then(value => {
            this.$router.go("/admin/kithi");
          });
        })
        .catch(err => {
          this.$bvModal.msgBoxOk("Sửa khong thành công");
        });
    },
    submitDel() {
      this.$bvModal
        .msgBoxOk("Bạn có muốn xóa  " + this.itemsSelected.name)
        .then(value => {
          if (value) {
            axios
              .delete(
                "http://localhost:63834/api/kithi/delete?id=" +
                  this.itemsSelected.kiThiId
              )
              .then(rsp => {
                this.$bvModal.msgBoxOk("Xóa thành công").then(value => {
                  axios
                    .get("http://localhost:63834/api/kithi/getall")
                    .then(response => {
                      let items = response.data;
                      let data = new Array();
                      items.forEach(element => {
                        let obj = {
                          value: element.kiThiId,
                          text: element.name
                        };
                        data.push(obj);
                      });

                      if (data != null) {
                        localStorage.kiThiId = data[0].value;
                      }
                    })
                    .catch(error => {
                      console.log(error.response.data);
                    });

                  this.$router.go("/admin/kithi");
                });
              })
              .catch(err => {
                this.$bvModal.msgBoxOk("Xóa khong thành công");
              });
          }
        })
        .catch(err => {
          // An error occurred
        });
    },
    getItemSelected(items) {
      this.itemsSelected = {
        kiThiId: items[0].kiThiId,
        name: items[0].name,
        createdDate: items[0].createdDate
      };
      console.log(this.itemsSelected);
    }
  }
};
</script>
<style scoped>
.custom {
  margin: 10px;
  color: blue;
}
.custom :hover {
  color: red;
}
</style>