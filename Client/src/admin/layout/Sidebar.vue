<template>
  <div>
    <div class="bg-light border-right" id="sidebar-wrapper">
      <div class="sidebar-heading">Start Bootstrap</div>
      <div class="list-group list-group-flush">
        <SidebarLink v-bind:links="links"></SidebarLink>
      </div>
    </div>
  </div>
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
          name: "Danh sách môn thi",
          link: "#/admin/monthi-hocphan"
        },
          {
          name:  "Danh sách sinh viên",
          link: "#/admin/dssinhvien"
        },
          {
          name: "Danh sách dự thi",
          link: "#/admin/dshocphan"
        },
          {
          name: "Danh sách lịch thi",
          link: "#/admin/dslichthi"
        }
      ]
    };
  },
  methods: {}
};
//  "Môn thi / Học Phần",
//         "Danh sách sinh viên",
//         "Danh sách sinh viên đủ / chưa đủ điều kiện thi",
//         "Quản lí kì thi",
//         "In danh sách sinh viên theo phòng/ca thi"
</script>
<style>
</style>