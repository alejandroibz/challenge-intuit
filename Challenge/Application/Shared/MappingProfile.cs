using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, GetAllClientsResponse>().ReverseMap();

            CreateMap<Cliente, GetClientByIdResponse>().ReverseMap();

            CreateMap<UpdateClientCommand, Cliente>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
