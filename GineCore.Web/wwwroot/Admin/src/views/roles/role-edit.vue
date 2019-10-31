<template>
  <i-form :model="editRoleInfo" ref="editRoleForm" :label-width="100" :rules="rules">
    <Form-item label="用户组名称" prop="roleName">
      <i-input v-model="editRoleInfo.roleName" placeholder="请输入用户组名称"></i-input>
    </Form-item>
    <Form-item label="用户组描述">
      <i-input v-model="editRoleInfo.describe" placeholder="请输入用户组描述"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      editRoleInfo: {
        roleName: "",
        describe: ""
      },
      rules: {
        roleName: [
          {required: true, message: "用户组名称不能为空", trigger: "blur" },
          {type: 'string', message: "用户组名称不得少于2位", min: 2, trigger: "blur"}
        ]
      }
    };
  },

  methods: {
    changeInfo(obj) {
      //不直接赋值是因为修改表单中的值会导致修改列表的值
      for (let index in obj) {
        this.editRoleInfo[index] = obj[index];
      }
    },

    clear() {
      this.editRoleInfo.roleName = "";
      this.editRoleInfo.describe = "";
    }
  }
};
</script>

