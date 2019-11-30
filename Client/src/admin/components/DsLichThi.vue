<template>
  <div>
    <b-container fluid class="mt-2">
      <b-row>
        <b-col lg="1">
          <b-button size="sm">Thêm mới</b-button>
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
          <ActionDsLichThi
            @changeMode="changeMode"
            @update="update"
            @add="add"
            :itemEdit="itemEdit"
            :idDel="idDel"
            :idEdit="idEdit"
            :acceptDel="acceptDel"
            :acceptEdit="acceptEdit"
          />
        </b-col>
      </b-row>
      <b-col lg="12" v-show="items==null ? true: false">Chưa có lịch thi nào!</b-col>
      <b-col lg="12" v-if="items==null ? false: true">
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
        >
          <template v-slot:cell(details)="row">
            <b-link :href="'#/admin/dslichthi/'+ row.item.id">Chi tiết</b-link>
          </template>
        </b-table>

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
import ActionDsLichThi from "./utils/ActionDsLichThi";
export default {
  components: {
    ActionDsLichThi
  },
  data() {
    return {
      fields: [
        { key: "LopHocPhan.Name", label:"Mã học phần", sortable: true },
        { key: "CaThi.Name",label:"Ca thi", sortable: true },
        { key: "PhongThi.Name",label:"Phòng thi", sortable: true },
        { key: "NgayThi",label:"Ngày Thi", sortable: true },
        { key: "GioBatDau",label:"Giờ bắt đầu", sortable: true },
        { key: "GioKetThuc",label:"Giờ kết thúc", sortable: true },
        { key: "Count",label:"Đã đăng ký", sortable: true },
        { key: "details", label: "" }
      ],
      //items: null,
      items: [
        {
          LichThiId: 0,
          LopHocPhan: { Name: "INT3306 2" },
          CaThi: { Name: "2" },
          PhongThi: { Name: "1" },
          NgayThi: "20/11/2019",
          GioBatDau: "17:00",
          GioKetThuc: "19:00",
          Count: "10"
        },
        {
          LichThiId: 1,
          LopHocPhan: { Name: "INT3306 1" },
          Cathi: { Name: "2" },
          Phongthi: { Name: "1" },
          NgayThi: "20/11/2019",
          GioBatDau: "15:00",
          GioKetThuc: "17:00",
          Count: "10"
        },
        {
          LichThiId: 2,
          LopHocPhan: { Name: "INT3306 1" },
          Cathi: { Name: "2" },
          Phongthi: { Name: "1" },
          NgayThi: "20/11/2019",
          GioBatDau: "07:00",
          GioKetThuc: "09:00",
          Count: "10"
        },
        {
          LichThiId: 3,
          LopHocPhan: { Name: "INT3306 1" },
          Cathi: { Name: "2" },
          Phongthi: { Name: "1" },
          NgayThi: "20/11/2019",
          GioBatDau: "07:00",
          GioKetThuc: "09:00",
          Count: "10"
        },
        {
          LichThiId: 4,
          LopHocPhan: { Name: "INT3306 1" },
          Cathi: { Name: "2" },
          Phongthi: { Name: "1" },
          NgayThi: "20/11/2019",
          GioBatDau: "07:00",
          GioKetThuc: "09:00",
          Count: "10"
        }
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
      searchCount: null,

      selectAble: false,
      selected: "edit",
      itemsSelected: Array,
      idEdit: null,
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
      // for (var i in this.items) {
      //   if (this.items[i].id == obj.id) {
      //     this.items[i].age = obj.age;
      //     this.items[i].first_name = obj.first_name;
      //     this.items[i].last_name = obj.last_name;
      //     // console.log(this.items[i])
      //   }
      // }
    },
    add(obj) {
      obj.id = null;
      this.items.push(obj);
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
  