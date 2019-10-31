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
              <Input icon="search" placeholder="请输入关键字搜索..." @on-enter="reload" />
            </Col>
          </Row>
          <Row :gutter="10" class="margin-top-10">
            <hs-table :options="options" ref="tableRef"></hs-table>
          </Row>
        </Card>
      </Col>
    </Row>
    <!--添加弹出窗体 -->
    <Modal v-model="addModal" :width="500" title="添加人员" :mask-closable="false" :scrollable="true">
      <user-add-form ref="userAddRef"></user-add-form>
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
      title="编辑人员信息"
      :mask-closable="false"
      :scrollable="true"
      @on-ok="add"
    >
      <user-edit-form ref="userEditRef"></user-edit-form>
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
import UserAddForm from "./user-add";
import UserEditForm from "./user-edit";

export default {
  name: "user-table",
  components: {
    HsTable,
    UserAddForm,
    UserEditForm
  },
  data() {
    let self = this;

    return {
      searchKey: "",
      addModal: false,
      editModal: false,
      editUserInfo: {},
      options: {
        url: "/UserInfo/GetPager",
        data: {
          Key: this.searchKey,
          page: 1,
          rows: 10
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "账号", key: "userName" },
          { title: "真实名称", key: "realName" },
          { title: "手机号码", key: "mobilePhone" },
          { title: "生日", key: "bithdayStr" },
          { title: "电子邮箱", key: "email" },
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
                        self.remove(params.row);
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
      var userAdd = this.$refs.userAddRef;
      userAdd.$refs.addUserForm.validate(valid => {
        if (valid) {
          var addModel = userAdd.addUserInfo;
          let _this = this;
          this.ajax.post({
            url: "/UserInfo/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                userAdd.clear();
                layer.msg("添加成功");
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
      this.$refs.userEditRef.changeInfo(row);
      this.editModal = true;
    },

    //编辑
    edit() {
      var userEdit = this.$refs.userEditRef;
      userEdit.$refs.editUserForm.validate(valid => {
        if (valid) {
          var editModel = userEdit.editUserInfo;
          let _this = this;
          this.ajax.post({
            url: "/UserInfo/Edit",
            data: editModel,
            success: function(data) {
              if (data.result) {
                _this.editModal = false;
                layer.msg("编辑成功");
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
    remove(row) {
      let _this = this;
      this.$Modal.confirm({
        title: "警告",
        content: "<p>是否确认删除该记录？</p>",
        onOk: () => {
          _this.ajax.get({
            url: "/UserInfo/Delete",
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
      this.options.data.key = this.searchKey;
      this.$refs.tableRef.reload(this.options);
    },

    //处理列表数据
    dealData(data) {
      return data;
    }
  }
};
</script>
