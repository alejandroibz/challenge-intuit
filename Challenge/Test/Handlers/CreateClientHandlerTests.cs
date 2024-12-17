using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Handler;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace MyApp.Tests.Handlers
{
    public class CreateClientHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public CreateClientHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
        }

        [Fact]
        public async Task AddClienteSuccess()
        {
            var command = new CreateClientCommand
            {
                Name = "Ale",
                LastName = "Ibz",
                Email = "ale.ibz@intuit.com",
                Cellphone = "123456789",
                CUIT = "12345678912",
                Address = "123 m",
                Birthdate = new DateTime(1990, 1, 1)
            };

            var client = new Cliente
            {
                Id = 1, 
                Name = command.Name,
                LastName = command.LastName,
                Email = command.Email,
                Cellphone = command.Cellphone,
                CUIT = command.CUIT,
                Address = command.Address,
                Birthdate = command.Birthdate
            };

            _clienteRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Cliente>()))
                .Callback<Cliente>(c => c.Id = client.Id) 
                .Returns(Task.CompletedTask);

            var handler = new CreateClientCommandHandler(_clienteRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.ClientId.Should().Be(1);
            result.Messages.Message.Should().Be("Ejecución exitosa");
            _clienteRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task AddClientThrowsException()
        {
            // Arrange
            var command = new CreateClientCommand
            {
                Name = "Ale",
                LastName = "Ibz",
                Email = "ale.ibz@intuit.com",
                Cellphone = "123456789",
                CUIT = "12345678912",
                Address = "123 m",
                Birthdate = new DateTime(1990, 1, 1)
            };

            _clienteRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Cliente>()))
                .ThrowsAsync(new Exception("Database error"));

            var handler = new CreateClientCommandHandler(_clienteRepositoryMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Database error");
            _clienteRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Cliente>()), Times.Once);
        }
    }
}
