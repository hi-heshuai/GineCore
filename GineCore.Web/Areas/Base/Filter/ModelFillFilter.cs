using GineCore.Service.UserInfoService;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Interception.Utilities;

namespace GineCore.Web.Areas.Base.Filter
{
    /// <summary>
    /// 模型填充过滤器， 用于填充 CreateBy. CreateAt
    /// </summary>
    public class ModelFillFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var parameters = context.ActionArguments;

            parameters.ForEach(parameter =>
            {
                var model = parameter.Value;
                if (model == null) return;
                var list = new ArrayList();

                if (typeof(ICollection).IsAssignableFrom(model.GetType()))
                {
                    list.AddRange(model as ICollection);
                }
                else
                {
                    list.Add(model);
                }

                list.ToArray().ForEach(item =>
                {
                    var propertys = item.GetType().GetProperties();
                    propertys.ForEach(p =>
                    {
                        // 替换' 解决sql注入问题
                        if (p.PropertyType.Name.ToLower().Contains("string") && p.GetValue(item) != null && p.GetSetMethod() != null)
                        {
                            p.SetValue(item, p.GetValue(item).ToString().Replace("'", "''"));
                        }
                    });
                });
            });

            var tokenObj = context.HttpContext.Request.Form["token"];

            if (!string.IsNullOrEmpty(tokenObj))
            {
                var token = tokenObj.ToString();
                var userInfoService = Ioc.GetService<IUserInfoService>();
                var user = userInfoService.Get<UserInfoModel>(string.Format(" LoginToken='{0}'", token));

                if (user != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        var model = parameter.Value;
                        if (model == null) return;

                        var list = new ArrayList();

                        if (typeof(ICollection).IsAssignableFrom(model.GetType()))
                        {
                            list.AddRange(model as ICollection);
                        }
                        else
                        {
                            list.Add(model);
                        }

                        list.ToArray().ForEach(item =>
                        {
                            var propertys = item.GetType().GetProperties();
                            //模型处于创建状态
                            bool isCreate = propertys.Any(p => p.Name.ToLower() == "id" &&
                                   (p.GetValue(item) == null ||
                                   string.IsNullOrEmpty(p.GetValue(item).ToString()) ||
                                   p.GetValue(item).ToString() == "0"));
                            if (isCreate)
                            {
                                propertys.ForEach(p =>
                                {
                                    //字段填充
                                    if (p.Name.ToLower() == "createdby" && p.GetSetMethod() != null && user != null)
                                        p.SetValue(item, Convert.ToInt32(user.Id));
                                    else if (p.Name.ToLower() == "createdat" && p.GetSetMethod() != null)
                                        p.SetValue(item, DateTime.Now);
                                });
                            }

                            //模型处于编辑状态
                            bool isUpdate = propertys.Any(p => p.Name.ToLower() == "id" &&
                                    (p.GetValue(item) != null &&
                                    !string.IsNullOrEmpty(p.GetValue(item).ToString()) &&
                                    p.GetValue(item).ToString() != "0"));
                            if (isUpdate)
                            {
                                propertys.ForEach(p =>
                                {
                                    //字段填充
                                    if (p.Name.ToLower() == "updatedby" && p.GetSetMethod() != null && user != null)
                                        p.SetValue(item, Convert.ToInt32(user.Id));
                                    else if (p.Name.ToLower() == "updatedat" && p.GetSetMethod() != null)
                                        p.SetValue(item, DateTime.Now);
                                });
                            }

                            //既不是创建也不是编辑状态
                            if (!isCreate && !isUpdate)
                            {
                                propertys.ForEach(p =>
                                {

                                });
                            }
                        });
                    });
                }
            }
        }

        /// <summary>
        /// 清楚敏感词汇
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsContainKey(string key)
        {
            return false;
        }
    }
}
