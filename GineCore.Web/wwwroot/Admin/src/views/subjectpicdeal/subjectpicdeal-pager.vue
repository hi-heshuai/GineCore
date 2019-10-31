<style lang="less">
@import "../../styles/common.less";
@import "../tables/components/table.less";
</style>

<template>
  <div>
    <Row class="margin-top-10">
      <Col span="24">
        <Card>
          <Row>
            <Col span="4">
              <Input style="width:80px" v-model="minIndex" />-
              <Input style="width:80px" v-model="maxIndex" />
            </Col>
            <Col span="4" push="3">
              <i-select v-model="taskId" style="width:200px">
                <i-option :value="''">全部</i-option>
                <i-option
                  v-for="(item, index) in taskList"
                  :value="item.id"
                  :key="index"
                >{{ item.title }}</i-option>
              </i-select>
            </Col>
            <Col span="4" push="4">
              <i-select v-model="status" style="width:200px">
                <i-option :value="''">全部</i-option>
                <i-option
                  v-for="(item, index) in statusList"
                  :value="item.key"
                  :key="index"
                >{{ item.value }}</i-option>
              </i-select>
            </Col>
            <Col span="4" push="5">
              <Input
                icon="search"
                v-model="searchKey"
                placeholder="请输入关键字搜索..."
                @on-enter="reload"
              />
            </Col>
            <Col span="4" push="6">
              <i-button type="info" @click="reload">
                <Icon type="ios-search-strong"></Icon>&nbsp;查询
              </i-button>
            </Col>
          </Row>
          <Row :gutter="10" class="margin-top-10">
            <hs-table :options="options" ref="tableRef"></hs-table>
          </Row>
        </Card>
      </Col>
    </Row>
  </div>
</template>

<script>
import HsTable from "../../components/hs-table";

export default {
  name: "subjectpicdeal-table",
  components: {
    HsTable
  },
  data() {
    let self = this;

    return {
      searchKey: "",
      status: "AlreadyDeal",
      minIndex: 0,
      taskId: "",
      maxIndex: 0,
      statusList: [],
      taskList: [],
      options: {
        url: "/SubjectPicDeal/GetPager",
        data: {
          Status: "AlreadyDeal",
          SearchKey: self.searchKey,
          MinIndex: self.minIndex,
          MaxIndex: self.maxIndex,
          TaskId: self.taskId,
          IsWeb: true,
          page: 1,
          rows: 10
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "试题编号", key: "subjectId", width: 250 },
          {
            title: "图片",
            key: "id",
            className: "images",
            render(h, params) {
              let row = params.row;
              return h("viewer", [
                h("img", {
                  attrs: {
                    src: self.hsConfig.apiFileUrl + row.picUrl,
                    "v-gallery": Math.random()
                  },
                  style: {
                    maxWidth: "400px",
                    maxHeight: "200px"
                  }
                })
              ]);
            }
          },
          {
            title: "识别文字",
            key: "content",
            render(h, params) {
              let row = params.row;
              return h("div", [
                h("Input", {
                  attrs: {
                    type: "textarea",
                    autosize: { minRows: 2, maxRows: 5 },
                    src: self.hsConfig.apiFileUrl + row.picUrl,
                    value: row.content
                  },
                  on: {
                    "on-change"(event) {
                      params.row.content = event.target.value;
                    }
                  }
                })
              ]);
            }
          },
          { title: "分词比例", key: "kAnalyzerPercentage", width: 80 },
          {
            title: "操作",
            key: "Id",
            width: 150,
            align: "center",
            render(h, params) {
              return h("div", [
                h(
                  "i-button",
                  {
                    props: {
                      type: "primary",
                      size: "small"
                    },
                    on: {
                      click: () => {
                        self.save(params.row);
                      }
                    }
                  },
                  "入库"
                ),
                h(
                  "i-button",
                  {
                    props: {
                      type: "error",
                      size: "small"
                    },
                    style: {
                      marginLeft: "5px"
                    },
                    on: {
                      click: () => {
                        self.fail(params.row);
                      }
                    }
                  },
                  "无需入库"
                )
              ]);
            }
          }
        ],
        success: this.dealData
      }
    };
  },

  mounted() {
    this.$refs.tableRef.getData();
    this.getStatusList();
    this.getTaskList();
  },

  methods: {
    //重新加载表格
    reload() {
      this.options.data.SearchKey = this.searchKey;
      this.options.data.Status = this.status;
      this.options.data.MinIndex = this.minIndex;
      this.options.data.MaxIndex = this.maxIndex;
      this.options.data.TaskId = this.taskId;
      this.$refs.tableRef.reload(this.options);
    },

    //入库
    save(row) {
      let _this = this;
      this.ajax.post({
        url: "/SubjectPicDeal/Edit",
        data: row,
        success: function(data) {
          if (data.result) {
            layer.alert("操作成功");
            _this.reload();
          } else {
            layer.alert("操作失败");
          }
        }
      });
    },

    //无需入库
    fail(row) {
      let _this = this;
      this.ajax.post({
        url: "/SubjectPicDeal/fail",
        data: { Id: row.id },
        success: function(data) {
          if (data.result) {
            layer.alert("操作成功");
            _this.reload();
          } else {
            layer.alert("操作失败");
          }
        }
      });
    },

    //获取状态列表
    getStatusList() {
      let _this = this;
      this.ajax.post({
        url: "/SubjectPicDeal/GetStatusList",
        success: function(data) {
          if (data.result) {
            _this.statusList = data.data;
          }
        }
      });
    },

    //获取任务列表
    getTaskList() {
      let _this = this;
      this.ajax.post({
        url: "/SystemTask/GetPager",
        data: {
          TaskCode: "SubjectPicToText",
          TaskStatus: "WaitWokerEdit",
          page: 1,
          rows: 999
        },
        success: function(data) {
          _this.taskList = data.rows;
        }
      });
    },

    //处理列表数据
    dealData(data) {
      return data;
    }
  }
};
</script>
