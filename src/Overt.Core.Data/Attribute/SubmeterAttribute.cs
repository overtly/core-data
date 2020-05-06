using System;

namespace Overt.Core.Data
{
    /// <summary>
    /// 分表标识
    /// </summary>
    [Obsolete("请使用TableNameFunc")]
    public class SubmeterAttribute : Attribute
    {
        /// <summary>
        /// 16进制位数
        /// 1 16
        /// 2 256
        /// 3 4096 
        /// ...
        /// </summary>
        public int Bit { get; set; }
    }
}
