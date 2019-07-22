namespace Sodao.Core.Data
{
    /// <summary>
    /// 排序字段对象
    /// </summary>
    public class OrderByField
    {
        /// <summary>
        /// 字段 使用nameof(Class.Property)
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public FieldSortType OrderBy { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="field"></param>
        /// <param name="orderBy"></param>
        public OrderByField(string field, FieldSortType orderBy = FieldSortType.Asc)
        {
            Field = field;
            OrderBy = orderBy;
        }

        /// <summary>
        /// 静态构造对象
        /// </summary>
        /// <param name="field"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static OrderByField Create(string field, FieldSortType orderBy = FieldSortType.Asc)
        {
            return new OrderByField(field, orderBy);
        }
    }

    /// <summary>
    /// 扩展类
    /// </summary>
    public static class OrderByFieldExtension
    {
        /// <summary>
        /// 顺序构造对象扩展方法
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static OrderByField OrderBy(this string field)
        {
            return new OrderByField(field, FieldSortType.Asc);
        }

        /// <summary>
        /// 倒序构造对象扩展方法
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static OrderByField OrderByDesc(this string field)
        {
            return new OrderByField(field, FieldSortType.Desc);
        }
    }
}
