<template>
  <div id="authority-user">
    <Col span="24">
      <row>
        <Col span="10">
          <Row>
            <Col span="10" push="14" style="margin-bottom: 10px;">
              <Input icon="search" placeholder="请输入关键字搜索..." v-model="searchKey" @on-click="reloadTable" />
            </Col>
          </Row>
          <hs-table :options="options" ref="tableRef"></hs-table>
        </Col>
        <Col span="4" style="text-algin:center;margin-top:100px;">
          <Row>
            <Col span="18" push="6">
              <i-button type="primary" icon="ios-arrow-thin-right" @click="bindUser">绑定</i-button>
            </Col>
          </Row>
          <Row style="margin-top: 30px;">
            <Col span="18" push="6">
              <i-button type="defalut" icon="ios-arrow-thin-left" @click="unBindUser">解除</i-button>
            </Col>
          </Row>
        </Col>
        <Col span="10">
          <Row>
            <Col span="10" push="14" style="margin-bottom: 10px;">
              <Input icon="search" placeholder="请输入关键字搜索..." v-model="searchKeyUser" @on-click="reloadTableUser" />
            </Col>
          </Row>
          <hs-table :options="optionsBindUser" ref="tableBindUserRef"></hs-table>
        </Col>
      </row>
    </Col>
    <div style="clear:both"></div>
  </div>
</template>

<style>
#authority-user .ivu-table-wrapper {
  min-height: 300px;
}
</style>

<script>
import HsTable from "../../components/hs-table";

export default {
  components: {
    HsTable
  },
  data() {
    return {
      roleId: null,
      searchKey: "",
      searchKeyUser: "",
      options: {
        url: "/UserInfo/GetPager",
        data: {
          Key: this.searchKey,
          page: 1,
          rows: 10,
          IsUserRole: true
        },
        fileds: [
          { type: "selection", width: 60, align: "center" },
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "账号", key: "userName" },
          { title: "真实名称", key: "realName" }
        ],
        success: this.dealData
      },
      optionsBindUser: {
        url: "/UserInfo/GetPager",
        data: {
          key: this.searchKeyUser,
          RoleId: this.roleId,
          page: 1,
          rows: 10,
          IsUserRole: true
        },
        fileds: [
          { type: "selection", width: 60, align: "center" },
          { title: "编号", key: "rowsNumber", width: 80 },
          { title: "账号", key: "userName" },
          { title: "真实名称", key: "realName" }
        ],
        success: this.dealData
      }
    };
  },

  mounted() {},

  methods: {
    loadData() {
      this.optionsBindUser.data.RoleId = this.roleId;
      this.$refs.tableRef.getData();
      this.$refs.tableBindUserRef.getData();
    },
    bindUser() {
      var sections = this.$refs.tableRef.getSelections();
      if (sections.length === 0) {
        layer.alert("请选择绑定用户");
        return;
      }
      let userIds = "";
      sections.forEach(section => {
        userIds += section.id + ",";
      });
      let _this = this;
      this.ajax.post({
        data: { roleId: this.roleId, userIds: userIds },
        url: "/Roles/BindUser",
        success: function(data) {
          if (data.result) {
            layer.alert("绑定成功");
            _this.loadData();
          } else {
            layer.alert(data.errorInfo);
          }
        }
      });
    },
    unBindUser() {
      var sections = this.$refs.tableBindUserRef.getSelections();
      if (sections.length === 0) {
        layer.alert("请选择解除绑定用户");
        return;
      }
      let userIds = "";
      sections.forEach(section => {
        userIds += section.id + ",";
      });
      let _this = this;
      this.ajax.post({
        data: { roleId: this.roleId, userIds: userIds },
        url: "/Roles/UnBindUser",
        success: function(data) {
          if (data.result) {
            layer.alert("解除绑定成功");
            _this.loadData();
          } else {
            layer.alert(data.errorInfo);
          }
        }
      });
    },

    //重新加载表格
    reloadTable() {
      this.options.data.key = this.searchKey;
      this.$refs.tableRef.reload(this.options);
    },

    //重新加载表格
    reloadTableUser() {
      this.optionsBindUser.data.key = this.searchKeyUser;
      this.$refs.tableBindUserRef.reload(this.optionsBindUser);
    },

    //处理列表数据
    dealData(data) {
      return data;
    }
  }
};
</script>
