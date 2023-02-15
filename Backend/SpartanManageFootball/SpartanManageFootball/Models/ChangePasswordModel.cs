using FluentValidation;

namespace SpartanManageFootball.Models
{
    public class ChangePasswordModel
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username shouldn't be empty").OverridePropertyName("errors");
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current Password shouldn't be empty").OverridePropertyName("errors");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password shouldn't be empty").OverridePropertyName("errors");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password shouldn't be empty").OverridePropertyName("errors");
        }
    }




}
