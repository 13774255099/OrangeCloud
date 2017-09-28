using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FR.Core
{
    public static class ComPublicStatic
    {
        public static string Safe(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
                return str.Replace("'", "");

            return str;
        }

        public static string Save(string sql)
        {
            var saveLog = Base.SaveLog;

            var savePath = Base.SavePath;

            if (!string.IsNullOrWhiteSpace(saveLog) && saveLog.ToLower() == "true")
            {
                if (string.IsNullOrWhiteSpace(savePath))
                    savePath = "d:\\Log\\";

                System.IO.File.AppendAllText(savePath + DateTime.Now.ToString("yyyyMMdd") + ".txt", DateTime.Now.ToString() + ":\r\n" + sql + "\r\n");
            }

            return sql;
        }

        public static MSqlData Save(string sql, DynamicParameters param)
        {
            Save(sql + "\r\n" + param.JsonSerialize());

            return new MSqlData() { Sql = sql, Param = param };
        }

        public static string GetSign(EMath sign)
        {
            if (sign == EMath.ADD)
                return "+";
            else if (sign == EMath.SUBTRACT)
                return "-";
            else if (sign == EMath.MULTIPLY)
                return "*";
            else if (sign == EMath.DIVIDE)
                return "/";

            return "";
        }
    }
}
