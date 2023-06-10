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
}
