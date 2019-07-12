using FluentValidation;
using Warehouse.Data.Entities;

namespace Warehouse.Api.Validation
{
    public class ValidationProduct : AbstractValidator<Product>
    {
        public ValidationProduct()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.Company).NotEmpty();
            RuleFor(p => p.Mail).NotEmpty();
            RuleFor(p => p.Color).NotEmpty();
            RuleFor(p => p.Seller).NotEmpty();
            RuleFor(p => p.Currency).NotEmpty();
            RuleFor(p => p.Category).NotEmpty();
            RuleFor(p => p.Ean13).NotEmpty();
        }
    }
}
