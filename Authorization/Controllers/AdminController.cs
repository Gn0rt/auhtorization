using Microsoft.AspNetCore.Mvc;
using Authorization.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Authorization.Controllers
{
    public class AdminController : Controller
    {
        //hien thi danh sach user
        public IActionResult Index()
        {
            //lay vai tro cua nguoi dang nhap
            var role = HttpContext.Session.GetString("Role");
            //khong phai Admin => chan truy cap
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }
            //lay toan bo danh sach nguoi dung
            var users = UserData.Users.Where(u => u.Role != "Admin").ToList();

            //tra ve danh sasch nguoi dung sang view
            return View(UserData.Users);
        }

        [HttpPost]
        public IActionResult UpdatePermissions(int userId, List<string> mainPermissions, List<string> subPermissions)
        {
            //lay user tu danh sach
            var user = UserData.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.MainPermissions = mainPermissions ?? new List<string>();
                user.SubPermissions = subPermissions ?? new List<string>();
                Console.WriteLine($"Cap nhat quyen cho {user.Username}");
                Console.WriteLine($"Main: {string.Join(", ", user.MainPermissions)}");
                Console.WriteLine($"Sub: {string.Join(", ", user.SubPermissions)}");

            }

            return RedirectToAction("Index");
        }
    }
}
