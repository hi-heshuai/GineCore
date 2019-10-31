using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GineCore.Common
{
    /// <summary>
    /// 模型验证器
    /// </summary>
    public class ModelValidate
    {
        private List<object> models = null;

        /// <summary>
        /// 需要验证的模型
        /// </summary>
        public List<object> Models
        {
            get { return models; }
        }

        /// <summary>
        /// 模型是否通过验证
        /// </summary>
        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }

        /// <summary>
        /// 验证结果
        /// </summary>
        public List<ValidationResult> Errors
        {
            get
            {
                if (Models == null) return new List<ValidationResult>();

                var errors = new List<ValidationResult>();
                foreach (var model in Models)
                {
                    var context = new ValidationContext(model, null, null);
                    var contextResults = new List<ValidationResult>();
                    Validator.TryValidateObject(model, context, errors, true);
                    errors.AddRange(contextResults);
                }

                return errors;
            }
        }

        /// <summary>
        /// 获取第一条验证错误信息（没有返回null）
        /// </summary>
        public ValidationResult FirstError
        {
            get
            {
                var errors = Errors;
                if (errors == null || errors.Count == 0) return null;

                return errors.First(r => true);
            }
        }

        /// <summary>
        /// 获取最后第一条验证错误信息（没有返回null）
        /// </summary>
        public ValidationResult LastError
        {
            get
            {
                var errors = Errors;
                if (errors == null || errors.Count == 0) return null;

                return errors.Last(r => true);
            }
        }

        public ModelValidate(params object[] models)
        {
            this.models = models.ToList();
        }
    }
}
