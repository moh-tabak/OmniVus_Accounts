using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniVus_Accounts.Data;
using OmniVus_Accounts.Models;
using OmniVus_Accounts.Models.Entities;

namespace OmniVus_Accounts.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        #region LogIn

        [HttpGet]
        public IActionResult LogIn([FromQuery] string? returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (returnUrl == null || returnUrl == "/")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    return LocalRedirect(returnUrl);
            }
            return View(new LoginForm());
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync([FromForm] LoginForm form, [FromQuery] string? returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var response = await _signInManager.PasswordSignInAsync(form.Email, form.Password, isPersistent: false, false);
                if (response.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        return LocalRedirect(returnUrl);
                }
            }

            //If it reached here, it wasn't succecful. return back to view.
            form.ErrorMsg = "Incorrect email or password";
            return View(form);
        }

        #endregion

        #region LogOut

        public async Task<IActionResult> LogOutAsync([FromQuery] string? returnUrl = null)
        {
            //Prevent from returning to DataController.
            if (returnUrl == null || returnUrl.ToLower().Contains("data"))
                returnUrl = "/";

            if (!_signInManager.IsSignedIn(User))
                return LocalRedirect(returnUrl);

            await _signInManager.SignOutAsync();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        #endregion

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View(new SignUpForm());
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromForm]SignUpForm form, [FromQuery] string? returnUrl = null)
        {
            string role = "user";

            if (ModelState.IsValid)
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                if (!_userManager.Users.Any())
                    role = "admin";

                var account = new IdentityUser()
                {
                    Email = form.Email,
                    UserName = form.Email
                };

                var result = await _userManager.CreateAsync(account, form.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(account, role);
                    await CreateProfileAsync(form, account.Id);

                    if (returnUrl == null || returnUrl == "/")
                    {
                        await _signInManager.SignInAsync(account, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            //If it reached here then it wasn't succecful, return back to view.
            return View(form);
        }
        #endregion

        private async Task CreateProfileAsync(SignUpForm form, string accountId)
        {
            var address = new AddressEntity()
            {
                Street = form.Street,
                PostalCode = form.PostalCode,
                City = form.City
            };

            var similarAddresses = await _context.Addresses
                .Where(x => x.Street == address.Street).ToListAsync();
            if (similarAddresses.Any())
            {
                var sameRecord = similarAddresses.FirstOrDefault(x => x.City == address.City);
                if (sameRecord != null)
                {
                    address.Id = sameRecord.Id;
                }
                else
                {
                    address.Id = _context.Addresses.Add(address).Entity.Id;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                address.Id = _context.Addresses.Add(address).Entity.Id;
                await _context.SaveChangesAsync();
            }

            UserInfoEntity user = new()
            {
                AccountId = accountId,
                FirstName = form.FirstName,
                LastName = form.LastName,
                AddressId = address.Id
            };
            await _context.UsersInfo.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
