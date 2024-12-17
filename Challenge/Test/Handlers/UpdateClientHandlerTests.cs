using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Handlers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace MyApp.Tests.Handlers
{
    public class UpdateClientHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateClientHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>();

            _mapperMock
                .Setup(mapper => mapper.Map(It.IsAny<UpdateClientCommand>(), It.IsAny<Cliente>()))
                .Callback<UpdateClientCommand, Cliente>((src, dest) =>
                {
                    dest.Name = src.Name;
                    dest.LastName = src.LastName;
                    dest.Email = src.Email;
                    dest.Cellphone = src.Cellphone;
                    dest.CUIT = src.CUIT;
                    dest.Address = src.Address;
                    dest.Birthdate = src.Birthdate;
                });
        }

        [Fact]
        public async Task UpdateClientSuccess()
        {
            // Arrange
            var client = new Cliente
            {
                Id = 1,
                Name = "OldName",
                LastName = "OldLastName",
                Email = "old.email@example.com",
                Cellphone = "123456789",
                CUIT = "20304050601",
                Address = "Old Address",
                Birthdate = new DateTime(1990, 1, 1)
            };

            var updatedCommand = new UpdateClientCommand
            {
                ClientId = 1,
                Name = "NewName",
                LastName = "NewLastName",
                Email = "new.email@example.com",
                Cellphone = "987654321",
                CUIT = "20304050602",
                Address = "New Address",
                Birthdate = new DateTime(1985, 1, 1)
            };

            _clienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(client);

            _clienteRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            var handler = new UpdateClientCommandHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(updatedCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.ClientId.Should().Be(1);
            result.Messages.Message.Should().Be("Ejecución exitosa");

            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _clienteRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Cliente>(c =>
                c.Name == updatedCommand.Name &&
                c.LastName == updatedCommand.LastName &&
                c.Email == updatedCommand.Email &&
                c.Cellphone == updatedCommand.Cellphone &&
                c.CUIT == updatedCommand.CUIT &&
                c.Address == updatedCommand.Address &&
                c.Birthdate == updatedCommand.Birthdate
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateClientFailIfNoExist()
        {
            // Arrange
            _clienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(2))
                .ReturnsAsync((Cliente?)null);

            var updatedCommand = new UpdateClientCommand
            {
                ClientId = 2,
                Name = "NewName",
                LastName = "NewLastName",
                Email = "new.email@example.com",
                Cellphone = "987654321",
                CUIT = "20304050602",
                Address = "New Address",
                Birthdate = new DateTime(1985, 1, 1)
            };

            var handler = new UpdateClientCommandHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(updatedCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Messages.Message.Should().Be("No existe un cliente con este Id");

            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(2), Times.Once);
            _clienteRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public async Task UpdateClientThrowsException()
        {
            // Arrange
            var client = new Cliente
            {
                Id = 1,
                Name = "OldName"
            };

            _clienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(client);

            _clienteRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Cliente>()))
                .ThrowsAsync(new Exception("Database error"));

            var updatedCommand = new UpdateClientCommand
            {
                ClientId = 1,
                Name = "NewName",
                LastName = "NewLastName",
                Email = "new.email@example.com",
                Cellphone = "987654321",
                CUIT = "20304050602",
                Address = "New Address",
                Birthdate = new DateTime(1985, 1, 1)
            };

            var handler = new UpdateClientCommandHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(updatedCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Database error");

            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _clienteRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Cliente>()), Times.Once);
        }
    }
}
