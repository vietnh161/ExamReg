<template>
  <!-- eslint-disable-next-line vue/max-attributes-per-line -->
  <div>
    <b-container class="mt-2">
      <b-row class="justify-content-md-center">
         <b-col lg="3">
          <b-form-group
            label="Hiển thị"
            label-align-sm="right"
            label-cols-sm="6"
            label-cols-md="4"
            label-cols-lg="3"
            label-size="sm"
            label-for="perPageSelect"
            class="mb-0"
          >
            <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions" @input="getItemPerPage"></b-form-select>
          </b-form-group>
        </b-col>
        <b-col lg="6" md="auto">
          <b-form-group
            label="Tìm kiếm"
            label-cols-sm="2"
            label-align-sm="right"
            label-size="sm"
            label-for="filterInput"
            class="mb-0"
          >
            <b-input-group size="sm">
              <b-form-input
                @keyup="search"
                v-model="filter"
                type="search"
                id="filterInput"
                placeholder="Mã học phần"
              ></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
          <label v-if="!filter == false">Tìm thấy {{searchCount}}</label>
        </b-col>
      </b-row>
      <b-list-group class="mt-2">
        <b-list-group-item
          :href="'#/admin/dsduthi/'+item.Title+'/' + item.LopHocPhanId"
          v-for="item in itemsPerPage"
          v-bind:key="item.LopHocPhanId"
          v-if="filter == false"
        >{{item.Title}} {{item.Name}}
        </b-list-group-item>
        <b-list-group-item
         :href="'#/admin/dsduthi/' +item.Title+'/' + item.LopHocPhanId"
          v-for="item in itemsSearch"
          v-bind:key="item.LopHocPhanId"
           v-if="!filter == false"
        >{{item.Title}} {{item.Name}}
        
        </b-list-group-item>
      </b-list-group>
        <b-row class="mt-2">
        <b-col lg="4"  v-if="filter == false">
          <p>Đang hiển thị {{perPage}} / {{totalRows}} item</p>
        </b-col>
         <b-col lg="4" v-if="!filter == false">
          <p>Đang hiển thị {{itemsSearch.length < perPage ? itemsSearch.length : perPage }} / {{searchCount}} item</p>
        </b-col>
        <b-col lg="4">
          <b-pagination
            v-if="filter == false"
            v-model="currentPage"
            :total-rows="totalRows"
            :per-page="perPage"
            align="fill"
            size="sm"
            class="my-0"
            @input="nextPage"
          ></b-pagination>
          
        </b-col>
        <b-col lg="4"></b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
export default {
  data() {
    return {
      filter: "",
      itemsSearch: [{}],
      searchCount:null,
      itemsPerPage: [{}],
      perPage: 15,
      pageOptions: [15, 25, 35],
      totalRows: 1,
      currentPage: 1,

      items: [
        { LopHocPhanId: 0, Title:"INT3306 1",Name: "Phat trien ung dung web" ,MonThiId:""},
        { LopHocPhanId: 1, Title:"INT3306 2",Name: "Phat trien ung dung web" ,MonThiId:""},
        { LopHocPhanId: 2, Title:"INT3307 1",Name: "Phat trien ung dung web" ,MonThiId:""},
        { LopHocPhanId: 3, Title:"INT3307 2",Name: "Phat trien ung dung web" ,MonThiId:""},
      ]
    };
  },
   mounted() {
    // Set the initial number of items
     this.itemsPerPage = new Array();
    this.totalRows = this.items.length;
    for(var i = 0; i < this.perPage; i++){
     
            if(this.items[i] != undefined)
            this.itemsPerPage.push(this.items[i]);
        }
  },
  methods: {
    search() {
        this.itemsSearch = new Array();
      var result = this.items.filter(x => x.Title.toUpperCase().includes(this.filter.toUpperCase()));
      this.searchCount = result.length;
      if(this.searchCount > this.perPage){
          for(var i = 0; i< this.perPage;i++){
              this.itemsSearch.push(result[i]);
          }
      }else{
           for(var i = 0; i< result.length;i++){
              this.itemsSearch.push(result[i]);
          }
      }
       this.currentPage = 1;
    },
    getItemPerPage(){
        this.itemsPerPage = new Array();
        for(var i = 0; i < this.perPage; i++){
            if(this.items[i] != null)
           this.itemsPerPage.push(this.items[i]);
        }
    },

    nextPage(page){
        this.itemsPerPage = new Array();
        for(var i = (page-1)*this.perPage; i < page*this.perPage; i++){
            if(this.items[i] != null)
           this.itemsPerPage.push(this.items[i]);
        }
    }
  }
};
</script>