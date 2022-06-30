using AutoMapper;
using WMS.Moudle.Entity.Dto.System;
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
            CreateMap<UserDto, sys_user>().ReverseMap();
            CreateMap<MenuDto, sys_menu>().ReverseMap();
            CreateMap<MenuTreeDto, sys_menu>().ReverseMap();
        }
    }
}
