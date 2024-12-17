using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task AddAsync(Cliente client);
        Task DeleteAsync(Cliente cliente);
        Task<(List<Cliente>, int totalCount)> GetAllPaginatedAsync(int page, int pageSize, string? textToSearch);
        Task<Cliente?> GetByIdAsync(int id);
        Task UpdateAsync(Cliente cliente);
    }
}
