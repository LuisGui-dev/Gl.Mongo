using System;

namespace Gl.Core.Shared.ModelViews.Customer
{
    public class EditCustomer : NewCustomer
    {
        public string Id { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}