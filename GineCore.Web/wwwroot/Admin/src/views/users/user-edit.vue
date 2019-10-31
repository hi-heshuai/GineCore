<template>
  <i-form :model="editUserInfo" ref="editUserForm" :label-width="80" :rules="rules">
    <Form-item label="账号" prop="userName">
      <i-input v-model="editUserInfo.userName" readonly="readonly" placeholder="请输入账号"></i-input>
    </Form-item>
    <Form-item label="真实名称" prop="realName">
      <i-input v-model="editUserInfo.realName" placeholder="请输入真实名称"></i-input>
    </Form-item>
    <Form-item label="出生日期">
      <Row>
        <i-col span="11">
          <Date-picker
            type="date"
            placeholder="选择日期"
            format="yyyy-MM-dd"
            @on-change="changeDate"
            v-model="editUserInfo.birthdayTemp"
          ></Date-picker>
        </i-col>
      </Row>
    </Form-item>
    <Form-item label="手机号码">
      <i-input v-model="editUserInfo.mobilePhone" placeholder="请输入手机号码"></i-input>
    </Form-item>
    <Form-item label="邮箱">
      <i-input v-model="editUserInfo.email" placeholder="请输入邮箱"></i-input>
    </Form-item>
    <Form-item label="自我介绍">
      <i-input
        v-model="editUserInfo.intro"
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
      editUserInfo: {
        userName: "",
        realName: "",
        sex: "",
        birthdayTemp: "",
        intro: "",
        email: "",
        mobilePhone: "",
        birthday: ""
      },
      rules: {
        userName: [
          { required: true, message: "账号不能为空", trigger: "blur" }
        ],
        realName: [
          { required: true, message: "真实名称不能为空", trigger: "blur" }
        ],
        birthday: [{ type: "date", message: "生日格式错误", trigger: "blur" }]
      }
    };
  },

  methods: {
    changeDate(date) {
      this.editUserInfo.birthday = date;
    },

    changeInfo(obj) {
      //不直接赋值是因为修改表单中的值会导致修改列表的值
      obj.birthdayTemp = obj.bithdayStr;
      for (let index in obj) {
        this.editUserInfo[index] = obj[index];
      }
    }
  }
};
</script>

