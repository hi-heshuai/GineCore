<template>
  <div>
    <Row class="margin-top-10">
      <Col span="24">
        <Card>
          <Row>
            <Col span="4">
              <i-button type="primary" icon="plus-round" @click="openAddModal">添加</i-button>
            </Col>
          </Row>
          <Row :gutter="10" style="margin-top: 10px">
            <tree-grid
              :items="data"
              :columns="columns"
              @on-row-click="rowClick"
              @on-selection-change="selectionClick"
              @on-sort-change="sortClick"
            ></tree-grid>
          </Row>
        </Card>
      </Col>
    </Row>
    <!--添加弹出窗体 -->
    <Modal v-model="addModal" :width="500" title="添加菜单" :mask-closable="false" :scrollable="true">
      <menu-add-form ref="menuAddRef"></menu-add-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="add">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="addModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加弹出窗体 结束-->

    <!--添加弹出窗体 -->
    <Modal v-model="editModal" :width="500" title="编辑菜单" :mask-closable="false" :scrollable="true">
      <menu-edit-form ref="menuEditRef"></menu-edit-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="edit">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="editModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加弹出窗体 结束-->
  </div>
</template>
<script>
import TreeGrid from "../../components/hs-tree-grid.vue";
import MenuAddForm from "./menu-add";
import MenuEditForm from "./menu-edit";
import htmlHelper from "../../libs/htmlHelp.js";

export default {
  data() {
    let _this = this;
    return {
      columns: [
        {
          title: "名称",
          key: "name",
          sortable: true,
          width: "150"
        },
        {
          title: "标识",
          key: "key",
          sortable: true,
          width: "100",
          align: "center"
        },
        {
          title: "类型",
          key: "typeName",
          width: "150",
          align: "center",
          formatter(value, row, column) {
            return htmlHelper.getTagHtml(value, "white");
          }
        },
        {
          title: "排序",
          key: "sort",
          width: "100",
          align: "center"
        },
        {
          title: "图标",
          key: "icon",
          width: "150",
          formatter(value, row, column) {
            return htmlHelper.getIconHtml(value);
          },
          align: "center"
        },
        {
          title: "状态",
          key: "enableMarked",
          width: "100",
          formatter(value, row, column) {
            if (value) {
              return htmlHelper.getBlueTagHtml("启用");
            } else {
              return htmlHelper.getRedTagHtml("停用");
            }
          },
          align: "center"
        },
        {
          title: "操作",
          type: "action",
          actions: [
            {
              type: "default",
              text: "新增子项",
              on: _this.openAddModal
            },
            {
              type: "primary",
              text: "编辑",
              on: _this.openEditModal
            },
            {
              type: "error",
              text: "删除",
              on: _this.del
            }
          ],
          width: "200"
        }
      ],
      data: [],
      addModal: false,
      editModal: false
    };
  },
  components: {
    TreeGrid,
    MenuAddForm,
    MenuEditForm
  },
  mounted() {
    this.getData();
  },
  methods: {
    //获取数据
    getData() {
      let _this = this;

      this.ajax.post({
        url: "/Menus/GetMenus",
        data: {},
        success: data => {
          _this.data = data;
        }
      });
    },

    //打开添加窗体
    openAddModal(item) {
      if (item) {
        this.$refs.menuAddRef.parent = item;
      } else {
        this.$refs.menuAddRef.parent = null;
      }

      this.$refs.menuAddRef.initModel();
      this.addModal = true;
    },

    //添加
    add() {
      this.$refs.menuAddRef.$refs.addMenuForm.validate(valid => {
        if (valid) {
          var addModel = this.$refs.menuAddRef.addMenuInfo;
          let _this = this;
          this.ajax.post({
            url: "/Menus/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                layer.msg("添加成功");
                _this.getData();
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    //打开编辑窗体
    openEditModal(item) {
      this.$refs.menuEditRef.initModel(item);
      this.editModal = true;
    },

    edit() {
      this.$refs.menuEditRef.$refs.editMenuForm.validate(valid => {
        if (valid) {
          var editModel = this.$refs.menuEditRef.editMenuInfo;
          editModel.parent = null;
          editModel.children = null;
          let _this = this;
          this.ajax.post({
            url: "/Menus/Edit",
            data: editModel,
            success: function(data) {
              if (data.result) {
                _this.editModal = false;
                layer.msg("编辑成功");
                _this.getData();
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    del(item) {
      this.$Modal.confirm({
        title: "警告",
        content: "<p>是否确认删除改菜单及它的子菜单？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.post({
            url: "/Menus/Delete",
            data: { id: item.id },
            success: function(data) {
              if (data.result) {
                layer.msg("删除成功");
                _this.getData();
              } else {
                layer.alert(data.errorInfo);
              }
            }
          });
        }
      });
    },

    rowClick(data, index, event) {
    },

    selectionClick(arr) {
    },
    sortClick(key, type) {
    }
  }
};
</script>
