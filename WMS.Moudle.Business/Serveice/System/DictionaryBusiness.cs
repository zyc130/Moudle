using SqlSugar;
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
    internal class DictionaryBusiness : IDictionaryBusiness
    {
        IDictionaryDataAccess dictionaryDataAccess;
        public DictionaryBusiness(IDictionaryDataAccess _dictionaryDataAccess)
        {
            dictionaryDataAccess = _dictionaryDataAccess;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool, string, sys_dictionary?) Add(sys_dictionary t)
        {
            //code是否已存在
            var items = FindAll();
            if (items?.Exists(a=>a.code==t.code)??false)
            {
                return (false, $"字典类型:{t.code},已存在！", null);
            }
            t.create_time = DateTime.Now;
            t.update_time = DateTime.Now;
            return (true, string.Empty, dictionaryDataAccess.Insert(t));
        }

        public bool Delete(long id)
        {
            //需删除明细
            return dictionaryDataAccess.Delete<sys_dictionary>(id);
        }

        public sys_dictionary Find(long id)
        {
            return dictionaryDataAccess.Find<sys_dictionary>(id);
        }

        public List<sys_dictionary> FindAll()
        {
            return dictionaryDataAccess.FindAll<sys_dictionary>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageData<sys_dictionary> QueryPage(SystemPageDto page)
        {
            Expressionable<sys_dictionary> express = new Expressionable<sys_dictionary>();
            express.AndIF(!string.IsNullOrWhiteSpace(page.code), a => a.code.Contains(page.code))
                .AndIF(!string.IsNullOrWhiteSpace(page.name), a => a.name.Contains(page.name))
                .AndIF(page.state!=null, a => a.state==page.state);
            return dictionaryDataAccess.QueryPage(page.pageIndex, page.pageSize, express.ToExpression(), a => a.update_time, false);
        }

        public (bool,string) Update(sys_dictionary t)
        { 
            //code是否已存在
            var items = FindAll();
            if (items?.Exists(a => a.code == t.code && a.id!=t.id) ?? false)
            {
                return (false, $"字典类型:{t.code},已存在!");
            }
            t.update_time = DateTime.Now;
            if (!dictionaryDataAccess.UpdateIgnore(t, a => new { a.create_id, a.create_time }))
            {
                return (false, "修改失败!");
            }
            return (true, "");
        }
    }
}
