using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gine.Common.Core
{
    /// <summary>
    /// 对多正则表达式进行验证
    /// 调用示例：        
    /// [RegularsExpression(
    ///  "^[0-9a-zA-Z_]*$", "密码只能由字母、数字、下划线组成", 
    ///  "^(?=.*[0-9])(?=.*[a-zA-Z])(.{0,})$", "密码应至少包含字母和数字")]
    /// </summary>
    public class RegularsExpression : ValidationAttribute
    {
        public string[] Validators { get; set; }

        public RegularsExpression(params string[] validators)
        {
            this.Validators = validators;
        }

        public override bool IsValid(object value)
        {
            var str = value as string;

            for (var i = 0; i < Validators.Length; i = i + 2)
            {
                var valid = Validators[i];
                var errMessage = Validators[i + 1];
                if (!Regex.IsMatch(str, valid))
                {
                    this.ErrorMessage = errMessage;
                    return false;
                }
            }
            return true;
        }
    }
}
