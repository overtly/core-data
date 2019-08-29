using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Overt.Core.Data
{
    /// <summary>
    /// 接口
    /// </summary>
    public interface IBaseRepository<TEntity> : IPropertyAssist where TEntity : class, new()
    {
        /// <summary>
        /// 获取主表名
        /// </summary>
        /// <returns></returns>
        string GetMainTableName();

        /// <summary>
        /// 获取表名:内部会调用TableNameFunc 从主库中查询
        /// </summary>
        /// <returns></returns>
        string GetTableName();

        /// <summary>
        /// 获取表名：以Submeter分表位数获取表名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetTableName(string key);

        /// <summary>
        /// 是否存在表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        bool IsExistTable(string tableName, bool isMaster = true);

        /// <summary>
        /// 是否存在表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        Task<bool> IsExistTableAsync(string tableName, bool isMaster = true);

        /// <summary>
        /// 是否存在字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        bool IsExistField(string tableName, string fieldName, bool isMaster = true);

        /// <summary>
        /// 是否存在字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        Task<bool> IsExistFieldAsync(string tableName, string fieldName, bool isMaster = true);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="returnLastIdentity">是否赋值最后一次的自增ID</param>
        /// <returns>添加后的数据实体</returns>
        bool Add(TEntity entity, bool returnLastIdentity = false);

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="returnLastIdentity">是否赋值最后一次的自增ID</param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity, bool returnLastIdentity = false);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">删除条件</param>
        /// <returns>是否成功</returns>
        bool Delete(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="expression">删除条件</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="fields">x=> x.SomeProperty1 or x=> new { x.SomeProperty1, x.SomeProperty2 }</param>
        /// <returns>是否成功</returns>
        bool Set(TEntity entity, Expression<Func<TEntity, object>> fields = null);

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="fields">x=> x.SomeProperty1 or x=> new { x.SomeProperty1, x.SomeProperty2 }</param>
        /// <returns>是否成功</returns>
        Task<bool> SetAsync(TEntity entity, Expression<Func<TEntity, object>> fields = null);

        /// <summary>
        /// 根据字段修改
        /// </summary>
        /// <param name="setExpress">修改字段表达式 object =>dynamic 是一个匿名类</param>
        /// <param name="whereExpress">条件表达式</param>
        /// <returns>是否成功</returns>
        bool Set(Expression<Func<object>> setExpress, Expression<Func<TEntity, bool>> whereExpress);

        /// <summary>
        /// 异步根据字段修改
        /// </summary>
        /// <param name="setExpress">修改字段表达式</param>
        /// <param name="whereExpress">条件表达式</param>
        /// <returns>是否成功</returns>
        Task<bool> SetAsync(Expression<Func<object>> setExpress, Expression<Func<TEntity, bool>> whereExpress);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns>实体</returns>
        TEntity Get(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false);

        /// <summary>
        /// 异步获取一条数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <param name="fieldExpressison">按字段返回</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns>实体</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false);

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
        IEnumerable<TEntity> GetList(int page, int rows, Expression<Func<TEntity, bool>> expression = null, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false, params OrderByField[] orderByFields);

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
        IEnumerable<TEntity> GetOffsets(int offset, int size, Expression<Func<TEntity, bool>> expression = null, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false, params OrderByField[] orderByFields);

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
        Task<IEnumerable<TEntity>> GetListAsync(int page, int rows, Expression<Func<TEntity, bool>> expression = null, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false, params OrderByField[] orderByFields);

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
        Task<IEnumerable<TEntity>> GetOffsetsAsync(int offset, int size, Expression<Func<TEntity, bool>> expression = null, Expression<Func<TEntity, object>> fieldExpressison = null, bool isMaster = false, params OrderByField[] orderByFields);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> expression = null, bool isMaster = false);

        /// <summary>
        /// 异步获取数量
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="isMaster">是否主从</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null, bool isMaster = false);
    }
}
