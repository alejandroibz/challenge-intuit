using Application.Shared;
using MediatR;

namespace Application.Features.Cliente.Queries
{
    public class GetAllClientsQuery : IRequest<TResponse<IEnumerable<GetAllClientsResponse>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? TextToSearch { get; set; }
    }
    public class GetAllClientsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
    }

}
