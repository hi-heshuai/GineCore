<template>
  <i-form :model="addSpecialDatabaseInfo" ref="addSpecialDatabaseForm" :label-width="100" :rules="rules">
    <Form-item label="专题库名称" prop="name">
      <i-input v-model="addSpecialDatabaseInfo.name" placeholder="请输入专题库名称"></i-input>
    </Form-item>
    <Form-item label="分类根节点">
      <i-input v-model="addSpecialDatabaseInfo.classId" placeholder="请输入分类根节点"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      addSpecialDatabaseInfo: {
        name: "",
        classId: "",
      },
      rules: {
        title: [
          { required: true, message: "专题库名称不能为空", trigger: "blur" },
          {
            type: "string",
            message: "专题库名称不得少于2位",
            min: 2,
            trigger: "blur"
          }
        ]
      }
    };
  },

  created() {
  },

  methods: {
    clear() {
      this.addSpecialDatabaseInfo.name = "";
      this.addSpecialDatabaseInfo.classId = "";
    },
  }
};
</script>

