using AutoMapper;
using Domain.Entities;
using Application.Vacancies.Dtos;
using Domain.Enums;
using System.Linq;
using System;

namespace Application.Vacancies
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<VacancyCreateDto, Vacancy>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => VacancyStatus.Active))
                .ForMember(dest=>dest.CreationDate, opt=> opt.MapFrom(x=>DateTime.Now))
                .ForMember(dest=>dest.Stages, opt=>opt.Ignore());
            CreateMap<Vacancy, VacancyCreateDto>();

            CreateMap<VacancyUpdateDto, Vacancy>()
                .ForMember(dest=>dest.ModificationDate, opt=> opt.MapFrom(x=>DateTime.Now));
            CreateMap<Vacancy, VacancyUpdateDto>();

            CreateMap<Vacancy, VacancyDto>();

            CreateMap<Vacancy, VacancyTableDto>();


            CreateMap<Vacancy, ShortVacancyWithStagesDto>();


            CreateMap<Vacancy, VacancyTableDto>();

        }
    }
}
