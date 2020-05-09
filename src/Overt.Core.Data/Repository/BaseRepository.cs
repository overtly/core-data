#if ASP_NET_CORE
using Microsoft.Extensions.Configuration;
#endif
using Overt.Core.Data.Expressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Overt.Core.Data
{
    /// <summary>
    /// 实现IBaseRepository
    /// </summary>
    public abstract class BaseRepository<TEntity> : PropertyAssist, IBaseRepository<TEntity> where TEntity : class, new()
    {
        #region Constructor 
#if ASP_NET_CORE
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="configuration">配置注入</param>
        /// <param name="dbStoreKey">数据库前缀</param>
        public BaseRepository(IConfiguration configuration, string dbStoreKey = "")
            : base(configuration, dbStoreKey)
        {
        }
#else
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="dbStoreKey">数据库前缀</param>
        public BaseRepository(string dbStoreKey = "")
            : base(dbStoreKey)
        {
        }
#endif
        #endregion

        #region Sync Method
        /// <summary>
        /// 获取主表名
        /// </summary>
        /// <returns></returns>
        public string GetMainTableName()
        {
            return typeof(TEntity).GetMainTableName();
        }

        /// <summary>
        /// 获取表名，调用TableNameFunc则是调用主库查询
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            var tableName = TableNameFunc?.Invoke() ?? typeof(TEntity).GetMainTableName();
            return tableName;
        }

        /// <summary>
        /// 获取表名，以Submeter分表标识值取模
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("请使用GetTableName")]
        public string GetTableName(string key)
        {
            var tableName = key.GetTableName<TEntity>(TableNameFunc);
            return tableName;
        }

        /// <summary>
        /// 是否存在表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        public bool IsExistTable(string tableName, bool isMaster = true)
        {
            return IsExistTableAsync(tableName, isMaster).Result;
        }

        /// <summary>
        /// 是否存在字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        public bool IsExistField(string tableName, string fieldName, bool isMaster = true)
        {
            return IsExistFieldAsync(tableName, fieldName, isMaster).Result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="returnLastIdentity">是否赋值最后一次的自增ID</param>
        /// <returns>添加后的数据实体</returns>
        public virtual bool Add(TEntity entity, bool returnLastIdentity = false)
        {
            return AddAsync(entity, returnLastIdentity).Result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        public virtual bool Add(params TEntity[] entities)
        {
            return AddAsync(entities).Result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">删除条件</param>
        /// <returns>是否成功</returns>
        public bool Delete(Expression<Func<TEntity, bool>> expression)
        {
            return DeleteAsync(expression).Result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="fields">x=> x.SomeProperty1 or x=> new { x.SomeProperty1, x.SomeProperty2 }</param>
        /// <returns>是否成功</returns>
        public bool Set(TEntity entity, Expression<Func<TEntity, object>> fields = null)
        {
            return SetAsync(entity, fields).Result;
        }

        /// <summary>
        /// 根据字段修改
        /// </summary>
        /// <param name="setExpress">修改字段表达式：() => new { SomeProperty1 = "", SomeProperty2 = "" } or Dictionary<string, object></string></param>
        /// <param name="whereExpress">条件表达式</param>
        /// <returns>是否成功</returns>
        public bool Set(Expression<Func<object>> setExpress, Expression<Func<TEntity, bool>> whereExpress)
        {
            return SetAsync(setExpress, whereExpress).Result;
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns>实体</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false)
        {
            return GetAsync(expression, fieldExpressison, isMaster).Result;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="expression">条件表达式</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <param name="orderByFields">排序字段集合</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(
            int page,
            int rows,
            Expression<Func<TEntity, bool>> expression = null,
            Expression<Func<TEntity, object>> fieldExpressison = null,
            bool isMaster = false,
            params OrderByField[] orderByFields)
        {
            return GetListAsync(page, rows, expression, fieldExpressison, isMaster, orderByFields).Result;
        }

        /// <summary>
        /// 获取列表 Offset
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="expression">条件表达式</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <param name="orderByFields">排序字段集合</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetOffsets(
            int offset,
            int size,
            Expression<Func<TEntity, bool>> expression = null,
            Expression<Func<TEntity, object>> fieldExpressison = null,
            bool isMaster = false,
            params OrderByField[] orderByFields)
        {
            return GetOffsetsAsync(offset, size, expression, fieldExpressison, isMaster, orderByFields).Result;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> expression = null, bool isMaster = false)
        {
            return CountAsync(expression, isMaster).Result;
        }
        #endregion

        #region Async Method
        /// <summary>
        /// 是否存在表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        public async Task<bool> IsExistTableAsync(string tableName, bool isMaster = true)
        {
            if (string.IsNullOrEmpty(tableName))
                return false;

            using (var connection = OpenConnection(isMaster, true))
            {
                return await connection.IsExistTableAsync(tableName, OutSqlAction);
            }
        }

        /// <summary>
        /// 是否存在字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        public async Task<bool> IsExistFieldAsync(string tableName, string fieldName, bool isMaster = true)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fieldName))
                return false;

            using (var conneciton = OpenConnection(isMaster, true))
            {
                return await conneciton.IsExistFieldAsync(tableName, fieldName, OutSqlAction);
            }
        }

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="returnLastIdentity">是否赋值最后一次的自增ID</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(TEntity entity, bool returnLastIdentity = false)
        {
            if (entity == null)
                return false;

            return await Execute(async (connection) =>
            {
                var tableName = entity.GetTableName(TableNameFunc);
                var result = await connection.InsertAsync(tableName, entity, Transaction, returnLastIdentity, OutSqlAction);
                return result > 0;
            }, true);
        }

        /// <summary>
        /// 异步批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(params TEntity[] entities)
        {
            if ((entities?.Count() ?? 0) <= 0)
                return false;

            return await Execute(async (connection) =>
            {
                var tableName = entities.First().GetTableName(TableNameFunc);
                var result = await connection.InsertAsync(tableName, entities, Transaction, OutSqlAction);
                return result > 0;
            }, true);
        }

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="expression">删除条件</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Execute(async (connection) =>
            {
                var tableName = expression.GetTableName(TableNameFunc);
                var task = await connection.DeleteAsync(tableName, expression, Transaction, OutSqlAction);
                return task > 0;
            }, true);
        }

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="fields">x=> x.SomeProperty1 or x=> new { x.SomeProperty1, x.SomeProperty2 }</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetAsync(TEntity entity, Expression<Func<TEntity, object>> fields = null)
        {
            if (entity == null)
                return false;

            var fieldNames = fields.GetFieldNames()?.ToList();
            return await Execute(async (connection) =>
            {
                var tableName = entity.GetTableName(TableNameFunc);
                var task = await connection.SetAsync(tableName, entity, fieldNames, Transaction, OutSqlAction);
                return task;
            }, true);
        }

        /// <summary>
        /// 异步根据字段修改
        /// </summary>
        /// <param name="setExpress">修改字段表达式：() => new { SomeProperty1 = "", SomeProperty2 = "" } or Dictionary<string, object></string></param>
        /// <param name="whereExpress">条件表达式</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetAsync(Expression<Func<object>> setExpress, Expression<Func<TEntity, bool>> whereExpress)
        {
            if (setExpress == null || whereExpress == null)
                return false;

            return await Execute(async (connection) =>
            {
                var tableName = whereExpress.GetTableName(TableNameFunc);
                var task = await connection.SetAsync(tableName, setExpress, whereExpress, Transaction, OutSqlAction);
                return task;
            }, true);
        }

        /// <summary>
        /// 异步获取一条数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns>实体</returns>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false)
        {
            if (expression == null)
                return default(TEntity);

            return await Execute(async (connection) =>
            {
                var tableName = expression.GetTableName(TableNameFunc);
                var task = await connection.GetAsync(tableName, expression, fieldExpressison, Transaction, OutSqlAction);
                return task;
            }, isMaster);
        }

        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="expression">条件表达式</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <param name="orderByFields">排序字段集合</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetListAsync(
            int page,
            int rows,
            Expression<Func<TEntity, bool>> expression = null,
            Expression<Func<TEntity, object>> fieldExpressison = null,
            bool isMaster = false,
            params OrderByField[] orderByFields)
        {
            if (page <= 0 || rows <= 0)
                return default(IEnumerable<TEntity>);

            return await Execute(async (connection) =>
            {
                var tableName = expression.GetTableName(TableNameFunc);
                var task = await connection.GetListAsync(tableName, page, rows, expression, fieldExpressison, orderByFields?.ToList(), Transaction, OutSqlAction);
                return task;
            }, isMaster);
        }

        /// <summary>
        /// 异步获取列表 Offset
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="expression">条件表达式</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <param name="orderByFields">排序字段集合</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetOffsetsAsync(
            int offset,
            int size,
            Expression<Func<TEntity, bool>> expression = null,
            Expression<Func<TEntity, object>> fieldExpressison = null,
            bool isMaster = false,
            params OrderByField[] orderByFields)
        {
            if (offset < 0 || size <= 0)
                return default(IEnumerable<TEntity>);

            return await Execute(async (connection) =>
            {
                var tableName = expression.GetTableName(TableNameFunc);
                var task = await connection.GetOffsetsAsync(tableName, offset, size, expression, fieldExpressison, orderByFields?.ToList(), Transaction, OutSqlAction);
                return task;
            }, isMaster);
        }

        /// <summary>
        /// 异步获取数量
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null, bool isMaster = false)
        {
            return await Execute(async (connection) =>
            {
                var tableName = expression.GetTableName(TableNameFunc);
                var task = await connection.CountAsync(tableName, expression, Transaction, OutSqlAction);
                return task;
            }, isMaster);
        }
        #endregion

        #region Protected
        /// <summary>
        /// 包含connection的方法执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="isMaster"></param>
        /// <param name="ignoreTransaction">是否忽略事务，true 将忽略事务，直接创建一个connection</param>
        /// <returns></returns>
        protected async Task<T> Execute<T>(Func<IDbConnection, Task<T>> func, bool isMaster = true, bool ignoreTransaction = false)
        {
            if (!this.CheckTableIfMissingCreate(isMaster))
                return default(T);

            if (InTransaction)
            {
                var connection = OpenConnection(isMaster, ignoreTransaction);
                return await func(connection);
            }
            using (var connection = OpenConnection(isMaster))
            {
                return await func(connection);
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 填充ExecuteScript
        /// </summary>
        /// <param name="sql"></param>
        private void OutSqlAction(string sql)
        {
            ExecuteScript = sql;
        }
        #endregion
    }
}
