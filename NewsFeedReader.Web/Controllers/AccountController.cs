using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsFeedReader.Domain.ApiModels.Request;
using NewsFeedReader.Domain.ApiModels.Response;
using NewsFeedReader.Domain.Models;
using NewsFeedReader.Logic.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeedReader.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Generates the Bearer token for the user in the request
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {
            var user = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("message", "Username or password are incorrect.");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("message", "Username or password are incorrect.");
                return BadRequest(ModelState);
            }
            else
            {
                var jsonWebToken = _tokenService.GenerateJwtToken(user);

                var requestedAt = DateTime.UtcNow;

                var returnModel = _mapper.Map<LoggedInUserDto>(user);
                returnModel.LastLoginDate = user.LastLoginDate;
                user.LastLoginDate = requestedAt;
                await _userManager.UpdateAsync(user);
                returnModel.Token = jsonWebToken.Token;
                returnModel.Expires = jsonWebToken.Expiration - requestedAt.Ticks;
                return Ok(returnModel);
            }
        }

        /// <summary>
        /// Registers a new user by receiving a username and a password
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto model)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);

            if (appUser != null)
            {
                throw new ArgumentException("User already exists.");
            }
            appUser = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);

                return Ok(_tokenService.GenerateJwtToken(appUser));
            }
            else
            {
                throw new ArgumentException(result.Errors.First().Description);
            }
        }
    }
}