using System;
using System.Collections.Generic;

namespace Authorization.Data
{
    public static class UserData
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "ketoan@gmail.com",
                Password = "123",
                Role = "Employee",
                MainPermissions = new List<string> {"baocao"},
                SubPermissions = new List<string> { }
            },
            new User
            {
                Id = 2,
                Username = "giamsat@gmail.com",
                Password = "123",
                Role = "Employee",
                MainPermissions = new List<string> {"giamsat"},
                SubPermissions = new List<string> {"phongban" }
            },
            new User
            {
                Id = 3,
                Username = "quanly@gmail.com",
                Password = "123",
                Role = "Employee",
                MainPermissions = new List<string> {"quanli"},
                SubPermissions = new List<string> { "nhanvien", "chucvu"}
            },
            new User
            {
                Id = 4,
                Username = "admin@gmail.com",
                Password = "123",
                Role = "Admin",
                MainPermissions = new List<string> {},
                SubPermissions = new List<string> { }
            },
        };

        public static void PrintUsers()
        {
            Console.WriteLine("==== User List ====");
            foreach (var user in Users)
            {
                Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Role: {user.Role}");
                Console.WriteLine($"Main Permissions: {string.Join(", ", user.MainPermissions)}");
                Console.WriteLine($"Sub Permissions: {string.Join(", ", user.SubPermissions)}");
                Console.WriteLine();
            }
        }
    }
}