using FluentValidation;
using Gl.Core.Shared.ModelViews.Customer;

namespace Gl.Manager.Validator.Customer
{
    public class EditCustomerValidator : AbstractValidator<EditCustomer>
    {
        public EditCustomerValidator()
        {
            Include(new NewCustomerValidator());
        }
    }
}