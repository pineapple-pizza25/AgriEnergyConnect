using Agri_Energy_Connect.Models;

namespace Agri_Energy_Connect.Services
{
    public class NavBarService
    {
        public List<NavBarItem> GetNavBarItems(string userName)
        {
            // Dummy implementation: Replace with actual logic
            var items = new List<NavBarItem>
        {
            new NavBarItem { Text = "Home", Action = "Index", Controller = "Home" },
            new NavBarItem { Text = "I'm a Farmer", Action = "Login", Controller = "Farmer" }
        };

            if (userName == "farmer")
            {
                items.Add(new NavBarItem { Text = "Admin", Action = "Index", Controller = "Admin" });
            }

            // Add more logic based on roles or other attributes

            return items;
        }
    }
}
