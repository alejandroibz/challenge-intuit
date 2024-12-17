using Application.Features.Cliente.Handlers;
using Application.Features.Cliente.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace MyApp.Tests.Handlers
{
    public class GetAllClientsHandlerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllClientsHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAllClientPaginatedSuccess()
        {
            // Arrange
            var clients = new List<Cliente>
            {
                new Cliente { Id = 1, Name = "Omar", LastName = "Ibañez" },
                new Cliente { Id = 2, Name = "Alejandro", LastName = "Cordoba" }
            };

            var mappedClients = new List<GetAllClientsResponse>
            {
                new GetAllClientsResponse { Id = 1, Name = "Omar", LastName = "Ibañez" },
                new GetAllClientsResponse { Id = 2, Name = "Alejandro", LastName = "Cordoba" }
            };

            _clienteRepositoryMock
                .Setup(repo => repo.GetAllPaginatedAsync(1, 10, null))
                .ReturnsAsync((clients, clients.Count));

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<GetAllClientsResponse>>(clients))
                .Returns(mappedClients);

            var handler = new GetAllClientsQueryHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            var query = new GetAllClientsQuery { Page = 1, PageSize = 10, TextToSearch = null };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.Should().BeEquivalentTo(mappedClients);
            result.Pagination.Should().NotBeNull();
            result.Pagination.TotalRecords.Should().Be(clients.Count);
            result.Pagination.Page.Should().Be(1);
            result.Pagination.PageSize.Should().Be(10);

            _clienteRepositoryMock.Verify(repo => repo.GetAllPaginatedAsync(1, 10, null), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<GetAllClientsResponse>>(clients), Times.Once);
        }

        [Fact]
        public async Task GetAllClientThrowsException()
        {
            // Arrange
            _clienteRepositoryMock
                .Setup(repo => repo.GetAllPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>()))
                .ThrowsAsync(new Exception("Database error"));

            var handler = new GetAllClientsQueryHandler(_clienteRepositoryMock.Object, _mapperMock.Object);

            var query = new GetAllClientsQuery { Page = 1, PageSize = 10, TextToSearch = null };

            // Act
            Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Database error");

            _clienteRepositoryMock.Verify(repo => repo.GetAllPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<GetAllClientsResponse>>(It.IsAny<List<Cliente>>()), Times.Never);
        }
    }
}
