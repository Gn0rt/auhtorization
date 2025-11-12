using Microsoft.AspNetCore.Mvc;
using Authorization.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Authorization.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            //lay username tu session
            var userId = HttpContext.Session.GetInt32("UserId");
            //Neu chua dang nhap => quay lai login
            if (userId == null)
            {
                //chua dnag nhap => chuyen ve login
                return RedirectToAction("Login", "Auth");
            }

            //tim user trong danh sach
            var user = UserData.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            //tra user sang view de hien thi quyen
            return View(user);
        }
    }
}