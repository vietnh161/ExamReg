<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="12">
          <h4
            class="mb-4 mt-2"
          >Phát triển ứng dụng web: INT3306 1 - Ngày 20/11/2019 - Ca 1(07:00-09:00) - Phòng 123</h4>
        </b-col>
        <b-col lg="1">
          
        </b-col>
        <b-col lg="2">
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
            <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
          </b-form-group>
        </b-col>
        <b-col lg="6">
          <b-form-group
            label="Tìm kiếm"
            label-cols-sm="1"
            label-align-sm="right"
            label-size="sm"
            label-for="filterInput"
            class="mb-0"
          >
            <b-input-group size="sm">
              <b-form-input
                v-model="filter"
                type="search"
                id="filterInput"
                placeholder="Type to Search"
              ></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-col>

        <b-col lg="3"></b-col>
      </b-row>
      <b-col lg="12" v-show="items==null ? true: false">Chưa có lịch thi nào!</b-col>
      <b-col lg="12" v-if="items==null ? false: true">
        <b-table
          class="mt-2"
          bordered
          outlined
          hover
          stacked="md"
          :id="id"
          :selectable="true"
          :select-mode="mode"
          :items="items"
          :fields="fields"
          :current-page="currentPage"
          :per-page="perPage"
          :filter="filter"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          @filtered="onFiltered"
          @row-selected="getItemSelected"
        ></b-table>
        <b-row>
          <b-col lg="4">
            <p
              v-if="filter == false || filter == null"
            >Đang hiển thị {{perPage > totalRows? totalRows : perPage}} / {{perPage > totalRows? perPage:totalRows}} item</p>
            <p
              v-if="!filter == false"
            >Đang hiển thị {{searchCount > perPage ? perPage: searchCount}} / {{searchCount > perPage ? searchCount: perPage}} item</p>
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
      </b-col>
    </b-container>
  </div>
</template>
<script>


export default {
  components: {

  },
  data() {
    return {
      fields: [
        { key: "mssv", sortable: true },
        { key: "fullname", sortable: true },
        {
          key: "qua_mon",
          label: "Qua môn",
          formatter: value => {
            return value ? "Có" : "Không";
          }
        }
      ],
      items: [
        { id: 1, mssv: 40, fullname: "Dickerson Macdonald", qua_mon: true },
        { id: 2, mssv: 21, fullname: "Larsen Shaw", qua_mon: true },
        { id: 3, mssv: 89, fullname: "Geneva Wilson", qua_mon: true },
        { id: 4, mssv: 38, fullname: "Jami Carney", qua_mon: true },
        { id: 5, mssv: 40, fullname: "Dickerson Carney", qua_mon: true },
        { id: 6, mssv: 21, fullname: "Larsen Macdonald", qua_mon: true },
        { id: 7, mssv: 89, fullname: "Geneva Shaw", qua_mon: false },
        { id: 8, mssv: 38, fullname: "Jami Wilson", qua_mon: true },
        { id: 9, mssv: 40, fullname: "Dickerson Shaw", qua_mon: true },
        { id: 10, mssv: 21, fullname: "Larsen Wilson", qua_mon: false },
        { id: 11, mssv: 89, fullname: "Geneva Macdonald", qua_mon: true },
        { id: 12, mssv: 38, fullname: "Jami Macdonald", qua_mon: false },
        { id: 13, mssv: 40, fullname: "Dickerson Carney", qua_mon: true },
        { id: 14, mssv: 21, fullname: "Larsen Carney", qua_mon: true },
        { id: 15, mssv: 89, fullname: "Geneva Shaw", qua_mon: false },
        { id: 16, mssv: 38, fullname: "Jami Wilson", qua_mon: true },
        { id: 17, mssv: 40, fullname: "Dickerson Wilson", qua_mon: true },
        { id: 18, mssv: 21, fullname: "Larsen Carney", qua_mon: true },
        { id: 19, mssv: 89, fullname: "Geneva Shaw", qua_mon: true },
        { id: 20, mssv: 38, fullname: "Jami Macdonald", qua_mon: false }
      ],

      id: "tableSv",
      perPage: 10,
      pageOptions: [10, 20, 40],
      filter: null,
      totalRows: 1,
      currentPage: 1,
      sortBy: "id",
      sortDesc: false,
      mode: "single",

      searchCount: null
    };
  },
  mounted() {
    // Set the initial number of items
    this.totalRows = this.items.length;
  },
  methods: {
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.searchCount = filteredItems.length;
      this.currentPage = 1;
    },
    update(obj) {
      console.log(obj);
      for (var i in this.items) {
        if (this.items[i].id == obj.id) {
          this.items[i].age = obj.age;
          this.items[i].first_name = obj.first_name;
          this.items[i].last_name = obj.last_name;
          // console.log(this.items[i])
        }
      }
    },
    add(obj) {
      obj.id = null;
      this.items.push(obj);
    },
     getItemSelected(items) {
      if (this.selected == "edit") {
        this.getItemEdit(items[0]);
      } else {
        this.getItemDel(items);
      }
    },
    getItemEdit(obj) {
      this.itemEdit = new Object();
      if (obj != null) this.acceptEdit = true;
      else this.acceptEdit = false;
      for (const item in obj) {
        this.itemEdit[item] = obj[item];
      }
      for (const item in this.itemEdit) {
        this.idEdit = this.itemEdit[item];
     
        delete this.itemEdit[item];
        
        break;
      }
    },
    getItemDel(arr) {
      this.idDel = new Array();
      this.acceptEdit = false;
      for (const item in arr) {
        this.idDel.push(arr[item].id);
      }
      if (this.idDel.length > 0) {
        this.acceptDel = true;
      } else this.acceptDel = false;
    }

  }
};
</script>