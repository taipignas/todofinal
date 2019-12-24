using ToDoFinal.Identity;
using System.Linq;
using ToDoFinal.Models;
using System.Collections.Generic;

namespace ToDoFinal.Services
{
    public class ManageUsers
    {
        private readonly ToDoUserContext _userContext;
        public ManageUsers(ToDoUserContext userContext)
        {
            _userContext = userContext;
        }

        public List<UserTasks> UsernameIdAll()
        {
            return _userContext.Users.Select(u => new UserTasks { Username = u.UserName, Id = u.Id }).ToList();
        }

        public string[] GetAllUsernames()
        {
            return _userContext.Users.Select(u => u.UserName).ToArray();
        }
    }
}
