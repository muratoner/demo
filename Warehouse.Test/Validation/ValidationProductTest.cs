using FluentValidation.TestHelper;
using Warehouse.Business.Validation;
using Xunit;

namespace Warehouse.Test.Validation
{
    public class ValidationProductTest
    {
        private readonly ValidationProduct _validation;

        #region Should Not Have Validation Error
        public ValidationProductTest()
        {
            _validation = new ValidationProduct();
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Name, "iPhone X");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Price_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Price, 15);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Company_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Company, "Apple");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Mail_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Mail, "info@muratoner.net");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Color_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Color, "olive");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Seller_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Seller, "Steve Jobs");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Currency_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Currency, "USD");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Category_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Category, "Phone");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Ean13_Is_Specified()
        {
            _validation.ShouldNotHaveValidationErrorFor(p => p.Ean13, "1112223334445");
        }
        #endregion

        #region Should Have Validation Error
        [Fact]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Name, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Price, 0);
        }

        [Fact]
        public void Should_Have_Error_When_Company_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Company, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Mail_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Mail, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Mail_Is_Wrong_Address()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Mail, "muratoner.net");
        }

        [Fact]
        public void Should_Have_Error_When_Color_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Mail, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Seller_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Seller, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Currency_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Currency, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Category_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Category, null as string);
        }

        [Fact]
        public void Should_Have_Error_When_Ean13_Is_Null()
        {
            _validation.ShouldHaveValidationErrorFor(p => p.Ean13, null as string);
        }
        #endregion
    }
}
