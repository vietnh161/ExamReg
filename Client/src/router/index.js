import Vue from 'vue'
import Router from 'vue-router'
import store from '../store/store'

Vue.use(Router)

import DsMonThiHocPhan from "../admin/components/DsMonThiHocPhan"
import DsTheoLichThi from "../admin/components/DsTheoLichThi"
import DsSinhVien from "../admin/components/DsSinhVien"
import DsHocPhan from "../admin/components/DsHocPhan"
import DsLichThi from "../admin/components/DsLichThi"
import DsDuThi from "../admin/components/DsDuThi"
import AdminLayout from "../layouts/Admin"

import StudenLayout from "../layouts/Student"

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: "/login",
      name: "Login",
      component: require('../login/Login.vue').default
    },
    {
      path: '/admin',
      name: 'admin-layout',
      meta: { requiresAuth: true, permission: 'Admin' },
      component: AdminLayout,
      children: [
        {
          path: "sinhvien",
          name: "Danh Sách Sinh Viên",
          component: DsSinhVien
        },

        {
          path: "monthi",
          alias: "",
          name: "Danh Sách Môn Thi ",

          component: DsMonThiHocPhan
        },
        {
          path: "monthi-hocphan/addmulti",
          name: "Thêm môn thi / học phần",

          component: require('../admin/components/utils/AddMultiMtHp.vue').default
        },
        {
          path: "monthi/:mahp?/:id?",
          name: "Danh Sách Học Phần ",

          component: DsHocPhan
        },

        {
          path: "sinhvien/addmulti",
          name: "Thêm danh sách sinh viên",

          component: require('../admin/components/utils/AddSvWithExel').default
        },
        {
          path: "hocphan",
          name: "Danh Sách Học Phần",

          component: DsHocPhan
        },
        {
          path: "sinhvien-hocphan/addmulti",
          name: "Thêm sinh viên / học phần",

          component: require('../admin/components/utils/AddMultiSvHp.vue').default
        },
        {
          path: "sinhvien-hocphan/addnotpass",
          name: "Thêm sinh viên không đủ điều kiện",

          component: require('../admin/components/utils/AddSvTach.vue').default
        },
        {
          path: "duthi/:mahp?/:id?",
          name: "Danh Sách sinh viên đã đăng ký",

          component: DsDuThi
        },
        {
          path: "lichthi/",
          name: "Danh Sách Lịch Thi",

          component: DsLichThi
        },
        {
          path: "lichthi/:id?",
          name: "Danh sách sinh viên đã đăng ký",

          component: DsTheoLichThi
        },
        {
          path: "kithi/",
          name: "Tạo kì thi",

          component: require('../admin/components/TaoKiThi.vue').default
        },
        {
          path: "indanhsachts/:id?",
          name: "In danh sách thí sinh",
          component: require('../admin/components/InDsThiSinh.vue').default
        },
      ]
    },
    {
      path: '/student',
      name: 'studen-layout',
      meta: { requiresAuth: true, permission: "Student" },
      component: StudenLayout,
      children: [
        {
          path: 'home',
          alias: '',
          name: 'Trang chủ',
          component: require('../student/components/Home.vue').default
        },
        {
          path: 'dang-ki-lich-thi',
          name: 'Đăng kí lịch thi',
          component: require('../student/components/DangKiLichThi.vue').default
        },

        {
          path: 'in-lich-thi',
          name: 'In lịch thi',
          component: require('../student/components/InLichThi.vue').default
        },
        {
          path: 'profile',
          name: 'Thông tin cá nhân',
          component: require('../student/components/ProFile.vue').default
        }


      ]

    },
    {
      path: "/*",
      name: "404",
      component: require('../layouts/NotFound.vue').default
    },
    {
      path: "/test",
      name: "test",
      meta: { layout: "student", requiresAuth: true },
      component: require('../admin/components/test').default
    },
  ]
})

router.beforeEach((to, from, next) => {

  const requiresAuth = to.matched.some(x => x.meta.requiresAuth)
  const permission = to.matched[0].meta.permission
  const isLoggedIn = store.getters.isLoggedIn;
  const role = store.state.user.role
  if (requiresAuth && !isLoggedIn) {
      next('/login')
  } else if (requiresAuth && isLoggedIn) {
      if(permission === role){
        next()
      }else{
        next('/')
      }
     
  } else {
      next()
  }
})

export default router;