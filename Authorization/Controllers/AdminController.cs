using Microsoft.AspNetCore.Mvc;
using Authorization.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

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

        // Thêm mới user
        [HttpPost]
        public IActionResult AddUser(string username, string password, string role)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Json(new { success = false, message = "Vui long nhap day du thong tin" });
            }
            //kiem tra user trung ten
            if (UserData.Users.Any(u => u.Username == username))
            {
                return Json(new { success = false, message = "Username da ton tai!" });
            }

            var newUser = new User
            {
                Id = UserData.Users.Max(u => u.Id) + 1,
                Username = username,
                Password = password,
                Role = role,
                MainPermissions = new List<string>(),
                SubPermissions = new List<string>()
            };
            UserData.Users.Add(newUser);
            Console.WriteLine($"Them user moi: {username} ({role})");
            return Json(new { success = true, message = "Them user thanh cong!" });
        }

        // Sửa user
        [HttpPost]
        public IActionResult EditUser(int id, string username, string password, string role)
        {
            var user = UserData.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Json(new { success = false, message = "Khong tim thay user!" });

            user.Username = username;
            user.Password = password;
            user.Role = role;
            Console.WriteLine($"Cap nhat user: {user.Username} ({role})");
            return Json(new { success = true, message = "Cap nhat user thanh cong!" });
        }

        // Xóa user
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = UserData.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Json(new { success = false, message = "User khong ton tai!" });
            UserData.Users.Remove(user);
            Console.WriteLine($"Xoa user: {user.Username}");
            return Json(new { success = true, message = "Xoa user thanh cong!" });
        }

        [HttpGet]
        public IActionResult GetPermission(int userId)
        {
            var user = UserData.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return Json(new { success = false });
            return Json(new { success = true, main = user.MainPermissions ?? new List<string>(), sub = user.SubPermissions ?? new List<string>() });
        }

        [HttpPost]
        public IActionResult UpdatePermissions(int userId, List<string> mainPermissions, List<string> subPermissions)
        {
            //lay user tu danh sach
            var user = UserData.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User khong ton tai!" });

            }
            user.MainPermissions = mainPermissions ?? new List<string>();
            user.SubPermissions = subPermissions ?? new List<string>();
            Console.WriteLine($"Cap nhat quyen cho {user.Username}");
            Console.WriteLine($"Main: {string.Join(", ", user.MainPermissions)}");
            Console.WriteLine($"Sub: {string.Join(", ", user.SubPermissions)}");

            return Json(new { success = true, message = "Cap nhat thanh cong!" });
        }
    }
}
