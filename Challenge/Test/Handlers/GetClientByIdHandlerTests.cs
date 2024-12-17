using Application.Features.Cliente.Handlers;
using Application.Features.Cliente.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace MyApp.Tests.Handlers
{
    public class GetClientByIdHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetClientByIdHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>();

        }

        [Fact]
        public async Task GetClientByIdSuccess()
        {
            // Arrange
            var client = new Cliente
            {
                Id = 1,
                Name = "Alejandro",
                LastName = "Ibz",
                Email = "ale.ibz@gmail.com",
                Cellphone = "123456789",
                CUIT = "132132132131",
                Address = "123 m",
                Birthdate = new System.DateTime(1990, 1, 1)
            };

            var clientDto = new GetClientByIdResponse
            {
                Id = client.Id,
                Name = client.Name,
                LastName = client.LastName,
                Email = client.Email,
                Cellphone = client.Cellphone,
                CUIT = client.CUIT,
                Address = client.Address,
                Birthdate = client.Birthdate
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(client);

            _mapperMock.Setup(mapper => mapper.Map<GetClientByIdResponse>(client))
                .Returns(clientDto);

            var handler = new GetClientByIdQueryHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            var query = new GetClientByIdQuery { Id = 1 };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Name.Should().Be("Alejandro");
            result.Messages.Message.Should().Be("Ejecución exitosa");
        }


        [Fact]
        public async Task GetClientByIdIfNoExist()
        {
            // Arrange
            _clienteRepositoryMock.Setup(repo => repo.GetByIdAsync(2))
                .ReturnsAsync((Cliente?)null);

            // Act
            var handler = new GetClientByIdQueryHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            var query = new GetClientByIdQuery { Id = 2 };
            
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Messages.Message.Should().Be("Client with ID 2 not found.");
        }
    }
}
