using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Commands;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Vacancies.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Abstractions;
using MediatR;

namespace Application.Vacancies.Commands.Create
{
    //public class CreateVacancyCommandHandler : CreateEntityCommandHandler<Vacancy, VacancyCreateDto>
    //{
    //    public CreateVacancyCommandHandler(IWriteRepository<Vacancy> repository, IMapper mapper) : base(repository, mapper) { }
    //}
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
        private readonly IMapper _mapper;
        protected readonly ICurrentUserContext _currentUserContext;

        public CreateVacancyCommandHandler(
                IWriteRepository<Vacancy> writeRepository,
                IMapper mapper,
                ICurrentUserContext currentUserContext
            )
        {
            _writeRepository = writeRepository;
            _mapper = mapper;
            _currentUserContext = currentUserContext;
        }

        public async Task<VacancyDto> Handle(CreateVacancyCommand command, CancellationToken _)
        {

            var user = await _currentUserContext.GetCurrentUser();
            if (user is null)
            {
                throw new NotFoundException(typeof(User), "unknown");
            }

            var newVacancy = _mapper.Map<Vacancy>(command.VacancyCreate);

            newVacancy.CompanyId = user.CompanyId;
            newVacancy.ResponsibleHrId = user.Id;
            newVacancy.CreationDate = DateTime.UtcNow;
            newVacancy.DateOfOpening = newVacancy.CreationDate; // Must be changed in future
            newVacancy.ModificationDate = DateTime.UtcNow;


            await _writeRepository.CreateAsync(newVacancy);
            var registeredVacancy = _mapper.Map<VacancyDto>(newVacancy);

            return registeredVacancy;
        }
    }
}
