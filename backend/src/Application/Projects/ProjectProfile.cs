using Application.Projects.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Projects.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Projects
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

            CreateMap<Project, ProjectGetDto>();
        }
    }
}
