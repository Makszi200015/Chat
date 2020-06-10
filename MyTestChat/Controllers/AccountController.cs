using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTestChat.Models;
using MyTestChat.Services;
using MyTestChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userman;
        private readonly SignInManager<User> _sigman;

        public AccountController(UserManager<User> usman, SignInManager<User> sigman)
        {
            _userman = usman;
            _sigman = sigman;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Login };
                var l = await _userman.FindByNameAsync(model.Login);
                if (l!=null && l.UserName == model.Login)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже зарегестрирован");
                }
                
                if (!user.Email.EndsWith("@gmail.com") )
                {
                   
                    ModelState.AddModelError("", "Your Email is not correct ");
                }


                var result = await _userman.CreateAsync(user, model.Password);
                
              
                if (result.Succeeded)
                {
                    var code = await _userman.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                      "ConfirmEmail",
                      "Account",
                      new { userid = user.Id, code = code },
                      protocol: HttpContext.Request.Scheme);
                    mailService mailService = new mailService();
                    await mailService.SendEmailAsyns(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
                    await _sigman.SignInAsync(user, false);
                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                    
                    
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code) 
        {
            if(userId == null || code == null) 
            {
                return View("Error");
            }
            var user = await _userman.FindByIdAsync(userId);
            if(user == null) 
            {
                return View("Error");
            }
            var result = await _userman.ConfirmEmailAsync(user, code);
            if (result.Succeeded)

                return RedirectToAction("MyPage", "Home");
            else
                return View("Error");


        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _sigman.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("MyPage", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _sigman.SignOutAsync();
            return RedirectToAction("Index1", "Home");
        }
    }
}