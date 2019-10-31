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
              <i-button type="primary" icon="plus-round" @click="openAddModal">创建</i-button>
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
      <special-database-add-form ref="specialDatabaseAddRef" ></special-database-add-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="add">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="addModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加弹出窗体 结束-->

     <!--编辑弹出窗体 -->
    <Modal v-model="editModal" :width="500" title="创建任务" :mask-closable="false" :scrollable="true">
      <special-database-edit-form ref="specialDatabaseEditRef" ></special-database-edit-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="add">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="editModal = false">取消</i-button>
      </div>
    </Modal>
    <!--编辑弹出窗体 结束-->
  </div>
</template>

<script>
import HsTable from "../../components/hs-table";
import specialDatabaseAddForm from "./special-database-add";
import specialDatabaseEditForm from "./special-database-edit";

export default {
  name: "special-database-table",
  components: {
    HsTable,
    specialDatabaseAddForm,
    specialDatabaseEditForm
  },
  data() {
    let self = this;

    return {
      searchKey: "",
      addModal: false,
      editModal: false,
      timeOut: null,
      options: {
        url: "/SpecialDataBase/GetPager",
        data: {
          SearchKey: this.searchKey,
          taskCode: this.taskCode,
          page: 1,
          rows: 10
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "专题库", key: "name" },
          { title: "分类根节点", key: "classId" },
          { title: "创建时间", key: "createTimeStr" },
          {
            title: "操作",
            key: "id",
            width: 100,
            align: "center",
            render(h, params) {
              let row = params.row;
              let oprateArr = [];

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

              return h("div", oprateArr);
            }
          }
        ],
        success: this.dealData
      }
    };
  },

  created() {
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
      var specialDatabaseAdd = this.$refs.specialDatabaseAddRef;

      specialDatabaseAdd.$refs.addSpecialDatabaseForm.validate(valid => {
        if (valid) {
          var addModel = specialDatabaseAdd.addSpecialDatabaseInfo;
          let _this = this;
          this.ajax.post({
            url: "/SpecialDataBase/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                layer.msg("添加成功");
                specialDatabaseAdd.clear();
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
            url: "/specialDatabase/Delete",
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
  },

  destroy() {
    if (this.timeOut !== null) {
      clearInterval(this.timeOut);
    }
  }
};
</script>
