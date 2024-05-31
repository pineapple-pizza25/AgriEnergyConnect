namespace Agri_Energy_Connect.Utils
{
    public class UserChecker
    {
        //HttpContext _context;

        public bool IsEmployee(string userId, string role)
        {
            
            if (userId == null || role == null) {  return false; }
            if (role == "farmer") { return false; }
            return true;
        }

        public bool IsFarmer(string userId, string role)
        {

            if (userId == null || role == null) { return false; }
            if (role == "employee") { return false; }
            return true;
        }

    }
}
