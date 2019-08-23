using System;
using System.Data;
#if ASP_NET_CORE
using Microsoft.Extensions.Configuration;
#else
using System.Configuration;
#endif

namespace Overt.Core.Data
{
    /// <summary>
    /// DbRespository 抽象类
    /// </summary>
    public abstract class PropertyAssist : IPropertyAssist
    {
        #region Private Property
#if ASP_NET_CORE
        private readonly IConfiguration _configuration;
#endif
        #endregion

        #region Constructor
#if ASP_NET_CORE
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="configuration">配置注入</param>
        /// <param name="dbStoreKey">数据库前缀</param>
        public PropertyAssist(IConfiguration configuration, string dbStoreKey = "")
        {
            _configuration = configuration;
            DbStoreKey = dbStoreKey;
        }
#else
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="dbStoreKey">数据库前缀</param>
        public PropertyAssist(string dbStoreKey = "")
        {
            DbStoreKey = dbStoreKey;
        }
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// 数据库名key
        /// </summary>
        public string DbStoreKey { get; set; }

#if ASP_NET_CORE
        /// <summary>
        /// 创建事务，可自动创建连接 已赋值 Transaction 属性
        /// </summary>
        /// <returns></returns>
        public virtual IDbTransaction BeginTransaction()
        {
            var connection = OpenConnection(true);
            Transaction = connection.BeginTransaction();
            return Transaction;
        }
#endif

        /// <summary>
        /// 事务
        /// </summary>
        public virtual IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 是否在事务中
        /// </summary>
        public virtual bool InTransaction
        {
            get
            {
#if ASP_NET_CORE
                return Transaction?.Connection != null;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 打开连接 已赋值 connection 属性
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection OpenConnection(bool isMaster = false, bool ignoreTransaction = false)
        {
            IDbConnection connection;

#if ASP_NET_CORE
            if (ignoreTransaction)
                connection = new DataContext(_configuration, isMaster, DbStoreKey, ConnectionFunc).DbConnection;
            else
                connection = Transaction?.Connection ??
                    new DataContext(_configuration, isMaster, DbStoreKey, ConnectionFunc).DbConnection;
#else
            connection = new DataContext(isMaster, DbStoreKey, ConnectionFunc).DbConnection;
#endif

            if (connection == null)
                throw new Exception("数据库连接创建失败，请检查连接字符串是否正确...");

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }


        /// <summary>
        /// 数据库连接方法
        /// </summary>
#if ASP_NET_CORE
        public virtual Func<(string, DatabaseType)> ConnectionFunc { get; set; }
#else
        public virtual Func<ConnectionStringSettings> ConnectionFunc { get; set; }
#endif

        /// <summary>
        /// 表名方法
        /// </summary>
        public virtual Func<string> TableNameFunc { get; set; }

        /// <summary>
        /// 创建表的脚本
        /// </summary>
        public virtual Func<string, string> CreateScriptFunc { get; set; }

        /// <summary>
        /// 执行的sql脚本
        /// </summary>
        public virtual string ExecuteScript { get; set; }
        #endregion
    }
}
