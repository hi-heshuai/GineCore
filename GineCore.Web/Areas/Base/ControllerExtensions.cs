using GineCore.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GineCore.Web.Controllers.Base
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// 获取第一条验证失败的信息
        /// </summary>
        public static ModelError FirstError(this Microsoft.AspNetCore.Mvc.Controller controller)
        {
            var error = new ModelError(string.Empty);
            foreach (var item in controller.ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    error = item.Errors.First();
                }
            }
            return error;
        }

        /// <summary>
        /// 自定义模型验证器
        /// </summary>
        public static ModelValidate ModelValidate(this Microsoft.AspNetCore.Mvc.Controller controller, params object[] models)
        {
            return new ModelValidate(models);
        }
    }
}
