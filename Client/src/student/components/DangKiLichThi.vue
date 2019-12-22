<template>
  <div>
    <b-container fluid>
      <b-row class="mt-2">
        <b-col lg="6">
          <h4>Danh sách lịch thi hiện có</h4>
        </b-col>
        <b-col lg="12" class="mt-2">
          <b-table
            :sticky-header="'400px'"
            bordered
            outlined
            hover
            :fields="fields1"
            :busy.sync="isBusy1"
            id="tb-lichthi"
            :items="items1"
          >
            <template v-slot:cell(action)="row">
              <b-button
                size="sm"
                @click="add(row.item)"
                v-if="!row.item.disable && row.item.status"
              >đăng ký</b-button>
              <b-button size="sm" disabled v-if="row.item.disable || !row.item.status">đăng ký</b-button>
            </template>
          </b-table>
        </b-col>
      </b-row>
      <b-row class="mt-2">
        <b-col lg="6">
          <h4>Danh sách lịch thi đã đăng kí</h4>
        </b-col>
        <b-col lg="12" class="mt-2">
          <b-table
            :sticky-header="'400px'"
            bordered
            outlined
            hover
            :fields="fields2"
            :items="items2"
            :busy.sync="isBusy2"
            id="tb-dadky"
          >
            <template v-slot:cell(action)="row">
              <b-button size="sm" @click="del(row.item)" class="btn-danger">Xóa</b-button>
            </template>
          </b-table>
        </b-col>
        <b-col>
          <b-button size="sm" @click="commit">Ghi nhận</b-button>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      fields1: [
        { key: "lopHocPhan.name", label: "Tên học phần" },
        { key: "lopHocPhan.title", label: "Mã học phần" },
        { key: "caThi.name", label: "Ca thi" },
        { key: "phongThi.name", label: "Phòng thi", sortable: true },
        { key: "ngayThi", label: "Ngày Thi", sortable: true },
        { key: "gioBatDau", label: "Giờ bắt đầu" },
        { key: "gioKetThuc", label: "Giờ kết thúc" },
        { key: "count", label: "Đã đăng ký", sortable: true },
        { key: "action", label: "" }
      ],

      fields2: [
        { key: "lopHocPhan.name", label: "Tên học phần" },
        { key: "lopHocPhan.title", label: "Mã học phần" },
        { key: "caThi.name", label: "Ca thi" },
        { key: "phongThi.name", label: "Phòng thi", sortable: true },
        { key: "ngayThi", label: "Ngày Thi", sortable: true },
        { key: "gioBatDau", label: "Giờ bắt đầu" },
        { key: "gioKetThuc", label: "Giờ kết thúc" },
        { key: "action", label: "" }
      ],

      items1: [],
      items2: [],
      listDisable: [],
      idDisable: [],
      isBusy1: false,
      isBusy2: false,
      ids: [],
      idsAdd: [],
      idsDel: []
    };
  },
  created() {
    this.myProvider2();
  },
  mounted() {
    this.myProvider1();
  },
  methods: {
    myProvider1() {
      this.isBusy1 = true;
      axios.get("http://localhost:63834/api/sinhvien/getlichthi").then(data => {
        this.items1 = data.data.result;
        if (this.listDisable != null) {
          this.items1.forEach(item => {
            item.ngayThi = item.ngayThi.slice(0, 10);
            this.listDisable.forEach(x => {
              if (this.check(item, x)) {
                item.disable = true;
              }
            
            });
          });
        }

        this.isBusy1 = false;
      });
    },
    myProvider2() {
      this.isBusy2 = true;
      axios
        .get("http://localhost:63834/api/sinhvien/getlichthidadky")
        .then(data => {
          this.items2 = data.data.result;
          // let a = [];
          this.items2.forEach(x => {
            x.ngayThi = x.ngayThi.slice(0, 10);
            this.listDisable.push(x);
            this.ids.push(x.lichThiId);
          });
          // this.listDisable = a;
          //  console.log(this.listDisable)
          this.isBusy2 = false;
        });
    },
    check(item1, item2) {
      if (item1.lopHocPhan.title === item2.lopHocPhan.title) return true;
      let d1 = new Date();
      let d2 = new Date();
      let dd1 = new Date();
      let dd2 = new Date();
      d1.setHours(
        item1.gioBatDau.slice(0, 2),
        item1.gioBatDau.slice(3, 5),
        item1.gioBatDau.slice(6, 8)
      );
      d2.setHours(
        item2.gioBatDau.slice(0, 2),
        item2.gioBatDau.slice(3, 5),
        item2.gioBatDau.slice(6, 8)
      );
      dd1.setHours(
        item1.gioKetThuc.slice(0, 2),
        item1.gioKetThuc.slice(3, 5),
        item1.gioKetThuc.slice(6, 8)
      );
      dd2.setHours(
        item2.gioKetThuc.slice(0, 2),
        item2.gioKetThuc.slice(3, 5),
        item2.gioKetThuc.slice(6, 8)
      );

      if (item1.ngayThi == item2.ngayThi) {
        if (
          Math.min(d1, dd1) <= Math.max(d2, dd2) &&
          Math.max(d1, dd1) >= Math.min(d2, dd2)
        ) {
          return true;
        }
        return false;
      } else {
        // console.log("no");
        return false;
      }
    },
    add(item) {
      if (this.isBusy2 == false) {
        this.items2.push(item);
        this.idsAdd.push(item.lichThiId);
        if (this.idsDel.indexOf(item.lichThiId) != -1)
          this.idsDel.splice(this.idsDel.indexOf(item.lichThiId), 1);

        let list = this.items1.filter(
          x => x.lopHocPhan.title === item.lopHocPhan.title
        );
        list.forEach(element => {
          element.disable = true;
        });
        this.items1.forEach(elm => {
          if (this.check(elm, item)) {
            elm.disable = true;
          }
        });
      }
    },
    del(item) {
      this.items2.splice(this.items2.indexOf(item), 1);
      this.idsDel.push(item.lichThiId);
      if (this.idsAdd.indexOf(item.lichThiId) != -1)
        this.idsAdd.splice(this.idsAdd.indexOf(item.lichThiId), 1);
      this.items1.forEach(elm => {
        if (this.check(elm, item)) {
          elm.disable = false;
        }
      });
      this.items2.forEach(elm => {
        this.items1.forEach(x => {
          if (x.lopHocPhan.title == elm.lopHocPhan.title) {
            x.disable = true;
          }
        });
      });
    },
    commit() {
      this.idsAdd = [...new Set(this.idsAdd)];
      this.idsDel = [...new Set(this.idsDel)];
      this.idsAdd = this.idsAdd.filter(x => !this.ids.includes(x));
      this.idsDel = this.idsDel.filter(x => this.ids.includes(x));

      let obj = {
        idsAdd: this.idsAdd,
        idsDel: this.idsDel
      };
      this.$bvModal
        .msgBoxOk("Bạn có muốn xóa lưu lại thay đổi không ")
        .then(value => {
          if (value) {
            axios
              .post("http://localhost:63834/api/sinhvien/dangkylichthi", obj)
              .then(rsp => {
                this.$bvModal
                  .msgBoxOk("Đăng ký thành công " + rsp.data.totalRow)
                  .then(value => {
                    this.$router.go("/student/dang-ki-lich-thi");
                  });
              })
              .catch(err => {
                //  console.log(err.response.data)
                if (err.response.data === "nothing change") {
                  this.$bvModal.msgBoxOk("Không có sự thay đổi");
                }
                if (err.response.data === "full") {
                  this.$bvModal.msgBoxOk("Thành công").then(value => {
                    this.$router.go("/student/dang-ki-lich-thi");
                  });
                }
              });
          }
        });
    }
  }
};
</script>