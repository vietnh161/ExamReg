<template>
  <div>
    <b-button @click="print" size="sm">Print</b-button>
    <b-button @click="exportHTML" size="sm">Dowload</b-button>
    <div id="print">
      <b-container fluid>
        <table style="width: 100%; border: none; border-collapse: collapse;">
          <tr>
            <td style="width: 40%; text-align: center; vertical-align: top;">
              <p
                style="text-transform: uppercase; font-weight: normal; margin: 0; padding: 0; font-size: 12pt;"
              >ĐẠI HỌC QUỐC GIA HÀ NỘI</p>
              <p
                style="text-transform:uppercase; margin: 0; padding: 0; font-size:12pt; font-weight:bold;"
              >TRƯỜNG ĐẠI HỌC C&#212;NG NGHỆ</p>
            </td>
            <td style="width: 60%; text-align: center; vertical-align: top;">
              <p
                style="text-transform: uppercase; font-weight: bold; margin: 0; padding: 0; font-size: 12pt;"
              >CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</p>
              <p style="margin: 0; padding: 0; font-weight: bold;">Độc lập - Tự do - Hạnh phúc</p>
            </td>
          </tr>
        </table>
        <h1
          style="text-align: center; text-transform: uppercase; font-weight: bold; font-size: 14pt; margin: 30px 0 0 0; padding: 0;"
        >DANH SÁCH THÍ SINH DỰ THI MÔN {{lichThi.tenLhp}}</h1>
        <table style="width: 100%; border: none; border-collapse: collapse; margin-top: 30px;">
          <tr>
            <td>Mã học phần</td>
            <td>
              <b>{{lichThi.maLhp}}</b>
            </td>
            <td>Ca Thi</td>
            <td>
              <b>{{lichThi.caThi}}</b>
            </td>
            <td>Phòng thi</td>
            <td>
              <b>{{lichThi.phongThi}}</b>
            </td>
          </tr>
          <tr>``
            <td>Ngày thi</td>
            <td>
              <b>{{lichThi.ngayThi}}</b>
            </td>
            <td>Giờ bắt đầu</td>
            <td>
              <b>{{lichThi.gioBatDau}}</b>
            </td>
            <td>Giờ kết thúc</td>
            <td>
              <b>{{lichThi.gioKetThuc}}</b>
            </td>
          </tr>
        </table>
        <br />
        <table style="border-bottom: 1px solid #000; width: 100%; border-collapse:collapse;">
          <tr>
            <th
              style="border-top:1px solid #000; border-left:1px solid #000; text-align:center;"
            >Stt</th>
            <th
              style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;"
            >Họ và tên</th>
            <th
              style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;"
            >MSSV</th>
            <th
              style="border-top: 1px solid #000; border-left: 1px solid #000;border-right:1px solid #000; text-align: center;"
            >Ngày sinh</th>
          </tr>
          <tr v-for="item in items" v-bind:key="item.lichThiId">
            <th
              style="border-top:1px solid #000; border-left:1px solid #000; text-align:center;"
            >{{item.stt}}</th>
            <th
              style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;"
            >{{item.fullName}}</th>
            <th
              style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;"
            >{{item.mssv}}</th>

            <th
              style="border-top: 1px solid #000; border-left: 1px solid #000; border-right:1px solid #000; text-align: center;"
            >{{item.birthDay}}</th>
          </tr>
        </table>
        <p style="font-size: 12pt; margin-top:15px;">Danh sách gồm có: {{total}} sinh viên</p>
        <table style="width: 100%; border: none; border-collapse: collapse; margin-top: 30px;">
          <tr>
            <td style="width: 50%; vertical-align: top; text-align: center;">
             
            </td>
            <td style="width: 50%; text-align: center; vertical-align: top; ">
              <p
                style="font-size: 12pt; margin:0; padding:0;"
              >Hà Nội, ngày ..... tháng ..... năm {{date.year}}</p>
              <p
                style="font-weight: bold; margin: 0; padding: 0; text-transform: uppercase; font-size: 12pt;"
              >XÁC NHẬN CỦA PHÒNG ĐÀO TẠO</p>
              <p style="font-weight: bold; margin-top: 80px;">&nbsp;</p>
            </td>
          </tr>
        </table>
      </b-container>
    </div>
  </div>
