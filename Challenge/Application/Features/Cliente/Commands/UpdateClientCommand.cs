using Application.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cliente.Commands
{
    public class UpdateClientCommand : IRequest<TResponse<ModifyClientResponse>>
    {
        [Required]
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        public string? CUIT { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
