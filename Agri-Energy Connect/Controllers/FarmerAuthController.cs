using Agri_Energy_Connect.Models.MathApp.Models;
using Agri_Energy_Connect.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agri_Energy_Connect.Controllers
{
    public class FarmerAuthController : Controller
    {
        FirebaseAuthProvider auth;

        private readonly AgriEnergyConnectContext _context;

        public FarmerAuthController(AgriEnergyConnectContext context)
        {
            _context = context;
            auth = new FirebaseAuthProvider(new FirebaseConfig(Environment.GetEnvironmentVariable("FirebaseAgriEnergy")));
        }

        public IList<FarmerCategory> GetDropdownOptions()
        {
            return _context.FarmerCategories.ToList();
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

                if (_context.Employees.Any(e => e.Id == currentUserId)) return View(); //https://stackoverflow.com/questions/23654057/check-if-record-exists-in-entity-framework

                if (currentUserId != null)
                {
                    HttpContext.Session.SetString("currentUser", currentUserId);
                    return RedirectToAction("Index", "Products");
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

            ViewBag.DropdownOptions = GetDropdownOptions();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string password, string firstName, string lastName, string adminAddress, 
            string phoneNumber, string email, DateOnly dateOfBirth, string gender, int categoryId)
        {
            try
            {
                await auth.CreateUserWithEmailAndPasswordAsync(email, password);

                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(email, password);
                string currentUserId = fbAuthLink.User.LocalId;
                HttpContext.Session.SetString("UserId", currentUserId);


                await CreateFarmer(currentUserId, firstName, lastName, adminAddress, phoneNumber, email, dateOfBirth, gender, categoryId);

                if (currentUserId != null)
                {
                    HttpContext.Session.SetString("currentUser", currentUserId);
                    return RedirectToAction("Index", "Products");
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

        //Adds the details of the new farmer to the farmer table in the database
        public async Task CreateFarmer(string id, string firstName, string lastName, string adminAddress, string phoneNumber, string email, DateOnly dateOfBirth, string gender, int categoryId)
        {
            Farmer farmer = new Farmer(id, firstName, lastName, adminAddress, phoneNumber, email, dateOfBirth, gender);

            if (categoryId != null) { farmer.FarmerCategoryId = categoryId; }

            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();

        }

    }
}
