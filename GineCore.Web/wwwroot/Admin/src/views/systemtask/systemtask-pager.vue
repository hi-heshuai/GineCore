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
              <i-button type="primary" icon="plus-round" @click="openAddModal">创建任务</i-button>
            </Col>
            <Col span="4" push="7">
              <i-select v-model="taskCode" style="width:200px">
                <i-option
                  v-for="(item, index) in taskTypes"
                  :value="item.key"
                  :key="index"
                >{{ item.value }}</i-option>
              </i-select>
            </Col>
            <Col span="5" push="8">
              <Input placeholder="请输入关键字搜索..." v-model="searchKey" />
            </Col>
            <Col span="4" push="9">
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
    <!--添加弹出窗体 -->
    <Modal v-model="addModal" :width="500" title="创建任务" :mask-closable="false" :scrollable="true">
      <systemtask-add-form ref="systemtaskAddRef" :taskTypes="taskTypes"></systemtask-add-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="add">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="addModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加弹出窗体 结束-->

    <!--添加知识点窗体 -->
    <Modal
      v-model="skillPointsModal"
      :width="1000"
      :top="0"
      title="创建知识点"
      :mask-closable="false"
      :scrollable="true"
    >
      <skill-point-add-form ref="skillPointsRef" :taskTypes="taskTypes"></skill-point-add-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="saveSkillPoints">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="skillPointsModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加知识点窗体 结束-->

     <!--重置任务弹出窗体 -->
    <Modal v-model="resetModal" :width="450" :top="300" title="重置任务时间" :mask-closable="false" :scrollable="true">
      <div>
        <Date-picker type="date" v-model="runTimeDate" placeholder="选择日期" style="width: 200px"></Date-picker>
        <Time-picker v-model="runTimeTime" placeholder="请输入运行时间"></Time-picker>
      </div>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="reset">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="resetModal = false">取消</i-button>
      </div>
    </Modal>
    <!--重置任务弹出窗体 结束-->
  </div>
</template>

<script>
import HsTable from "../../components/hs-table";
import SystemtaskAddForm from "./systemtask-add";
import SkillPointAddForm from "./skillPoint-add";

