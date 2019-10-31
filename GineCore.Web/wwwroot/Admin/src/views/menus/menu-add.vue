<template>
  <i-form :model="addMenuInfo" ref="addMenuForm" :label-width="80" :rules="rules">
    <Form-item label="父级菜单" v-if="addMenuInfo.parent">
      <i-input v-model="addMenuInfo.parentName" readonly="readonly"></i-input>
    </Form-item>
    <Form-item label="菜单名称" prop="name">
      <i-input v-model="addMenuInfo.name" placeholder="请输入菜单名称"></i-input>
    </Form-item>
    <Form-item label="菜单标识" prop="key">
      <i-input v-model="addMenuInfo.key" placeholder="请输入菜单标识"></i-input>
    </Form-item>
    <Form-item label="图标">
      <i-input v-model="addMenuInfo.icon" placeholder="请输入图标"></i-input>
    </Form-item>
    <Form-item label="状态">
      <Radio-group v-model="addMenuInfo.enableMarked">
        <Radio label="true">启用</Radio>
        <Radio label="false">停用</Radio>
      </Radio-group>
    </Form-item>
    <Form-item label="排序" >
      <i-input v-model="addMenuInfo.sort" placeholder="请输入排序"></i-input>
    </Form-item>
    <Form-item label="类型">
      <Radio-group v-model="type">
        <Radio :label="0">菜单</Radio>
        <Radio :label="1">按钮</Radio>
      </Radio-group>
    </Form-item>
    <Form-item label="按钮类型" v-if="type == 1">
      <i-select v-model="addMenuInfo.type" style="width:200px">
        <i-option v-for="(item, index) in menuBtnTypes" :value="item.key" :key="index">{{ item.value }}</i-option>
      </i-select>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      parent: null,
      addMenuInfo: {
        Name: "",
        linkUrl: "",
        icon: "",
        key: "",
        enableMarked: "true",
        sort: "1",
        type: null,
        parent: null,
        parentName: ""
      },
      type: 0,
      menuBtnTypes: [],
      rules: {
        /*name: [
          { required: true, message: "菜单名称不能为空", trigger: "blur" },
          {
            type: "string",
            message: "菜单名称不得少于2位",
            min: 2,
            trigger: "blur"
          },
          {
            type: "string",
            message: "菜单名称不得多于6位",
            max: 6,
            trigger: "blur"
          }
        ],
        sort: [{ required: true, message: "排序号不能为空", trigger: "blur" }]*/
      }
    };
  },

  created() {
    this.initModel();
    this.getMenuBtnTypes();
  },

  methods: {
    initModel() {
      let parentId = null,
          parentName = "";
      if (this.parent) {
        parentId = this.parent.id;
        parentName = this.parent.name;
      }

      this.addMenuInfo = {
        name: "",
        linkUrl: "",
        icon: "",
        enableMarked: "true",
        sort: "1",
        type: null,
        parentId: parentId,
        parentName: parentName
      };
    },

    getMenuBtnTypes(){
      let _this = this;
      this.ajax.post({
        url: '/menus/GetMenuBtnTypes',
        success: function(data) {
          if(data.result){
            _this.menuBtnTypes = data.data;
          }
        }
      });
    },

  }
};
</script>

