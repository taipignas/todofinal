using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoFinal.Identity;
using ToDoFinal.Models;
using ToDoFinal.Services;

namespace ToDoFinal.web.Pages
{

    [Authorize(Roles = "Administrator")]
    public class TaskAdminModel : PageModel
    {
        private readonly UserManager<ToDoUser> _userManager;
        private readonly SignInManager<ToDoUser> _signInManager;
        private readonly ManageTasks _manageTasks;
        private readonly AdminTasks _adminTasks;
        private readonly ManageUsers _manageUsers;

        public TaskAdminModel(
            UserManager<ToDoUser> userManager,
            SignInManager<ToDoUser> signInManager,
            ManageTasks manageTasks,
            AdminTasks adminTasks,
            ManageUsers manageUsers
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _manageTasks = manageTasks;
            _adminTasks = adminTasks;
            _manageUsers = manageUsers;
        }

        public const string Hide = "_Name";
        public List<UserTasks> Users { get; set; }
        public List<ToDoTask> Tasks { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public int HideCompleted { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        

        public class InputModel
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            [StringLength(90, MinimumLength = 3)]
            public string Description { get; set; }
            public Priority Priority { get; set; }
            [Required]
            public DateTime DueDate { get; set; }
            public Status Status { get; set; }
        }

        private async Task LoadAsync(ToDoUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);

            Tasks = _adminTasks.GetAll();
            Users = _manageUsers.UsernameIdAll();
            foreach(UserTasks userTasks in Users)
            {
                userTasks.Tasks = Tasks.Where(t => t.ToDoUserId == userTasks.Id).ToList();
            }
            HideCompleted = HttpContext.Session.GetInt32(Hide) ?? default(int);
            Input = new InputModel{};
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await _signInManager.RefreshSignInAsync(user);
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAddTaskAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid || Input.Description == null)
            {
                await LoadAsync(user);
                StatusMessage = "Incorrect task data";
                return Page();
            }

            if (ModelState.IsValid)
            {
                var dueDate = Input.DueDate.ToUniversalTime();
                ToDoTask task = new ToDoTask { Description = Input.Description, DueDate = dueDate, Priority = Input.Priority, Status = Input.Status, ToDoUserId = Input.UserId };
                _manageTasks.AddTask(task);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Task has been added";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateTaskAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                StatusMessage = "Incorrect task data";
                return Page();
            }

            if (ModelState.IsValid)
            {
                ToDoTask task = new ToDoTask { DueDate = Input.DueDate.ToUniversalTime(), Priority = Input.Priority, Status = Input.Status };
                _manageTasks.UpdateTask(task, Input.Id);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Task has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteTaskAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                StatusMessage = "Corrupt data";
                return Page();
            }

            if (ModelState.IsValid)
            {
                _manageTasks.DeleteTask(Input.Id);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Task has been deleted";
            return RedirectToPage();
        }

        public IActionResult OnPostHideAsync()
        {
            if (HttpContext.Session.GetInt32(Hide) == 1)
            {
                HttpContext.Session.SetInt32(Hide, 0);
                StatusMessage = "Completed tasks will be displayed";
            }
            else
            {
                HttpContext.Session.SetInt32(Hide, 1);
                StatusMessage = "Completed tasks will be hidden";
            }
            HideCompleted = HttpContext.Session.GetInt32(Hide) ?? default(int);
            return RedirectToPage();
        }
    }
}
