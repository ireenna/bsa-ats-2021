﻿
using System.Collections.Generic;
using Application.Common.Models;
using Application.VacancyCandidates.Dtos;
using Domain.Entities;
using Domain.Enums;


namespace Application.Stages.Dtos
{

    public class StageDto : Dto
    {
        public string Name { get; set; }
        public StageType Type { get; set; }
        public int Index { get; set; }
        public bool IsReviewable { get; set; }
        public string VacancyId { get; set; }
        public ICollection<ActionDto> Actions { get; set; }
        public ICollection<ShortVacancyCandidateWithApplicantDto> CandidateToStages { get; set; }
    }

}