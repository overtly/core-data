using System;
using System.Text.RegularExpressions;
#if ASP_NET_CORE
using Microsoft.Extensions.Configuration;
#else
using System.Configuration;
#endif

namespace Overt.Core.Data
{
    /// <summary>
    /// 连接配置信息获取
    /// 1. master / secondary
    /// 2. xx.master / xx.secondary
    /// </summary>
    public class DataSettings
    {
        #region Static Private Members
        static readonly string _connNmeOfMaster = "master";
        static readonly string _connNameOfSecondary = "secondary";
        static readonly string _connNameOfPoint = ".";
        static string _connNameOfPrefix = "";
        #endregion

        #region Private Member
        /// <summary>
        /// 主库
        /// </summary>
        private string Master
        {
            get { return $"{_connNameOfPrefix}{_connNmeOfMaster}"; }
        }
        /// <summary>
        /// 从库
        /// </summary>
        private string Secondary
        {
            get
            {
                return $"{_connNameOfPrefix}{_connNameOfSecondary}";
            }
        }
        #endregion

        #region Single Instance
        static readonly object lockHelper = new object();
        static volatile DataSettings _Default;
        /// <summary>
        /// 单例模式
        /// </summary>
        static public DataSettings Default
        {
            get
            {
                if (_Default == null)
                {
                    lock (lockHelper)
                    {
                        _Default = _Default ?? new DataSettings();
                    }
                }
                return _Default;
            }
        }
        #endregion

        #region Construct Method
        /// <summary>
        /// 构造函数
        /// </summary>
        private DataSettings()
        {
        }
        #endregion

        #region Public Method
#if ASP_NET_CORE
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="isMaster"></param>
        /// <param name="dbStoreKey"></param>
        /// <param name="connectionFunc"></param>
        /// <returns></returns>
        public (string, DatabaseType) Get(IConfiguration configuration, bool isMaster, string dbStoreKey, Func<bool, string> connectionFunc = null)
        {
            var connectionString = string.Empty;
            if (connectionFunc != null)
            {
                connectionString = connectionFunc.Invoke(isMaster);
                return ResolveConnectionString(connectionString);
            }

            if (configuration == null)
            {
                throw new Exception($"请注入IConfiguration");
                //configuration = new ConfigurationBuilder()
                //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                //    .AddJsonFile("appsettings.json")
                //    .Build();
            }

            var connectionKey = GetKey(isMaster, dbStoreKey);
            connectionString = configuration.GetConnectionString(connectionKey);
            if (string.IsNullOrEmpty(connectionString) && !isMaster)
            {
                // 从库转主库
                connectionKey = GetKey(true, dbStoreKey);
                connectionString = configuration.GetConnectionString(connectionKey);
            }

            return ResolveConnectionString(connectionString);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private (string, DatabaseType) ResolveConnectionString(string connectionString, string param = "DbType")
        {
            var dbTypeRegex = new Regex($@"(^|;){param}=(?<dbtype>[A-Za-z]+)(;|$)");
            var m = dbTypeRegex.Match(connectionString);
            var dbTypeString = m?.Groups["dbtype"].Value;
            var dbType = DatabaseType.MySql;
            var parseResult = Enum.TryParse(dbTypeString, out dbType);
            if (!parseResult)
                dbType = DatabaseType.MySql;

            connectionString = Regex.Replace(connectionString, $@"{param}=([A-Za-z]+)(;|$)", "");
            return (connectionString, dbType);
        }
#else
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="isMaster"></param>
        /// <param name="dbStoreKey"></param>
        /// <param name="connectionFunc"></param>
        /// <returns></returns>
        public ConnectionStringSettings Get(bool isMaster, string dbStoreKey, Func<bool, ConnectionStringSettings> connectionFunc = null)
        {
            if (connectionFunc != null)
                return connectionFunc.Invoke(isMaster);

            var connectionKey = GetKey(isMaster, dbStoreKey);
            var connectionSetting = ConfigurationManager.ConnectionStrings[connectionKey];
            if (string.IsNullOrEmpty(connectionSetting?.ConnectionString) && !isMaster)
            {
                connectionKey = GetKey(true, dbStoreKey);
                connectionSetting = ConfigurationManager.ConnectionStrings[connectionKey];
            }
            return connectionSetting;
        }
#endif
        #endregion

        #region Private Method
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="isMaster"></param>
        /// <param name="store">不能包含点</param>
        /// <returns></returns>
        private string GetKey(bool isMaster = false, string store = "")
        {
            _connNameOfPrefix = string.IsNullOrEmpty(store) ? "" : $"{store}{_connNameOfPoint}";
            var connName = string.Empty;
            if (isMaster)
                connName = Master;
            else
                connName = Secondary;

            return connName;
        }
        #endregion
    }
}
