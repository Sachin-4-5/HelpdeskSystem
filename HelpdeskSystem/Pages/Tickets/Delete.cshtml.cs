using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpdeskSystem.Pages.Tickets
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(
            ITicketService ticketService,
            UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            var ticket = await _ticketService
                .GetTicketForUserAsync(id, userId!, isAdmin);

            if (ticket == null)
                return Forbid();

            Ticket = ticket;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            var ticket = await _ticketService
                .GetTicketForUserAsync(Ticket.Id, userId!, isAdmin);

            if (ticket == null)
                return Forbid();

            await _ticketService.DeleteTicketAsync(ticket.Id);

            TempData["Success"] = "Ticket deleted successfully.";
            return RedirectToPage("Index");
        }
    }
}