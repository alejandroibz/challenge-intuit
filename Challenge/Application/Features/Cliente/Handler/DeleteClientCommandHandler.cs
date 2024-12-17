using Application.Features.Cliente.Commands;
using Application.Shared;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handlers
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, TResponse<ModifyClientResponse>>
    {
        private readonly IClienteRepository _clienteRepository;

        public DeleteClientCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<TResponse<ModifyClientResponse>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {            
            try
            {
                var existingClient = await _clienteRepository.GetByIdAsync(request.ClientId);

                if (existingClient == null)
                {
                    return new TResponse<ModifyClientResponse>
                    {
                        Success = false,
                        Data = null,
                        Messages = new Messages
                        {
                            StatusCode = 404,
                            Message = $"Client with ID {request.ClientId} not found."
                        }
                    };
                }

                await _clienteRepository.DeleteAsync(existingClient);

                return new TResponse<ModifyClientResponse>
                {
                    Success = true,
                    Data = new ModifyClientResponse { ClientId = request.ClientId },
                    Messages = new Messages
                    {
                        StatusCode = 200,
                        Message = "Client deleted successfully."
                    }
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
