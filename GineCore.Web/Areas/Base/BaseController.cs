using GineCore.Common;
using GineCore.Common.Appsetting;
using GineCore.Entity;
using GineCore.Entity.Entities;
using GineCore.Service;
using GineCore.Service.LogService;
using GineCore.Service.UserInfoService;
using GineCore.Service.UserRoleService;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GineCore.Web.Controllers.Base
{
    public abstract class BaseController<TEntity> :
        Controller where TEntity : class, new()
    {
        protected readonly IBaseService<TEntity> entityService = Ioc.GetService<IBaseService<TEntity>>();
        private readonly ILogService logService = Ioc.GetService<ILogService>();
        private readonly IUserInfoService userInfoService = Ioc.GetService<IUserInfoService>();

        //配置文件
        protected readonly AppsettingModel appsetting;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseController()
        {

        }

        /// <summary>
        /// 需要使用appsetting的构造函数
        /// </summary>
        /// <param name="setting"></param>
        public BaseController(IOptions<AppsettingModel> setting)
        {
            appsetting = setting.Value;
        }

        #region 基本业务逻辑
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TAddModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create<TAddModel>(TAddModel model) where TAddModel : class, TEntity, new()
        {
            var validate = new ModelValidate(model);//模型验证对象
            if (!validate.IsValid)
            {
                return Fail(validate.FirstError.ErrorMessage);
            }
            var entity = MapperHelper.Mapper<TAddModel, TEntity>(model);
            await entityService.Create(entity);
            return Success("创建成功");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEditModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit<TEditModel>(TEditModel model, params string[] param) where TEditModel : class, TEntity, new()
        {
            var validate = new ModelValidate(model);//模型验证对象
            if (!validate.IsValid)
            {
                return Fail(validate.FirstError.ErrorMessage);
            }
            var entity = MapperHelper.Mapper<TEditModel, TEntity>(model);

            param.Append("UpdatedAt");
            param.Append("UpdatedBy");

            await entityService.Update(entity, param);
            return Success("修改成功");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var entities = await entityService.GetEntities(ids);
            await entityService.Delete(entities);
            return Success();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await entityService.Get(id);
            await entityService.Delete(entity);
            return Success();
        }
        #endregion

        #region 返回数据
        public IActionResult ResponseInfo<T>(ResponseModel<T> model, string operateName = "", string loginName = "")
        {
            if (!string.IsNullOrEmpty(operateName))
            {
                var user = GetLoginUser();

                var logEntity = new Log()
                {
                    OperateName = operateName,
                    Description = model.errorInfo,
                    Result = model.result,
                    OperateUserId = loginName == "" ? user.UserId : 0,
                    OperateUserName = loginName == "" ? user.UserName : loginName,
                    OperateTime = DateTime.Now,
                };
                logService.WriteLog(logEntity);
            }

            return Json(model);
        }

        /// <summary>
        /// 成功返回格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="operateName">存在则写日志</param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IActionResult Success<T>(T data, string operateName = "", string loginName = "")
        {
            ResponseModel<T> model = new ResponseModel<T>()
            {
                result = true,
                errorInfo = "",
                data = data
            };
            return ResponseInfo(model, operateName, loginName);
        }

        /// <summary>
        /// 成功返回格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="operateName">存在则写日志</param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IActionResult Success()
        {
            ResponseModel<object> model = new ResponseModel<object>()
            {
                result = true,
                errorInfo = "",
                data = "操作成功"
            };
            return ResponseInfo(model);
        }

        /// <summary>
        /// 失败返回格式
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <param name="operateName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IActionResult Fail(string errorInfo, string operateName = "", string loginName = "")
        {
            ResponseModel<object> model = new ResponseModel<object>()
            {
                result = false,
                errorInfo = errorInfo,
                data = null
            };

            return ResponseInfo(model, operateName, loginName);
        }
        #endregion

        #region cookie操作
        /// <summary>
        /// 写入cookie
        /// </summary>
        /// <param name=""></param>
        public void WriteCookie(string key, string objStr)
        {
            Response.Cookies.Append(key, objStr, new CookieOptions() { IsEssential = true });
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCookie(string key)
        {
            string value = string.Empty;
            HttpContext.Request.Cookies.TryGetValue(key, out value);

            return value;
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="key"></param>
        public void DeleteCookie(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }
        #endregion

        #region 登陆用户信息
        private readonly string LoginUserKey = "gine_loginkey_2017";

        public OperatorModel GetLoginUser()
        {
            OperatorModel user = new OperatorModel();
            user = DESEncrypt.Decrypt(GetCookie(LoginUserKey)).ToObject<OperatorModel>();

            return user;
        }

        /// <summary>
        /// 添加当前登录用户
        /// </summary>
        /// <param name="user"></param>
        public void CreateLoginUser(OperatorModel user)
        {
            WriteCookie(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
        }

        /// <summary>
        /// 获取token
        /// </summary>
        public string GetToken()
        {
            return HttpContext.Request.Form["token"].ToString();
        }

        /// <summary>
        /// 获取api登陆用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserInfoModel> GetTokenUser()
        {
            IUserRoleService userRoleService = Ioc.GetService<IUserRoleService>();

            string token = GetToken();
            var model = await userInfoService.Get<UserInfoModel>(string.Format(" LoginToken='{0}'", token));
            if (!model.IsRoot)
            {
                var userRole = await userRoleService.Get<UserRole>(string.Format("UserInfoId={0}", model.Id));
                model.RoleId = userRole.RolesId;
            }
            else
            {
                model.RoleId = -1; //超级无敌管理员
            }

            return model;
        }
        #endregion
    }
}
