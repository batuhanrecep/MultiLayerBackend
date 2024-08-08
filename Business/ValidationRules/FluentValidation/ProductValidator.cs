using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // RuleFor(p => p.ProductName).NotEmpty().Length(2, 30);  - It can be written like this but, it's better to write validations one by one.
            // With this way, we can write messages for every one of them. (Its just example)
            // Also, these rules should be about the entities. (Do not try to connect these with db or mix the business codes with these) 

            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product cannot be empty"); //MAGIC STRING - Add message class for these
            RuleFor(p => p.ProductName).Length(2, 30);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartsWithA);
            //...
        }

        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
