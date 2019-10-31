<template>
  <i-form :model="editMenuInfo" ref="editMenuForm" :label-width="80" :rules="rules">
    <Form-item label="父级菜单" v-if="editMenuInfo.parentName !== '' && editMenuInfo.parentName !== null">
      <i-input v-model="editMenuInfo.parentName" readonly="readonly"></i-input>
    </Form-item>
    <Form-item label="菜单名称" prop="name">
      <i-input v-model="editMenuInfo.name" placeholder="请输入菜单名称"></i-input>
    </Form-item>
    <Form-item label="菜单标识" prop="key">
      <i-input v-model="editMenuInfo.key" placeholder="请输入菜单标识"></i-input>
    </Form-item>
    <Form-item label="图标">
      <i-input v-model="editMenuInfo.icon" placeholder="请输入图标"></i-input>
    </Form-item>
    <Form-item label="状态">
      <Radio-group v-model="editMenuInfo.enableMarked">
        <Radio label="true">启用</Radio>
        <Radio label="false">停用</Radio>
      </Radio-group>
    </Form-item>
    <Form-item label="排序" prop="sort">
      <i-input v-model="editMenuInfo.sort" placeholder="请输入排序"></i-input>
    </Form-item>
    <Form-item label="类型">
      <Radio-group v-model="editMenuInfo.menuType">
        <Radio :label="0">菜单</Radio>
        <Radio :label="1">按钮</Radio>
      </Radio-group>
    </Form-item>
    <Form-item label="按钮类型" v-if="editMenuInfo.menuType == 1">
      <i-select v-model="editMenuInfo.type" style="width:200px">
        <i-option v-for="(item, index) in menuBtnTypes" :value="item.key" :key="index">{{ item.value }}</i-option>
      </i-select>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      editMenuInfo: {
        name: "",
        linkUrl: "",
        icon: "",
        enableMarked: "true",
        sort: "1",
        type: null,
        key: "",
        menuType: 0,
        parentId: null,
        parentName: ""
      },
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
    //this.initModel();
    this.getMenuBtnTypes();
  },

  methods: {
    initModel(obj) {
      if(obj){
        console.log(obj);
        for(let index in obj){
          this.editMenuInfo[index] = obj[index];
        }
      }
      
      if(this.editMenuInfo.type == null){
        this.editMenuInfo.menuType = 0;
      }else{
        this.editMenuInfo.menuType = 1;
      }
      
      this.editMenuInfo.enableMarked = this.editMenuInfo.enableMarked + "";
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

