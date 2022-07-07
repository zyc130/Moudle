using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Moudle.Business.Interface.Base;
using WMS.Moudle.DataAccess.Interface.Base;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Models;

namespace WMS.Moudle.Business.Serveice.Base
{
    internal class LocationBusiness : ILocationBusiness
    {
        ILocationDataAccess locationDataAccess;
        IMapper mapper;
        public LocationBusiness(
               ILocationDataAccess _locationDataAccess
            , IMapper _mapper)
        {
            locationDataAccess = _locationDataAccess;
            mapper=_mapper;
        }

        /// <summary>
        /// 初始化货位
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (bool,string) Init(List<LocationInitDto> ts,long createId)
        {
            var all = FindAll();
            if (all?.Count>0)
            {
                return (false, "数据库已有货位!");
            }

            if (ts.Select(a=>a.RowIndex).Distinct().Count()!=ts.Count)
            {
                return (false, "行号不能重复!");
            }
            List<base_location> items = new();
            //生成货位
            ts?.ForEach(row =>
            {
                for (int c = 1; c <= row.ColumnCount; c++)
                {
                    for (int f = 1; f <= row.FloorCount; f++)
                    {
                        items.Add(new base_location()
                        {
                            roadway_no =row.RowIndex>2?2:1,
                            row_no=row.RowIndex,
                            column_no = c,
                            floor_no =f,
                            location_code = $"{row.RowIndex}-{c}-{f}",
                            location_name = $"{c}列{f}层",
                            location_type=0,
                            state=1,
                            create_time=DateTime.Now,
                            update_time=DateTime.Now,
                            remark=string.Empty,
                            create_id= createId,
                            update_id= createId
                        });
                    }
                }
            });

            if (ts?.Count>0)
            {
                if (locationDataAccess.Insert(items) >0)
                {
                    return (true, $"初始化成功，共计:{items.Count}个货位!");
                }
            }
            return (false, $"初始化失败!");
        }

        public List<LocationShowDto> QueryAll()
        {
            List<LocationShowDto> data = new();
           var items = FindAll();
            items?.Select(a => a.row_no).Distinct().OrderBy(a =>a).ToList().ForEach(a =>
            {
                data.Add(new()
                {
                    RowNo=a,
                    Name=$"{a}行",
                    DataList = mapper.Map<List<LocationDto>>(items.FindAll(b=>b.row_no==a))
                });
            });
            return data;
        }

        public LocationShowDto QueryByRowNo(int rowNo)
        {
            var items = FindAll();
            LocationShowDto data = new LocationShowDto()
            {
                RowNo = rowNo,
                Name = $"{rowNo}行",
                DataList = mapper.Map<List<LocationDto>>(items.FindAll(b => b.row_no == rowNo))
            };
           return data;
        }

        /// <summary>
        /// 获取货架行数
        /// </summary>
        /// <returns></returns>
        public List<int> QueryRows()
        {
            return locationDataAccess.Query<base_location>(null)
                .Select(a => a.row_no).Distinct().OrderBy("row_no").ToList();
        }

        private List<base_location> FindAll()
        {
            return locationDataAccess.FindAll<base_location>();
        }
    }
}
