<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="1">
          <b-button size="sm">Add Multi</b-button>
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

        <b-col lg="3">
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
        </b-col>
      </b-row>
      <b-table
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
    </b-container>
  </div>
</template>

<script>
import ActionDsSv from "./utils/ActionDsSv";
export default {
  components: {
    ActionDsSv
  },
  data() {
    return {
      fields: [
        { key: "MSSV", sortable: true },
        { key: "FullName", sortable: true },
        { key: "Address", sortable: true },
        { key: "BirthDay", sortable: true },
        { key: "Phone", sortable: true },
        { key: "Email", sortable: true },
        { key: "User.UserName", sortable: true }
      ],
      items: [
        {
          SinhVienid: 1,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 2,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 3,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 4,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 5,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 6,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 7,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 8,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 9,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 10,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 11,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 12,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 13,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 14,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 15,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 16,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 17,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 18,
          MSSV: 40,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        },
        {
          SinhVienid: 19,
          MSSV: 39,
          FullName: "Larsen Larsen",
          Address: "Hn",
          BirthDay: "1999-11-16",
          Phone: "0987654431",
          Email: "123@vnu.edu.vn",
          UserId: 1,
          User: { UserName: "huyviet" }
        }
      ],

      id: "tableSv",
      perPage: 10,
      pageOptions: [10, 20, 40],
      filter: null,
      totalRows: 1,
      currentPage: 1,
      sortBy: "MSSV",
      sortDesc: false,
      mode: "single",
      searchCount: null,

      selected: "edit",
      itemsSelected: Array,
      itemEdit: Object,
      idDel: Array,
      acceptDel: null,
      acceptEdit: null
    };
  },
  computed: {},
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
    },
    add(obj) {
      obj.id = null;
     console.log(obj);
    },
    del(ids){

    },
    changeMode(mode, selected) {
      this.mode = mode;
      if (selected == "edit") {
        this.acceptDel = false;
      } else {
        this.acceptEdit = false;
      }
      this.selected = selected;
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