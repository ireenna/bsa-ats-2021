using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Domain.Common;
using Domain.Interfaces;
using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Common.Queries
{
    public class GetElasticDocumentsListBySearchRequestQuery<TDto> : IRequest<IEnumerable<TDto>>
        where TDto : Dto
    {
        public string SearchRequest { get; }

        public GetElasticDocumentsListBySearchRequestQuery(string searchRequest)
        {
            SearchRequest = searchRequest;
        }
    }

    public class GetElasticDocumentsListBySearchRequestQueryHandler<TDocument, TDto> : IRequestHandler<GetElasticDocumentsListBySearchRequestQuery<TDto>, IEnumerable<TDto>>
        where TDocument : Entity
        where TDto : Dto
    {
        protected readonly IElasticReadRepository<TDocument> _repository;
        protected readonly IMapper _mapper;

        public GetElasticDocumentsListBySearchRequestQueryHandler(IElasticReadRepository<TDocument> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> Handle(GetElasticDocumentsListBySearchRequestQuery<TDto> query, CancellationToken _)
        {
            IEnumerable<TDocument> result = await _repository.SearchByQuery(query.SearchRequest);

            return _mapper.Map<IEnumerable<TDto>>(result);
        }
    }
}