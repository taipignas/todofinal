using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ToDoFinal.web.Pages.Account
{
    public class IndexModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
    }
}
