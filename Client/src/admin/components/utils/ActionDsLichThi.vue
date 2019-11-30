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
    <b-modal
      id="modal-edit"
      ref="edit"
      title="BootstrapVue"
      hide-header
      hide-footer
   
    >
      <b-form @submit.stop.prevent="EditSubmit">
        <b-form-group :label="'Mã học phần'">
          <b-form-select v-model="itemEdit.LopHocPhanId" :options="mahp" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Ca thi'">
          <b-form-select v-model="itemEdit.CaThiId" :options="cathi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Phòng thi'">
          <b-form-select v-model="itemEdit.PhongThiId" :options="phongthi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Ngày thi'">
          <b-form-input type="date" v-model="itemEdit.NgayThi" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ bắt đầu'">
          <b-form-input type="time" v-model="itemEdit.GioBatDau" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ kết thúc'">
          <b-form-input type="time" v-model="itemEdit.GioKetThuc" required></b-form-input>
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
        <b-form-group label="Mã học phần">
          <b-form-select v-model="itemAdd.LopHocPhanId" :options="mahp" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Ca thi'">
          <b-form-select v-model="itemAdd.CaThiId" :options="cathi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Phòng thi'">
          <b-form-select v-model="itemAdd.PhongThiId" :options="phongthi" required></b-form-select>
        </b-form-group>
        <b-form-group :label="'Ngày thi'">
          <b-form-input type="date" v-model="itemAdd.NgayThi" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ bắt đầu'">
          <b-form-input type="time" v-model="itemAdd.GioBatDau" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Giờ kết thúc'">
          <b-form-input type="time" v-model="itemAdd.GioKetThuc" required></b-form-input>
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
export default {
  data() {
    return {
      mahp: [
        { text: "INT3306 2", value: "INT3306 1" },
        { text: "INT3306 1", value: "INT3306 2" },
        { text: "INT3306 3", value: "INT3306 3" }
      ],
      cathi: [
        { text: "ca 1", value: "1" },
        { text: "ca 2", value: "2" },
        { text: "ca 3", value: "3" }
      ],
      phongthi: [
        { text: "phong 1", value: "1" },
        { text: "phong 2", value: "2" },
        { text: "phong 3", value: "3" }
      ],

      selected: "edit",
      options: [
        { text: "Edit", value: "edit" },
        { text: "Delete", value: "delete" }
      ],
      itemAdd: {
        LopHocPhanId: null,
        CathiId: null,
        PhongthiId: null,
        NgayThi: "",
        GioBatDau: "",
        GioKetThuc: "",
        Count: "0"
      },
      isAdd: false,
  
    };
  },
  props: {
    itemEdit: null,
    idDel: null,
    acceptDel: null,
    acceptEdit: null
  },

  methods: {
    // handleEditOk(bvModalEvt) {
    //   // Prevent modal from closing
    //   bvModalEvt.preventDefault();
    //   // Trigger submit handler
    //   this.EditSubmit();
    // },
    // handleAddOk(bvModalEvt) {
    //   // Prevent modal from closing
    //   bvModalEvt.preventDefault();
    //   // Trigger submit handler
    //   this.AddSubmit();
    // },
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
      //console.log(Object.keys(this.item)[0]);
      //console.log(this.itemEdit)
      this.$emit("update", this.itemEdit);
      this.$nextTick(() => {
        this.$refs.edit.hide();
      });
    },
    AddSubmit() {
      this.$emit("add", this.itemAdd);
      this.$nextTick(() => {
        this.$refs.add.hide();
      });
    },
    showMsgDelete() {
      var canDel = false;
      this.$bvModal
        .msgBoxConfirm("Are you sure?")
        .then(value => {
          canDel = value;
        })
        .catch(err => {
          // An error occurred
        });
      if (canDel) {
        this.$emit("deleteItems", this.idDel);
      }
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