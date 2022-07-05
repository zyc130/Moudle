using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.System;
using WMS.Moudle.DataAccess.Interface.System;
using WMS.Moudle.Entity;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.System
{
    /// <summary>
    /// 字典明细
    /// </summary>
    internal class DictionaryDetailBusiness : IDictionaryDetailBusiness
    {
        IDictionaryDetailDataAccess dictionaryDetailDataAccess;
        public DictionaryDetailBusiness(IDictionaryDetailDataAccess _dictionaryDetailDataAccess)
        {
            dictionaryDetailDataAccess = _dictionaryDetailDataAccess;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string, sys_dictionary_detail?) Add(sys_dictionary_detail t)
        {
            //value是否已存在
            var items = FindByCode(t.dic_code);
            if (items?.Exists(a => a.dic_value == t.dic_value) ?? false)
            {
                return (false, $"字典值:{t.dic_value},已存在！", null);
            }
            t.create_time = DateTime.Now;
            t.update_time = DateTime.Now;
            return (true, string.Empty, dictionaryDetailDataAccess.Insert(t));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            return dictionaryDetailDataAccess.Delete<sys_dictionary_detail>(id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public sys_dictionary_detail Find(long id)
        {
            return dictionaryDetailDataAccess.Find<sys_dictionary_detail>(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="code">类型编码</param>
        /// <returns></returns>
        public List<sys_dictionary_detail> FindByCode(string code)
        {
            return dictionaryDetailDataAccess.Query<sys_dictionary_detail>(a => a.dic_code == code)?.OrderBy(o=>o.dic_sort).ToList();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool,string) Update(sys_dictionary_detail t)
        {
            //value是否已存在
            var items = FindByCode(t.dic_code);
            if (items?.Exists(a => a.dic_value == t.dic_value && t.id!=a.id) ?? false)
            {
                return (false, $"字典值:{t.dic_value},已存在！");
            }
            t.update_time = DateTime.Now;
            if (!dictionaryDetailDataAccess.UpdateIgnore(t, a => new { a.create_id, a.create_time, a.dic_code }))
            {
                return (false, $"修改失败!");
            }
            return (true, $"修改失败!");
        }
    }
}
