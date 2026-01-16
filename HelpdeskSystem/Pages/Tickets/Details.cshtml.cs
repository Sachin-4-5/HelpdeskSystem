using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using HelpdeskSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpdeskSystem.Pages.Tickets
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITicketService _ticketService;

        public DetailsModel(ITicketService ticketService, UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        public Ticket Ticket { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            var ticket = await _ticketService
                .GetTicketForUserAsync(id, userId!, isAdmin);

            if (ticket == null)
                return Forbid(); // or NotFound()

            Ticket = ticket;
            return Page();
        }

    }
}