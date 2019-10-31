<template>
  <i-form :model="editSpecialChapter" ref="editSpecialChapterForm" :label-width="100" :rules="rules">
    <Form-item label="章节名称" prop="chapterName">
      <i-input v-model="editSpecialChapter.chapterName" placeholder="请输入章节名称"></i-input>
    </Form-item>
     <Form-item label="章节关键词">
      <i-input v-model="editSpecialChapter.keyWords" type="textarea" :autosize="{minRows: 4,maxRows: 5}" placeholder="请输入章节章节关键词,英（中）文逗号间隔"></i-input>
    </Form-item>
    <Form-item label="章节排序号">
      <i-input v-model="editSpecialChapter.rankNum" placeholder="请输入章节排序号"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      editSpecialChapter: {
        chapterName: "",
        rankNum: "1",
        keyWords: ""
      },
      rules: {
        chapterName: [
          {required: true, message: "章节名称不能为空", trigger: "blur" },
          {type: 'string', message: "章节名称不得少于2位", min: 2, trigger: "blur"}
        ]
      }
    };
  },
  methods: {
    changeInfo(obj) {
      //不直接赋值是因为修改表单中的值会导致修改列表的值
      for (let index in obj) {
        this.editSpecialChapter[index] = obj[index];
      }
    },

    clear() {
      this.editSpecialChapter.chapterName = "";
      this.editSpecialChapter.rankNum = "";
    }
  }
};
</script>

