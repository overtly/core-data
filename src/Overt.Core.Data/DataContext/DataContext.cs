#if ASP_NET_CORE
using Microsoft.Extensions.Configuration;
#endif
using MySql.Data.MySqlClient;
using System;
#if !ASP_NET_CORE
using System.Configuration;
#endif
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace Overt.Core.Data
{
    /// <summary>
    /// 数据库连接工具类
    /// </summary>
    public class DataContext : IDisposable
    {
        #region Private Members
        DbProviderFactory _dbFactory;
#if ASP_NET_CORE
        IConfiguration _configuration;
#endif

        private readonly bool _isMaster;
        private readonly string _dbStoreKey;
        private readonly Func<string> _connectionFunc;
        #endregion

        #region Public Members
        private IDbConnection _dbConnection;
        /// <summary>
        /// 连接对象
        /// </summary>
        public IDbConnection DbConnection
        {
            get
            {
                return _dbConnection;
            }
        }
        private DatabaseType _dbType = DatabaseType.MySql;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DbType
        {
            get { return _dbType; }
        }
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
        public DataContext(IConfiguration configuration, bool isMaster = false, Func<string> connectionFunc = null, string dbStoreKey = "")
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
        public DataContext(bool isMaster = false, Func<string> connectionFunc = null, string dbStoreKey = "")
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
            var providerName = string.Empty;

            #region 1. ExecuteFunc
            if (_connectionFunc != null)
            {
                connectionString = _connectionFunc?.Invoke();
            }
            #endregion

            #region 2. 配置文件 
            else
            {
#if ASP_NET_CORE
                #region 自编译Configuration
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();
                }
                #endregion

                #region 从库转主库
                var connectionKey = DataContextConfig.Default.Setting(_isMaster, _dbStoreKey);
                connectionString = _configuration.GetConnectionString(connectionKey);
                if (string.IsNullOrEmpty(connectionString) && !_isMaster)
                {
                    connectionKey = DataContextConfig.Default.Setting(true, _dbStoreKey);
                    connectionString = _configuration.GetConnectionString(connectionKey);
                }
                #endregion
#else
                #region 从库转主库
                var connectionKey = DataContextConfig.Default.Setting(_isMaster, _dbStoreKey);
                var connectionSetting = ConfigurationManager.ConnectionStrings[connectionKey];
                connectionString = connectionSetting?.ConnectionString;
                providerName = connectionSetting?.ProviderName;
                if (string.IsNullOrEmpty(connectionString) && !_isMaster)
                {
                    connectionKey = DataContextConfig.Default.Setting(true, _dbStoreKey);
                    connectionSetting = ConfigurationManager.ConnectionStrings[connectionKey];
                    connectionString = connectionSetting.ConnectionString;
                    providerName = connectionSetting.ProviderName;
                }
                #endregion
#endif
            }
            #endregion

            #region 3. 创建连接
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"连接字符串获取为空，请检查Repository是否指定了dbStoreKey以及检查配置文件是否存在");

#if ASP_NET_CORE
            connectionString = FixConnectionString(connectionString);
            _dbFactory = GetFactory(_dbType);
#else
            _dbFactory = DbProviderFactories.GetFactory(providerName);
#endif
            _dbConnection = _dbFactory.CreateConnection();
            _dbConnection.ConnectionString = connectionString;
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            #endregion
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string FixConnectionString(string connectionString, string param = "DbType")
        {
            var dbTypeRegex = new Regex($@"(^|;){param}=(?<dbtype>[A-Za-z]+)(;|$)");
            var m = dbTypeRegex.Match(connectionString);
            var dbTypeString = m?.Groups["dbtype"].Value;
            _dbType = DatabaseType.MySql;
            var parseResult = Enum.TryParse(dbTypeString, out _dbType);
            if (!parseResult)
                _dbType = DatabaseType.MySql;

            connectionString = Regex.Replace(connectionString, $@"{param}=([A-Za-z]+)(;|$)", "");
            return connectionString;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 垃圾回收
        /// </summary>
        public void Dispose()
        {
            if (_dbConnection == null)
                return;
            try
            {
                _dbConnection.Dispose();
            }
            catch { }
        }
        #endregion
    }
}
