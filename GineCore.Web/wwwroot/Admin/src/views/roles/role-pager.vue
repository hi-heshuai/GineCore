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
    <Modal v-model="addModal" :width="500" title="添加用户组" :mask-closable="false" :scrollable="true">
      <role-add-form ref="roleAddRef"></role-add-form>
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
      title="编辑用户组信息"
      :mask-closable="false"
      :scrollable="true"
      @on-ok="add"
    >
      <role-edit-form ref="roleEditRef"></role-edit-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="edit">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="editModal = false">取消</i-button>
      </div>
    </Modal>
    <!--编辑弹出窗体 结束-->

    <!--权限树窗体 -->
    <Modal
      v-model="authorityModal"
      :width="500"
      title="编辑用户组权限"
      :mask-closable="false"
      :scrollable="true"
      @on-ok="add"
    >
      <authority-form ref="authorityRef"></authority-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="authority">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="authorityModal = false">取消</i-button>
      </div>
    </Modal>
    <!--权限树窗体 结束-->

    <!--分配用户窗体 -->
    <Modal
      v-model="authorityUserModal"
      :width="1000"
      title="绑定用户"
      :mask-closable="false"
      :scrollable="true"
      @on-ok="add"
    >
      <authority-user-form ref="authorityUserRef"></authority-user-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="authorityUserModal = false">关闭</i-button>
      </div>
    </Modal>
    <!--分配用户窗体 结束-->
  </div>
</template>

<script>
import HsTable from "../../components/hs-table";
import RoleAddForm from "./role-add";
import RoleEditForm from "./role-edit";
import AuthorityForm from "./authority";
import AuthorityUserForm from "./authority-user";

export default {
  name: "role-table",
  components: {
    HsTable,
    RoleAddForm,
    RoleEditForm,
    AuthorityForm,
    AuthorityUserForm
  },
  data() {
    let self = this;

    return {
      searchKey: "",
      addModal: false,
      editModal: false,
      authorityModal: false,
      authorityUserModal: false,
      editroleInfo: {},
      options: {
        url: "/Roles/GetPager",
        data: {
          SearchKey: this.searchKey,
          page: 1,
          rows: 10
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "用户组名称", key: "roleName" },
          { title: "用户组描述", key: "describe" },
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
      var roleAdd = this.$refs.roleAddRef;

      roleAdd.$refs.addRoleForm.validate(valid => {
        if (valid) {
          var addModel = roleAdd.addRoleInfo;
          let _this = this;
          this.ajax.post({
            url: "/Roles/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                layer.msg("添加成功");
                roleAdd.clear();
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
      this.$refs.roleEditRef.changeInfo(row);
      this.editModal = true;
    },

    //编辑
    edit() {
      var roleEdit = this.$refs.roleEditRef;

      roleEdit.$refs.editRoleForm.validate(valid => {
        if (valid) {
          var editModel = roleEdit.editRoleInfo;
          let _this = this;
          this.ajax.post({
            url: "/Roles/Edit",
            data: editModel,
            success: function(data) {
              if (data.result) {
                _this.editModal = false;
                layer.msg("编辑成功");
                roleEdit.clear();
                _this.$refs.tableRef.reloadEdit(editModel);
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    //打开权限窗体
    openAuthority(row){
      this.authorityModal = true;
      this.$refs.authorityRef.initTreeData(row.id);
    },

    //保存权限
    authority() {
      var nodeOjb = this.$refs.authorityRef.getCheckedNodes();
      if(!nodeOjb.nodes){
        layer.alert("请选择菜单");
        return;
      }

      let idsStr = "";
      for(var i = 0; i < nodeOjb.nodes.length; i ++){
        var node = nodeOjb.nodes[i];
        idsStr += node.id + ",";
      }
      
      let _this = this;
      this.ajax.post({
        data: {roleId: nodeOjb.roleId, menuIdsStr: idsStr},
        url: "/Roles/SaveMenusByRoles",
        success: function(data) {
          if(data.result){
            layer.alert("保存成功");
            _this.authorityModal = false;
          }else{
            layer.alert("保存失败");
          }
        }
      });
    },

    //打开分配用户窗体
    openAuthorityUser(row){
      this.authorityUserModal = true;
      this.$refs.authorityUserRef.roleId = row.id;
      this.$refs.authorityUserRef.loadData();
    },

    //删除
    del(row) {
      this.$Modal.confirm({
        title: "警告",
        content: "<p>是否确认删除改菜单及它的子菜单？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.get({
            url: "/Roles/Delete",
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
