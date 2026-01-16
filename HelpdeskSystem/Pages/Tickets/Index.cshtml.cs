using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace HelpdeskSystem.Pages.Tickets
{
    [Authorize] //Only authenticated users can access
    public class IndexModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ITicketService ticketService,UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public async Task OnGetAsync()
        {
            // Logged-in user's ID (from AspNetUsers table)
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            // Admin -> All tickets
            // User  -> Only own tickets
            Tickets = await _ticketService.GetTicketsForUserAsync(userId!, isAdmin);
        }
    }
}