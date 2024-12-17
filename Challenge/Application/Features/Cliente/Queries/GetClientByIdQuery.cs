using Application.Shared;
using MediatR;

namespace Application.Features.Cliente.Queries
{
    public class GetClientByIdQuery : IRequest<TResponse<GetClientByIdResponse>>
    {
        public int Id { get; set; }
    }
    public class GetClientByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string CUIT { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
