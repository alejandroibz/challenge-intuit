using Application.Features.Cliente.Commands;
using Application.Shared;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handler
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, TResponse<ModifyClientResponse>>
    {
        private readonly IClienteRepository _clienteRepository;

        public CreateClientCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<TResponse<ModifyClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = new Domain.Entities.Cliente
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    Cellphone = request.Cellphone,
                    CUIT = request.CUIT,
                    Address = request.Address,
                    Birthdate = request.Birthdate
                };

                await _clienteRepository.AddAsync(client);

                return new TResponse<ModifyClientResponse>
                {
                    Data = new ModifyClientResponse { ClientId = client.Id },
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
