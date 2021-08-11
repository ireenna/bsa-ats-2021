using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Projects.Dtos;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Read;
using MediatR;

namespace Application.Projects.Commands
{
    public class GetProjectsByCurrentHRCompanyCommand : IRequest<List<ProjectDto>>
    {
        public GetProjectsByCurrentHRCompanyCommand()
        {
        }
    }

    public class GetProjectsByCurrentHRCompanyCommandHandler : IRequestHandler<GetProjectsByCurrentHRCompanyCommand, List<ProjectDto>>
    {
        protected readonly ISender _mediator;
        protected readonly IProjectReadRepository _projectRepository;

        protected readonly ICurrentUserContext _currentUserContext;
        protected readonly IMapper _mapper;

        public GetProjectsByCurrentHRCompanyCommandHandler(ISender mediator, IProjectReadRepository projectRepository,
                                   ICurrentUserContext currentUserContext, IMapper mapper)
        {
            _mediator = mediator;
            _projectRepository = projectRepository;
            _currentUserContext = currentUserContext;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsByCurrentHRCompanyCommand command, CancellationToken _)
        {
            //(await _currentUserContext.GetCurrentUser()).CompanyId
            List<Project> projects = await _projectRepository.GetByCompanyIdAsync("1");
            
            return _mapper.Map<List<ProjectDto>>(projects);
        }
    }
}
