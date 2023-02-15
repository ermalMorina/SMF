using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.JwtToken;
using SpartanManageFootball.Persistence;
using SpartanManageFootball.Models;
using SpartanManageFootball.Application.Core;
using FluentValidation;

namespace SpartanManageFootball.Application.Login
{
    public class AuthCommand : IRequest<Result<UserDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthCommandValidator : AbstractValidator<AuthCommand>
    {
        public AuthCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Username shouldn't be empty").OverridePropertyName("error");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password shouldn't be empty").OverridePropertyName("error");
        }
    }
    public class AuthCommandHandler : IRequestHandler<AuthCommand, Result<UserDTO>>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<RegisterUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<RegisterUser> _signInManager;
        private readonly IIdentityService _identityService;
        private readonly SMFContext _context;
        public AuthCommandHandler(ITokenGenerator tokenGenerator,
            UserManager<RegisterUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<RegisterUser> signInManager,
            IIdentityService identityService,
            SMFContext context)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _identityService = identityService;
            _context = context;
        }

        public async Task<Result<UserDTO>> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.Email, request.Password);
           
            if (!result)
            {
                return Result<UserDTO>.Failure("Please verify your email or check your credentials");
            }
            
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.Email));

            string token = _tokenGenerator.GenerateJWTToken((fullName: fullName, userName: userName, roles: roles));
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            var rolesOfUser = await _userManager.GetRolesAsync(user);
            string role = rolesOfUser[0];

            return Result<UserDTO>.Success(new UserDTO()
            {
                UserName = fullName,
                Email = email,
                Token = token,
                Role = role,
                UserId = userId,
            }
            );
        }
    }
}