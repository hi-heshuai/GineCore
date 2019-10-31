<template>
  <i-form :model="addRoleInfo" ref="addRoleForm" :label-width="100" :rules="rules">
    <Form-item label="用户组名称" prop="roleName">
      <i-input v-model="addRoleInfo.roleName" placeholder="请输入用户组名称"></i-input>
    </Form-item>
    <Form-item label="用户组描述">
      <i-input v-model="addRoleInfo.describe" placeholder="请输入用户组描述"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      addRoleInfo: {
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
    clear() {
      this.addRoleInfo.roleName = "";
      this.addRoleInfo.describe = "";
    }
  }
};
</script>

