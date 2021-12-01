using FluentValidation;
using Gl.Core.Shared.ModelViews.Customer;

namespace Gl.Manager.Validator.Customer
{
    public class NewCustomerValidator : AbstractValidator<NewCustomer>
    {
        public NewCustomerValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(5).MaximumLength(150);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).NotNull().NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty().NotNull();
        }
    }
}