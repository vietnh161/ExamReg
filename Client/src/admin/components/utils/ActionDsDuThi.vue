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
      @show="updateProp"
    >
      <b-form @submit.stop.prevent="EditSubmit">
        <b-form-group :label="'MSSV'">
          <b-form-input v-model="MSSV" disabled></b-form-input>
        </b-form-group>
        <b-form-group :label="'Họ và tên'">
          <b-form-input v-model="nameSv" disabled></b-form-input>
        </b-form-group>
        <b-form-group :label="'Qua môn'">
          <b-form-select
            v-model="itemEdit.DuDieuKien"
            :options="[{value:true,text:'Có'},{value:false,text:'Không'}]"
          ></b-form-select>
        </b-form-group>
          <b-row align-h="end">
          <b-col lg="4">
            <b-button size="sm" @click="closeModal('add')">Cancel</b-button>
            <b-button type="submit" variant="primary" size="sm">OK</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-modal>
    <b-modal id="modal-add" ref="add" title="BootstrapVue" hide-header hide-footer>
      <b-form @submit.stop.prevent="AddSubmit">
        <b-form-group :label="'MSSV'">
          <b-form-input type="number" v-model="itemEdit.MSSV" required></b-form-input>
        </b-form-group>
        <b-form-group :label="'Qua môn'">
          <b-form-select
            required
            v-model="itemEdit.DuDieuKien"
            :options="[{value:true,text:'Có'},{value:false,text:'Không'}]"
          ></b-form-select>
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
      selected: "edit",
      options: [
        { text: "Edit", value: "edit" },
        { text: "Delete", value: "delete" }
      ],

      itemAdd: {
        MSSV:null,
        DuDieuKien:null
      },

      nameSv: "",
      MSSV: ""
    };
  },
  props: {
    itemEdit: null,
    idDel: null,
    acceptDel: null,
    acceptEdit: null
  },

  methods: {
    updateProp() {
      if (this.itemEdit && this.itemEdit.SinhVien) {
        this.nameSv = this.itemEdit.SinhVien.Name;
        this.MSSV = this.itemEdit.SinhVien.MSSV;
      }
    },
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