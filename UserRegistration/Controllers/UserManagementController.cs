using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetTableData()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var users = await userManager.Users
                .Where(x => x.Id != currentUserId)
                .OrderByDescending(x => x.LastLogin)
                .ToListAsync();

            return Json(users);
        }


        [HttpGet]
        public async Task<IActionResult> ManageUser()
        {
         
            return View();
        }
        public async Task<IActionResult> DeleteUser(string[] email)
        {
            if(email.Count()>0)
            {
                foreach (var id in email)
                {
                    var user = await userManager.FindByEmailAsync(id);
                    if (user != null)
                    {
                      var result = await userManager.DeleteAsync(user);
                        
                
                    }
                }
            }
            return RedirectToAction("ManageUser");
        }
        public async Task<IActionResult> BlockUser(string[] email)
        {
            if (email.Count() > 0)
            {
                foreach (var id in email)
                {
                    var user = await userManager.FindByEmailAsync(id);
                    if (user != null && user.LockoutEnd == null)
                    {
                        user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
                       await userManager.UpdateAsync(user);


                    }
                }
            }
            return View("ManageUser");
        }
        public async Task<IActionResult> UnBlockUser(string[] email)
        {
            if (email.Count() > 0)
            {
                foreach (var id in email)
                {
                    var user = await userManager.FindByEmailAsync(id);

                    if (user != null && await userManager.IsLockedOutAsync(user))
                    {
                        user.LockoutEnd = null;
                        await userManager.UpdateAsync(user);


                    }
                }
            }
            return View("ManageUser");
        }


    }
}
