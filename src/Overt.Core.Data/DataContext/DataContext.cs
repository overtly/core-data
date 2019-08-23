#if ASP_NET_CORE
using Microsoft.Extensions.Configuration;
using System.IO;
#endif
using MySql.Data.MySqlClient;
using System;
#if !ASP_NET_CORE
using System.Configuration;
#endif
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Overt.Core.Data
{
    /// <summary>
    /// 数据库连接工具类
    /// </summary>
    public class DataContext : IDisposable
    {
        #region Private Members
        DbProviderFactory _dbFactory;
        readonly bool _isMaster;
        readonly string _dbStoreKey;

#if ASP_NET_CORE
        readonly IConfiguration _configuration;
        readonly Func<(string, DatabaseType)> _connectionFunc;
#else
        readonly Func<ConnectionStringSettings> _connectionFunc;
#endif


        #endregion

        #region Public Members
        /// <summary>
        /// 连接对象
        /// </summary>
        public IDbConnection DbConnection { get; private set; }
        #endregion

        #region Constructor
#if ASP_NET_CORE
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">if null 则从appsettings.json中获取</param>
        /// <param name="isMaster">是否从库</param>
        /// <param name="dbStoreKey">存储字符串标识</param>
        /// <param name="connectionFunc">连接字符串Func</param>
        public DataContext(IConfiguration configuration, bool isMaster = false, string dbStoreKey = "", Func<(string, DatabaseType)> connectionFunc = null)
        {
            _configuration = configuration;
            _isMaster = isMaster;
            _dbStoreKey = dbStoreKey;
            _connectionFunc = connectionFunc;

            // 打开连接
            CreateAndOpen();
        }
#else
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isMaster">是否从库</param>
        /// <param name="dbStoreKey">存储字符串标识</param>
        /// <param name="connectionFunc">连接字符串Func</param>
        public DataContext(bool isMaster = false, string dbStoreKey = "", Func<ConnectionStringSettings> connectionFunc = null)
        {
            _isMaster = isMaster;
            _dbStoreKey = dbStoreKey;
            _connectionFunc = connectionFunc;

            // 打开连接
            CreateAndOpen();
        }
#endif
        #endregion

        #region Private Method
        /// <summary>
        /// 打开链接
        /// </summary>
        private void CreateAndOpen()
        {
            var connectionString = string.Empty;
            var settings = DataSettings.Default;

            // 获取连接
#if ASP_NET_CORE
            var connectionSetting = settings.Get(_configuration, _isMaster, _dbStoreKey, _connectionFunc);
            connectionString = connectionSetting.Item1;
            _dbFactory = GetFactory(connectionSetting.Item2);
#else
            var connectionSetting = settings.Get(_isMaster, _dbStoreKey, _connectionFunc);
            connectionString = connectionSetting?.ConnectionString;
            _dbFactory = DbProviderFactories.GetFactory(connectionSetting?.ProviderName);
#endif

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"连接字符串获取为空，请检查Repository是否指定了dbStoreKey以及检查配置文件是否存在");

            DbConnection = _dbFactory.CreateConnection();
            DbConnection.ConnectionString = connectionString;
            if (DbConnection.State != ConnectionState.Open)
                DbConnection.Open();
        }

        /// <summary>
        /// 获取Factory
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private DbProviderFactory GetFactory(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    return SqlClientFactory.Instance;
                case DatabaseType.MySql:
                    return MySqlClientFactory.Instance;
                case DatabaseType.SQLite:
#if ASP_NET_CORE
                    AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));
                    return Microsoft.Data.Sqlite.SqliteFactory.Instance;
#else
                    return System.Data.SQLite.SQLiteFactory.Instance;
#endif
            }
            return null;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 垃圾回收
        /// </summary>
        public void Dispose()
        {
            if (DbConnection == null)
                return;
            try
            {
                DbConnection.Dispose();
            }
            catch { }
        }
        #endregion
    }
}
