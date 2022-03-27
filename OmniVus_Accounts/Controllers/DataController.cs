using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniVus_Accounts.Data;
using OmniVus_Accounts.Models;
using OmniVus_Accounts.Models.Entities;
using OmniVus_Accounts.Models.ViewModels;

namespace OmniVus_Accounts.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DataController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> UserProfileAsync()
        {
            var account = await _userManager.GetUserAsync(User);
            if (account == null) return BadRequest();
            IEnumerable<string> roles = await _userManager.GetRolesAsync(account);
            var user = await _context.UsersInfo.FindAsync(account.Id);
            if (user == null) return NotFound();
            var address = await _context.Addresses.FindAsync(user.AddressId);
            if (address == null) return NotFound();
            
            UserProfileViewModel model = new UserProfileViewModel(roles, user.FirstName, user.LastName, account.Email, account.PhoneNumber, address.Street, address.PostalCode, address.PostalCode);
            return View(model);
        }
    }
}
