<template>
  <div>
    <b-form-group>
      <b-button class="btn btn-info" v-b-modal.modal-add size="sm">Add</b-button>
      <b-form-radio-group
        id="btn-radios-2"
        v-model="selected"
        :options="options"
        buttons
        button-variant="outline-primary"
        size="sm"
        name="radio-btn-outline"
        @input="changeMode"
      ></b-form-radio-group>
      <b-button class="btn btn-info" v-b-modal.modal-edit size="sm" v-show="acceptEdit">Edit</b-button>
      <b-button
        class="btn btn-info btn-danger"
        size="sm"
        v-show="this.acceptDel"
        @click="showMsgDelete"
      >Delete</b-button>
    </b-form-group>
    <b-modal id="modal-edit" ref="edit" title="BootstrapVue" hide-header hide-footer>
      <b-form @submit.stop.prevent="EditSubmit">
        <label style="color:red">{{message}}</label>
        <b-form-group :label="'Mã học phần'">
          <b-form-input v-model="maHocPhanEdit" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Ca thi'">
          <b-form-select v-model="itemEdit.caThiId" :options="cathi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Phòng thi'">
          <b-form-select v-model="itemEdit.phongThiId" :options="phongthi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Ngày thi'">
          <b-form-input type="date" v-model="itemEdit.ngayThi" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ bắt đầu'">
          <b-form-input type="time" v-model="itemEdit.gioBatDau" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ kết thúc'">
          <b-form-input type="time" v-model="itemEdit.gioKetThuc" required></b-form-input>
        </b-form-group>
        <b-row align-h="end">
          <b-col lg="4">
            <b-button size="sm" @click="closeModal('edit')">Cancel</b-button>
            <b-button type="submit" variant="primary" size="sm">OK</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-modal>
    <b-modal id="modal-add" ref="add" title="BootstrapVue" hide-header hide-footer>
      <b-form @submit.stop.prevent="AddSubmit">
        <label style="color:red">{{message}}</label>
        <b-form-group label="Mã học phần">
          <b-form-input v-model="itemAdd.lopHocPhan.title" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Ca thi'">
          <b-form-select v-model="itemAdd.caThiId" :options="cathi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Phòng thi'">
          <b-form-select v-model="itemAdd.phongThiId" :options="phongthi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Ngày thi'">
          <b-form-input type="date" v-model="itemAdd.ngayThi" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ bắt đầu'">
          <b-form-input type="time" v-model="itemAdd.gioBatDau" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ kết thúc'">
          <b-form-input type="time" v-model="itemAdd.gioKetThuc" required></b-form-input>
        </b-form-group>
        <b-row align-h="end">
          <b-col lg="4">
            <b-button size="sm" @click="closeModal('add')">Cancel</b-button>
            <b-button type="submit" variant="primary" size="sm">OK</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-modal>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      lophp: [],
      cathi: [],
      phongthi: [],

      selected: "edit",
      options: [
        { text: "Edit", value: "edit" },
        { text: "Delete", value: "delete" }
      ],
      itemAdd: {
        lopHocPhanId: null,
        caThiId: null,
        phongThiId: null,
        lopHocPhan: {
          title: ""
        },
        ngayThi: "",
        gioBatDau: "",
        gioKetThuc: "",
        count: "0",
        kiThiId: null
      },
      isAdd: false,
      maHocPhanEdit: ""
    };
  },
  props: {
    itemEdit: null,
    idDel: null,
    acceptDel: null,
    acceptEdit: null,
    message: null
  },
  created() {
    axios
      .get(
        "http://localhost:63834/Api/LichThi/getallprop?kiThiId=" +
          localStorage.kiThiId
      )
      .then(rsp => {
        rsp.data.caThi.forEach(element => {
          let data = {
            text: element.name,
            value: element.caThiId
          };
          //console.log(data);
          this.cathi.push(data);
        });
        rsp.data.phongThi.forEach(element => {
          let data = {
            text: element.name,
            value: element.phongThiId
          };
          this.phongthi.push(data);
        });
      })
      .catch(err => {
        console.log(err);
      });
  },

  methods: {
    closeModal(str) {
      if (str == "edit") {
        this.$nextTick(() => {
          this.$refs.edit.hide();
        });
      }
      if (str == "add") {
        this.$nextTick(() => {
          this.$refs.add.hide();
        });
      }
    },
    EditSubmit() {
      this.itemEdit.lopHocPhan.title = this.maHocPhanEdit;
      this.$emit("update", this.itemEdit);
      // this.$nextTick(() => {
      //   this.$refs.edit.hide();
      // });
    },
    AddSubmit() {
      this.itemAdd.kiThiId = 4;
      this.$emit("add", this.itemAdd);
      // this.$nextTick(() => {
      //   this.$refs.add.hide();
      // });
    },
    showMsgDelete() {
      this.$bvModal
        .msgBoxConfirm("Are you sure?")
        .then(value => {
          if (value) {
            this.$emit("del", this.idDel);
          }
        })
        .catch(err => {
          // An error occurred
        });
    },
    changeMode() {
      var mode = "";
      if (this.selected === "edit") {
        mode = "single";
      }
      if (this.selected === "delete") {
        mode = "multi";
      }
      this.$emit("changeMode", mode, this.selected);
    }
  }
};
</script>