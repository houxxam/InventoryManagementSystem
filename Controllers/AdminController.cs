using InvWebApp.Data;
using InvWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvWebApp.Controllers
{
	public class AdminController : Controller
	{
		private readonly AppDbContext _context;

		public AdminController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Login()
		{
			ClaimsPrincipal claimUser = HttpContext.User;
			if (claimUser.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(User UserLogin)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserLogin.UserName);

			string hashed = HashPassword(UserLogin.Password);

			if (user != null)
			{
				//if (UserLogin.UserName == user.UserName && UserLogin.Password == user.Password)

				if (UserLogin.UserName == user.UserName && VerifyPassword(UserLogin.Password, hashed))
				{
					List<Claim> claims = new List<Claim>
					{
					new Claim(ClaimTypes.NameIdentifier, UserLogin.UserName)
					};
					ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

					AuthenticationProperties properties = new AuthenticationProperties()
					{
						AllowRefresh = true,
						IsPersistent = UserLogin.KeepLoggedIn
					};
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimIdentity), properties);




					return RedirectToAction("Index", "Home");
				}
			}

			ViewData["ValidateMessage"] = "User Not Found";
			return View();
		}



		public IActionResult Register()
		{
			ViewBag._class = "alert alert-danger d-none";
			return View();
		}

		[HttpPost]
		public IActionResult Register(User model)
		{
			ViewBag._class = "alert alert-danger d-none";
			// check username if is exists allready
			var existingUser = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
			// check Server if is allready exists before update
			if (existingUser != null)
			{
				ModelState.AddModelError("UserName", "Username already exists.");
				ViewBag._class = "alert alert-danger d-block";
			}
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// Hash the password
			string hashedPassword = HashPassword(model.Password);

			// Create a user object
			var newUser = new User
			{
				UserName = model.UserName,
				// Other properties as needed
				Password = hashedPassword // Store the hashed password
			};

			// Call the user service to add the new user to the database
			_context.Users.Add(newUser);
			_context.SaveChanges();

			return RedirectToAction("Login", "Admin");
		}



		// Hashing Passwords using bcrypt
		public string HashPassword(string password)
		{
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
			return hashedPassword;
		}

		// Validating Passwords against hashed password
		public bool VerifyPassword(string password, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}

		// Generating JWT Token
		public string GenerateJwtToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("Sceinfo@01");

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    // Add more claims as needed
                }),
				Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}


	}
}
