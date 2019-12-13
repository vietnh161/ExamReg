<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="1">
          <b-link size="sm" href="/admin/dssinhvien/addmulti">Add Multi</b-link>
        </b-col>
        <b-col lg="2">
          <b-form-group
            label="Hien thi"
            label-align-sm="right"
            label-cols-sm="6"
            label-cols-md="4"
            label-cols-lg="3"
            label-size="sm"
            label-for="perPageSelect"
            class="mb-0"
          >
            <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
          </b-form-group>
        </b-col>
        <b-col lg="6">
          <b-form-group
            label="Tim kiem"
            label-cols-sm="1"
            label-align-sm="right"
            label-size="sm"
            label-for="filterInput"
            class="mb-0"
          >
            <b-input-group size="sm">
              <b-form-input
                debounce="1000"
                v-model="filter"
                type="search"
                id="filterInput"
                placeholder="Mssv hoặc Họ và tên"
              ></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-col>

        <b-col lg="3">
          <b-row>
          <ActionDsSv
            @changeMode="changeMode"
            @update="update"
            @add="add"
            @del="del"
            :itemEdit="itemEdit"
            :idDel="idDel"
            :acceptDel="acceptDel"
            :acceptEdit="acceptEdit"
          />
          <b-col lg="6">
          <b-spinner label="loading..." v-show="isBusy"></b-spinner>
          </b-col>
          </b-row>
        </b-col>
        
      </b-row>
      <b-table
        bordered
        outlined
        hover
        stacked="md"
        :busy.sync="isBusy"
        :id="id"
        :selectable="true"
        :select-mode="mode"
        :items="myProvider"
        :fields="fields"
        :current-page="currentPage"
        :per-page="perPage"
        :filter="filter"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        @row-selected="getItemSelected"
      >
       
           
      </b-table>
      <b-row>
        <b-col lg="4">
         <p>Đang hiển thị {{totalRows > perPage ? perPage: totalRows}} / {{totalRows > perPage ? totalRows: perPage}} item</p>
        </b-col>
        <b-col lg="4">
          <b-pagination
            v-model="currentPage"
            pills
            :total-rows="totalRows"
            :per-page="perPage"
            align="fill"
            size="sm"
            class="my-0"
          ></b-pagination>
        </b-col>
        <b-col lg="4"></b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import xlsx from 'xlsx'
export default {
  data(){
    return{
      file1: null,
    }
  },
  methods:{
    previewFiles(e) {
          var files = e.target.files, f = files[0];
          var reader = new FileReader();
          reader.onload = function(e) {
            var data = new Uint8Array(e.target.result);
            var workbook = xlsx.read(data, {type: 'array', cellDates: true, dateNF: 'YYYY-MM-DD'});
            let sheetName = workbook.SheetNames[0]
            /* DO SOMETHING WITH workbook HERE */
            console.log(workbook);
            let worksheet = workbook.Sheets[sheetName];
            let item = xlsx.utils.sheet_to_json(worksheet);
            console.log(item[0])
            item[0].birthDay.setDate(item[0].birthDay.getDate()+1)
           console.log(item[0].birthDay.toISOString())
          };
          reader.readAsArrayBuffer(f);
       }
  }

}
</script>