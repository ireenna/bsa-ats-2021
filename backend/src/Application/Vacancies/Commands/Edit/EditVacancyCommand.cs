using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Commands;
using Application.Common.Exceptions;
using Application.Stages.Commands;
using Application.Vacancies.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Abstractions;
using MediatR;

namespace Application.Vacancies.Commands.Edit
{
    public class EditVacancyCommand : IRequest<VacancyDto>
    {
        public VacancyUpdateDto VacancyUpdate { get; set; }
        public string Id { get; set; }

        public EditVacancyCommand(VacancyUpdateDto vacancyUpdate, string id)
        {
            VacancyUpdate = vacancyUpdate;
            Id = id;
        }
    }

    public class EditVacancyCommandHandler : IRequestHandler<EditVacancyCommand, VacancyDto>
    {
        private readonly IWriteRepository<Vacancy> _writeRepository;
        private readonly IReadRepository<Vacancy> _readRepository;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public EditVacancyCommandHandler(
            IWriteRepository<Vacancy> writeRepository,
            IReadRepository<Vacancy> readRepository,
            IMapper mapper, ISender mediator
        )
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<VacancyDto> Handle(EditVacancyCommand command, CancellationToken _)
        {
            var updatableVacancy = _mapper.Map<VacancyDto>(command.VacancyUpdate);
            var query = new UpdateEntityCommand<VacancyDto>(updatableVacancy);
            var updatedVacancy = await _mediator.Send(query);

            return updatedVacancy;
        }
    }
}
