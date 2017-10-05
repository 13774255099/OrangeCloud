using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace FR.Core
{
    public static class ORM
    {
        /// <summary>
        /// SQL插入 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="IsReturnId">是否返回Id</param>
        /// <param name="IsSplitTable">是否需要分表</param>
        /// <param name="config">分表配置</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int Insert<T>(T t, bool IsReturnId = false, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            return DataBase.Instance.Insert(t, IsReturnId, IsSplitTable, config, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL插入 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="IsReturnId">是否返回Id</param>
        /// <param name="IsSplitTable">是否需要分表</param>
        /// <param name="config">分表配置</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static long Insert64<T>(T t, bool IsReturnId = false, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            return DataBase.Instance.Insert64(t, IsReturnId, IsSplitTable, config, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL插入 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <param name="IsReturnId">是否返回Id</param>
        /// <param name="IsSplitTable">是否需要分表</param>
        /// <param name="config">分表配置</param>
        /// <returns></returns>
        public static MSqlData Insert<T>(IList<MSqlData> listSqlData, T t, bool IsReturnId = false, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            return DataBase.Instance.Insert(listSqlData, t, IsReturnId, IsSplitTable, config);
        }

        /// <summary>
        /// SQL插入 - 批处理 - 传入List Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listT"></param>
        /// <param name="IsReturnId"></param>
        /// <param name="IsSplitTable"></param>
        /// <param name="config"></param>
        public static void InsertList<T>(IList<T> listT, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            DataBase.Instance.InsertList(listT, IsSplitTable, config);
        }

        /// <summary>
        /// SQL插入 - 批处理 - 传入List Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listSqlData"></param>
        /// <param name="listT"></param>
        /// <param name="IsReturnId"></param>
        /// <param name="IsSplitTable"></param>
        /// <param name="config"></param>
        public static void InsertList<T>(IList<MSqlData> listSqlData, IList<T> listT, bool IsReturnId = false, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            foreach (var t in listT)
            {
                DataBase.Instance.Insert(listSqlData, t, IsReturnId, IsSplitTable, config);
            }
        }

        /// <summary>
        /// SQL插入 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <param name="IsReturnId">是否返回Id</param>
        /// <param name="IsSplitTable">是否需要分表</param>
        /// <param name="config">分表配置</param>
        /// <returns></returns>
        public static MSqlData Insert64<T>(IList<MSqlData> listSqlData, T t, bool IsReturnId = false, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            return DataBase.Instance.Insert64(listSqlData, t, IsReturnId, IsSplitTable, config);
        }

        /// <summary>
        /// SQL插入 - 批处理 - 传入List Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listSqlData"></param>
        /// <param name="listT"></param>
        /// <param name="IsReturnId"></param>
        /// <param name="IsSplitTable"></param>
        /// <param name="config"></param>
        public static void Insert64List<T>(IList<MSqlData> listSqlData, IList<T> listT, bool IsReturnId = false, bool IsSplitTable = false, MSplitTableConfig config = null)
        {
            foreach (var t in listT)
            {
                DataBase.Instance.Insert64(listSqlData, t, IsReturnId, IsSplitTable, config);
            }
        }

        /// <summary>
        /// SQL修改 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int Update<T>(T t)
        {
            return DataBase.Instance.Update(t, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL修改 - 根据搜索条件 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int Update<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            return DataBase.Instance.Update(t, bizExp, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL修改 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public static MSqlData Update<T>(IList<MSqlData> listSqlData, T t)
        {
            return DataBase.Instance.Update(listSqlData, t);
        }

        /// <summary>
        /// SQL修改 - 批处理 - 传入List Model
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public static void UpdateList<T>(IList<MSqlData> listSqlData, IList<T> listT)
        {
            foreach (var t in listT)
            {
                DataBase.Instance.Update(listSqlData, t);
            }
        }

        /// <summary>
        /// SQL修改 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <returns></returns>
        public static MSqlData Update<T>(IList<MSqlData> listSqlData, T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            return DataBase.Instance.Update(listSqlData, t, bizExp);
        }

        /// <summary>
        /// SQL物理删除 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int Delete<T>(T t)
        {
            return DataBase.Instance.Delete(t, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL物理删除 - 根据搜索条件 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int Delete<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            return DataBase.Instance.Delete(t, bizExp, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL物理删除 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public static MSqlData Delete<T>(IList<MSqlData> listSqlData, T t)
        {
            return DataBase.Instance.Delete(listSqlData, t);
        }

        /// <summary>
        /// SQL物理删除 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public static void Delete<T>(IList<MSqlData> listSqlData, List<T> listT)
        {
            foreach (var t in listT)
            {
                DataBase.Instance.Delete(listSqlData, t);
            }
        }

        /// <summary>
        /// SQL物理删除 - 根据搜索条件 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="listSqlData">工作集</param>
        /// <param name="t">实体</param>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <returns></returns>
        public static MSqlData Delete<T>(IList<MSqlData> listSqlData, T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            return DataBase.Instance.Delete(listSqlData, t, bizExp);
        }

        /// <summary>
        /// SQL逻辑删除 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int LogicalDelete<T>(T t)
        {
            return DataBase.Instance.LogicalDelete(t, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL逻辑删除 - 根据搜索条件 - 立即执行
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static int LogicalDelete<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            return DataBase.Instance.LogicalDelete(t, bizExp, EConnectionMode.Write);
        }

        /// <summary>
        /// SQL逻辑删除 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static MSqlData LogicalDelete<T>(IList<MSqlData> listSqlData, T t)
        {
            return DataBase.Instance.LogicalDelete(listSqlData, t);
        }

        /// <summary>
        /// SQL逻辑删除 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static void LogicalDeleteList<T>(IList<MSqlData> listSqlData, List<T> listT)
        {
            foreach (var t in listT)
            {
                DataBase.Instance.LogicalDelete(listSqlData, t);
            }
        }

        /// <summary>
        /// SQL逻辑删除 - 根据搜索条件 - 批处理
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="t">实体</param>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="ConnMode">读库/写库（默认为写库）</param>
        /// <returns></returns>
        public static MSqlData LogicalDelete<T>(IList<MSqlData> listSqlData, T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            return DataBase.Instance.LogicalDelete(listSqlData, t, bizExp);
        }

        //public static MTrans InsertByTrans<T>(T t, bool IsIncrement = false, string ParaName = null, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.InsertByTrans(t, IsIncrement, ParaName, IDName, ConnMode);
        //}

        //public static MTrans UpdateByTrans<T>(T t, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.UpdateByTrans(t, IDName, ConnMode);
        //}

        //public static MTrans UpdateByTrans<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.UpdateByTrans(t, bizExp, IDName, ConnMode);
        //}

        //public static MTrans LogicalDeleteByTrans<T>(T t, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.LogicalDeleteByTrans(t, IDName, ConnMode);
        //}

        //public static MTrans LogicalDeleteByTrans<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.LogicalDeleteByTrans(t, bizExp, IDName, ConnMode);
        //}

        //public static MTrans DeleteByTrans<T>(T t, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.DeleteByTrans(t, IDName, ConnMode);
        //}

        //public static MTrans DeleteByTrans<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write)
        //{
        //    return DataBase.Instance.DeleteByTrans(t, bizExp, IDName, ConnMode);
        //}

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <returns></returns>
        public static TransactionScope TransMaster(System.Transactions.IsolationLevel isolationLevel = System.Transactions.IsolationLevel.Serializable)
        {
            return DataBase.Instance.TransMaster(isolationLevel);
        }

        //public static bool ExecuteWithTrans(List<MTrans> sqlList)
        //{
        //    return DataBase.Instance.ExecuteWithTrans(sqlList);
        //}

        /// <summary>
        /// SQL查询 - 按Id查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <returns></returns>
        public static T Get<T>(string id, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Get<T>(id, showCols, nolock, ConnMode);
        }

        /// <summary>
        /// SQL查询 - 按Id查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <typeparam name="NT">映射的实体</typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <returns></returns>
        public static NT Get<T, NT>(string id, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Get<T, NT>(id, showCols, nolock, ConnMode);
        }

        /// <summary>
        /// SQL查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <returns></returns>
        public static IList<T> Get<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Get(bizExp, showCols, nolock, ConnMode);
        }

        /// <summary>
        /// SQL查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <typeparam name="NT">映射的实体</typeparam>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <returns></returns>
        public static IList<NT> Get<T, NT>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Get<T, NT>(bizExp, showCols, nolock, ConnMode);
        }

        /// <summary>
        /// SQL查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="where">搜索条件 - Sql Where</param>
        /// <param name="orderBy">排序 - Sql Order By</param>
        /// <param name="param">参数（传入的参数必须参数化）</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <returns></returns>
        public static IList<T> GetWhere<T>(string where, string orderBy, object param = null, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.GetWhere<T>(where, orderBy, param, showCols, nolock, ConnMode);
        }

        /// <summary>
        /// SQL查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <typeparam name="NT">映射的实体</typeparam>
        /// <param name="where">搜索条件 - Sql Where</param>
        /// <param name="orderBy">排序 - Sql Order By</param>
        /// <param name="param">参数（传入的参数必须参数化）</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <returns></returns>
        public static IList<NT> GetWhere<T, NT>(string where, string orderBy, object param = null, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.GetWhere<T, NT>(where, orderBy, param, showCols, nolock, ConnMode);
        }

        /// <summary>
        /// SQL分页查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页行数</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <param name="sqlVersion">Sql版本（影响分页性能）</param>
        /// <returns></returns>
        public static MPageData<T> Get<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, int pageIndex, int pageSize, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Get(bizExp, pageIndex, pageSize, showCols, nolock, ConnMode, sqlVersion);
        }

        /// <summary>
        /// SQL分页查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <typeparam name="NT">映射的实体</typeparam>
        /// <param name="bizExp">搜索条件 - Lambda表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页行数</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <param name="sqlVersion">Sql版本（影响分页性能）</param>
        /// <returns></returns>
        public static MPageData<NT> Get<T, NT>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, int pageIndex, int pageSize, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Get<T, NT>(bizExp, pageIndex, pageSize, showCols, nolock, ConnMode, sqlVersion);
        }

        /// <summary>
        /// SQL分页查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="where">搜索条件 - Sql Where</param>
        /// <param name="orderBy">排序 - Sql Order By</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页行数</param>
        /// <param name="param">参数（传入的参数必须参数化）</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <param name="sqlVersion">Sql版本（影响分页性能）</param>
        /// <returns></returns>
        public static MPageData<T> GetWhere<T>(string where, string orderBy, int pageIndex, int pageSize, object param = null, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.GetWhere<T>(where, orderBy, pageIndex, pageSize, param, showCols, nolock, ConnMode, sqlVersion);
        }

        /// <summary>
        /// SQL分页查询 - 按条件查询
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <typeparam name="NT">映射的实体</typeparam>
        /// <param name="where">搜索条件 - Sql Where</param>
        /// <param name="orderBy">排序 - Sql Order By</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页行数</param>
        /// <param name="param">参数（传入的参数必须参数化）</param>
        /// <param name="showCols">需查询的字段，默认为（*）全部</param>
        /// <param name="nolock">是否忽略锁，默认为忽略</param>
        /// <param name="ConnMode">读库/写库（默认为读库）</param>
        /// <param name="sqlVersion">Sql版本（影响分页性能）</param>
        /// <returns></returns>
        public static MPageData<NT> GetWhere<T, NT>(string where, string orderBy, int pageIndex, int pageSize, object param = null, string showCols = "*", EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.GetWhere<T, NT>(where, orderBy, pageIndex, pageSize, param, showCols, nolock, ConnMode, sqlVersion);
        }

        public static IList<MLeftJoin> LeftJoin<TLeft, TRight>(string On, string leftCols = "*", string rightCols = "*", bool nolock = false)
        {
            List<MLeftJoin> list = new List<MLeftJoin>();

            list.Add(DataBase.Instance.LeftJoin<TLeft, TRight>(null, On, leftCols, rightCols, nolock));

            return list;
        }

        public static IList<MLeftJoin> LeftJoin<TLeft, TRight>(this IList<MLeftJoin> list, string On, string rightCols = "*", bool nolock = false)
        {
            list.Add(DataBase.Instance.LeftJoin<TLeft, TRight>(list, On, "*", rightCols, nolock));

            return list;
        }

        public static string GetSql(this IList<MLeftJoin> list, string wheres, string orderBy)
        {
            return DataBase.Instance.GetSql(list, wheres, orderBy);
        }

        public static MGetSql GetSql(this IList<MLeftJoin> list, string wheres, string orderBy, int pageIndex, int pageSize)
        {
            return DataBase.Instance.GetSql(list, wheres, orderBy, pageIndex, pageSize);
        }

        public static IList<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode);
        }

        public static IList<TReturn> Get<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode);
        }

        public static IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode);
        }

        public static IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode);
        }

        public static IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode);
        }

        public static IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new()
            where TSeventh : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode);
        }

        public static MPageData<TReturn> Get<TFirst, TSecond, TReturn>(MGetSql sql, Func<TFirst, TSecond, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode, sqlVersion);
        }

        public static MPageData<TReturn> Get<TFirst, TSecond, TThird, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode, sqlVersion);
        }

        public static MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode, sqlVersion);
        }

        public static MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode, sqlVersion);
        }

        public static MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode, sqlVersion);
        }

        public static MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new()
            where TSeventh : new()
        {
            return DataBase.Instance.Get(sql, map, param, ConnMode, sqlVersion);
        }

        public static IList<T> RunProc<T>(string procName, object param, string dbName)
        {
            return DataBase.Instance.RunProc<T>(procName, param, dbName);
        }

        public static IList<T> RunSql<T>(string sql, object param, string dbName)
        {
            return DataBase.Instance.RunSql<T>(sql, param, dbName);
        }

        public static MPageData<T> RunSql<T>(string sql, string orderBy, object param, int pageIndex, int pageSize, string dbName)
        {
            return DataBase.Instance.RunSql<T>(sql, orderBy, param, pageIndex, pageSize, dbName);
        }

        /// <summary>
        /// SQL批提交
        /// </summary>
        /// <param name="list">工作单元</param>
        /// <param name="isTransactionScope">是否开启跨库事务</param>
        public static int Submit(this IList<MSqlData> list, bool isTransactionScope = false)
        {
            return DataBase.Instance.Submit(list);
        }

        public static decimal Sum<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string SumCol, EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Sum(bizExp, SumCol, nolock, ConnMode);
        }

        public static int Count<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, EConnectionMode ConnMode = EConnectionMode.Read)
        {
            //读库加nolock，写库不加nolock
            bool nolock = (ConnMode == EConnectionMode.Read ? true : false);

            return DataBase.Instance.Count(bizExp, nolock, ConnMode);
        }

        public static IList<MKeyValue> Add(this IList<MKeyValue> list, string Key, object Value)
        {
            list.Add(new MKeyValue() { Key = Key, Value = Value });

            return list;
        }

    }
}
