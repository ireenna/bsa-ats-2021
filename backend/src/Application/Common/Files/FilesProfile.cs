using Application.Common.Files.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Files
{
    public class FilesProfile : Profile
    {
        public FilesProfile()
        {
            CreateMap<FileInfo, CvFileInfoDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.PublicUrl));
        }
    }
}
