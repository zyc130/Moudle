using AutoMapper;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Dto.Stock;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;
using static WMS.Moudle.Entity.Enum.TaskEnum;

namespace WMS.Moudle.Api.Custom
{
    /// <summary>
    /// auto映射
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<SystemDto, sys_dept>().ReverseMap();
            CreateMap<SystemDto, sys_role>().ReverseMap();
            CreateMap<SystemDto, sys_dictionary>().ReverseMap();
            CreateMap<DicDetailDto, sys_dictionary_detail>().ReverseMap();
            CreateMap<UserDto, sys_user>().ReverseMap();
            CreateMap<MenuDto, sys_menu>().ReverseMap();
            CreateMap<MenuTreeDto, sys_menu>().ReverseMap();
            CreateMap<ImportMaterialDto, base_material>().ReverseMap();
            CreateMap<ConfigDto, sys_config>().ReverseMap();
            CreateMap<LocationDto, base_location>().ReverseMap();
            CreateMap<MoudleInDto, task>().ReverseMap();
            CreateMap<TaskDetailDto, base_material>().ReverseMap();
            CreateMap<TaskDetailDto, task_detail>().ReverseMap();
            CreateMap<StockDto, task>().ReverseMap()
                .ForMember(s => s.location_code, t => 
                t.MapFrom(to =>to.is_in_stock==EIsInStock.No.GetHashCode()?to.start_point:
                to.end_point))
                .ForMember(s => s.task_id, t => t.MapFrom(to => to.id));
            CreateMap<StockDto, stock>().ReverseMap();
            CreateMap<TaskDetailDto, stock_detail>().ReverseMap();
            CreateMap<TaskPartDto, task>().ReverseMap();
        }
    }
}
