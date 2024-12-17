using Application.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cliente.Commands
{
    public class DeleteClientCommand : IRequest<TResponse<ModifyClientResponse>>
    {
        [Required]
        public int ClientId { get; set; }
    }
}
