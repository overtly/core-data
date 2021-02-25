using Dapper;
using System;
using System.Collections.Concurrent;
using System.Transactions;

namespace Overt.Core.Data
{
    /// <summary>
    /// 表管理
    /// </summary>
    public static class TableExtensions
    {
        private static ConcurrentDictionary<string, object> lockObjectMap = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, bool> tableExistMap = new ConcurrentDictionary<string, bool>();

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="isMaster"></param>
        /// <returns>false, 直接返回默认数据即可</returns>
        public static bool CheckTableIfMissingCreate<T>(this IBaseRepository<T> repository, bool isMaster)
            where T : class, new()
        {
            var tableName = repository.TableNameFunc?.Invoke();
            var createScript = repository.CreateScriptFunc?.Invoke(tableName);
            if (string.IsNullOrEmpty(createScript))
                return true;
            if (string.IsNullOrEmpty(tableName))
                throw new Exception($"CheckTableIfMissingCreate: TableNameFunc 必须提供");

            var tableKey = $"{repository.DbStoreKey}_{tableName}_{isMaster}";
            lock (lockObjectMap.GetOrAdd(tableKey, new object()))
            {
                var existTable = tableExistMap.GetOrAdd(tableKey, k => repository.IsExistTable(tableName, isMaster));
                if (existTable)
                    return true;

                tableExistMap.TryRemove(tableKey, out existTable);
                if (!isMaster)
                    return false;

                try
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled))
                    using (var connection = repository.OpenConnection(true))
                    {
                        connection.Execute(createScript);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
