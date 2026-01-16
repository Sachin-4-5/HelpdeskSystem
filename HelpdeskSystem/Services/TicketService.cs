using HelpdeskSystem.DTOs;
using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskSystem.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public TicketService(ITicketRepository ticketRepository, UserManager<IdentityUser> userManager)
        {
            _ticketRepository = ticketRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            // Business rules can go here
            await _ticketRepository.AddAsync(ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _ticketRepository.DeleteAsync(id);
        }

        public async Task<List<Ticket>> GetTicketsForUserAsync(string userId, bool isAdmin)
        {
            if (isAdmin)
            {
                return await _ticketRepository.GetAllTicketsAsync();
            }

            return await _ticketRepository.GetTicketsByUserIdAsync(userId);
        }

        public async Task<Ticket?> GetTicketForUserAsync(int ticketId, string userId, bool isAdmin)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
                return null;

            if (isAdmin)
                return ticket;

            if (ticket.CreatedByUserId != userId)
                return null;

            return ticket;
        }


        // ============================
        // DASHBOARD SUMMARY
        // ============================
        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(string userId, bool isAdmin)
        {
            var summary = new DashboardSummaryDto
            {
                TotalTickets = await _ticketRepository.GetTotalTicketsCountAsync(userId, isAdmin),
                OpenTickets = await _ticketRepository.GetOpenTicketsCountAsync(userId, isAdmin),
                ResolvedTickets = await _ticketRepository.GetResolvedTicketsCountAsync(userId, isAdmin),
                IsAdmin = isAdmin
            };

            // ADMIN-ONLY DATA
            if (isAdmin)
            {
                summary.TotalUsers = _userManager.Users.Count();
            }
            return summary;
        }
    }
}