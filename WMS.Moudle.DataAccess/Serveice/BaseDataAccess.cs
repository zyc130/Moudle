using WMS.Moudle.Entity;
using SqlSugar;
using System.Linq.Expressions;
using WMS.Moudle.DataAccess.Interface;
using WMS.Moudle.Utility.Extend;

namespace WMS.Moudle.DataAccess.Serveice
{
    internal class BaseDataAccess : IBaseDataAccess
    {
        protected ISqlSugarClient _client;

        private static string create_time= "create_time";
        private static string update_time = "update_time";
        public BaseDataAccess(ISqlSugarClient client)
        {
            _client = client;
        }

        #region delete

        /// <summary>
        /// 根据对象删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Delete<T>(T t) where T : class, new()
        {
            return _client.Deleteable<T>(t).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete<T>(long id) where T : class, new()
        {
            return _client.Deleteable<T>(id).ExecuteCommand() > 0;
        }


        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> funcFilter) where T : class, new()
        {
            return _client.Deleteable<T>(funcFilter).ExecuteCommand() > 0;
        }

        #endregion

        #region query

        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(object id) where T : class, new()
        {
            return _client.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> FindAll<T>() where T : class, new()
        {
            return _client.Queryable<T>().ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcFilter"></param>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>>? funcFilter) where T : class, new()
        {
            return _client.Queryable<T>().WhereIF(funcFilter!=null,funcFilter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ISugarQueryable<T> QuerySql<T>(string sql) where T : class, new()
        {
            return _client.SqlQueryable<T>(sql);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="funcFilter"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public PageData<T> QueryPage<T>(int pageIndex, int pageSize, Expression<Func<T, bool>> funcFilter, Expression<Func<T, object>> orderBy, bool isAsc = true) where T : class
        {
            
            var items = _client.Queryable<T>()
                .WhereIF(funcFilter != null, funcFilter)
                .OrderByIF(orderBy != null, orderBy, isAsc ? OrderByType.Asc : OrderByType.Desc);
            return new PageData<T>()
            {
                DataList = items.ToPageList(pageIndex, pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = items.Count()
            };
        }

        #endregion

        #region insert

        /// <summary>
        /// insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Insert<T>(T t) where T : class, new()
        {
            t.SetNowTime(new List<string>() { create_time,update_time });
            return _client.Insertable(t).ExecuteReturnEntity();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public int Insert<T>(List<T> ts) where T : class, new()
        {
            ts.SetNowTime(new List<string>() { create_time, update_time });
            return _client.Insertable(ts).ExecuteCommand();
        }

        #endregion

        #region update 

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t) where T : class, new()
        {
            t.SetNowTime(new List<string>() { update_time });
            return _client.Updateable<T>(t).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 修改(过滤不参与更新属性)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool UpdateIgnore<T>(T t, Expression<Func<T, object>> columns) where T :class,new()
        {
            t.SetNowTime(new List<string>() { update_time });
            return _client.Updateable(t)
                .IgnoreColumnsIF(columns!=null,columns)
                .ExecuteCommandHasChange();
        }

        /// <summary>
        /// 修改(指定属性)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateColumns<T>(T t, Expression<Func<T, object>> columns, Expression<Func<T, bool>> where) where T : class, new()
        {
            t.SetNowTime(new List<string>() { update_time });
            return _client.Updateable(t)
                .UpdateColumnsIF(columns!=null,columns)
                .Where(where)
                .ExecuteCommandHasChange();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool Update<T>(List<T> ts) where T : class, new()
        {
            ts.SetNowTime(new List<string>() { update_time });
            return _client.Updateable<T>(ts).ExecuteCommandHasChange();
        }

        #endregion

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
