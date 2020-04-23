using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSApp
{
    public class Product
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public enum Categories { Electronics, Clothing, Kitchen }
        public Categories Category { get; set; }

        public Product() { }
        public Product(Categories cat, string name, int value)
        {
            Category = cat;
            Name = name;
            Value = value;
        }
        internal static async Task<List<Product>> Fetch()
        {
            var l = new List<Product>()
            {
                new Product(Categories.Electronics, "TV", 2000),
                new Product(Categories.Electronics, "Playstation", 400),
                new Product(Categories.Electronics, "Stereo", 1600),
                new Product(Categories.Clothing, "Shirts", 1100),
                new Product(Categories.Clothing, "Jeans", 1100),
                new Product(Categories.Kitchen, "Pots and Pans", 3000),
                new Product(Categories.Kitchen, "Flatware", 500),
                new Product(Categories.Kitchen, "Knife Set", 500),
                new Product(Categories.Kitchen, "Misc", 1000)
            };
            return l;
        }

        internal Task Save()
        {
            throw new NotImplementedException();
        }

        internal Task Delete()
        {
            throw new NotImplementedException();
        }
    }
}
