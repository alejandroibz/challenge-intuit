using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Cliente cliente)
        {
            _context.Clients.Remove(cliente); 
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clients.Update(cliente); 
            await _context.SaveChangesAsync(); 
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(List<Cliente>, int totalCount)> GetAllPaginatedAsync(int page, int pageSize, string? textToSearch)
        {
            var response = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(textToSearch))
            {
                response = response.Where(x => x.Name == textToSearch);
            }

            var totalCount = await _context.Clients.CountAsync();

            var listClients = response
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (listClients, totalCount);
        }

    }
}
