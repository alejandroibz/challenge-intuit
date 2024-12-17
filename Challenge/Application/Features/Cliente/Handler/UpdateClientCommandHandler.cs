using Application.Features.Cliente.Commands;
using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, TResponse<ModifyClientResponse>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<TResponse<ModifyClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientToModify = await _clienteRepository.GetByIdAsync(request.ClientId);

                if (clientToModify == null)
                {
                    return new TResponse<ModifyClientResponse>
                    {
                        Success = false,
                        Data = null,
                        Messages = new Messages
                        {
                            StatusCode = 404,
                            Message = "No existe un cliente con este Id"
                        }
                    };
                }

                _mapper.Map(request, clientToModify);

                await _clienteRepository.UpdateAsync(clientToModify);

                return new TResponse<ModifyClientResponse>
                {
                    Data = new ModifyClientResponse { ClientId = clientToModify.Id },
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
