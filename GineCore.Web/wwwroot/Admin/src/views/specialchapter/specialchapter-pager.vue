<style lang="less">
@import "../../styles/common.less";
@import "../tables/components/table.less";
</style>

<template>
  <div id="specialchapter-page">
    <Row class="margin-top-10">
      <Col span="24">
        <Card>
          <Row>
            <Col span="4">
              <i-select v-model="searchInfo.specialId" @on-change="changeSpecial">
                <i-option
                  v-for="(item, index) in specialChapterList"
                  :value="item.key"
                  :key="index"
                >{{ item.value }}</i-option>
              </i-select>
            </Col>
          </Row>
        </Card>
        <Card class="margin-top-10">
          <Row>
            <Col span="4">
              <Tree :data="examClassTreeData" @on-select-change="treeChange"></Tree>
            </Col>
            <Col span="19" push="1" v-show="isShowTable">
              <Row>
                <Col span="4">
                  <i-button type="primary" icon="plus-round" @click="openAddModual">创建</i-button>
                </Col>
                <Col span="4" push="13">
                  <Input
                    icon="search"
                    v-model="searchInfo.searchKey"
                    placeholder="请输入关键字搜索..."
                    @on-enter="reload"
                  />
                </Col>
                <Col span="4" push="14">
                  <i-button type="info" @click="reload">
                    <Icon type="ios-search-strong"></Icon>&nbsp;查询
                  </i-button>
                </Col>
              </Row>
              <Row :gutter="10" class="margin-top-10">
                <viewer>
                  <hs-table :options="options" ref="tableRef"></hs-table>
                </viewer>
              </Row>
            </Col>
          </Row>
        </Card>
      </Col>
    </Row>
    <!--添加弹出窗体 -->
    <Modal v-model="addModal" :width="500" title="添加章节" :mask-closable="false" :scrollable="true">
      <special-chapter-add-form ref="specialChapterAddRef"></special-chapter-add-form>
      <div slot="footer">
        <i-button type="info" htmlType="button" @click="add">保存</i-button>
        <i-button type="ghost" htmlType="button" @click="addModal = false">取消</i-button>
      </div>
    </Modal>
    <!--添加弹出窗体 结束-->
    <!--编辑弹出窗体 -->
    <Modal v-model="editModal" :width="500" title="编辑章节" :mask-closable="false" :scrollable="true">
      <special-chapter-edit-form ref="specialChapterEditRef"></special-chapter-edit-form>
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
import SpecialChapterAddForm from "./specialchapter-add";
import SpecialChapterEditForm from "./specialchapter-edit";

export default {
  components: {
    HsTable,
    SpecialChapterAddForm,
    SpecialChapterEditForm
  },
  data() {
    let self = this;

    return {
      searchInfo: {
        specialId: 1312,
        searchKey: "",
        examClassId: 0
      },

      specialChapterList: [
        { key: 1312, value: "三基专题库" },
        { key: 110, value: "出版社专题库" }
      ],
      isShowTable: false,
      addModal: false,
      editModal: false,
      examClassTreeData: [],
      options: {
        url: "/SpecialChapter/GetPager",
        data: {
          page: 1,
          rows: 10,
          sidx: "RankNum",
          sord: "asc"
        },
        fileds: [
          { title: "编号", key: "rowsNumber", width: 80, align: "center" },
          { title: "章节序号", key: "rankNum", width: 100, align: "center" },
          {
            title: "章节名称",
            key: "chapterName",
            width: 210,
            align: "center"
          },
          {
            title: "关键字",
            key: "keyWords",
            width: 207,
            align: "center"
          },
          {
            title: "创建时间",
            key: "createTimeStr",
            width: 200,
            align: "center"
          },
          {
            title: "操作",
            key: "id",
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

  created() {
    this.getTree();
  },

  methods: {
    //重新加载表格
    reload() {
      this.options.data.searchKey = this.searchInfo.searchKey;
      this.options.data.examClassId = this.searchInfo.examClassId;

      this.$refs.tableRef.reload(this.options);
    },

    treeChange(node) {
      this.isShowTable = true;
      this.searchInfo.examClassId = node[0].id;
      this.reload();
    },

    changeSpecial() {
      this.getTree();
    },

    getTree() {
      this.searchInfo.examClassId = 0;
      this.isShowTable = false;
      let _this = this;
      this.ajax.post({
        url: "/ExamClass/GetExamClassTree",
        data: { specialId: this.searchInfo.specialId },
        success: function(data) {
          if (data.result) {
            _this.examClassTreeData = data.data;
          }
        }
      });
    },

    openAddModual() {
      this.$refs.specialChapterAddRef.clear();
      this.addModal = true;
    },

    add() {
      let _this = this;
      var specialChapterAdd = this.$refs.specialChapterAddRef;
      specialChapterAdd.$refs.addSpecialChapterForm.validate(valid => {
        if (valid) {
          var addModel = specialChapterAdd.addSpecialChapter;
          addModel.specialId = _this.searchInfo.specialId;
          addModel.classId = _this.searchInfo.examClassId;
          _this.ajax.post({
            url: "/specialChapter/Create",
            data: addModel,
            success: function(data) {
              if (data.result) {
                _this.addModal = false;
                layer.msg("添加成功");
                specialChapterAdd.clear();
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
      this.$refs.specialChapterEditRef.changeInfo(row);
      this.editModal = true;
    },

    //编辑
    edit() {
      var specialChapterEdit = this.$refs.specialChapterEditRef;

      specialChapterEdit.$refs.editSpecialChapterForm.validate(valid => {
        if (valid) {
          var editModel = specialChapterEdit.editSpecialChapter;
          let _this = this;
          this.ajax.post({
            url: "/SpecialChapter/Edit",
            data: editModel,
            success: function(data) {
              if (data.result) {
                _this.editModal = false;
                layer.msg("编辑成功");
                specialChapterEdit.clear();
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
        content: "<p>是否确认删除该章节？</p>",
        onOk: () => {
          let _this = this;
          this.ajax.get({
            url: "/SpecialChapter/Delete",
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
    }
  }
};
</script>

<style>
#specialchapter-page .ivu-tree {
  height: 550px;
  overflow-y: scroll;
  overflow-x: hidden;
}
</style>


