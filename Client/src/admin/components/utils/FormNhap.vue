<template>
  
    <b-card>
      <b-card-body>
        <div>
          <label>Title</label>
          <b-input type="text" v-model="editItem.title" required v-if="isEdit"></b-input>
          <b-input type="text" v-model="title" required v-else></b-input>
        </div>
        <div class="mt-2">
          <label>Name</label>
          <b-input type="text" v-model="editItem.name" required v-if="isEdit"></b-input>
          <b-input type="text" v-model="name" required v-else></b-input>
        </div>
      </b-card-body>
      <b-card-footer class="text-center">
        <b-button class="btn btn-primary btn-sm" @click="update" v-if="isEdit">Edit</b-button>
        <b-button class="btn btn-primary btn-sm" @click="add" v-else>Create</b-button>
        <b-button class="btn btn-danger btn-sm" @click="closeForm">Cancel</b-button>
      </b-card-footer>
    </b-card>
   
  
</template>

<script>
export default {
  props: {
      item:null,
      isEdit: Boolean
    },
    data() {
    return {
      title: "",
      name: "",
      editItem:  {
          id: this.item.id,
          title: this.item.title,
          name: this.item.name,
      }
    };
  },
  methods: {
    add() {
      if (this.title != "" && this.name != "") {
        const obj = {
          id: null,
          title: this.title,
          name: this.name
        };
        this.$emit("add", obj);
      }
      this.title = "";
      this.name = "";
    },
    update() {
         if (this.editItem.title != "" && this.editItem.name != "") {
    //     const obj = {
    //       id: this.item.id,
    //       title: this.item.title,
    //       name: this.item.name
    //     };
         this.$emit("update", this.editItem);
       }
    //   this.title = "";
    //   this.name = "";
    },
    closeForm() {
      this.$emit("closeForm", false);
    }
  }
};
</script>