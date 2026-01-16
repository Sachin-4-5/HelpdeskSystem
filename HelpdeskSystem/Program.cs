using HelpdeskSystem.Data;
using HelpdeskSystem.Interfaces;
using HelpdeskSystem.Repositories;
using HelpdeskSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register Services
//builder.Services.AddRazorPages(); // Registers Razor Pages services, Enables PageModel binding, Adds antiforgery support.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Tickets");
    options.Conventions.AuthorizeFolder("/Dashboard");
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));    //Db Registration.
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";     // Landing page
    options.AccessDeniedPath = "/Index";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();      // Redirect HTTP to HTTPS.
app.UseStaticFiles();           // Serves css/js/images.
app.UseRouting();               // Matches URL to Endpoints.
app.UseAuthentication();        // Enforces identity.
app.UseAuthorization();         // Enforces access roles.

// =============
// ROLE SEEDING
// =============
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role)) {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// =====================
// ADMIN USER SEEDING (FIXED)
// =====================
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string adminEmail = "admin@helpdesk.com";
    string adminPassword = "Admin@123";
    string adminFullName = "Admin";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // FORCE FIX: Remove old FullName if exists
    var claims = await userManager.GetClaimsAsync(adminUser);
    var existingClaim = claims.FirstOrDefault(c => c.Type == "FullName");

    if (existingClaim != null)
    {
        await userManager.RemoveClaimAsync(adminUser, existingClaim);
    }

    // Add correct FullName claim
    await userManager.AddClaimAsync(
        adminUser,
        new System.Security.Claims.Claim("FullName", adminFullName)
    );
}


app.MapRazorPages();            // Map razor pages routes.
app.Run();