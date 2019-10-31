using GineCore.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Areas.Base.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new ResponseModel<string>()
            {
                code = 500,
                result = false,
                errorInfo = context.Exception.Message
            };

            LogFileHelper.WriteLine(context.Exception.Message + "\r\n" + context.Exception.StackTrace); ;
            context.Result = new JsonResult(response);
        }
    }
}
