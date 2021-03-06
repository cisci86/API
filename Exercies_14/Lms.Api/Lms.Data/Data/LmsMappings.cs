using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;

namespace Lms.Data.Data
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleCreateDto>().ReverseMap();
            CreateMap<Course, CourseModifyDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Module, ModuleModifyDto>().ReverseMap();
            CreateMap<Course, CoursesWithModulesDto>().ReverseMap();
            CreateMap<Module, CoursesWithModulesDto>().ReverseMap();
        }
    }
}
