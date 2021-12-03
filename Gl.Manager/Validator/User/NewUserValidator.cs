using FluentValidation;
using Gl.Core.Shared.ModelInput.User;
using Gl.Core.Shared.ModelViews.User;

namespace Gl.Manager.Validator.User
{
    public class NewUserValidator : AbstractValidator<NewUser>
    {
        public NewUserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(10).MaximumLength(30);
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6).MaximumLength(15);
        }
    }
}