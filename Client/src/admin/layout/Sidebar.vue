<template>
  <nav id="sidebar">
    <ul class="list-unstyled components">
      <li class="nav-item">
        <h3 class="custom">Admin Page</h3>
      </li>
      <SidebarLink :links="links"></SidebarLink>
    </ul>
  </nav>
</template>
<script>
import SidebarLink from "./SidebarLink";

export default {
  name: "Sidebar",
  components: {
    SidebarLink
  },
  data() {
    return {
      links: [
        {
          name: "Thêm kì thi",
          link: "/admin/kithi"
        },
        {
          name: "Danh sách môn thi",
          link: "/admin/monthi"
        },
        {
          name: "Danh sách sinh viên",
          link: "/admin/sinhvien"
        },
        {
          name: "Danh sách dự thi",
          link: "/admin/hocphan"
        },
        {
          name: "Danh sách lịch thi",
          link: "/admin/lichthi"
        },
        {
          name: "Thêm danh sách sinh viên",
          link: "/admin/sinhvien/addmulti"
        },
      
       
        
      ]
    };
  },
  methods: {}
};
</script>
<style scoped>
.custom{
  text-align: center;
  padding-top: 5px;
}
</style>