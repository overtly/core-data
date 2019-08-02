namespace Overt.Core.Data
{
    /// <summary>
    /// 连接配置信息获取
    /// 1. master / secondary
    /// 2. xx.master / xx.secondary
    /// </summary>
    public class DataContextConfig
    {
        #region Static Private Members
        static private string _connNmeOfMaster = "master";
        static private string _connNameOfSecondary = "secondary";
        static private string _connNameOfPoint = ".";
        static private string _connNameOfPrefix = "";
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
        static private readonly object lockHelper = new object();
        static private volatile DataContextConfig _Default;
        /// <summary>
        /// 单例模式
        /// </summary>
        static public DataContextConfig Default
        {
            get
            {
                if (_Default == null)
                {
                    lock (lockHelper)
                    {
                        _Default = new DataContextConfig();
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
        private DataContextConfig()
        {
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="isMaster"></param>
        /// <param name="store">不能包含点</param>
        /// <returns></returns>
        public string Setting(bool isMaster = false, string store = "")
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
