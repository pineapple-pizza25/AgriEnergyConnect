using Agri_Energy_Connect.Models;
using Agri_Energy_Connect.Models.MathApp.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Agri_Energy_Connect.Controllers
{
    public class EmployeeAuthController : Controller
    {
        FirebaseAuthProvider auth;

        private readonly AgriEnergyConnectContext _context;

        public EmployeeAuthController(AgriEnergyConnectContext context)
        {
            _context = context;
            auth = new FirebaseAuthProvider(new FirebaseConfig(Environment.GetEnvironmentVariable("FirebaseAgriEnergy")));
        }

        

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(email, password);
                string currentUserId = fbAuthLink.User.LocalId;

                if (_context.Farmers.Any(e => e.Id == currentUserId)) return View(); //https://stackoverflow.com/questions/23654057/check-if-record-exists-in-entity-framework

                if (currentUserId != null)
                {
                    HttpContext.Session.SetString("currentUser", currentUserId);
                    return RedirectToAction("Index", "Farmers");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("currentUser");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string password, string firstName, string lastName, string adminAddress, string phoneNumber, string email, DateOnly dateOfBirth, string gender)
        {
            try
            {
                await auth.CreateUserWithEmailAndPasswordAsync(email, password);

                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(email, password);
                string currentUserId = fbAuthLink.User.LocalId;
                HttpContext.Session.SetString("UserId", currentUserId);


               await CreateEmployee(currentUserId, firstName, lastName, adminAddress, phoneNumber, email, dateOfBirth, gender);

                if (currentUserId != null)
                {
                    HttpContext.Session.SetString("currentUser", currentUserId);
                    return RedirectToAction("Index", "Farmers");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View();
            }

            return View();
        }

        //Adds the details of the new employee to the employee table in the database
        public async Task CreateEmployee(string employeeId, string firstName, string lastName, string adminAddress, string phoneNumber, string email, DateOnly dateOfBirth, string gender)
        {
            Employee employee = new Employee(employeeId, firstName, lastName, adminAddress, phoneNumber, email, dateOfBirth, gender);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

        }

    }
}
