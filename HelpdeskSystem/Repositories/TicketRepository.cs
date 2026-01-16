using HelpdeskSystem.Data;
using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskSystem.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task AddAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.Tickets
                .Where(t => t.CreatedByUserId == userId)
                .OrderByDescending(t => t.CreatedOn)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .OrderByDescending(t => t.CreatedOn)
                .ToListAsync();
        }


        // ============================
        // DASHBOARD COUNTS
        // ============================
        public async Task<int> GetTotalTicketsCountAsync(string userId, bool isAdmin)
        {
            return isAdmin
                ? await _context.Tickets.CountAsync()
                : await _context.Tickets.CountAsync(t => t.CreatedByUserId == userId);
        }

        public async Task<int> GetOpenTicketsCountAsync(string userId, bool isAdmin)
        {
            return isAdmin
                ? await _context.Tickets.CountAsync(t => t.Status == "Open")
                : await _context.Tickets.CountAsync(t =>
                    t.CreatedByUserId == userId && t.Status == "Open");
        }

        public async Task<int> GetResolvedTicketsCountAsync(string userId, bool isAdmin)
        {
            return isAdmin
                ? await _context.Tickets.CountAsync(t => t.Status == "Resolved")
                : await _context.Tickets.CountAsync(t =>
                    t.CreatedByUserId == userId && t.Status == "Resolved");
        }

    }
}