using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FR.Core
{
    public static class SqlMapping
    {
        /// <summary>
        /// 随机负载读取
        /// </summary>
        /// <param name="ConnectionStringKey"></param>
        /// <returns></returns>
        public static System.Data.IDbConnection GetConnection(string ConnectionStringKey)
        {
            //AlwaysOn读写分离与故障转移
            //<add key="FLogRead" value="server=10.173.241.231\SQL2014,10.173.241.231\SQL2014,10.173.241.231\SQL2014;database=flog;uid=;pwd="/>
            //<add key="FLogWrite" value="server=10.173.241.231\SQL2014,10.173.241.231\SQL2014,10.173.241.231\SQL2014;database=flog;uid=;pwd="/>
            var SqlConnStr = ComConfig.AppSettings[ConnectionStringKey];

            var IsEncryption = ComConfig.GetAppConfig("FR.Core.DatabaseConfig.Encryption", false);

            //如果Config加密，则先解密
            if (IsEncryption == true)
                SqlConnStr = Common.ComDes.Decrypt(SqlConnStr);
            
            var serverIps = SqlConnStr.Split(';')[0].Split('=')[1].Trim();

            List<string> serverIpList = serverIps.TrimEnd(',').Split(',').ToList();

            string tempConnStr = SqlConnStr.Replace(serverIps, "{0}");

            int index = 0;

            if (serverIpList.Count > 0)
            {
                Random r = new Random();

                index = r.Next(serverIpList.Count);
            }

            string serverIP = serverIpList[index];

            return new System.Data.SqlClient.SqlConnection(string.Format(tempConnStr, serverIP));
        }
        
        /// <summary>
        /// 拼接插入语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static MSqlData SqlServerInsert<T>(T obj, string tableName, int index, bool IsIncrement = false)
        {
            Type t = obj.GetType();

            List<string> listKey = new List<string>();

            List<string> listValue = new List<string>();

            DynamicParameters param = new DynamicParameters();

            System.Reflection.PropertyInfo[] Property = t.GetProperties();

            var primaryKey = GetPrimaryKey(t, obj);

            foreach (System.Reflection.PropertyInfo pi in Property)
            {
                //跳过自增主键
                if (primaryKey.IsIncrement && pi.Name == primaryKey.KeyName) continue;

                //传入的值
                var myVal = GetValue(pi.Name, obj);

                //传入的参数
                var myParamVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_{0}", pi.Name));

                //传入的表达式
                //var myMathVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_Math_{0}", pi.Name));

                if (myVal != null)
                {
                    var paramName = string.Format("@{0}_ox{1}xo", pi.Name, index);

                    listKey.Add(pi.Name);

                    listValue.Add(paramName);

                    param.Add(paramName, myVal);
                }
                else if (myParamVal != null)
                {
                    listKey.Add(pi.Name);

                    listValue.Add(myParamVal.ToString());
                }
                //else if (myMathVal != null)
                //{
                //    listKey.Add(pi.Name);

                //    listValue.Add(myMathVal.ToString());
                //}
            }

            return ComPublicStatic.Save(string.Format("INSERT INTO {0} ({1}) values ({2});", tableName, string.Join(",", listKey), string.Join(",", listValue)), param);
        }

        /// <summary>
        /// 拼接修改语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <param name="whereKey"></param>
        /// <returns></returns>
        public static MSqlData SqlServerUpdate<T>(T obj, string tableName, int index)
        {
            Type t = obj.GetType();

            List<string> listKey = new List<string>();

            StringBuilder sbWhere = new StringBuilder();

            DynamicParameters param = new DynamicParameters();

            System.Reflection.PropertyInfo[] Property = t.GetProperties();

            var primaryKey = GetPrimaryKey(t, obj);

            foreach (System.Reflection.PropertyInfo pi in Property)
            {
                //传入的值
                var myVal = GetValue(pi.Name, obj);

                //传入的参数
                var myParamVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_{0}", pi.Name));

                //传入的表达式
                var myMathVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_Math_{0}", pi.Name));

                if (myVal != null)
                {
                    var paramName = string.Format("@{0}_ox{1}xo", pi.Name, index);

                    var keyValue = string.Format("{0}={1}", pi.Name, paramName);

                    //如果室主键，则指定查询条件
                    if (pi.Name == primaryKey.KeyName)
                    {
                        sbWhere.AppendFormat("{0}={1}", pi.Name, paramName);

                        param.Add(paramName, myVal);

                        continue;
                    }

                    listKey.Add(keyValue);

                    param.Add(paramName, myVal);
                }
                else if (myParamVal != null)
                {
                    var keyValue = string.Format("{0}={1}", pi.Name, myParamVal);

                    listKey.Add(keyValue);
                }
                else if (myMathVal != null)
                {
                    var keyValue = string.Format("{0}={1}", pi.Name, myMathVal);

                    listKey.Add(keyValue);
                }

            }

            return ComPublicStatic.Save(string.Format("UPDATE {0} SET {1} WHERE {2};", tableName, string.Join(",", listKey), sbWhere.ToString()), param);
        }

        public static MSqlData SqlServerUpdate<T>(T obj, string tableName, string wheres, int index)
        {
            Type t = obj.GetType();

            List<string> listKey = new List<string>();

            DynamicParameters param = new DynamicParameters();

            System.Reflection.PropertyInfo[] Property = t.GetProperties();

            var primaryKey = GetPrimaryKey(t, obj);

            foreach (System.Reflection.PropertyInfo pi in Property)
            {
                //跳过主键
                if (pi.Name == primaryKey.KeyName)
                    continue;

                //传入的值
                var myVal = GetValue(pi.Name, obj);

                //传入的参数
                var myParamVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_{0}", pi.Name));

                //传入的表达式
                var myMathVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_Math_{0}", pi.Name));

                if (myVal != null)
                {
                    var paramName = string.Format("@{0}_ox{1}xo", pi.Name, index);

                    var keyValue = string.Format("{0}={1}", pi.Name, paramName);

                    listKey.Add(keyValue);

                    param.Add(paramName, myVal);
                }
                else if (myParamVal != null)
                {
                    var keyValue = string.Format("{0}={1}", pi.Name, myParamVal);

                    listKey.Add(keyValue);
                }
                else if (myMathVal != null)
                {
                    var keyValue = string.Format("{0}={1}", pi.Name, myMathVal);

                    listKey.Add(keyValue);
                }
            }

            return ComPublicStatic.Save(string.Format("UPDATE {0} SET {1} {2};", tableName, string.Join(",", listKey), wheres), param);
        }

        /// <summary>
        /// 拼接删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <param name="whereKey"></param>
        /// <returns></returns>
        public static MSqlData SqlServerDelete<T>(T obj, string tableName, int index)
        {
            Type t = obj.GetType();

            StringBuilder sbWhere = new StringBuilder();

            DynamicParameters param = new DynamicParameters();

            System.Reflection.PropertyInfo[] Property = t.GetProperties();

            //主键名称
            var primaryKey = GetPrimaryKey(t, obj);

            foreach (System.Reflection.PropertyInfo pi in Property)
            {
                //传入的值
                var myVal = GetValue(pi.Name, obj);

                //传入的参数
                var myParamVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_{0}", pi.Name));

                //传入的表达式
                var myMathVal = ComMapping.ToPrivate.GetFieldValue(obj, string.Format("_Math_{0}", pi.Name));

                if (pi.Name == primaryKey.KeyName)
                {
                    if (myVal != null)
                    {
                        var paramName = string.Format("@{0}_ox{1}xo", pi.Name, index);

                        sbWhere.AppendFormat("{0}={1}", pi.Name, paramName);

                        param.Add(paramName, myVal);
                    }
                    else if (myParamVal != null)
                    {
                        sbWhere.AppendFormat("{0}={1}", pi.Name, myParamVal);
                    }

                    break;
                }
            }

            return ComPublicStatic.Save(string.Format("DELETE FROM {0} WHERE {1};", tableName, sbWhere.ToString()), param);
        }

        public static MSqlData SqlServerDelete<T>(T obj, string tableName, string wheres, int index)
        {
            Type t = obj.GetType();

            return ComPublicStatic.Save(string.Format("DELETE FROM {0} {1};", tableName, wheres), new DynamicParameters());
        }

        public static string SqlServerGet<T>(string tableName, string ID, string showCols = "*", bool nolock = true)
        {
            Type t = typeof(T);

            StringBuilder sbCols = new StringBuilder();

            StringBuilder sbWhere = new StringBuilder();

            if (showCols == "*")
            {
                System.Reflection.PropertyInfo[] Property = t.GetProperties();

                foreach (System.Reflection.PropertyInfo pi in Property)
                {
                    sbCols.Append(pi.Name.Safe() + ",");
                }

                sbCols.Remove(sbCols.Length - 1, 1);
            }
            else
            {
                sbCols.Append(showCols.Safe());

                //Type c = showCols.GetType();

                //System.Reflection.PropertyInfo[] pInfo = c.GetProperties();

                //foreach (System.Reflection.PropertyInfo pio in pInfo)
                //{
                //    //Type pioType = pio.PropertyType;

                //    sbCols.Append(pio.Name.x() + ",");
                //}
            }

            //var primaryKey = (MPrimaryKey)GetKeyName.Invoke(System.Activator.CreateInstance<T>(), null);
            var primaryKey = GetPrimaryKey(t, System.Activator.CreateInstance<T>());

            sbWhere.AppendFormat("{0}='{1}'", primaryKey.KeyName, ID.Safe());

            return ComPublicStatic.Save(string.Format("SELECT {0} FROM {1} {3} WHERE {2};", sbCols.ToString(), tableName, sbWhere.ToString(), NoLock(nolock)));
        }

        public static string SqlServerGetList<T>(string tableName, string where, string orderBy, string showCols = "*", bool nolock = true)
        {
            Type t = typeof(T);

            StringBuilder sbCols = new StringBuilder();

            if (showCols == "*")
            {
                System.Reflection.PropertyInfo[] Property = t.GetProperties();

                foreach (System.Reflection.PropertyInfo pi in Property)
                {
                    sbCols.Append(pi.Name.Safe() + ",");
                }

                sbCols.Remove(sbCols.Length - 1, 1);
            }
            else
            {
                sbCols.Append(showCols.Safe());

                //Type c = showCols.GetType();

                //System.Reflection.PropertyInfo[] pInfo = c.GetProperties();

                //foreach (System.Reflection.PropertyInfo pio in pInfo)
                //{
                //    //Type pioType = pio.PropertyType;

                //    sbCols.Append(pio.Name.x() + ",");
                //}
            }

            return ComPublicStatic.Save(string.Format("SELECT {0} FROM {1} {4} {2} {3};", sbCols.ToString(), tableName, where, orderBy, NoLock(nolock)));
        }

        public static string SqlServerGetList<T>(string tableName, string where, string orderBy, int pageIndex, out string sqlCount, int pageSize, string showCols = "*", ESqlVersion sqlVersion = ESqlVersion.SqlServer2012, bool nolock = true)
        {
            //List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();

            Type t = typeof(T);

            StringBuilder sbSql = new StringBuilder();

            StringBuilder sbCount = new StringBuilder();

            StringBuilder sbCols = new StringBuilder();

            if (showCols == "*")
            {
                System.Reflection.PropertyInfo[] Property = t.GetProperties();

                foreach (System.Reflection.PropertyInfo pi in Property)
                {
                    sbCols.Append(pi.Name.Safe() + ",");
                }

                sbCols.Remove(sbCols.Length - 1, 1);
            }
            else
            {
                sbCols.Append(showCols.Safe());

                //Type c = showCols.GetType();

                //System.Reflection.PropertyInfo[] pInfo = c.GetProperties();

                //foreach (System.Reflection.PropertyInfo pio in pInfo)
                //{
                //    //Type pioType = pio.PropertyType;

                //    sbCols.Append(pio.Name.x() + ",");
                //}
            }

            sbCount.AppendFormat("SELECT Count(1) from {0} {2} {1};", tableName, where, NoLock(nolock));

            sqlCount = sbCount.ToString();

            sbSql.AppendFormat("SELECT {0} FROM {1} {3} {2}", sbCols.ToString(), tableName, where, NoLock(nolock));

            if (sqlVersion == ESqlVersion.SqlServer2012)
                return ComPublicStatic.Save(GetSqlServerPagedSqlWith2012(sbSql.ToString(), pageIndex, pageSize, orderBy));
            else
                return ComPublicStatic.Save(GetSqlServerPagedSqlWith2005(sbSql.ToString(), pageIndex, pageSize, orderBy));
        }

        public static string ExecuteSqlPage(string sql, string where, string orderBy, int pageIndex, out string sqlCount, int pageSize, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
        {
            StringBuilder sbSql = new StringBuilder();

            StringBuilder sbCount = new StringBuilder();

            sbCount.AppendFormat("SELECT Count(1) from ({0} {1}) as t;", sql, where);

            sqlCount = ComPublicStatic.Save(sbCount.ToString());

            sbSql.AppendFormat("{0} {1}", sql, where);

            if (sqlVersion == ESqlVersion.SqlServer2012)
                return ComPublicStatic.Save(GetSqlServerPagedSqlWith2012(sbSql.ToString(), pageIndex, pageSize, orderBy));
            else
                return ComPublicStatic.Save(GetSqlServerPagedSqlWith2005(sbSql.ToString(), pageIndex, pageSize, orderBy));
        }

        public static string SqlServerGetLeftJoinList(string sql, string where, string orderBy, int pageIndex, int pageSize, out string sqlCount, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
        {
            StringBuilder sbSql = new StringBuilder();

            StringBuilder sbCount = new StringBuilder();

            var arrSql = sql.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            arrSql[1] = "Count(1)";

            sbCount.Append(string.Join(" ", arrSql) + " " + where);

            sqlCount = sbCount.ToString();

            sbSql.Append(sql + " " + where);

            if (sqlVersion == ESqlVersion.SqlServer2012)
                return ComPublicStatic.Save(GetSqlServerPagedSqlWith2012(sbSql.ToString(), pageIndex, pageSize, orderBy));
            else
                return ComPublicStatic.Save(GetSqlServerPagedSqlWith2005(sbSql.ToString(), pageIndex, pageSize, orderBy));
        }

        public static MLeftJoin SqlServerGetLeftJoinSql<TLeft, TRight>(IList<MLeftJoin> list, string LeftTableName, string RightTableName, string On, string leftByName, string rightByName, string leftCols = "*", string rightCols = "*", bool nolock = true)
        {
            MLeftJoin m = new MLeftJoin();

            Type tLeft = typeof(TLeft);

            Type tRight = typeof(TRight);

            StringBuilder sbSql = new StringBuilder();

            StringBuilder sbCols = new StringBuilder();

            var LeftByName = leftByName.Safe().Trim();

            var RightByName = rightByName.Safe().Trim();

            if (list == null)
            {
                if (leftCols == "*")
                {
                    System.Reflection.PropertyInfo[] PropertyLeft = tLeft.GetProperties();

                    foreach (System.Reflection.PropertyInfo pi in PropertyLeft)
                    {
                        sbCols.AppendFormat("{0}.{1},", LeftByName, pi.Name.Safe());
                    }
                }
                else
                {
                    var LeftCols = leftCols.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var o in LeftCols)
                    {
                        if (o.Split('.').Length == 1)
                            sbCols.AppendFormat("{0}.{1},", LeftByName, o.Safe().Trim());
                        else
                            sbCols.AppendFormat("{0},", o.Safe().Trim());
                    }
                }
            }

            if (rightCols == "*")
            {
                System.Reflection.PropertyInfo[] PropertyRight = tRight.GetProperties();

                foreach (System.Reflection.PropertyInfo pi in PropertyRight)
                {
                    sbCols.AppendFormat("{0}.{1},", RightByName, pi.Name.Safe());
                }
            }
            else
            {
                var RightCols = rightCols.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var o in RightCols)
                {
                    if (o.Split('.').Length == 1)
                        sbCols.AppendFormat("{0}.{1},", RightByName, o.Safe().Trim());
                    else
                        sbCols.AppendFormat("{0},", o.Safe().Trim());
                }
            }

            m.Cols = sbCols.ToString();

            var onStr = On.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

            if (list == null)
            {
                m.Tables = string.Format(" {0} {1} {6} left join {2} {3} {6} on {4}={5}"
                , LeftTableName
                , LeftByName
                , RightTableName
                , RightByName
                , onStr[0].Trim().Safe()
                , onStr[1].Trim().Safe()
                , NoLock(nolock));
            }
            else
            {
                m.Tables = string.Format(" left join {0} {1} {4} on {2}={3}"
                , RightTableName
                , RightByName
                , onStr[0].Trim().Safe()
                , onStr[1].Trim().Safe()
                , NoLock(nolock));
            }

            return m;
        }

        public static string GetSql(IList<MLeftJoin> list, string wheres, string orderBy)
        {
            StringBuilder sbCols = new StringBuilder();

            StringBuilder sbTables = new StringBuilder();

            foreach (var o in list)
            {
                sbCols.AppendFormat("{0}", o.Cols);

                sbTables.AppendFormat(" {0}", o.Tables);
            }

            sbCols.Remove(sbCols.Length - 1, 1);

            return ComPublicStatic.Save(string.Format("select {0} from {1} {2} {3}", sbCols.ToString(), sbTables.ToString(), wheres, orderBy));
        }

        public static string GetSql(IList<MLeftJoin> list)
        {
            StringBuilder sbCols = new StringBuilder();

            StringBuilder sbTables = new StringBuilder();

            foreach (var o in list)
            {
                sbCols.AppendFormat("{0}", o.Cols);

                sbTables.AppendFormat(" {0}", o.Tables);
            }

            sbCols.Remove(sbCols.Length - 1, 1);

            return ComPublicStatic.Save(string.Format("select {0} from {1}", sbCols.ToString(), sbTables.ToString()));
        }

        private static object GetValue(string FieldName, object obj)
        {
            Type Ts = obj.GetType();

            object o = Ts.GetProperty(FieldName).GetValue(obj, null);

            if (o == null)
                return null;
            else
                return o;
        }

        private static string NoLock(bool noLock)
        {
            if (noLock)
                return " (NOLOCK) ";
            else
                return "";
        }

        /// <summary>
        /// Get DataTable KeyId
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static MPrimaryKey GetPrimaryKey<T>() where T : new()
        {
            T obj = new T();

            var SType = obj.GetType();

            //var GetKeyName = SType.GetMethod("GetKeyName");

            //return GetKeyName.Invoke(obj, null).ToString();

            return GetPrimaryKey(SType, obj);
        }

        public static MPrimaryKey GetPrimaryKey<T>(Type type, T obj)
        {
            var GetKeyName = type.GetMethod("GetKeyName");

            return (MPrimaryKey)GetKeyName.Invoke(obj, null);
        }

        /// <summary>
        /// 获取创建数据表的Sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GetCreateTableSql<T>(ref string tableName, MSplitTableConfig config)
        {
            var obj = System.Activator.CreateInstance<T>();

            var type = obj.GetType();

            var GetMethod = type.GetMethod("GetSqlByCreateTable");

            if (config.Type == ESplitTableType.DateTime)
            {
                tableName = tableName + DateTime.Now.ToString(config.DateTimeConfig);
            }

            var sqlCreateTable = GetMethod.Invoke(obj, new object[] { tableName }).ToString();

            var sql = @"
                declare @str varchar(100)
                if OBJECT_ID('{0}') is null
                begin
                    {1}
                end
            ";

            return ComPublicStatic.Save(string.Format(sql, tableName, sqlCreateTable));
        }

        /// <summary>
        /// Convert to DynamicParameters
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DynamicParameters ToDynamicParameters(this IList<MKeyValue> list)
        {
            DynamicParameters dy = new DynamicParameters();

            foreach (var item in list)
            {
                dy.Add(item.Key, item.Value);
            }

            return dy;
        }

        public static DynamicParameters AddDynamicParameters(this object list, object obj)
        {
            DynamicParameters listObj;

            if (list != null)
                listObj = (DynamicParameters)list;
            else
                listObj = new DynamicParameters();

            listObj.AddDynamicParams(obj);

            return listObj;
        }

        public static object XParameters(object param)
        {
            if (param != null && param.GetType().ToString().Contains("OrangeCloud.Core.MKeyValue"))
                return ToDynamicParameters((IList<MKeyValue>)param);

            return param;
        }

        /// <summary>
        /// 生成数据分页sql，适用于Sql Server2005及以上版本
        /// </summary>
        /// <param name="selectSql">例如select t1.aaa, t2.bbb from table1 t1, table2 t2 where t1.id = t2.id and t1.aaa = 'abc'</param>
        /// <param name="pageIndex">页索引，第一页从1开始</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBySql">用于排序的sql语句，例如 t1.id desc</param>
        /// <returns></returns>
        public static string GetSqlServerPagedSqlWith2005(string selectSql, int pageIndex, int pageSize, string orderBySql = null)
        {
            string select = "SELECT";
            int sIndex = selectSql.IndexOf(select, 0, StringComparison.CurrentCultureIgnoreCase);
            if (sIndex != -1) selectSql = " " + selectSql.Substring(sIndex + select.Length);
            int start = pageSize * (pageIndex - 1) + 1;
            int end = pageSize * (pageIndex - 1) + pageSize;
            if (orderBySql == null)
                orderBySql = "(SELECT 0)";
            string strSql = string.Format("select * from ( "
                + "select row_number() over({0}) as rowNum, {1} "
                + ") t where rowNum between {2} and {3} ", orderBySql, selectSql, start, end);
            return strSql;
        }

        /// <summary>
        /// 生成数据分页sql，适用于Sql Server2012及以上版本，性能更好
        /// </summary>
        /// <param name="selectSql">例如select t1.aaa, t2.bbb from table1 t1, table2 t2 where t1.id = t2.id and t1.aaa = 'abc'</param>
        /// <param name="pageIndex">页索引，第一页从0开始</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBySql">用于排序的sql语句，例如 t1.id desc</param>
        /// <returns></returns>
        public static string GetSqlServerPagedSqlWith2012(string selectSql, int pageIndex, int pageSize, string orderBySql = null)
        {
            string select = "SELECT";
            int sIndex = selectSql.IndexOf(select, 0, StringComparison.CurrentCultureIgnoreCase);
            if (sIndex != -1) selectSql = " " + selectSql.Substring(sIndex + select.Length);
            //int start = pageSize * pageIndex + 1;
            int start = pageSize * (pageIndex - 1);
            //int end = pageSize * pageIndex + pageSize;
            if (orderBySql == null)
                orderBySql = "(SELECT 0)";
            string strSql = string.Format("SELECT {1} {0} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY ",
                orderBySql, selectSql, start, pageSize);
            return strSql;
        }

    }
}
