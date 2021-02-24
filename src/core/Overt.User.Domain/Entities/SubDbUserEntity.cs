using Overt.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Overt.User.Domain.Entities
{
    [Table("User")]
    public class SubDbUserEntity
    {
        // 第一种分表标识，Md5(UserId)取{Bit}位 尽量不用，使用自定义模式
        //[Submeter(Bit = 2)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public DateTime AddTime { get; set; }
        public bool IsSex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Json类型
        /// </summary>
        public string JsonValue { get; set; }
    }

}
