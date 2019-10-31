<template>
  <div>
    <Row span="24">
      <Col span="12" v-for="(item, index) in list" :key="index" style="margin-top: 10px">
        <Col span="4">{{item.viewName}}: </Col>
        <Col span="18">
          <i-input
            type="textarea"
            v-model="item.skillPoints"
            :autosize="{minRows: 2,maxRows: 5}"
            placeholder="请输入..."
          ></i-input>
        </Col>
      </Col>
    </Row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      parentId: null,
      taskId: null,
      list: []
    };
  },

  created() {},

  methods: {
    getExamClassList() {
      let _this = this;
      this.ajax.post({
        data: { parentId: this.parentId },
        url: "/ExamClass/GetChildList",
        success: function(data) {
          if (data.result) {
            _this.list = data.data;
          }
        }
      });
    }
  }
};
</script>
