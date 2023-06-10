using AutoMapper;
using Core.Application.Contracts.Features.Employee.Commands.Create;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Employee.Commands.Create
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly List<string> _validationErrors;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        public CreateEmployeeCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateEmployeeCommandHandler> logger, IMapper mapper, IValidator<CreateEmployeeCommand> validator)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _validationErrors = new List<string>();
        }
        #endregion

        public async Task<Response<int>> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                #region validation check
                var validation = _validator.Validate(command);
                if (!validation.IsValid)
                    return Response<int>.Fail(EmployeeResource.FAILED);
                #endregion

                #region Data mapping
                var employee = _mapper.Map<Domain.Persistence.Entities.Employee>(command);
                #endregion

                await _persistenceUnitOfWork.Employee.AddAsync(employee);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<int>.Success(employee.Id, EmployeeResource.SUCCESS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _validationErrors.Add(ex.Message);
                return Response<int>.Fail(EmployeeResource.FAILED, _validationErrors);
            }
        }
    }
}
