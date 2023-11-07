using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidadeDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidadeDomain(name, description, price, stock, image);
        }

        private void ValidadeDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            DomainExceptionValidation.When(String.IsNullOrEmpty(description), "Invalid description. Description is required");
            DomainExceptionValidation.When(description.Length < 5, "Invalid description, too short, minimum 5 characters");
            DomainExceptionValidation.When(price < 0, "Invalid price. Price is required");
            DomainExceptionValidation.When(stock < 0, "Invalid stock. Stock is required");
            DomainExceptionValidation.When(image.Length > 250, "Invalid image, too long, maximum 250 characters");

            Name = name;
            Description = description;
            Price = price; 
            Stock = stock;
            Image = image;
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidadeDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
