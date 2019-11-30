import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

 import DsMonThiHocPhan from "../admin/components/DsMonThiHocPhan"
 import DsTheoLichThi from "../admin/components/DsTheoLichThi"
 import DsSinhVien from "../admin/components/DsSinhVien"
 import DsHocPhan from "../admin/components/DsHocPhan"
 import DsLichThi from "../admin/components/DsLichThi"
 import DsDuThi from "../admin/components/DsDuThi"

export default new Router({
  routes: [
    // {
    //   path: '/admin/',
    //   name: 'Hello',
    //   component: Hello,
    // },
    {
      path: "/admin/monthi-hocphan",
      name: "Môn thi / Học phần",
      component: DsMonThiHocPhan
    },
    {
      path: "/admin/dssinhvien",
      name: "Danh Sách Sinh Viên",
      component: DsSinhVien
    },
    {
      path: "/admin/dshocphan",
      name: "Danh Sách Học Phần",
      component: DsHocPhan
    },
    {
      path: "/admin/dsduthi/:mahp?/:id?",
      name: "Danh Sách sinh viên đã đăng ký",
      component: DsDuThi
    },
    {
      path: "/admin/dslichthi/",
      name: "Danh Sách Lịch Thi",
      component: DsLichThi
    },
    {
      path: "/admin/dslichthi/:id?",
      name: "Danh sách sinh viên đã đăng ký",
      component: DsTheoLichThi
    },
  ]
})
