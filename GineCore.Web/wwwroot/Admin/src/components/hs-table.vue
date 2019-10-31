<style lang="less">
@import "../styles/common.less";
.hs-page-box {
  margin-top: 10px;
  text-align: center;
}
</style>

<template>
  <div>
    <Row>
      <Col span="24">
        <div>
          <i-table border ref="hsTableRef" :columns="options.fileds" :data="dataList" @on-selection-change="onSelectionChange"></i-table>
        </div>
        <div class="hs-page-box">
          <Page
            :total="totalRecord"
            show-sizer
            show-elevator
            :showTotal="true"
            :pageSizeOpts="[5, 10, 15, 20]"
            @on-change="pageChange"
            @on-page-size-change="pageSizeChange"
          ></Page>
        </div>
      </Col>
    </Row>
  </div>
</template>

<script>
export default {
  name: "base-table",
  components: {},
  data() {
    return {
      dataList: [],
      totalRecord: 0,
      page: 1,
      pageSize: 10
    };
  },

  props: {
    options: {
      type: Object,
      required: true
    }
  },

  methods: {
    getData() {
      let _this = this;
      this.options.data.page = this.page;
      this.options.data.rows = this.pageSize;

      this.ajax.pager({
        url: this.options.url,
        data: this.options.data,
        success: function(data) {
          if (_this.options.success) {
            data = _this.options.success(data);
          }
          _this.totalRecord = data.records;
          _this.dataList = data.rows;
        }
      });
    },

    pageChange(page) {
      this.page = page;
      this.getData();
    },

    pageSizeChange(pageSize){
      this.page = 1;
      this.pageSize = pageSize;
      this.getData();
    },

    reload(options) {
      this.options = options;
      this.page = 1;
      this.getData();
    },

    reloadCurrentPage() {
      this.getData();
    },

    reloadEdit() {
      this.getData();
    },

    getSelections() {
      return this.$refs.hsTableRef.getSelection();
    },

    onSelectionChange(selection, index) {
      this.$refs.hsTableRef.getSelection()
    }
  },

  created() {
    //this.getData();
  }
};
</script>
