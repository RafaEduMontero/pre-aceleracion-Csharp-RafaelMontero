using ChallengeAlkemyDisney.Interfaces;
using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.ViewModels.Auth.Login;
using ChallengeAlkemyDisney.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager,IMailService mailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _mailService = mailService;
        }
        //Registro
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestViewModel register)
        {
            //Revisar si existe el ususario
            var userExist = await _userManager.FindByNameAsync(register.Username);
            //Si existe devolver error
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            //Si existe lo registramos
            var user = new User
            {
                UserName = register.Username,
                Email = register.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "Error",
                    Message = $"User Creation Failed! Errors: {string.Join(", ", result.Errors.Select(x => x.Description))}"
                });
            }

            await _mailService.SendMail(user);

            return Ok(new
            {
                status = "Success",
                Message = "User created successfuly"
            });
        }

        //Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestViewModel login)
        {
            //Chequear si el ususario existe
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(login.Username);

                if (currentUser.IsActive)
                {
                    //generar el token
                    return Ok(await GetToken(currentUser));
                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                status = "Error",
                Message = $"El ususario {login.Username} no está autorizado"
            });
        }

        private async Task<LoginResponseViewModel> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(u => new Claim(ClaimTypes.Role, u)));

            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

            var token = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
            );

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
