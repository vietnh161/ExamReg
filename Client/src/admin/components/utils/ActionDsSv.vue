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
      @show="updateUsername()"
    >
      <b-form @submit.stop.prevent="EditSubmit">
        <b-form-group label="MSSV">
          <b-form-input v-model="itemEdit.MSSV" type="number" required></b-form-input>
        </b-form-group>
        <b-form-group label="FullName">
          <b-form-input v-model="itemEdit.FullName" required></b-form-input>
        </b-form-group>
        <b-form-group label="Address">
          <b-form-input v-model="itemEdit.Address"></b-form-input>
        </b-form-group>
        <b-form-group label="BirthDay">
          <b-form-input v-model="itemEdit.BirthDay" type="date" required></b-form-input>
        </b-form-group>
        <b-form-group label="Phone">
          <b-form-input v-model="itemEdit.Phone" type="number"></b-form-input>
        </b-form-group>
        <b-form-group label="Email">
          <b-form-input v-model="itemEdit.Email"></b-form-input>
        </b-form-group>
        <b-form-group label="UserName">
          <b-form-input v-model="username" disabled></b-form-input>
        </b-form-group>
        <b-row align-h="end">
          <b-col lg="4">
            <b-button size="sm" @click="closeModal('edit')">Cancel</b-button>
            <b-button type="submit" variant="primary" size="sm">OK</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-modal>
    <b-modal id="modal-add" ref="add" title="BootstrapVue" hide-header hide-footer >
      <b-form @submit.stop.prevent="AddSubmit">
        <b-form-group label="MSSV">
          <b-form-input id="MSSV" v-model="itemAdd.MSSV" type="number" required></b-form-input>
        </b-form-group>
        <b-form-group label="FullName">
          <b-form-input id="FullName" v-model="itemAdd.FullName" required></b-form-input>
        </b-form-group>
        <b-form-group label="Address">
          <b-form-input id="Address" v-model="itemAdd.Address"></b-form-input>
        </b-form-group>
        <b-form-group label="BirthDay">
          <b-form-input id="BirthDay" v-model="itemAdd.BirthDay" type="date" required></b-form-input>
        </b-form-group>
        <b-form-group label="Phone">
          <b-form-input id="Phone" v-model="itemAdd.Phone" type="number"></b-form-input>
        </b-form-group>
        <b-form-group label="Email">
          <b-form-input id="Email" v-model="itemAdd.Email"></b-form-input>
        </b-form-group>
        <b-form-group label="UserName">
          <b-form-input id="UserName" v-model="itemAdd.User.UserName" required></b-form-input>
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
        
          MSSV: null,
          FullName: "",
          Address: "",
          BirthDay: "",
          Phone: "",
          Email: "",
          UserId: null,
          User: { UserName: "" }
      },
      isAdd: false,
      username: ""
    };
  },
  props: {
    itemEdit: null,
    idDel: null,
    acceptDel: null,
    acceptEdit: null
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
    updateUsername() {
      
       this.username =
        this.itemEdit && this.itemEdit.User ? this.itemEdit.User.UserName : "";
        
    },
    EditSubmit() {
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
        this.$emit("del", this.idDel);
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