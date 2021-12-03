using System;

namespace Gl.Core.Shared.ModelInput.Customer
{
    public class EditCustomer : NewCustomer
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}