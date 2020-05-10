using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookShop.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class BaseController : Controller
    {
        protected void Alert(string message, bool isError = false)
        {
            if (isError)
            {
                TempData["error"] = message;
            }
            else
            {
                TempData["success"] = message;
            }
        }
    }
}