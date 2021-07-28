using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Business.Entities;
using WildBunch.Business.Services;
using WildBunch.Models;

namespace WildBunch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<WildBunchUser> _signInManager;
        private readonly UserManager<WildBunchUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly IWildBunchTokenProvider _tokenProvider;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<WildBunchUser> userManager,
            SignInManager<WildBunchUser> signInManager,
            ILogger<AccountController> logger,
            //IEmailSender emailSender
            IWildBunchTokenProvider tokenProvider,
            IUserService userService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _tokenProvider = tokenProvider;
            _userService = userService;
        }

        public class RegisterUserViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class LoginUserViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UiResponse>> Register([FromBody]RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = new UiResponse();
            
            try
            {
                var user = new WildBunchUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    response.Success = true;
                    response.Messages.Add("Registration Successful!");
                    response.Result = new { UserId = user.Id };

                    return response;
                }

                response.Messages.AddRange( result.Errors.Select(e => e.Description).ToList() );

            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Messages.Add( ex.Message );
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UiResponse>> Login([FromBody] LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = new UiResponse();

            try
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    response.Messages.Add("Logged in successfully");
                    response.Success = true;
                } 
                else if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    response.Messages.Add("User account locked out.");
                    return response;
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt.");
                    response.Messages.Add("Invalid Login Attempt");
                    return response;
                }

                var user = await _userManager.FindByNameAsync(model.Email);

                var token = _tokenProvider.GenerateToken(user);

                var activeGameId = await _userService.GetActiveGameIdAsync(user.Id);

                response.Result = new { UserId = user.Id, Email = user.Email, RememberMe = model.RememberMe, Token = token, ActiveGameId = activeGameId };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Messages.Add( ex.Message );
            }

            return response;
        }
    }
}
