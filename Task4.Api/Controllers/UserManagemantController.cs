using Microsoft.AspNetCore.Mvc;
using Task4.Service.Interfaces;

namespace Task4.Api.Controllers;
public class UserManagemantController : Controller
{
    private IUserService userService;
    private IAuthorizationService authorization;

    public UserManagemantController(IUserService userService, IAuthorizationService authorization)
    {
        this.userService = userService;
        this.authorization = authorization;
    }

    // GET: UserManagement
    public async Task<ActionResult> Index()
    {
        var authResult = await authorization.AuthorizeAsync();

        if (!authResult)
            return RedirectToAction("Login", "Account");

        var users = await this.userService.GetAllAsync();

        return View(users);
    }

    // POST: BlockUsers
    [HttpPost]
    public async Task<ActionResult> BlockUsers(int[] userIds)
    {
        var authResult = await authorization.AuthorizeAsync();

        if (!authResult)
            return RedirectToAction("Login", "Account");

        var token = Request.Cookies["AuthToken"];
        foreach (var userId in userIds)
            await this.userService.BlockAsync(userId);

        return RedirectToAction("Index", "UserManagement");
    }

    // POST: UnblockUsers
    [HttpPost]
    public async Task<ActionResult> UnblockUsers(int[] userIds)
    {
        var authResult = await authorization.AuthorizeAsync();

        if (!authResult)
            return RedirectToAction("Login", "Account");

        foreach (var userId in userIds)
            await this.userService.UnBlockAsync(userId);

        return RedirectToAction("Index", "UserManagement");
    }

    // POST: DeleteUsers
    [HttpPost]
    public async Task<ActionResult> DeleteUsers(int[] userIds)
    {
        var authResult = await authorization.AuthorizeAsync();

        if (!authResult)
            return RedirectToAction("Login", "Account");

        foreach (var userId in userIds)
            await this.userService.DeleteAsync(userId);

        return RedirectToAction("Index", "UserManagement");
    }
}
