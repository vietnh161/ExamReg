<template>
  
    <b-card>
             <label style="color:red">{{message}}</label>
      <b-card-body>
        <div>
          <label>Title</label>
          <b-input type="text" v-model="itemEdit.title" required v-if="isEdit"></b-input>
          <b-input type="text" v-model="itemAdd.title" required v-else></b-input>
        </div>
        <div class="mt-2">
          <label>Name</label>
          <b-input type="text" v-model="itemEdit.name" required v-if="isEdit"></b-input>
          <b-input type="text" v-model="itemAdd.name" required v-else></b-input>
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
      isEdit: Boolean,
      itemEdit:{},
      message:""
    },
    data() {
    return {
      itemAdd:{
        title:"",
        name:""
      }
    };
  },
  methods: {
    add() {
      if (this.itemAdd.Title != "" && this.itemAdd.Name != "") {
        this.$emit("add", this.itemAdd);
      }
    },
    update() {
         if (this.itemEdit.Title != "" && this.itemEdit.Name != "") {
    
         this.$emit("update", this.itemEdit);
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