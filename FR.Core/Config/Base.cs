using System;
using System.Collections.Generic;
using System.Text;

namespace FR.Core
{
    public class Base
    {
        /// <summary>
        /// Cookie - 登录
        /// </summary>
        public static readonly string LoginCookieName = "";

        /// <summary>
        /// KEY
        /// </summary>
        public static readonly string Key = "";

        /// <summary>
        /// CPKey
        /// </summary>
        public static readonly string CPKey = "";

        /// <summary>
        /// 使用的数据库
        /// </summary>
        public static readonly string DataBase = "SqlServer";

        /// <summary>
        /// 数据表主键名称
        /// </summary>
        public static readonly string DataTableIdName = "";

        /// <summary>
        /// 数据库Key前缀
        /// </summary>
        public static readonly string DataBaseKeyPrefix = "";

        /// <summary>
        /// 数据库Key后缀
        /// </summary>
        public static readonly string DataBaseKeySuffix = "";

        #region AppSettings Key名称设置

        /// <summary>
        /// （AppSettings）是否把SQL记录到文本日志
        /// </summary>
        public static readonly string SaveLog = "SaveLog";

        /// <summary>
        /// （AppSettings）文本日志路径
        /// </summary>
        public static readonly string SavePath = "SavePath";

        #endregion

        /// <summary>
        /// 主键名称
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public static string GetPrimaryKey(string tbName)
        {
            if (string.IsNullOrWhiteSpace(DataTableIdName))
                return tbName + "ID";
            else
                return DataTableIdName;
        }
    }
}
