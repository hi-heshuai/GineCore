<template>
  <div>
    <Tree :data="treeData" show-checkbox ref="authorityTree"></Tree>
  </div>
</template>

<script>
export default {
  data() {
    return {
      treeData: [],
      roleId: 0,
    };
  },

  methods: {
    initTreeData(roleId) {
      this.roleId = roleId;
      let _this = this;
      this.ajax.post({
        data: { roleId: roleId },
        url: "/Roles/GetMenusByRoles",
        success: function(data) {
          if (data.result) {
            _this.treeData = data.data;
          }else{
            layer.alert(data.errorInfo);
          }
        }
      });
    },

    getCheckedNodes() {
      var nodes = this.$refs.authorityTree.getCheckedNodes();
      return {nodes: nodes, roleId: this.roleId};
    }
  }
};
</script>
