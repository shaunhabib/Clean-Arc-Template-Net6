using Core.Application.Contracts.Features.Employee.Commands.Create;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EmployeeController : BaseApiController
    {

        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
