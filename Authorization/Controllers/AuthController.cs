using Microsoft.AspNetCore.Mvc;
using Authorization.Data;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Authorization.Controllers
{
    public class AuthController : Controller
    {
        //hien thi trang login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //xu ly dang nhap
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //tim user trong danh sach
            // => so sanh
            var user = UserData.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetInt32("UserId", user.Id);
                Console.WriteLine($"{user.Username} đăng nhập thành công ({user.Role})");
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }
            }
            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            //xoa session
            HttpContext.Session.Clear();
            // Xóa cookie auth
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

            // Session dùng để lưu tạm các dữ liệu như UserId, Role

            // Cookie auth dùng để xác thực [Authorize] trên controller/action

            // Kết hợp cả 2 -> logout hoàn toàn, tránh user vẫn còn quyền truy cập.
        }
    }
}