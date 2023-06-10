using Core.Application.Contracts.Features.Employee.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Validation.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                                .Length(2, 10);

            RuleFor(c => c.Email).NotEmpty();
                
        }
    }
}
