using System.Collections.Generic;
using System.Threading.Tasks;
using HelpdeskSystem.Models;

namespace HelpdeskSystem.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(int id);
        Task<List<Ticket>> GetTicketsByUserIdAsync(string userId);
        Task<List<Ticket>> GetAllTicketsAsync();


        // ============================
        // DASHBOARD COUNTS (NEW)
        // ============================
        Task<int> GetTotalTicketsCountAsync(string userId, bool isAdmin);
        Task<int> GetOpenTicketsCountAsync(string userId, bool isAdmin);
        Task<int> GetResolvedTicketsCountAsync(string userId, bool isAdmin);
    }
}