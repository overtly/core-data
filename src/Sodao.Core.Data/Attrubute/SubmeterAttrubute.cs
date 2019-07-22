using System;

namespace Sodao.Core.Data
{
    /// <summary>
    /// 分表标识
    /// </summary>
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
