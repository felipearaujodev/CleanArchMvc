using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System.Data;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.99m, 1, "Image");
            action.Should()
                .NotThrow<DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 99.99m, 1, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");

        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 99.99m, 1, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");

        }

        [Fact]
        public void CreateProduct_WithNullNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, null, "Product Description", 99.99m, 1, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionShortDescription()
        {
            Action action = () => new Product(1, "Product Name", "Prod", 99.99m, 1, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid description, too short, minimum 5 characters");

        }

        [Fact]
        public void CreateProduct_WithNullDescriptionValue_DomainExceptionShortDescription()
        {
            Action action = () => new Product(1, "Product Name", null, 99.99m, 1, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_NegativePriceValue_DomainExceptionInvalidPrice()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -99.99m, 1, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price. Price is required");

        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_NegativeStockValue_DomainExceptionInvalidStock(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.99m, value, "Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid stock. Stock is required");

        }

        [Fact]
        public void CreateProduct_LongImageValue_DomainExceptionLongImage()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.99m, 1, "Image Loooooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnngggaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$4");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid image, too long, maximum 250 characters");

        }

        [Fact]
        public void CreateProduct_WithNullImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.99m, 1, null);
            action.Should()
                .NotThrow<DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_WithNullImageValue_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.99m, 1, null);
            action.Should()
                .NotThrow<NullReferenceException>();

        }
    }
}
