using HelpdeskSystem.DTOs;
using HelpdeskSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpdeskSystem.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
        Task<List<Ticket>> GetTicketsForUserAsync(string userId, bool isAdmin);
        Task<Ticket?> GetTicketForUserAsync(int ticketId, string userId, bool isAdmin);

        Task<DashboardSummaryDto> GetDashboardSummaryAsync(string userId,bool isAdmin);
    }
}