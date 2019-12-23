<template>
  <div>
    <b-container class="mt-2">
      <b-row>
        <b-col lg="12">
          <h5>Hãy chắc chắn rằng file được chọn có format giống như bên dưới hình:</h5>
        </b-col>
        <b-col lg="6">
          <b-form-file  v-model="data" @change="previewFiles" size="sm"></b-form-file>
        </b-col>
        <b-col lg="1"  v-if="isLoading">
          <b-spinner label="Loading..."></b-spinner>
        </b-col>
        <b-col lg="1"  v-if="this.items!=false && isLoading==false">
          <b-button @click="commit" size="sm">Commit</b-button>
        </b-col>
        
        <b-col lg="12" class="mt-2" md="auto">
          <img src="../assets/addSvHp.png" class="img-fluid" alt="Responsive image" />
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import xlsx from "xlsx";
import axios from "axios";
export default {
  data() {
    return {
      data:null,
      items: [],
      img: "addSv.png",
        isLoading:false
      
    };
  },
  methods: {
    previewFiles(e) {
     
      var files = e.target.files,
        f = files[0];
      var reader = new FileReader();
      var data1 = this.items;
      reader.onload = function(e) {
        var data = new Uint8Array(e.target.result);
        var workbook = xlsx.read(data, {
          type: "array",
        });
        let sheetName = workbook.SheetNames[0];
        /* DO SOMETHING WITH workbook HERE */
        // console.log(workbook);
        let worksheet = workbook.Sheets[sheetName];
        let item = xlsx.utils.sheet_to_json(worksheet);
        // data1 = item.slice(0,item.lenth);
        // console.log(data1)
        item.forEach(element => {  
          element.kiThiId= localStorage.kiThiId,
         
          data1.push(element);
        });
      };
      //
    //  console.log(data1);
    
      reader.readAsArrayBuffer(f);
      this.items = data1;
      //  console.log(this.items);
    },
    commit() {
      var a = this;
      a.isLoading = true;
      axios
        .post("http://localhost:63834/api/sinhvienlophp/addmulti", this.items)
        .then(function(response) {
          a.isLoading = false;
          console.log(response);
          var message = 'Thêm thành công '+response.data.successCount+ ' - Thất bại ' + response.data.notSuccessCount;
           a.$bvModal.msgBoxOk(message);
           this.items = [];
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>
<style scoped>
</style>