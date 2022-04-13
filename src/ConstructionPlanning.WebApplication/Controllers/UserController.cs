using ConstructionPlanning.WebApplication.Data;
using ConstructionPlanning.WebApplication.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Email, Forename = model.Forename, Surname = model.Surname };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User);
                    if (model.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Admin);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        AddErrorToModelState(error);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Forename = user.Forename,
                Surname = user.Surname,
                IsAdmin = userRoles.Any(x => x == Roles.Admin) ? true : false,
            };

            model.IsCurrentAdmin = model.IsAdmin && User.Identity.Name == user.UserName ? true : false;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Forename = model.Forename;
                    user.Surname = model.Surname;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await ProcessRoles(model, user);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            AddErrorToModelState(error);
                        }
                    }
                }
            }

            return View(model);
        }

        private async Task ProcessRoles(EditUserViewModel model, User user)
        {
            if (model.IsAdmin)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            }
            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Any(x => x == Roles.Admin))
                {
                    await _userManager.RemoveFromRoleAsync(user, Roles.Admin);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

        private void AddErrorToModelState(IdentityError error)
        {
            if (error.Code == "DuplicateUserName")
            {
                ModelState.AddModelError(string.Empty, "Пользователь с такой почтой уже существует.");
            }
            else if (error.Code == "PasswordTooShort")
            {
                ModelState.AddModelError(string.Empty, "Пароль должен содержать не меннее 6 символов.");
            }
            else if (error.Code == "PasswordRequiresNonAlphanumeric")
            {
                ModelState.AddModelError(string.Empty, "Пароль должен содержать хотя бы один небуквенно-цифровой символ.");
            }
            else if (error.Code == "PasswordRequiresDigit")
            {
                ModelState.AddModelError(string.Empty, "Пароль должен состоять как минимум из одной цифры ('0'-'9').");
            }
            else if (error.Code == "PasswordRequiresUpper")
            {
                ModelState.AddModelError(string.Empty, "Пароль должен иметь хотя бы один верхний регистр ('A' - 'Z').");
            }
            else if (error.Code == "PasswordRequiresLower")
            {
                ModelState.AddModelError(string.Empty, "Пароли должны иметь хотя бы одну строчную букву ('a' - 'z').");
            }
            else
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
