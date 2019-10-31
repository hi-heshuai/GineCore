<style lang="less">
@import "./login.less";
</style>

<template>
  <div class="login" @keydown.enter="handleSubmit">
    <div class="login-con">
      <Card :bordered="false">
        <p slot="title">
          <Icon type="log-in"></Icon>欢迎登录
        </p>
        <div class="form-con">
          <Form ref="loginForm" :model="form" :rules="rules">
            <FormItem prop="userName">
              <Input v-model="form.userName" placeholder="请输入用户名">
                <span slot="prepend">
                  <Icon :size="16" type="person"></Icon>
                </span>
              </Input>
            </FormItem>
            <FormItem prop="password">
              <Input type="password" v-model="form.password" placeholder="请输入密码">
                <span slot="prepend">
                  <Icon :size="14" type="locked"></Icon>
                </span>
              </Input>
            </FormItem>
            <FormItem>
              <Button @click="handleSubmit" type="primary" long>{{loginText}}</Button>
            </FormItem>
          </Form>
          <p v-if="false" class="login-tip">输入任意用户名和密码即可</p>
        </div>
      </Card>
    </div>
  </div>
</template>

<script>
import Cookies from "js-cookie";
import util from "../libs/util";

export default {
  data() {
    return {
      loginText: "登陆",
      form: {
        userName: "",
        password: ""
      },
      rules: {
        userName: [
          { required: true, message: "账号不能为空", trigger: "blur" }
        ],
        password: [{ required: true, message: "密码不能为空", trigger: "blur" }]
      }
    };
  },
  methods: {
    handleSubmit() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loginText = "登陆...";
          let _this = this;
          this.ajax.post({
            url: "/UserInfo/Login",
            data: this.form,
            loadding: false,
            success(data) {
              _this.loginText = "登陆";
              if (data.result) {
                _this.loginSuccess(data.data);
              } else {
                _this.form.password = "";

                _this.$Modal.warning({
                  title: "登陆失败",
                  content: data.errorInfo
                });
              }
            }
          });
        }
      });
    },

    loginSuccess(data) {
      Cookies.set("user", data.userName);
      Cookies.set("password", data.userKey);
      Cookies.set("token", data.loginToken);
      let headPic =
        "https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=3448484253,3685836170&fm=27&gp=0.jpg";
      if (data.headIcon) {
        headPic = data.headIcon;
      }
      this.$store.commit("setAvator", headPic);
      /*if (this.form.userName === "iview_admin") {
        Cookies.set("access", 0);
      } else {
        Cookies.set("access", 1);
      }*/

      let _this = this;

      this.ajax.post({
        async: false,
        url: "/Menus/GetLoginUserMenusTree",
        success(data) {
          if (data.result) {
            debugger
            Cookies.set("menuList", JSON.stringify(data.data));
          }

          _this.$store.state.app.pageOpenedList = [];
          _this.$router.push({
            name: "home_index"
          });
        }
      });
    }
  }
};
</script>

<style>
</style>
