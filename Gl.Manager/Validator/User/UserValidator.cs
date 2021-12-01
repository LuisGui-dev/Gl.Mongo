using FluentValidation;

namespace Gl.Manager.Validator.User
{
    public class UserValidator : AbstractValidator<Core.Domain.User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Login).NotNull().NotEmpty().MinimumLength(10).MaximumLength(30);
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6).MaximumLength(15);
        }
    }
}