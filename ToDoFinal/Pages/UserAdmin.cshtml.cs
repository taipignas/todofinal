using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoFinal.Identity;
using ToDoFinal.Services;

namespace ToDoFinal.web.Pages
{
    [Authorize(Roles = "Administrator")]
    public class UserAdminModel : PageModel
    {
        private readonly UserManager<ToDoUser> _userManager;
        private readonly SignInManager<ToDoUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ManageUsers _manageUsers;

        public UserAdminModel(
            UserManager<ToDoUser> userManager,
            SignInManager<ToDoUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ManageUsers manageUsers
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _manageUsers = manageUsers;
        }

        public string[] Users { get; set; }
        public List<bool> IsAdmin { get; set; }
        public List<bool> IsUserAdmin { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
        }

        private async Task LoadAsync()
        {
            Users = _manageUsers.GetAllUsernames();
            Users = Users.Where(u => u != "admin").ToArray();
            IsAdmin = new List<bool>();

            foreach (var username in Users)
            {
                IsAdmin.Add(await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(username), "Administrator"));
            }

            Input = new InputModel
            {
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostToggleAdminAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            if (ModelState.IsValid)
            {
                var userForRole = await _userManager.FindByNameAsync(Input.Username);
                
                if (await _userManager.IsInRoleAsync(userForRole, "Administrator"))
                {
                    await _userManager.RemoveFromRoleAsync(userForRole, "Administrator");
                    StatusMessage = $"User {userForRole.Email} has been discharged from Administrators";
                }
                else
                {
                    await _userManager.AddToRoleAsync(userForRole, "Administrator");
                    StatusMessage = $"User {userForRole.Email} has been assigned Administrator role";
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            if (await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}
