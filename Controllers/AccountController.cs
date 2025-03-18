using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ELNETBAI.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> users = new List<User>();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ViewBag.ErrorMessage = "All fields are required.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
                return View();
            }

            if (users.Exists(u => u.Username == username || u.Email == email))
            {
                ViewBag.ErrorMessage = "Username or Email already exists.";
                return View();
            }

            users.Add(new User { Username = username, Email = email, Password = password });

            return RedirectToAction("Login");
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
