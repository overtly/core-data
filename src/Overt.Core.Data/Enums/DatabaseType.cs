namespace Overt.Core.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
	public enum DatabaseType
    {
        /// <summary>
        /// SqlServer
        /// </summary>
		SqlServer,
        /// <summary>
        /// >=SqlServer2012
        /// </summary>
		GteSqlServer2012,
        /// <summary>
        /// Mysql
        /// </summary>
		MySql,
        /// <summary>
        /// Sqlite
        /// </summary>
        SQLite,
    }
}