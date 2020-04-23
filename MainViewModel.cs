using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace NSApp
{
    public class MainViewModel
    {
        public Product NewProduct { get; set; } = new Product();

        public ObservableCollection<CategoryGroup> CategoryGroups { get; set; } = new ObservableCollection<CategoryGroup>();
        public async Task Refresh()
        {
            CategoryGroups.Clear();
            List<Product> l = await Product.Fetch();
            var g= GroupItemsByCategory(l);
            foreach (var cat in g)
                CategoryGroups.Add(cat);
        }
        private List<CategoryGroup> GroupItemsByCategory(List<Product> products)
        {
            var groups = products.GroupBy(x => x.Category)
                 .Select(x => new CategoryGroup
                 {
                     CategoryName = x.First().Category,
                     CategoryValue = x.Sum(y => y.Value),
                     Products = x.ToList()
                 }).ToList();
            return groups;
        }

        public class CategoryGroup
        {
            public Product.Categories CategoryName { get; set; }
            public int CategoryValue { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}
