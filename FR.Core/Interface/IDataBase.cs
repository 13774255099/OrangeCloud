using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;

namespace FR.Core
{
    public abstract class IDataBase
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">插入的对象</param>
        /// <param name="IsIncrement">ID是否为自增</param>
        /// <param name="IDName">ID名称，Null=约定的ID名称</param>
        /// <returns></returns>
        public abstract int Insert<T>(T t, bool IsIncrement = false, bool IsSplitTable = false, MSplitTableConfig config = null, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract long Insert64<T>(T t, bool IsIncrement = false, bool IsSplitTable = false, MSplitTableConfig config = null, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract void InsertList<T>(IList<T> listT, bool IsSplitTable = false, MSplitTableConfig config = null, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract MSqlData Insert<T>(IList<MSqlData> listSqlData, T t, bool IsIncrement = false, bool IsSplitTable = false, MSplitTableConfig config = null);

        public abstract MSqlData Insert64<T>(IList<MSqlData> listSqlData, T t, bool IsIncrement = false, bool IsSplitTable = false, MSplitTableConfig config = null);

        /// <summary>
        /// 添加数据（事务）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">插入的对象</param>
        /// <param name="IsIncrement">ID是否为自增</param>
        /// <param name="ParaName">参数名称，默认为null，例如@id</param>
        /// <param name="IDName">ID名称，Null=约定的ID名称</param>
        /// <returns></returns>
        //public abstract MTrans InsertByTrans<T>(T t, bool IsIncrement = false, string ParaName = null, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">修改的对象</param>
        /// <param name="IDName">ID名称，Null=约定的ID名称</param>
        /// <returns></returns>
        public abstract int Update<T>(T t, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract int Update<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract MSqlData Update<T>(IList<MSqlData> listSqlData, T t);

        public abstract MSqlData Update<T>(IList<MSqlData> listSqlData, T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp);

        /// <summary>
        /// 修改数据（事务）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">修改的对象</param>
        /// <param name="IDName">ID名称，Null=约定的ID名称</param>
        /// <returns></returns>
        //public abstract MTrans UpdateByTrans<T>(T t, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);

        //public abstract MTrans UpdateByTrans<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract int LogicalDelete<T>(T t, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract int LogicalDelete<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract MSqlData LogicalDelete<T>(IList<MSqlData> listSqlData, T t);

        public abstract MSqlData LogicalDelete<T>(IList<MSqlData> listSqlData, T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp);

        //public abstract MTrans LogicalDeleteByTrans<T>(T t, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);

        //public abstract MTrans LogicalDeleteByTrans<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">删除的对象</param>
        /// <param name="IDName">ID名称，Null=约定的ID名称</param>
        /// <returns></returns>
        public abstract int Delete<T>(T t, EConnectionMode ConnMode = EConnectionMode.Write);

        /// <summary>
        /// 删除数据 - 根据条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="bizExp"></param>
        /// <param name="IDName"></param>
        /// <returns></returns>
        public abstract int Delete<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, EConnectionMode ConnMode = EConnectionMode.Write);

        public abstract MSqlData Delete<T>(IList<MSqlData> listSqlData, T t);

        public abstract MSqlData Delete<T>(IList<MSqlData> listSqlData, T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp);

        /// <summary>
        /// 删除数据（事务）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">删除的对象</param>
        /// <param name="IDName">ID名称，Null=约定的ID名称</param>
        /// <returns></returns>
        //public abstract MTrans DeleteByTrans<T>(T t, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);

        //public abstract MTrans DeleteByTrans<T>(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string IDName = null, EConnectionMode ConnMode = EConnectionMode.Write);


        /// <summary>
        /// 查询单个实体 - 通过ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="showCols"></param>
        /// <returns></returns>
        public abstract T Get<T>(string id, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        public abstract NT Get<T, NT>(string id, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        /// <summary>
        /// 查询List - 通过查询条件(兰姆达表达式)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bizExp"></param>
        /// <param name="showCols"></param>
        /// <returns></returns>
        public abstract IList<T> Get<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        public abstract IList<NT> Get<T, NT>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        /// <summary>
        /// 查询List - 通过查询条件（sql语句）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="showCols"></param>
        /// <param name="nolock"></param>
        /// <returns></returns>
        public abstract IList<T> GetWhere<T>(string where, string orderBy = "", object param = null, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        public abstract IList<NT> GetWhere<T, NT>(string where, string orderBy, object param = null, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        /// <summary>
        /// 查询分页List - 通过查询条件(兰姆达表达式)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bizExp"></param>
        /// <param name="showCols"></param>
        /// <returns></returns>
        public abstract MPageData<T> Get<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, int pageIndex, int pageSize, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012);

        public abstract MPageData<NT> Get<T, NT>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, int pageIndex, int pageSize, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012);

        /// <summary>
        /// 查询分页List - 通过查询条件（sql语句）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="showCols"></param>
        /// <param name="nolock"></param>
        /// <returns></returns>
        public abstract MPageData<T> GetWhere<T>(string where, string orderBy, int pageIndex, int pageSize, object param = null, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012);

        public abstract MPageData<NT> GetWhere<T, NT>(string where, string orderBy, int pageIndex, int pageSize, object param = null, string showCols = "*", bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012);

        public abstract MLeftJoin LeftJoin<TLeft, TRight>(IList<MLeftJoin> list, string On, string leftCols = "*", string rightCols = "*", bool nolock = true);

        public abstract string GetSql(IList<MLeftJoin> list, string wheres, string orderBy);

        public abstract MGetSql GetSql(IList<MLeftJoin> list, string wheres, string orderBy, int pageIndex, int pageSize);

        public abstract IList<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new();

        public abstract IList<TReturn> Get<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new();

        public abstract IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new();

        public abstract IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new();

        public abstract IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new();

        public abstract IList<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new()
            where TSeventh : new();

        public abstract MPageData<TReturn> Get<TFirst, TSecond, TReturn>(MGetSql sql, Func<TFirst, TSecond, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new();

        public abstract MPageData<TReturn> Get<TFirst, TSecond, TThird, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new();

        public abstract MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new();

        public abstract MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new();

        public abstract MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new();

        public abstract MPageData<TReturn> Get<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(MGetSql sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, EConnectionMode ConnMode = EConnectionMode.Read, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012)
            where TSecond : new()
            where TThird : new()
            where TFourth : new()
            where TFifth : new()
            where TSixth : new()
            where TSeventh : new();

        public abstract TransactionScope TransMaster(System.Transactions.IsolationLevel isolationLevel = System.Transactions.IsolationLevel.Serializable);

        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        //public abstract bool ExecuteWithTrans(List<MTrans> sqlList);

        public abstract IList<T> RunProc<T>(string procName, object param, string dbName);

        public abstract IList<T> RunSql<T>(string sql, object param, string dbName);

        public abstract MPageData<T> RunSql<T>(string sql, string orderBy, object param, int pageIndex, int pageSize, string dbName, ESqlVersion sqlVersion = ESqlVersion.SqlServer2012);

        public abstract int Submit(IList<MSqlData> list, bool isTransactionScope = false);

        public abstract decimal Sum<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, string SumCol, bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);

        public abstract int Count<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp, bool nolock = true, EConnectionMode ConnMode = EConnectionMode.Read);
    }
}
