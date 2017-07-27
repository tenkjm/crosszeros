using CrossZeros.Models;
using CrossZeros.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrossZeros.Controllers.Auth
{
    public class AuthController:Controller
    {
        private SignInManager<CrossZeroUser> _signInManager;
        UserManager<CrossZeroUser> _userManager;

        public AuthController(SignInManager<CrossZeroUser> signInManager, UserManager<CrossZeroUser> userManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("Auth/signup")]
        public async Task<JsonResult> CreateUser([FromBody] UserCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(vm.Email) == null)
                {
                    //Add the user
                    var newUser = new CrossZeroUser()
                    {
                        UserName = vm.Name,
                        Email = vm.Email,
                        NickName = vm.NickName                        
                    };

                    var result = await _userManager.CreateAsync(newUser, vm.Password);
                    if (result.Succeeded)
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json("User Creation is OK");
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Json("User Creation Failed");
                    }

                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    return Json("User already exists");
                }

            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Invalid create model");
            }
        }


        [HttpPost("Auth/login")]
        public async Task<JsonResult> Login([FromBody] LoginViewModel vm )
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                    vm.Username,
                    vm.Password,
                    true, false);

                if (signInResult.Succeeded)
                {
                    Response.StatusCode = (int)HttpStatusCode.Accepted;
                    return Json("Login is ok");
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    return Json("Invalid login");
                }
            }
             else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error in login model");
            }
        }

        public async Task<JsonResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            return Json("Logout is ok");
        }
    }
}
