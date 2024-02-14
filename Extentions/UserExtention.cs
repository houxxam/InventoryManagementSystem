using InvWebApp.Data;
using System.Security.Claims;

namespace InvWebApp.Extentions
{
    public static class UserExtention
    {

        public static int getUserId(this ClaimsPrincipal user, AppDbContext _context)
        {
            string userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.Users.Where(u => u.UserName == userId).FirstOrDefault().Id;
            return currentUser;
        }
    }
}
