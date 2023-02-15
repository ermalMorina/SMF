using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Application;
using SpartanManageFootball.Application.Admin;
using SpartanManageFootball.Application.Login;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Models;
namespace SpartanManageFootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly UserManager<RegisterUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<RegisterUser> _signInManager;
        private readonly IEmailSender _emailsender;
        private readonly IIdentityService _identityServices;

        public UserController(IMediator mediator,
            UserManager<RegisterUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<RegisterUser> signInManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            IIdentityService identityServices
            )
        {
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailsender = emailSender;
            _identityServices = identityServices;
        } 
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Create.Command command)
        {

            return HandleResult(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
           return HandleResult(await _mediator.Send(command));
        }

        [HttpGet("confirmemail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return this.Redirect("http://localhost:3000/confirmemail");
            }

            return BadRequest();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await _identityServices.ForgetPasswordAsync(email);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityServices.ResetPasswordAsync(model);

                if (result.IsSuccess)
                {
                    return this.Redirect("http://localhost:3000/reset-password");
                }
              
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpGet("GetUserDetails")]
        [ProducesDefaultResponseType(typeof(List<UserDTO>))]
        public async Task<IActionResult> GetUserDetailsAsync()
        {
            var result = await _identityServices.GetUsersDetailsAsync();
          
            if (result == null)

            {
                throw new Exception("Something went wrong");
            }
           
            return Ok(result);
        }
        
        [HttpPut("EditUserRoles")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<ActionResult> EditUserRoles(UpdateUserRolesCommand command)
        {
            var result = await _mediator.Send(command);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpDelete("Delete/{userId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = userId });
            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityServices.ChangePasswordAsync(model);

                if (result.IsSuccess)
                {
                    return Ok("Password has been changed Successfully");
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = _signInManager.SignOutAsync();

            if (result.IsCompletedSuccessfully)
            {
                return this.Redirect("http://localhost:3000/login");
            }

            return BadRequest();
        }
    }
}