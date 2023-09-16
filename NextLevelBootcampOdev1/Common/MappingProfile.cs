using AutoMapper;
using NextLevelBootcampOdev1.Models;
using NextLevelBootcampOdev1.ViewModel;

namespace NextLevelBootcampOdev1.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddEmployeeModel, Employee>();
        }
    }
}
