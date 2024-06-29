using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
        }

        public string Name
        {
            get => name;

            private set
            {
                Validator.ValidateString(value, nameof(Name));

                name = value;
            }
        }

        public decimal Money
        {
            get => money;

            private set
            {
                Validator.ValidateMoney(value);

                money = value;
            }
        }

        public IReadOnlyCollection<Product> Products
            => products.AsReadOnly();

        public bool BuyProduct(Product product)
        {
            if(Money >= product.Cost)
            {
                products.Add(product);
                Money -= product.Cost;

                return true;
            }

            return false;
        }
    }
}
