using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebApp.Models;
using WebApp.Service.Interfaces;
using WebApp.ViewModels;
using static WebApp.Helpers.Helper;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }
        #region Login
        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.Users.Where(m => m.PhoneNumber == loginVM.PhoneNumber).FirstOrDefaultAsync();

            if (user is null)
            {
                ModelState.AddModelError("", "PhoneNumber or Password is Wrong");
                return View(loginVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "PhoneNumber or Password is Wrong");
                return View(loginVM);
            }

            var roles = await _userManager.GetRolesAsync(user); // Kullanıcının rollerini alın
            string token = _tokenService.GenerateJwtToken(user, roles.ToList());

            // Diğer işlemler
            // ...

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var rolesClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (rolesClaim == null || rolesClaim.Value.Contains("Admin") || rolesClaim.Value.Contains("Mentor"))
            {
                // Token'i HTTP Only Cookie içine saklama
                Response.Cookies.Append("jwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(1) // Token süresine uygun olarak ayarlayabilirsiniz.
                });
                // Sadece Admin rolüne sahip kullanıcılara izin verilen kodlar
                return RedirectToAction("Index", "Home", new { area = "AdminArea" });
            }
            else if (rolesClaim == null || rolesClaim.Value.Contains("User"))
            {
                // User rolüne sahip kullanıcılara izin verilmiyor, geri dönüş yap ve mesaj göster
                // Cookie'den JWT tokenini sil
                Response.Cookies.Delete("jwtToken");
                await _signInManager.SignOutAsync();
                ViewBag.ErrorMessage = "Bu sayfaya giriş izniniz yok.";
                ModelState.AddModelError("", "Bu səhifəyə giriş icazəniz yoxdur.");
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "AdminArea" });
        }

        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            // Cookie'den JWT tokenini sil
            Response.Cookies.Delete("jwtToken");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        #endregion

   
        //[Route("CreateRole")]
        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}


        //public IActionResult Register()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterWM registerWM)
        //{
        //    AppUser appUser = new AppUser() 
        //    { 
        //        UserName=registerWM.Username,
        //        Email= registerWM.Email, 
        //        PhoneNumber=registerWM.PhoneNumber
        //    };
        //    var result = await _userManager.CreateAsync(appUser, registerWM.Password);
        //    if (!result.Succeeded)
        //    {
        //        throw new System.Exception("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        //    }

        //    await _userManager.AddToRoleAsync(appUser, "User");
        //    var roles = await _userManager.GetRolesAsync(appUser);
        //    return View(appUser);
        //}
    }

}