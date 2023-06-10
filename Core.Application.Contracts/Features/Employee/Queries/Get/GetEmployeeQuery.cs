using Core.Application.Contracts.Features.Employee.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Employee.Queries.Get
{
    public class GetEmployeeQuery : IRequest<Response<EmployeeVm>>
    {
        public int Id { get; set; }
    }
}
