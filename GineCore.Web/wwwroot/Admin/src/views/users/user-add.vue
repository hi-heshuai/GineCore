<template>
  <i-form :model="addUserInfo" ref="addUserForm" :label-width="80" :rules="rules">
    <Form-item label="账号" prop="userName">
      <i-input v-model="addUserInfo.userName" placeholder="请输入账号"></i-input>
    </Form-item>
    <Form-item label="头像">
      <Avatar :src="addUserInfo.avatar" />
      <Upload :on-success="saveFile" :action="hsConfig.apiBaseUrl + '/file/SaveFile'">
        <Button icon="ios-cloud-upload-outline">上传头像</Button>
    </Upload>
    </Form-item>
    <Form-item label="真实名称" prop="realName">
      <i-input v-model="addUserInfo.realName" placeholder="请输入真实名称"></i-input>
    </Form-item>
    <Form-item label="出生日期" prop="birthday">
      <Row>
        <i-col span="11">
          <Date-picker type="date" placeholder="选择日期" format="yyyy-MM-dd" 
          @on-change="changeDate"  v-model="addUserInfo.birthdayTemp"></Date-picker>
        </i-col>
      </Row>
    </Form-item>
    <Form-item label="手机号码">
      <i-input v-model="addUserInfo.mobilePhone" placeholder="请输入手机号码"></i-input>
    </Form-item>
    <Form-item label="邮箱">
      <i-input v-model="addUserInfo.email" placeholder="请输入邮箱"></i-input>
    </Form-item>
    <Form-item label="自我介绍">
      <i-input
        v-model="addUserInfo.intro"
        type="textarea"
        :autosize="{minRows: 2,maxRows: 5}"
        placeholder="请介绍下自己"
      ></i-input>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      addUserInfo: {
        userName: "",
        realName: "",
        birthdayTemp: "2019-05-15",
        intro: "",
        email: '',
        mobilePhone: "",
        birthday: "2019-05-15",
        avatar: "https://i.loli.net/2017/08/21/599a521472424.jpg"
      },
      rules:{
        userName: [
          { required: true, message: "账号不能为空", trigger: "blur" }
        ],
        realName: [
          { required: true, message: "真实名称不能为空", trigger: "blur" }
        ]
      }
    };
  },

  methods: {
    saveFile (response, file, fileList) {
      this.addUserInfo.avatar = this.hsConfig.serverBaseUrl + "/" + response;
      console.log({response: response, file: file, fileList: fileList});
    },

    changeDate(date) {
      this.addUserInfo.birthday = date;
    },

    clear() {
      this.addUserInfo = {
        userName: "",
        realName: "",
        birthdayTemp: "2019-05-15",
        intro: "",
        email: '',
        mobilePhone: "",
        birthday: "2019-05-15"
      };
    }
  }
};
</script>

<style lang="less">
.ivu-upload{
  display: inline-block;
  margin-left: 20px;
}
.ivu-upload-list{
  display: none;
}
</style>

