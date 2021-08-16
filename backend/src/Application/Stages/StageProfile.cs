using System.Linq;
using AutoMapper;
using Domain.Entities;
using Application.Stages.Dtos;

namespace Application.Stages
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            CreateMap<Stage, StageWithCandidatesDto>()
                .ForMember(
                    dto => dto.Candidates,
                    opt => opt.MapFrom(s =>
                        s.CandidateToStages.Select(cts => cts.Candidate))
                );
            CreateMap<StageCreateDto, Stage>();
            CreateMap<StageUpdateDto, Stage>();
            CreateMap<Stage, StageDto>();
            CreateMap<Action, ActionDto>();
            CreateMap<ActionDto,Action>();
        }
    }
}
