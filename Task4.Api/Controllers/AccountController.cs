using Microsoft.AspNetCore.Mvc;
using Task4.Service.DTOs;
using Task4.Service.Interfaces;

namespace Task4.Api.Controllers;

public class AccountController : Controller
{
    private IUserService userService;
    private IConfiguration configuration;
    private IAuthenticationService authenticationService;

    public AccountController(IAuthenticationService authenticationService, IUserService userService, IConfiguration configuration)
    {
        this.userService = userService;
        this.configuration = configuration;
        this.authenticationService = authenticationService;
    }
    public IActionResult Index()
    {
        return View();
    }


    // POST: Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(UserForCreationDto creationDto)
    {
        if (ModelState.IsValid)
        {
            var createdUser = await this.userService.AddAsync(creationDto);

            if (createdUser is null)
            {
                ModelState.AddModelError("", "This email is already used.");
                return View(creationDto);
            }

            return RedirectToAction("Login");
        }

        return View(creationDto);
    }

    // GET: Login
    public ActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(UserForResultDto dto)
    {
        if (ModelState.IsValid)
        {
            string token = await this.authenticationService.AuthenticateAsync(dto);

            if (token is null)
            {
                ModelState.AddModelError("", "Invalid password or email.");
                return View(dto);
            }
            else if (token == "b")
            {
                ModelState.AddModelError("", "Your account was blocked!");
                return View(dto);
            }

            Response.Cookies.Append("Token", token);

            return RedirectToAction("Index", "UserManagement");
        }

        return View(dto);
    }
}
