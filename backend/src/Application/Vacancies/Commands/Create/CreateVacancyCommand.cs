using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Commands;
using Application.Interfaces;
using Application.Stages.Commands;
using Application.Vacancies.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Abstractions;
using MediatR;

namespace Application.Vacancies.Commands.Create
{
    public class CreateVacancyCommand : IRequest<VacancyDto>
    {
        public VacancyCreateDto VacancyCreate { get; set; }

        public CreateVacancyCommand(VacancyCreateDto vacancyCreate)
        {
            VacancyCreate = vacancyCreate;
        }
    }

        public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, VacancyDto>
        {
            private readonly IWriteRepository<Vacancy> _writeRepository;
            private readonly IWriteRepository<Stage> _writeStageRepository;
            private readonly IMapper _mapper;
            private readonly ISender _mediator;
        private readonly ICurrentUserContext _currUser;

        public CreateVacancyCommandHandler(
                IWriteRepository<Vacancy> writeRepository,
                IWriteRepository<Stage> writeStageRepository,
                IMapper mapper, ISender mediator, ICurrentUserContext currUser
            )
            {
                _writeStageRepository = writeStageRepository;
                _writeRepository = writeRepository;
                _mapper = mapper;
                _mediator = mediator;
            _currUser = currUser;
            }

            public async Task<VacancyDto> Handle(CreateVacancyCommand command, CancellationToken _)
            {
                var user = await _currUser.GetCurrentUser();
                if (user is null)
                {
                    throw new Exception("user not found");
                }

                command.VacancyCreate.ResponsibleHrId = user.Id;
                command.VacancyCreate.CompanyId = user.CompanyId;

                var newVacancy = _mapper.Map<Vacancy>(command.VacancyCreate);
                await _writeRepository.CreateAsync(newVacancy);

                //command.VacancyCreate.Stages.ToList().ForEach(async x=>await _mediator.Send(new CreateVacancyStageCommand(x, newVacancy.Id)) );
                
                var registeredVacancy = _mapper.Map<VacancyDto>(newVacancy);

                return registeredVacancy;
            }
        }
    }
