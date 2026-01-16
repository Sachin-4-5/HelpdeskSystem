using HelpdeskSystem.DTOs;
using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpdeskSystem.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(
            ITicketService ticketService,
            UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        
        public DashboardSummaryDto Summary { get; private set; } = new();
        public IList<Ticket> RecentTickets { get; private set; } = new List<Ticket>();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            var isAdmin = User.IsInRole("Admin");

            // Existing summary
            Summary = await _ticketService.GetDashboardSummaryAsync(userId, isAdmin);

            // NEW: Load latest 5 tickets
            var tickets = await _ticketService.GetTicketsForUserAsync(userId, isAdmin);
            RecentTickets = tickets
                .OrderByDescending(t => t.CreatedOn)
                .Take(5)
                .ToList();
        }
    }
}