</template>
<script>
import printd from "printd";
import axios from "axios";
export default {
  data() {
    return {
      stt: 0,
      date: {
        day: "",
        month: "",
        year: ""
      },
      lichThi: {},
      fields: [
        { key: "lopHocPhan.name", label: "Tên học phần" },
        { key: "lopHocPhan.title", label: "Mã học phần" },
        { key: "caThi.name", label: "Ca thi" },
        { key: "phongThi.name", label: "Phòng thi", sortable: true },
        { key: "ngayThi", label: "Ngày Thi", sortable: true },
        { key: "gioBatDau", label: "Giờ bắt đầu" },
        { key: "gioKetThuc", label: "Giờ kết thúc" }
      ],
      items: [],
      total: null,
      isBusy: false
    };
  },
  created() {
    axios
      .get(
        "http://localhost:63834/api/lichthi/getbyid/" + this.$route.params.id
      )
      .then(rsp => {
        this.lichThi = {
          tenLhp: rsp.data.lopHocPhan.name,
          maLhp: rsp.data.lopHocPhan.title,
          ngayThi: rsp.data.ngayThi.substr(0, 10),

          caThi: rsp.data.caThi.name,
          gioBatDau: rsp.data.gioBatDau.substr(0, 5),
          gioKetThuc: rsp.data.gioKetThuc.substr(0, 5),
          phongThi: rsp.data.phongThi.name
        };
        rsp.data.lopHocPhan.name +
          " - " +
          rsp.data.lopHocPhan.title +
          " - " +
          rsp.data.ngayThi.substr(0, 10) +
          " - " +
          rsp.data.caThi.name +
          " ( " +
          rsp.data.gioBatDau.substr(0, 5) +
          "-" +
          rsp.data.gioKetThuc.substr(0, 5) +
          " ) " +
          rsp.data.phongThi.name;
      });
    this.stt = 1;
    var d = new Date();
    this.date = {
      day: d.getDate(),
      month: d.getMonth() + 1,
      year: d.getFullYear()
    };

    this.myprovider();
  },
  methods: {
    getUser() {
      axios.get("http://localhost:63834/api/account/getProfile").then(rsp => {
        this.sinhVien = rsp.data;

        this.sinhVien.birthDay = this.sinhVien.birthDay.slice(0, 10);
      });
    },
    myprovider() {
      this.isBusy = true;
      axios
        .get(
          "http://localhost:63834/api/sinhvien/getallbylichthi?lichThiId=" +
            this.$route.params.id
        )
        .then(data => {
          this.items = data.data.result;
          this.total = data.data.totalRow;
          this.items.forEach(x => {
            x.birthDay = x.birthDay.slice(0, 10);
            x.stt = this.stt++;
          });
          this.isBusy = false;
        });
    },
    print() {
      const p = new printd();
      p.print(document.getElementById("print"));
    },
    exportHTML() {
      var vm = this,
        word = `<html xmlns:o='urn:schemas-microsoft-com:office:office xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta charset='utf-8'><title>Export HTML to Word Document with JavaScript</title></head><body>${vm.word}</body></html>`;

      var source =
        "data:application/vnd.ms-word;charset=utf-8," +
        encodeURIComponent(document.getElementById("print").innerHTML);
      var fileDownload = document.createElement("a");
      document.body.appendChild(fileDownload);
      fileDownload.href = source;
      fileDownload.download = "document.doc";
      fileDownload.click();
      document.body.removeChild(fileDownload);
    }
  }
};
</script>