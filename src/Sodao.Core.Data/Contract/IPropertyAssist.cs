using System;
using System.Data;

namespace Sodao.Core.Data
{
    /// <summary>
    /// 通用的接口
    /// </summary>
    public interface IPropertyAssist
    {
        #region DbStoreKey
        /// <summary>
        /// 数据库key
        /// </summary>
        string DbStoreKey { get; set; }
        #endregion

        #region Transaction 
#if ASP_NET_CORE
        /// <summary>
        /// 提供内部事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();
#endif

        /// <summary>
        /// 事务
        /// </summary>
        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 是否在事务中
        /// </summary>
        bool InTransaction { get; }
        #endregion

        #region TableName
        /// <summary>
        /// 表名生成方法
        /// </summary>
        Func<string> TableNameFunc { get; set; }

        /// <summary>
        /// 创建表的sql语句
        /// 参数：表名
        /// </summary>
        Func<string, string> CreateScriptFunc { get; set; }
        #endregion

        #region ConnectionString
        /// <summary>
        /// 连接方法创建
        /// </summary>
        Func<string> ConnectionFunc { get; set; }
        #endregion

        #region Connection
        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        IDbConnection OpenConnection(bool isMaster = false, bool ignoreTransaction = false);
        #endregion

        #region ExecuteScript
        /// <summary>
        /// 执行的sql脚本
        /// </summary>
        string ExecuteScript { get; set; }
        #endregion
    }
}
