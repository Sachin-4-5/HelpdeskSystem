using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpdeskSystem.Pages.Tickets
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(
            ITicketService ticketService,
            UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = new();

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
            if (!ModelState.IsValid)
                return Page();

            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            var existingTicket = await _ticketService
                .GetTicketForUserAsync(Ticket.Id, userId!, isAdmin);

            if (existingTicket == null)
                return Forbid();

            // Everyone can update these
            existingTicket.Title = Ticket.Title;
            existingTicket.Description = Ticket.Description;

            // ADMIN-ONLY: status update
            if (isAdmin)
            {
                existingTicket.Status = Ticket.Status;
            }

            await _ticketService.UpdateTicketAsync(existingTicket);

            TempData["Success"] = "Ticket updated successfully.";
            return RedirectToPage("Index");
        }
    }
}