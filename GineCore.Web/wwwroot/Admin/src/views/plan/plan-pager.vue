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
              <i-button type="primary" icon="plus-round" @click="openAddModal">添加</i-button>
            </Col>
            <Col span="5" push="15">
              <Input
                icon="search"
                placeholder="请输入关键字搜索..."
                v-model="searchKey"
                @on-click="reload"
              />
            </Col>
          </Row>
          <Row :gutter="10" class="margin-top-10">
            <hs-table :options="options" ref="tableRef"></hs-table>
          </Row>
        </Card>
      </Col>
    </Row>
    <!--添加弹出窗体 -->
    <Modal v-model="addModal" :width="500" title="添加计划" :mask-closable="false" :scrollable="true">
      <Plan-add-form ref="planAddRef"></Plan-add-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="add">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="addModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加弹出窗体 结束-->

    <!--编辑弹出窗体 -->
    <Modal
      v-model="editModal"
      :width="500"
      title="编辑计划信息"
      :mask-closable="false"
      :scrollable="true"
      @on-ok="add"
    >
      <Plan-edit-form ref="PlanEditRef"></Plan-edit-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="edit">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="editModal = false">取消</i-button>
      </div>
    </Modal>
    <!--编辑弹出窗体 结束-->
  </div>
</template>

<script>
import HsTable from "../../components/hs-table";
import PlanAddForm from "./plan-add";

export default {
  name: "Plan-table",
  components: {
    HsTable,
    PlanAddForm
  },
  data() {
    let self = this;

    return {
      searchKey: "",
      addModal: false,
      editPlanInfo: {},
      options: {
        url: "/Plan/GetPager",
        data: {
          SearchKey: this.searchKey,
          page: 1,
          rows: 10
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "计划名称", key: "title" },
          { title: "计划描述", key: "description" },
          { title: "创建时间", key: "createTimeStr" },
          {
            title: "操作",
            key: "Id",
            width: 280,
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
                        self.openEditModal(params);
                      }
                    }
                  },
                  "编辑"
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
                        self.openAuthority(params.row);
                      }
                    }
                  },
                  "绑定权限"
                ),
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
                        self.openAuthorityUser(params.row);
                      }
                    }
                  },
                  "绑定用户"
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
                        self.del(params.row);
                      }
                    }
                  },
                  "删除"
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
  },

  methods: {
    //打开添加窗体
    openAddModal() {
      this.addModal = true;
    },

    //添加
    add() {
      var planAdd = this.$refs.planAddRef;

      planAdd.$refs.addPlanForm.validate(valid => {
        if (valid) {
          var addModel = planAdd.addPlan;
          let _this = this;
          this.ajax.post({
            url: "/Plan/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                layer.msg("添加成功");
                planAdd.clear();
                _this.reload();
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    //打开编辑窗体
    openEditModal(params) {
      params.row.index = params.index;
      let row = params.row;
      this.$refs.PlanEditRef.changeInfo(row);
      this.editModal = true;
    },

    //编辑
    edit() {
      var PlanEdit = this.$refs.PlanEditRef;

      PlanEdit.$refs.editPlanForm.validate(valid => {
        if (valid) {
          var editModel = PlanEdit.editPlanInfo;
          let _this = this;
          this.ajax.post({
            url: "/Plan/Edit",
            data: editModel,
            success: function(data) {
              if (data.result) {
                _this.editModal = false;
                layer.msg("编辑成功");
                PlanEdit.clear();
                _this.$refs.tableRef.reloadEdit(editModel);
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
        content: "<p>是否确认删除计划？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.get({
            url: "/Plan/Delete",
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

    //重新加载表格
    reload() {
      this.options.data.SearchKey = this.searchKey;
      this.$refs.tableRef.reload(this.options);
    },

    //处理列表数据
    dealData(data) {
      return data;
    }
  }
};
</script>
