using Application.Features.Cliente.Queries;
using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handlers
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, TResponse<GetClientByIdResponse>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<TResponse<GetClientByIdResponse>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clienteRepository.GetByIdAsync(request.Id);

                if (client == null)
                {
                    return new TResponse<GetClientByIdResponse>
                    {
                        Success = false,
                        Messages = new Messages
                        {
                            StatusCode = 404,
                            Message = $"Client with ID {request.Id} not found."
                        }
                    };
                }

                var clientDto = _mapper.Map<GetClientByIdResponse>(client);

                return new TResponse<GetClientByIdResponse>
                {
                    Data = clientDto,
                    Messages = new Messages()
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
