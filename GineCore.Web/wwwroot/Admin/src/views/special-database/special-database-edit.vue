<template>
  <i-form :model="editSpecialDatabaseInfo" ref="editSpecialDatabaseForm" :label-width="100" :rules="rules">
   <Form-item label="专题库名称" prop="name">
      <i-input v-model="editSpecialDatabaseInfo.name" placeholder="请输入专题库名称"></i-input>
    </Form-item>
    <Form-item label="分类根节点">
      <i-input v-model="editSpecialDatabaseInfo.classId" placeholder="请输入分类根节点"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      editSpecialDatabaseInfo: {
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

  methods: {
    changeInfo(obj) {
      //不直接赋值是因为修改表单中的值会导致修改列表的值
      for (let index in obj) {
        this.editSpecialDatabaseInfo[index] = obj[index];
      }
    },

    clear() {
      this.editSpecialDatabaseInfo.name = "";
      this.editSpecialDatabaseInfo.classId = "";
    }
  }
};
</script>

