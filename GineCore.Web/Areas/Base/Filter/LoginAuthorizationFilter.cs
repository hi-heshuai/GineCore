using GineCore.Common;
using GineCore.Service.UserInfoService;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Areas.Base.Filter
{
    public class LoginAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Path == "/")
            {
                return;
            }

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                if(controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                   .Any(a => a.GetType().Equals(typeof(NoLoginAttribute))))
                {
                    return;
                }
            }

            var tokenObj = context.HttpContext.Request.Form["token"];

            if (!string.IsNullOrEmpty(tokenObj))
            {
                var token = tokenObj.ToString();
                var userInfoService = Ioc.GetService<IUserInfoService>();
                var model = userInfoService.Get<UserInfoModel>(string.Format(" LoginToken='{0}'", token));

                if (model == null)
                {
                    context.Result = new JsonResult(new ResponseModel<string>()
                    {
                        code = 201,
                        result = false,
                        errorInfo = "登陆用户不合法"
                    });
                }
            }
            else
            {
                context.Result = new JsonResult(new ResponseModel<string>()
                {
                    code = 201,
                    result = false,
                    errorInfo = "未登陆系统"
                });
            }

        }
    }
}
