using Dapper;
using Overt.Core.Data.Expressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Overt.Core.Data
{
    /// <summary>
    /// Dapper扩展
    /// </summary>
    public static partial class DapperExtensions
    {
        #region Public Method
        /// <summary>
        /// 是否存在表
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="outSqlAction"></param>
        /// <returns></returns>
        public static async Task<bool> IsExistTableAsync(this IDbConnection connection, string tableName, Action<string> outSqlAction = null)
        {
            if (string.IsNullOrEmpty(tableName))
                return false;
            var dbType = connection.GetDbType();
            var dbName = connection.Database;
            var sql = dbType.ExistTableSql(dbName, tableName);
            outSqlAction?.Invoke(sql); // 返回sql

            var result = await connection.QueryFirstOrDefaultAsync<int>(sql);
            return result > 0;
        }

        /// <summary>
        /// 是否存在字段
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="outSqlAction"></param>
        /// <returns></returns>
        public static async Task<bool> IsExistFieldAsync(this IDbConnection connection, string tableName, string fieldName, Action<string> outSqlAction = null)
        {
            if (string.IsNullOrEmpty(tableName))
                return false;
            var dbType = connection.GetDbType();
            var dbName = connection.Database;
            var sql = dbType.ExistFieldSql(dbName, tableName, fieldName);
            outSqlAction?.Invoke(sql);

            var result = await connection.QueryFirstOrDefaultAsync<int>(sql);
            return result > 0;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="entity"></param>
        /// <param name="returnLastIdentity">是否返回自增的数据</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns>-1 参数为空</returns>
        public static async Task<int> InsertAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            TEntity entity,
            bool returnLastIdentity = false,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var addFields = new List<string>();
            var atFields = new List<string>();
            var dbType = connection.GetDbType();

            var pis = typeof(TEntity).GetProperties();
            var identityPropertyInfo = entity.GetIdentityField();
            foreach (var pi in pis)
            {
                if (identityPropertyInfo?.Name == pi.Name)
                    continue;

                addFields.Add($"{pi.Name.ParamSql(dbType)}");
                atFields.Add($"@{pi.Name}");
            }

            var sql = $"insert into {tableName.ParamSql(dbType)}({string.Join(", ", addFields)}) values({string.Join(", ", atFields)});";
            outSqlAction?.Invoke(sql);

            int result;
            if (identityPropertyInfo != null && returnLastIdentity)
            {
                sql += dbType.SelectLastIdentity();
                result = await connection.ExecuteScalarAsync<int>(sql, entity);
                if (result > 0)
                    identityPropertyInfo.SetValue(entity, result);
            }
            else
            {
                result = await connection.ExecuteAsync(sql, entity);
            }

            return result;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="entities"></param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns>-1 参数为空</returns>
        public static async Task<int> InsertAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            IEnumerable<TEntity> entities,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if ((entities?.Count() ?? 0) <= 0)
                throw new ArgumentNullException(nameof(entities));

            var addFields = new List<string>();
            var atFields = new List<string>();
            var dbType = connection.GetDbType();

            var pis = typeof(TEntity).GetProperties();
            var identityPropertyInfo = entities.First().GetIdentityField();
            foreach (var pi in pis)
            {
                if (identityPropertyInfo?.Name == pi.Name)
                    continue;

                addFields.Add($"{pi.Name.ParamSql(dbType)}");
                atFields.Add($"@{pi.Name}");
            }

            var sql = $"insert into {tableName.ParamSql(dbType)}({string.Join(", ", addFields)}) values({string.Join(", ", atFields)});";
            outSqlAction?.Invoke(sql);

            var result = await connection.ExecuteAsync(sql, entities);
            return result;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="whereExpress"></param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns>-1 参数为空</returns>
        public static async Task<int> DeleteAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            Expression<Func<TEntity, bool>> whereExpress,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (whereExpress == null)
                throw new ArgumentNullException(nameof(whereExpress));

            var dbType = connection.GetDbType();
            var sqlExpression = SqlExpression.Delete<TEntity>(dbType, tableName).Where(whereExpress);
            outSqlAction?.Invoke(sqlExpression.Script);

            var result = await connection.ExecuteAsync(sqlExpression.Script, sqlExpression.DbParams);
            return result;
        }

        /// <summary>
        /// 对象修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="entity"></param>
        /// <param name="fields">选择字段</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<bool> SetAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            TEntity entity,
            IEnumerable<string> fields = null,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var setFields = new List<string>();
            var whereFields = new List<string>();
            var dbType = connection.GetDbType();

            var pis = typeof(TEntity).GetProperties();
            foreach (var pi in pis)
            {
                var obs = pi.GetCustomAttributes(typeof(KeyAttribute), false);
                if (obs?.Count() > 0)
                    whereFields.Add($"{pi.Name.ParamSql(dbType)} = @{pi.Name}");
                else
                {
                    if ((fields?.Count() ?? 0) <= 0 || fields.Contains(pi.Name))
                        setFields.Add($"{pi.Name.ParamSql(dbType)} = @{pi.Name}");
                }
            }
            if (whereFields.Count <= 0)
                throw new Exception($"实体[{nameof(TEntity)}]未设置主键Key属性");
            if (setFields.Count <= 0)
                throw new Exception($"实体[{nameof(TEntity)}]未标记任何更新字段");

            var sql = $"update {tableName.ParamSql(dbType)} set {string.Join(", ", setFields)} where {string.Join(", ", whereFields)}";
            outSqlAction?.Invoke(sql);

            var result = await connection.ExecuteAsync(sql, entity);
            return result > 0;
        }

        /// <summary>
        /// 条件修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection">连接</param>
        /// <param name="tableName">表名</param>
        /// <param name="setExpress">修改内容表达式</param>
        /// <param name="whereExpress">条件表达式</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<bool> SetAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            Expression<Func<object>> setExpress,
            Expression<Func<TEntity, bool>> whereExpress,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (setExpress == null || whereExpress == null)
                throw new ArgumentNullException($"{nameof(setExpress)} / {nameof(whereExpress)}");

            var dbType = connection.GetDbType();
            var sqlExpression = SqlExpression.Update<TEntity>(dbType, setExpress, tableName).Where(whereExpress);
            outSqlAction?.Invoke(sqlExpression.Script); // 返回sql

            var result = await connection.ExecuteAsync(sqlExpression.Script, sqlExpression.DbParams);
            return result > 0;
        }

        /// <summary>
        /// 条件修改 在字段上增减
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="connection">连接</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">增减的字段</param>
        /// <param name="value">增减的值</param>
        /// <param name="whereExpress">条件表达式</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<bool> IncrAsync<TEntity, TValue>(this
            IDbConnection connection,
            string tableName,
            string field,
            TValue value,
            Expression<Func<TEntity, bool>> whereExpress,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(field, "增减字段不能为空");

            var dbType = connection.GetDbType();
            var setExpressString = $"{field.ParamSql(dbType)} = {field.ParamSql(dbType)} + ({value})";
            var sqlExpression = SqlExpression.Update<TEntity>(dbType, () => setExpressString, tableName).Where(whereExpress);
            outSqlAction?.Invoke(sqlExpression.Script); // 返回sql

            var result = await connection.ExecuteAsync(sqlExpression.Script, sqlExpression.DbParams);
            return result > 0;
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName">表名</param>
        /// <param name="whereExpress">条件表达式</param>
        /// <param name="fieldExpress">选择字段，默认为*</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<TEntity> GetAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            Expression<Func<TEntity, bool>> whereExpress,
            Expression<Func<TEntity, object>> fieldExpress = null,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (whereExpress == null)
                throw new ArgumentNullException(nameof(whereExpress));

            var dbType = connection.GetDbType();
            var sqlExpression = SqlExpression.Select(dbType, fieldExpress, tableName).Where(whereExpress);
            outSqlAction?.Invoke(sqlExpression.Script); // 返回sql

            var result = await connection.QueryFirstOrDefaultAsync<TEntity>(sqlExpression.Script, sqlExpression.DbParams);
            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="whereExpress">条件表达式</param>
        /// <param name="fieldExpress">选择字段，默认为*</param>
        /// <param name="orderByFields">排序字段集合</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> GetListAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            int page,
            int rows,
            Expression<Func<TEntity, bool>> whereExpress,
            Expression<Func<TEntity, object>> fieldExpress = null,
            List<OrderByField> orderByFields = null,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            var dbType = connection.GetDbType();
            var sqlExpression = SqlExpression.Select(dbType, fieldExpress, tableName);
            if (whereExpress != null)
                sqlExpression.Where(whereExpress);

            var orderBy = string.Empty;
            if ((orderByFields?.Count ?? 0) > 0)
                orderBy = $" {string.Join(", ", orderByFields.Select(oo => oo.Field.ParamSql(dbType) + " " + oo.OrderBy))}";
            sqlExpression.OrderBy(orderBy).Limit(page, rows);
            outSqlAction?.Invoke(sqlExpression.Script); // 返回sql

            var result = await connection.QueryAsync<TEntity>(sqlExpression.Script, sqlExpression.DbParams);
            return result;
        }

        /// <summary>
        /// 获取分页数据 Offset
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="whereExpress">条件表达式</param>
        /// <param name="fieldExpress">选择字段，默认为*</param>
        /// <param name="orderByFields">排序字段集合</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> GetOffsetsAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            int offset,
            int size,
            Expression<Func<TEntity, bool>> whereExpress,
            Expression<Func<TEntity, object>> fieldExpress = null,
            List<OrderByField> orderByFields = null,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            var dbType = connection.GetDbType();
            var sqlExpression = SqlExpression.Select(dbType, fieldExpress, tableName);
            if (whereExpress != null)
                sqlExpression.Where(whereExpress);

            var orderBy = string.Empty;
            if ((orderByFields?.Count ?? 0) > 0)
                orderBy = $" {string.Join(", ", orderByFields.Select(oo => oo.Field.ParamSql(dbType) + " " + oo.OrderBy))}";
            sqlExpression.OrderBy(orderBy).Offset(offset, size);
            outSqlAction?.Invoke(sqlExpression.Script); // 返回sql

            var result = await connection.QueryAsync<TEntity>(sqlExpression.Script, sqlExpression.DbParams);
            return result;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="whereExpress">条件表达式</param>
        /// <param name="outSqlAction">返回sql语句</param>
        /// <returns></returns>
        public static async Task<int> CountAsync<TEntity>(this
            IDbConnection connection,
            string tableName,
            Expression<Func<TEntity, bool>> whereExpress,
            Action<string> outSqlAction = null)
            where TEntity : class, new()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            var dbType = connection.GetDbType();
            var sqlExpression = SqlExpression.Count<TEntity>(dbType, tableName: tableName).Where(whereExpress);
            outSqlAction?.Invoke(sqlExpression.Script); // 返回sql

            var result = await connection.QueryFirstOrDefaultAsync<int>(sqlExpression.Script, sqlExpression.DbParams);
            return result;
        }
        #endregion
    }
}
