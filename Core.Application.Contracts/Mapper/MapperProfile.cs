using AutoMapper;
using Core.Application.Contracts.Features.Employee.Commands.Create;
using Core.Application.Contracts.Features.Employee.Queries.GetAll;
using Core.Domain.Persistence.Entities;

namespace Core.Application.Contracts.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, EmployeeVm>().ReverseMap();
        }
    }
}
