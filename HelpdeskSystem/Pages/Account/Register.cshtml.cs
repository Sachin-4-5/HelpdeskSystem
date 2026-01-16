using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HelpdeskSystem.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string FullName, string Email, string Password, string ConfirmPassword)
        {
            if (Password != ConfirmPassword)
            {
                TempData["Error"] = "Passwords do not match.";
                return RedirectToPage("/Index");
            }

            var user = new IdentityUser
            {
                UserName = Email,
                Email = Email
            };

            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.First().Description;
                return RedirectToPage("/Index");
            }

            // ADD FULL NAME CLAIM (KEY FIX)
            await _userManager.AddClaimAsync(user, new Claim("FullName", FullName));
            await _userManager.AddToRoleAsync(user, "Admin");

            // OPTIONAL (future-ready)
            await _userManager.AddToRoleAsync(user, "User");

            // SIGN IN AGAIN SO CLAIM IS AVAILABLE IMMEDIATELY
            await _signInManager.SignInAsync(user, isPersistent: false);

            TempData["Success"] = "Registration successful!";
            return RedirectToPage("/Index");
        }
    }
}