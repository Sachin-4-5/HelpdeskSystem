using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HelpdeskSystem.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync(string Email,string Password, bool RememberMe)
        {
            // Find user
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid login attempt.";
                return RedirectToPage("/Index");
            }

            // Validate password
            var passwordValid = await _userManager.CheckPasswordAsync(user, Password);
            if (!passwordValid)
            {
                TempData["Error"] = "Invalid login attempt.";
                return RedirectToPage("/Index");
            }

            // Load existing claims
            var claims = await _userManager.GetClaimsAsync(user);

            // Ensure FullName claim exists
            if (!claims.Any(c => c.Type == "FullName"))
            {
                // fallback value (future-proof)
                await _userManager.AddClaimAsync(user,
                    new Claim("FullName", user.Email!));
            }

            // Re-fetch claims (important)
            claims = await _userManager.GetClaimsAsync(user);

            // Sign-in with claims
            await _signInManager.SignInWithClaimsAsync(
                user,
                RememberMe,
                claims);

            TempData["Success"] = "Login successful!";
            return RedirectToPage("/Index");
        }
    }
}