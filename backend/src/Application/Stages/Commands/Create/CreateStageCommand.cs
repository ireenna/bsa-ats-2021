using Application.Stages.Dtos;
using Application.Stages.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Abstractions;
using Domain.Interfaces.Read;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stages.Commands.Create
{
    public class CreateStageCommand : IRequest<StageDto>
    {
        public StageCreateDto StageCreate { get; }

        public CreateStageCommand(StageCreateDto stageCreate)
        {
            StageCreate = stageCreate;
        }
    }

    public class CreateStageCommandHandler : IRequestHandler<CreateStageCommand, StageDto>
    {
        private readonly IWriteRepository<Stage> _writeRepository;
        private readonly IStageReadRepository _readRepository;

        private readonly IMapper _mapper;

        public CreateStageCommandHandler(
                IWriteRepository<Stage> writeRepository,
                IStageReadRepository readRepository,
                IMapper mapper
            )
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<StageDto> Handle(CreateStageCommand command, CancellationToken _)
        {
            var newStage = _mapper.Map<Stage>(command.StageCreate);

            if ((await _readRepository
                .GetByVacancyAsync(command.StageCreate.VacancyId))
                .Stages
                .Any(x => x.Index == command.StageCreate.Index))
            {
                throw new StageWithThisIndexAlreadyExist();
            }
            if ((await _readRepository
                .GetByVacancyAsync(command.StageCreate.VacancyId))
                .Stages
                .Any(x => x.Index == command.StageCreate.Type))
            {
                throw new StageWithThisTypeAlreadyExist();
            }
            await _writeRepository.CreateAsync(newStage);
            var registeredStage = _mapper.Map<StageDto>(newStage);

            return registeredStage;
        }
    }
}
