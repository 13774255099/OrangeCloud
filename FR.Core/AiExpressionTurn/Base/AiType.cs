using System;
using System.Collections.Generic;

namespace FR.Core
{
    public static class AiType
    {
        /// <summary>
        /// in查询：例（select * from tblDemo where id in(1, 2, 3)）
        /// </summary>
        /// <param name="my"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool In(this object my, object obj)
        {
            return true;
        }

        public static bool NotIn(this object my, object obj)
        {
            return true;
        }

        /// <summary>
        /// in查询：例（select * from tblDemo where id in(1, 2, 3)）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="my"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool In<T>(this object my, List<T> obj)
        {
            return true;
        }

        public static bool NotIn<T>(this object my, List<T> obj)
        {
            return true;
        }

        /// <summary>
        /// in查询：例（select * from tblDemo where id in(1, 2, 3)）
        /// </summary>
        /// <param name="my"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool In(this object my, params dynamic[] obj)
        {
            return true;
        }

        public static bool NotIn(this object my, params dynamic[] obj)
        {
            return true;
        }

        /// <summary>
        /// like模糊查询：例（select * from tblDemo where name like '%yellow%'）
        /// </summary>
        /// <param name="my"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Like(this object my, string obj)
        {
            return true;
        }

        public static bool NotLike(this object my, string obj)
        {
            return true;
        }
























        public static T? ConvertTo<T>(this IConvertible convertibleValue) where T : struct
        {
            if (null == convertibleValue)
            {
                return null;
            }
            return (T?)Convert.ChangeType(convertibleValue, typeof(T));
        }

        /// <summary>
        /// 获取当前Nullable《T》对象的值,为Null则返回该类型的默认值
        /// </summary>
        /// <param name="my"></param>
        /// <returns></returns>
        public static decimal Value(this decimal? my)
        {
            if (my == null)
                return default(decimal);
            else
                return my.Value;
        }

        /// <summary>
        /// 获取当前Nullable《T》对象的值,为Null则返回该类型的默认值
        /// </summary>
        /// <param name="my"></param>
        /// <returns></returns>
        public static int Value(this int? my)
        {
            if (my == null)
                return default(int);
            else
                return my.Value;
        }

        /// <summary>
        /// 获取当前Nullable《T》对象的值,为Null则返回该类型的默认值
        /// </summary>
        /// <param name="my"></param>
        /// <returns></returns>
        public static long Value(this long? my)
        {
            if (my == null)
                return default(long);
            else
                return my.Value;
        }

        /// <summary>
        /// 获取当前Nullable《T》对象的值,为Null则返回该类型的默认值
        /// </summary>
        /// <param name="my"></param>
        /// <returns></returns>
        public static short Value(this short? my)
        {
            if (my == null)
                return default(short);
            else
                return my.Value;
        }

        /// <summary>
        /// 获取当前Nullable《T》对象的值,为Null则返回该类型的默认值(1900-01-01)
        /// </summary>
        /// <param name="my"></param>
        /// <returns></returns>
        public static DateTime Value(this DateTime? my)
        {
            if (my == null)
                return Convert.ToDateTime("1900-01-01");
            else
                return my.Value;
        }

        /// <summary>
        /// 获取当前Nullable《T》对象的值,为Null则返回该类型的默认值
        /// </summary>
        /// <param name="my"></param>
        /// <returns></returns>
        public static bool Value(this bool? my)
        {
            if (my == null)
                return default(bool);
            else
                return my.Value;
        }
    }
}
