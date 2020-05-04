using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Application.Models
{
    public class UserPostModel : IValidatableObject
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public DateTime AddTime { get; set; }
        public bool IsSex { get; set; }
        /// <summary>
        /// Json类型
        /// </summary>
        public string JsonValue { get; set; }


        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(UserName))
                yield return new ValidationResult("用户名必须提供", new[] { nameof(UserName) });

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult("密码必须提供", new[] { nameof(Password) });
        }
    }

}
