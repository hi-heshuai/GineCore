<template>
  <i-form :model="addSpecialChapter" ref="addSpecialChapterForm" :label-width="100" :rules="rules">
    <Form-item label="章节名称" prop="chapterName">
      <i-input v-model="addSpecialChapter.chapterName" placeholder="请输入章节名称"></i-input>
    </Form-item>
    <Form-item label="章节关键词">
      <i-input v-model="addSpecialChapter.keyWords" type="textarea" :autosize="{minRows: 4,maxRows: 5}" placeholder="请输入章节章节关键词,英（中）文逗号间隔"></i-input>
    </Form-item>
    <Form-item label="章节排序号">
      <i-input v-model="addSpecialChapter.rankNum" placeholder="请输入章节排序号"></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      addSpecialChapter: {
        chapterName: "",
        rankNum: "1",
        keyWords: ''
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
    clear() {
      this.addSpecialChapter.chapterName = "";
      this.addSpecialChapter.rankNum = "1";
      this.addSpecialChapter.keyWords = "";
    }
  }
};
</script>

