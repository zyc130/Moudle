using AutoMapper;
using WMS.Moudle.Entity.Dto.Base;
using WMS.Moudle.Entity.Dto.System;
using WMS.Moudle.Entity.Dto.Task;
using WMS.Moudle.Entity.Models;

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
            CreateMap<base_material, TaskDetailDto>().ReverseMap();
            CreateMap<task_detail, TaskDetailDto>().ReverseMap();
        }
    }
}
