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
using WMS.Moudle.Utility.Interface;

namespace WMS.Moudle.Business.Serveice.System
{
    internal class DictionaryBusiness : IDictionaryBusiness
    {
        IDictionaryDataAccess dictionaryDataAccess;
        IDictionaryDetailBusiness dictionaryDetailBusiness;
        IExcuteHelper excuteHelper;
        public DictionaryBusiness(
            IDictionaryDataAccess _dictionaryDataAccess
            , IDictionaryDetailBusiness _dictionaryDetailBusiness
            , IExcuteHelper _excuteHelper)
        {
            dictionaryDataAccess = _dictionaryDataAccess;
            dictionaryDetailBusiness = _dictionaryDetailBusiness;
            excuteHelper = _excuteHelper;
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

        public (bool,string) Delete(long id)
        {
            var dic = Find(id);
            if (dic==null)
            {
                return (false, "对象不存在!");
            }
            //需删除明细
           return excuteHelper.Tran(() =>
            {
                //是否存在明细
                if (dictionaryDetailBusiness.FindByCode(dic.code)?.Count>0)
                {
                    //执行明细删除
                    if (!dictionaryDetailBusiness.DeleteByCode(dic.code))
                    {
                        return (false, "删除明细失败!");
                    }
                }
                if (!dictionaryDataAccess.Delete<sys_dictionary>(id))
                {
                    return (false, "删除失败!");
                }
                return (true,string.Empty);
            });
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public (bool,string) Update(sys_dictionary t)
        {
            var dic = Find(t.id);
            if (dic != null)
            {
                return (false, "对象不存在!");
            }
            //code是否已存在
            var items = FindAll();
            if (items?.Exists(a => a.code == t.code && a.id!=t.id) ?? false)
            {
                return (false, $"字典类型:{t.code},已存在!");
            }
            t.update_time = DateTime.Now;

            return excuteHelper.Tran(() =>
            {
                if (dic.code!=t.code)
                {
                    //是否存在明细
                    if (dictionaryDetailBusiness.FindByCode(dic.code)?.Count > 0)
                    {
                        var detail = new sys_dictionary_detail()
                        {
                            dic_code = dic.code,
                            update_id = dic.update_id
                        };
                        //执行明细更新
                        if (!dictionaryDetailBusiness.UpdateByCode(detail))
                        {
                            return (false, "修改明细失败!");
                        }
                    }
                }

                if (!dictionaryDataAccess.UpdateIgnore(t, a => new { a.create_id, a.create_time }))
                {
                    return (false, "修改失败!");
                }
                return (true, "");
            });
        }
    }
}
