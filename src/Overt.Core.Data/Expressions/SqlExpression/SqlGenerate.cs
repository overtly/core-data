using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Overt.Core.Data.Expressions
{
    /// <summary>
    /// SqlGenrate
    /// </summary>
	public class SqlGenerate
    {
        #region Private Property
        private static readonly List<string> S_listEnglishWords = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", };

        private Dictionary<string, string> _dicTableName = new Dictionary<string, string>();
        private Queue<string> _queueEnglishWords = new Queue<string>(S_listEnglishWords);
        #endregion

        #region Public Property
        /// <summary>
        /// 字段
        /// </summary>
        public List<string> SelectFields { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段字符串
        /// </summary>
        public string SelectFieldsStr
        {
            get
            {
                return string.Join(", ", this.SelectFields);
            }
        }

        /// <summary>
        /// sql长度
        /// </summary>
        public int Length
        {
            get
            {
                return Sql.Length;
            }
        }

        /// <summary>
        /// 脚本
        /// </summary>
        public StringBuilder Sql { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// 数据库参数
        /// </summary>
        public Dictionary<string, object> DbParams { get; private set; }

        /// <summary>
        /// 索引数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get
            {
                return this.Sql[index];
            }
        }
        #endregion


        #region Constructor 
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlGenerate()
        {
            DbParams = new Dictionary<string, object>();
            Sql = new StringBuilder();
            SelectFields = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlGenerate"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlGenerate operator +(SqlGenerate sqlGenerate, string sql)
        {
            sqlGenerate.Sql.Append(sql);
            return sqlGenerate;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            SelectFields.Clear();
            Sql.Clear();
            DbParams.Clear();
            _dicTableName.Clear();
            _queueEnglishWords = new Queue<string>(S_listEnglishWords);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterValue"></param>
		public void AddDbParameter(object parameterValue)
        {
            if (parameterValue == null || parameterValue == DBNull.Value)
                Sql.Append(" null");
            else
            {
                var name = DatabaseType.ParamPrefix() + "param" + DbParams.Count;
                DbParams.Add(name, parameterValue);
                Sql.Append(" " + name);
            }
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Sql.ToString();
        }
        #endregion
    }
}