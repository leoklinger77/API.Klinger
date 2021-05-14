using Api.Klinger.Extensions;
using Api.Klinger.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Klinger.Controllers
{
    [Route("Api/Account")]
    public class AuthenticationController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthenticationController(INotifier notifier, IMapper mapper,
                                        UserManager<IdentityUser> userManager,
                                        SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings) : base(notifier, mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                
                return CustomResponse(registerUser);
            }

            foreach (var error in result.Errors)
            {
                ErrorNotifier(error.Description);
            }
            return CustomResponse(registerUser);
        }
        [HttpPost("AccessAccount")]
        public async Task<ActionResult> AccessAccount(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
            if (result.Succeeded)
            {                
                return CustomResponse(await GeneratorToken(""));
            }
            else if (result.IsLockedOut)
            {
                ErrorNotifier("Usuario temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }
            ErrorNotifier("Usuario ou enha incorretos");
            return CustomResponse();
        }

        private async Task<string> GeneratorToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
