import axios from 'axios'
import qs from 'qs'
import './jsformat'
import hsConfig from './config'
import cookies from "js-cookie"
import util from './util'
import router from '../router/index'

const ajaxUrl = hsConfig.apiBaseUrl;

//定义请求拦截器
axios.interceptors.request.use(function (config) {
  return config;
}, function (error) {
  return Promise.reject(error);
});

//定义一个响应拦截器
axios.interceptors.response.use(function (config) {
  var data = config.data;
  if (data.code == 500) {
    router.replace({ path: '/500' });
  }
  if (data.code == 201) {
    router.replace({ path: '/404' })
  }
  return config
}, function (error) {
  if (error.response) {
    switch (error.response.status) {
      case 404: router.replace({ path: '/404' });
        break;
    }
  }
  return Promise.reject(error);
});

var ajax = {
  //post查询
  post: function (options) {
    if (options.data == null) {
      options.data = {};
    }

    options.data.token = cookies.get("token");
    options.url = ajaxUrl + options.url;

    axios.post(options.url, qs.stringify(options.data)).then(response => {
      var data = response.data;
      options.success(data);
    });
  },

  //get查询
  get: function (options) {
    options.data.token = cookies.get("token");
    options.url = ajaxUrl + options.url;
    var paramsStr = "";
    for (var key in options.data) {
      paramsStr += "&{0}={1}".format(key, options.data[key]);
    }
    if (options.url.indexOf("?") > -1) {
      options.url += paramsStr;
    } else {
      if (paramsStr) {
        options.url = "{0}?{1}".format(options.url, paramsStr.substring(1, paramsStr.length - 1));
      }
    }

    axios.get(options.url).then(response => {
      var data = response.data;
      options.success(data);
    });
  },

  //用于分页查询
  pager: function (options) {
    if (!options.data.rows || options.data.rows <= 0) {
      options.data.rows = hsConfig.rows;
    }
    if (!options.data.page || options.data.page <= 0) {
      options.data.page = 1;
    }

    var index;
    if (options.loadding != false) {
      index = layer.load(2, {
        shade: [0.1, '#fff'],
        time: 8000
      });
    }

    var userSuccess = options.success;
    options.success = function (data) {
      for (var i = 0; i < data.rows.length; i++) {
        var model = data.rows[i];
        var rowsNumber = (data.page - 1) * options.data.rows + i + 1;
        model.rowsNumber = rowsNumber;
      }

      userSuccess(data);

      if (index) {
        layer.close(index);
      }
    }

    this.post(options);
  }
};

export default ajax;