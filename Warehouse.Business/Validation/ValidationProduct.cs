using FluentValidation;
using Warehouse.Data.Entities;

namespace Warehouse.Business.Validation
{
    /// <summary>
    /// All validation rules defined in class for Product entity file.
    /// </summary>
    public class ValidationProduct : AbstractValidator<Product>
    {
        public ValidationProduct()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Mail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Company).NotEmpty();
            RuleFor(p => p.Color).NotEmpty();
            RuleFor(p => p.Seller).NotEmpty();
            RuleFor(p => p.Currency).NotEmpty();
            RuleFor(p => p.Category).NotEmpty();
            RuleFor(p => p.Ean13).NotEmpty();
        }
    }
}
