using MediatR;
using Microsoft.AspNetCore.Identity;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Persistence;
using System.Web;
using SpartanManageFootball.Models;
using SpartanManageFootball.Application.Core;
using FluentValidation;
using System.Text.RegularExpressions;

namespace SpartanManageFootball.Application.Admin
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public int IdentityNumber { get; set; }
            public DateTime Birthdate { get; set; }
            public string Password { get; set; }
            //public string RoleId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username shouldn't be empty").MinimumLength(10).WithMessage("Username must contain 10 or more characters").OverridePropertyName("error");
                RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Name shouldn't be empty").OverridePropertyName("error");
                RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number shouldn't be empty").OverridePropertyName("error")
                .MinimumLength(10).WithMessage("Phone Number must not be less than 10 characters.").OverridePropertyName("error")
                .MaximumLength(20).WithMessage("Phone Number must not exceed 50 characters.").OverridePropertyName("error");                
               
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email shouldn't be empty").EmailAddress().WithMessage("Invalid Email").OverridePropertyName("error");
                RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().WithMessage("Identity number shouldn't be empty").OverridePropertyName("error");
                RuleFor(x => x.Birthdate).NotNull().NotEmpty().WithMessage("Birthdate shouldn't be empty").OverridePropertyName("error");     
                RuleFor(request => request.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must contain 8 or more characters.").OverridePropertyName("error")
                .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.").OverridePropertyName("error")
                .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.").OverridePropertyName("error")
                .Matches(@"\d").WithMessage("Password must contain one or more digits.").OverridePropertyName("error")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Password must contain one or more special characters.").OverridePropertyName("error")
                .Matches("^[^£# “”]*$").WithMessage("Password must not contain the following characters £ # “” or spaces.").OverridePropertyName("error");
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly UserManager<RegisterUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IConfiguration _configuration;
            private readonly SMFContext _context;
            private readonly IEmailSender _emailSender;

            public Handler(
                UserManager<RegisterUser> userManager,
                RoleManager<IdentityRole> roleManager,
                SMFContext context,
                IEmailSender emailSender,
                IConfiguration configuration)
            {

                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
                _context = context;
                _emailSender = emailSender;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userExists = await _userManager.FindByNameAsync(request.Username);


                if (userExists != null)
                {
                    return Result<Unit>.Failure("This username is taken");
                }

                RegisterUser user = new()
                {
                    FullName = request.FullName,
                    PhoneNumber=request.PhoneNumber,
                    UserName = request.Username,  
                    Email = request.Email,
                    IdentityNumber = request.IdentityNumber,
                    BirthDate = request.Birthdate,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString(),
                };


                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return Result<Unit>.Failure("Something went wrong");
                }
                else
                {
                    var userFromDb = await _userManager.FindByEmailAsync(request.Email);
                    //Send email to user for confirming email
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

                    var uriBuilder = new UriBuilder(_configuration["ReturnPaths:ConfirmEmail"]);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                    query["token"] = token;
                    query["userid"] = userFromDb.Id;
                    uriBuilder.Query = query.ToString();

                    var urlString = uriBuilder.ToString();
                    var senderEmail = _configuration["ReturnPaths:SenderEmail"];

                    await _emailSender.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);
                    await _userManager.AddToRoleAsync(user, "admin");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

