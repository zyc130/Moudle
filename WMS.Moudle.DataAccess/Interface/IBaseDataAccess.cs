using WMS.Moudle.Entity;
using SqlSugar;
using System.Linq.Expressions;

namespace WMS.Moudle.DataAccess.Interface
{
    /// <summary>
    /// base dal interface
    /// </summary>
    public interface IBaseDataAccess : IDisposable
    {
        #region 查询

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find<T>(object id) where T : class, new();

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> FindAll<T>() where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcFilter"></param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcFilter) where T : class, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="funcFilter">查询条件</param>
        /// <param name="orderBy">排序属性</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        PageData<T> QueryPage<T>(int pageIndex, int pageSize, Expression<Func<T, bool>> funcFilter, Expression<Func<T, object>> orderBy, bool isAsc = true) where T : class;

        #endregion

        #region add

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T Insert<T>(T t) where T : class, new();

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        int Insert<T>(List<T> ts) where T : class, new();

        #endregion

        #region update

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update<T>(T t) where T : class, new();

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        bool Update<T>(List<T> ts) where T : class, new();

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        bool UpdateIgnore<T>(T t, Expression<Func<T, object>> columns) where T : class, new();

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        bool UpdateColumns<T>(T t, Expression<Func<T, object>> columns, Expression<Func<T, bool>> where) where T : class, new();

        #endregion

        #region delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Delete<T>(T t) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete<T>(long id) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete<T>(Expression<Func<T, bool>> funcFilter) where T : class, new();

        #endregion

        #region other

        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ISugarQueryable<T> QuerySql<T>(string sql) where T : class, new();

        #endregion
    }
}