export default {
  name: "systemtask-table",
  components: {
    HsTable,
    SystemtaskAddForm,
    SkillPointAddForm
  },
  data() {
    let self = this;

    return {
      searchKey: "",
      runTimeDate: '',
      runTimeTime:'',
      addModal: false,
      resetModal: false,
      resetTaskId: '',
      authorityModal: false,
      skillPointsModal: false,
      taskTypes: [],
      taskCode: "",
      timeOut: null,
      options: {
        url: "/SystemTask/GetPager",
        data: {
          page: 1,
          rows: 10
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "任务名称", key: "title" },
          { title: "分类根节点", key: "examClassId" },
          { title: "状态", key: "taskStatusName" },
          { title: "任务分类", key: "taskCodeName" },
          { title: "任务总量", key: "totalNumber" },
          { title: "运行数量", key: "runNumber" },
          { title: "任务定时执行时间", key: "runTimeStr"},
          { title: "任务开始时间", key: "startTimeStr" },
          { title: "任务结束时间", key: "endTimeStr" },
          { title: "创建时间", key: "createTimeStr" },
          {
            title: "操作",
            key: "id",
            width: 100,
            align: "center",
            render(h, params) {
              let row = params.row;
              let oprateArr = [];

              if (false) {
                if (row.taskCode === "ExamPaperChapter") {
                  if (row.taskStatus === "NoWork") {
                    oprateArr.push(
                      h(
                        "i-button",
                        {
                          props: {
                            type: "primary",
                            size: "small"
                          },
                          style: {
                            marginLeft: "5px"
                          },
                          on: {
                            click: () => {
                              self.runTask(params.row);
                            }
                          }
                        },
                        "开始任务"
                      )
                    );
                  } else if (row.taskStatus === "WaitWorkChapterMessage") {
                    oprateArr.push(
                      h(
                        "i-button",
                        {
                          props: {
                            type: "primary",
                            size: "small"
                          },
                          style: {
                            marginLeft: "5px"
                          },
                          on: {
                            click: () => {
                              self.workChapterMessage(params.row);
                            }
                          }
                        },
                        "生成关系"
                      )
                    );
                  }
                }
                if (row.taskCode === "ExamPaperKnowledge") {
                  if (row.taskStatus === "NoWork") {
                    oprateArr.push(
                      h(
                        "i-button",
                        {
                          props: {
                            type: "primary",
                            size: "small"
                          },
                          style: {
                            marginLeft: "5px"
                          },
                          on: {
                            click: () => {
                              self.runTask(params.row);
                            }
                          }
                        },
                        "开始任务"
                      )
                    );
                  }
                }
              }

              if (row.taskCode === "SubjectPicToText") {
                if (row.taskStatus === "WaitWokerEdit") {
                  oprateArr.push(
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
                            self.workerEditDone(params.row);
                          }
                        }
                      },
                      "编辑完成"
                    )
                  );
                }
              }
              if (row.taskStatus === "NoWork") {
                oprateArr.push(
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
                          self.del(params.row);
                        }
                      }
                    },
                    "删除"
                  )
                );
              }

              return h("div", oprateArr);
            }
          }
        ],
        success: this.dealData
      }
    };
  },

  created() {
    this.getTaskTypes();
    /*this.ajax.post({
      url: "/Subject/InsertData",
      success: function(data) {}
    });*/
  },

  mounted() {
    this.$refs.tableRef.getData();
    /*this.timeOut = setInterval(() => {
      this.reloadCurrentPage();
    }, 5000);*/
  },

  methods: {
    //打开添加窗体
    openAddModal() {
      this.addModal = true;
    },

    //添加
    add() {
      var systemtaskAdd = this.$refs.systemtaskAddRef;

      systemtaskAdd.$refs.addsystemtaskForm.validate(valid => {
        if (valid) {
          var addModel = systemtaskAdd.addSystemtaskInfo;
          let _this = this;
          this.ajax.post({
            url: "/SystemTask/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                layer.msg("添加成功");
                systemtaskAdd.clear();
                _this.reload();
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    //删除
    del(row) {
      this.$Modal.confirm({
        title: "警告",
        content: "<p>是否确认删除该任务记录？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.get({
            url: "/systemtask/Delete",
            data: { id: row.id },
            success: function(data) {
              if (data.result) {
                layer.msg("删除成功");
                _this.reload();
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    //运行任务
    runTask(row) {
      let _this = this;
      this.$Modal.confirm({
        title: "警告",
        content: "<p>是否确认运行该任务，不能停止？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.post({
            url: "/SystemTask/RunSystemTask",
            data: { id: row.id },
            success: function(data) {
              if (data.result) {
                layer.alert("已经运行该任务，请耐心等待任务完成");
                _this.reload();
              }
            }
          });
        }
      });
    },

    //打开重置弹出框
    oepnReset(row){
      this.resetModal = true;
      this.resetTaskId = row.id;
    },

    //重置任务
    reset() {
      if(!this.runTimeDate || !this.runTimeTime){
        layer.msg("请录入时间");
        return;
      }
      var time = "{0} {1}".format(this.runTimeDate, this.runTimeTime);

      this.ajax.post({
        url: '/SystemTask/ResetTask',
        data:{runTime: time, Id: this.resetTaskId},
        success: function(data) {
          if(data.result){
            layer.alert("重置任务成功");
          }else{
            layer.alert(data.errorInfo);
          }
        }
      });
    },

    //打开录入知识点框框
    openInsertSkillPoints(row) {
      this.$refs.skillPointsRef.parentId = row.examClassId;
      this.$refs.skillPointsRef.taskId = row.id;
      this.$refs.skillPointsRef.getExamClassList();
      this.skillPointsModal = true;
    },

    //重新加载表格
    reload() {
      this.options.data.SearchKey = this.searchKey;
      this.options.data.taskCode = this.taskCode;
      this.$refs.tableRef.reload(this.options);
    },

    //刷新当页数据
    reloadCurrentPage() {
      this.$refs.tableRef.reloadCurrentPage();
    },

    //获取任务类型
    getTaskTypes() {
      let _this = this;
      this.ajax.post({
        url: "/SystemTask/GetTaskTypes",
        success: function(data) {
          if (data.result) {
            data.data.unshift({ key: "", value: "--请选择--" });
            _this.taskTypes = data.data;
          }
        }
      });
    },

    //保存知识点
    saveSkillPoints() {
      var parentId = this.$refs.skillPointsRef.parentId;
      var taskId = this.$refs.skillPointsRef.taskId;
      var list = this.$refs.skillPointsRef.list;

      var skillPointsStr = JSON.stringify(list);
      let _this = this;
      this.ajax.post({
        data: {
          taskId: taskId,
          parentId: parentId,
          skillPointsStr: skillPointsStr
        },
        url: "/ExamClassSkillPoints/Create",
        success: function(data) {
          if (data.result) {
            _this.skillPointsModal = false;
            layer.alert("知识点录入成功");
          } else {
            layer.alert(data.errorInfo);
          }
        }
      });
    },

    //编辑完成
    workerEditDone(row) {
      this.$Modal.confirm({
        title: "警告",
        content: "<p>是否确认运行该任务，不能停止？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.post({
            data: {
              taskId: row.id
            },
            url: "/SystemTask/WorkerEditDone",
            success: function(data) {
              if (data.result) {
                _this.reload();
                layer.alert("操作成功");
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    //处理列表数据
    dealData(data) {
      return data;
    }
  },

  destroy() {
    if (this.timeOut !== null) {
      clearInterval(this.timeOut);
    }
  }
};
</script>
