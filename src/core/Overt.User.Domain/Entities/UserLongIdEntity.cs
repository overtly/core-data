using Overt.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Overt.User.Domain.Entities
{
    [Table("UserLongId")]
    public class UserLongIdEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

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
