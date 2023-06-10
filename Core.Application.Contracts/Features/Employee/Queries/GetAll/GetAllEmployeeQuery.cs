using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Employee.Queries.GetAll
{
    public class GetAllEmployeeQuery : IRequest<Response<IReadOnlyList<EmployeeVm>>>
    {
    }

    public class EmployeeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
