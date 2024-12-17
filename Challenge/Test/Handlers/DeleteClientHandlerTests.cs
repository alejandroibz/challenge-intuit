using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Handlers;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace MyApp.Tests.Handlers
{
    public class DeleteClientHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public DeleteClientHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
        }

        [Fact]
        public async Task DeleteClientSuccess()
        {
            // Arrange
            var client = new Cliente
            {
                Id = 1,
                Name = "Ale Ibz"
            };

            _clienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(client);

            _clienteRepositoryMock
                .Setup(repo => repo.DeleteAsync(client))
                .Returns(Task.CompletedTask);

            var handler = new DeleteClientCommandHandler(_clienteRepositoryMock.Object);

            var command = new DeleteClientCommand { ClientId = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.ClientId.Should().Be(1);
            result.Messages.Message.Should().Be("Client deleted successfully.");

            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _clienteRepositoryMock.Verify(repo => repo.DeleteAsync(client), Times.Once);
        }

        [Fact]
        public async Task DeleteClientFailWithId()
        {
            // Arrange
            _clienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(2))
                .ReturnsAsync((Cliente?)null);

            var handler = new DeleteClientCommandHandler(_clienteRepositoryMock.Object);

            var command = new DeleteClientCommand { ClientId = 2 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Messages.Message.Should().Be("Client with ID 2 not found.");

            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
            _clienteRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public async Task DeleteClientThrowsException()
        {
            // Arrange
            var client = new Cliente
            {
                Id = 1,
                Name = "Ale Ibz"
            };

            _clienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(client);

            _clienteRepositoryMock
                .Setup(repo => repo.DeleteAsync(client))
                .ThrowsAsync(new Exception("Database error"));

            var handler = new DeleteClientCommandHandler(_clienteRepositoryMock.Object);

            var command = new DeleteClientCommand { ClientId = 1 };

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Database error");

            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _clienteRepositoryMock.Verify(repo => repo.DeleteAsync(client), Times.Once);
        }
    }
}
