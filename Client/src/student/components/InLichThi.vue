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
        >KẾT QUẢ ĐĂNG KÝ MÔN HỌC - HỌC KỲ 2 NĂM HỌC 2019-2020</h1>
        <p
          style="text-align: center; font-weight: bold; margin: 0; padding: 0; font-size: 14pt;"
        >Ngày {{date.day}} tháng {{date.month}} năm {{date.year}}</p>
        <table style="width: 100%; border: none; border-collapse: collapse; margin-top: 30px;">
          <tr>
            <td>Họ và tên</td>
            <td>
              <b>{{sinhVien.fullName}}</b>
            </td>
            <td>Ngày sinh</td>
            <td>
              <b>{{sinhVien.birthDay}}</b>
            </td>
            <td>Mã sinh viên</td>
            <td>
              <b>{{sinhVien.mssv}}</b>
            </td>
          </tr>
        </table>
        <br />
        <table style="border-bottom: 1px solid #000; width: 100%; border-collapse:collapse;">
             <tr>
                <th style="border-top:1px solid #000; border-left:1px solid #000; text-align:center;">Tên học phần</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">Mã học phần</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">Ca thi</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">Phòng thi</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">Ngày thi</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">Giờ bắt đầu</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; border-right:1px solid #000; text-align: center;">Giờ kết thúc</th>
            </tr>
            <tr v-for="item in items" v-bind:key="item.lichThiId">
                <th style="border-top:1px solid #000; border-left:1px solid #000; text-align:center;">{{item.lopHocPhan.name}}</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">{{item.lopHocPhan.title}}</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">{{item.caThi.name}}</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">{{item.phongThi.name}}</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">{{item.ngayThi}}</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; text-align: center;">{{item.gioBatDau}}</th>
                <th style="border-top: 1px solid #000; border-left: 1px solid #000; border-right:1px solid #000; text-align: center;">{{item.gioKetThuc}}</th>
            </tr>

        </table>
        <p style="font-size: 12pt; margin-top:15px;">Tổng số lịch thi đã đăng ký: {{total}}</p>
        <table style="width: 100%; border: none; border-collapse: collapse; margin-top: 30px;">
          <tr>
            <td style="width: 50%; vertical-align: top; text-align: center;">
              <p
                style="font-weight: bold; margin: 0; padding: 0; font-size: 12pt; text-transform:uppercase;"
              >SINH VIÊN</p>
              <p
                style="margin: 0; padding: 0; font-size: 11pt; font-style: italic;"
              >(Ký và ghi rõ họ tên)</p>
              <p style="margin-top:80px;">
                <b>{{sinhVien.fullName}}</b>
              </p>
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
      date: {
          day: '',
          month:'',
          year:'',
      },
      sinhVien: {},
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
      total:null,
      isBusy: false
    };
  },
  created() {
      
      var d = new Date();
      this.date= {
         day: d.getDate(),
         month: d.getMonth() + 1,
         year: d.getFullYear(),
      }
      this.getUser();
    this.myprovider();
  },
  methods: {
      getUser(){
          axios.get("http://localhost:63834/api/account/getProfile")
        .then(rsp => {
            this.sinhVien = rsp.data;
           
            this.sinhVien.birthDay = this.sinhVien.birthDay.slice(0,10);
        })
      },
    myprovider() {
      this.isBusy = true;
      axios
        .get("http://localhost:63834/api/sinhvien/getlichthidadky")
        .then(data => {
          this.items = data.data.result;
           this.total = data.data.totalRow;
          this.items.forEach(x => {
            x.ngayThi = x.ngayThi.slice(0, 10);
          });
          this.isBusy = false;
        });
    },
    print() {
      const p = new printd();
      p.print(document.getElementById("print"));
    },
      exportHTML(){
      var vm = this, word = `<html xmlns:o='urn:schemas-microsoft-com:office:office xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta charset='utf-8'><title>Export HTML to Word Document with JavaScript</title></head><body>${vm.word}</body></html>`;

      var source = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(document.getElementById("print").innerHTML);
      var fileDownload = document.createElement("a");
      document.body.appendChild(fileDownload);
      fileDownload.href = source;
      fileDownload.download = 'document.doc';
      fileDownload.click();
      document.body.removeChild(fileDownload);
    }
  }
};
</script>