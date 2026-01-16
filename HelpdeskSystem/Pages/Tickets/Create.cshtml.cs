using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace HelpdeskSystem.Pages.Tickets
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(ITicketService ticketService, UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Logged-in User ID
            var userId = _userManager.GetUserId(User);

            Ticket.CreatedByUserId = userId!;
            Ticket.CreatedBy = User.Identity!.Name!;
            Ticket.CreatedOn = DateTime.UtcNow;
            Ticket.Status = "Open";

            await _ticketService.CreateTicketAsync(Ticket);

            TempData["Success"] = "Ticket created successfully";
            return RedirectToPage("Index");
        }

    }
}