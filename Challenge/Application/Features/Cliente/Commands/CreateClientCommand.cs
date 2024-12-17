using Application.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cliente.Commands
{
    public class CreateClientCommand : IRequest<TResponse<ModifyClientResponse>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Cellphone { get; set; }
        [Required]
        public string CUIT { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class ModifyClientResponse
    {
        public int ClientId { get; set; }
    }
}
