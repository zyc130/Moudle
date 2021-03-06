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
using WMS.Moudle.Utility;
using static WMS.Moudle.Entity.Enum.CommonEnum;
using static WMS.Moudle.Entity.Enum.TaskEnum;

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

        public bool DeleteByCode(string code)
        {
            return dictionaryDetailDataAccess.Delete<sys_dictionary_detail>(a => a.dic_code == code);
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
            return (true, string.Empty);
        }

        public bool UpdateByCode(sys_dictionary_detail t,string oldDicCode)
        {
            t.update_time = DateTime.Now;
            return dictionaryDetailDataAccess.UpdateColumns(t, a => new { a.dic_code,a.update_id,a.update_time }, w => w.dic_code == oldDicCode);
        }


        #region option

        /// <summary>
        /// 模具入库类型
        /// </summary>
        /// <returns></returns>
        public List<sys_dictionary_detail> GetMoudleInType()
        {
            return FindByCode(EDicCode.task_type.ToString())?
                 .FindAll(a => ConvertHelper.ToInt(a.dic_value) >= ETaskType.MoudleIn.GetHashCode()
                 && ConvertHelper.ToInt(a.dic_value) <= ETaskType.OtherIn.GetHashCode());
        }

        public List<sys_dictionary_detail> GetMoudleType()
        {
            return FindByCode(EDicCode.material_type.ToString())?
                 .FindAll(a => ConvertHelper.ToInt(a.dic_value) >= EMaterialType.Part.GetHashCode()
                 && ConvertHelper.ToInt(a.dic_value) <= EMaterialType.Spare.GetHashCode());
        }

        public List<sys_dictionary_detail> GetPalletType()
        {
            return FindByCode(EDicCode.material_type.ToString())?
                 .FindAll(a => ConvertHelper.ToInt(a.dic_value) >= EMaterialType.PalletMoudle.GetHashCode()
                 && ConvertHelper.ToInt(a.dic_value) <= EMaterialType.PalletSet.GetHashCode());
        }

        #endregion
    }
}
