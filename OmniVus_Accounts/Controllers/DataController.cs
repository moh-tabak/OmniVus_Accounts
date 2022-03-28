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
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DataController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
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
            if(String.IsNullOrEmpty(user.Picture))
                model.ProfilePicName = "default.png";
            else
                model.ProfilePicName = user.Picture;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfileAsync([FromForm] UserProfileViewModel form)
        {
            var account = await _userManager.GetUserAsync(User);
                if (account == null) return NotFound();
            var user = await _context.UsersInfo.FindAsync(account.Id);

            if (ModelState.IsValid)
            {
                account.Email = form.Email;
                account.PhoneNumber = form.Phone;

                var result = await _userManager.UpdateAsync(account);
                if (result.Succeeded)
                {
                    int addressId = await CheckAdressAsync(new AddressEntity()
                    {
                        Street = form.Street,
                        City = form.City,
                        PostalCode = form.PostalCode,
                    });

                   

                    var imageFile = form.ProfilePic;
                    if (imageFile == null || imageFile.Length == 0)
                    {
                        form.ProfilePicName = user!.Picture;
                    }
                    else
                    {
                        //Save Image
                        form.ProfilePicName = imageFile.FileName;
                        string savePath = Path.Combine(_hostingEnvironment.WebRootPath, "ProfileImages", form.ProfilePicName);
                        using(var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                    }
                    user!.FirstName = form.FirstName;
                    user!.LastName = form.LastName;
                    user!.AddressId = addressId;
                    user!.Picture = form.ProfilePicName;
                    
                    _context.SaveChanges();
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }

            if (String.IsNullOrEmpty(user!.Picture))
                form.ProfilePicName = "default.png";
            else
                form.ProfilePicName = user!.Picture;
            form.Roles = await _userManager.GetRolesAsync(account);
            return View(form);
        }

        [Route("/Manage/Users")]
        [HttpGet]
        public IActionResult ManageUsers()
        {
            return View();
        }

        /// <summary>
        ///     Check if the address already exists in the database. Add it to the database if it doesn't exist.
        /// </summary>
        /// <param name="address">The address which being checked.</param>
        /// <returns>The old/new Id of the record.</returns>
        private async Task<int> CheckAdressAsync(AddressEntity address)
        {
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
            return address.Id;
        }
    }
}
