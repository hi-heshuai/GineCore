const serverUrl = "http://localhost:9090";

const hsconfig = 
{
  serverBaseUrl: serverUrl,
  apiBaseUrl: serverUrl + '/api',//api接口基础路径
  apiFileUrl:  serverUrl + "/api/File/GetPic?path=",
  rows: 10, //分页默认长度
}

export default hsconfig;