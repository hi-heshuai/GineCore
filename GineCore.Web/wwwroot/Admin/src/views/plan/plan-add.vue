<template>
  <i-form :model="addPlan" ref="addRoleForm" :label-width="100" :rules="rules">
    <Form-item label="计划名称" prop="title">
      <i-input v-model="addPlan.title" placeholder="请输入计划名称"></i-input>
    </Form-item>
    <Form-item label="计划描述">
      <i-input v-model="addPlan.description" placeholder="请输入计划描述"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      addPlan: {
        title: "",
        description: ""
      },
      rules: {
        title: [
          {required: true, message: "计划名称不能为空", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    clear() {
      this.addPlan.title = "";
      this.addPlan.description = "";
    }
  }
};
</script>

