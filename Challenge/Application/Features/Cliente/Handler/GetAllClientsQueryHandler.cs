using Application.Features.Cliente.Queries;
using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handlers
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, TResponse<IEnumerable<GetAllClientsResponse>>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<TResponse<IEnumerable<GetAllClientsResponse>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var (list, totalCount) = await _clienteRepository.GetAllPaginatedAsync(request.Page, request.PageSize, request.TextToSearch);

                var response = new TResponse<IEnumerable<GetAllClientsResponse>>
                {
                    Data = _mapper.Map<IEnumerable<GetAllClientsResponse>>(list),
                    Messages = new Messages(),
                    Pagination = new Pagination
                    {
                        TotalRecords = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                };

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
