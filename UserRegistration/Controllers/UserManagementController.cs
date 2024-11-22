using Elfie.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRegistration.Migrations;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserManagementController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetTableData()
        {
            //var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var users = await userManager.Users
                .OrderByDescending(x => x.LastLogin)
                .ToListAsync();

            return Json(users);
        }


        [HttpGet]
        public async Task<IActionResult> ManageUser()
        {
         
            return View();
        }
        [HttpPost]
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
        [HttpPost]
        public async Task<IActionResult> BlockUser(string[] email)
        {
            if (email.Length > 0)
            {
                foreach (var id in email)
                {
                    var user = await userManager.FindByEmailAsync(id); 
                    if (user != null)
                    {
                        
                        if (user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.UtcNow)
                        {
                            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
                            
                            await userManager.UpdateAsync(user);
                            
                        }


                        var currentUserEmail = userManager.GetUserName(User);
                        var currentUser = await userManager.FindByEmailAsync(currentUserEmail);
                        if (currentUser != null && currentUser.LockoutEnd > DateTimeOffset.UtcNow)
                        {
                            await signInManager.SignOutAsync();
                            
                        }

                    }
                }
            }
            Response.Redirect(Request.Path);
            return RedirectToAction("Index");
        }


        [HttpPost]
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
