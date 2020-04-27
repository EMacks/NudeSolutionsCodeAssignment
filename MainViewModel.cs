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
        public string NewProductName { get; set; } = "";
        public string NewProductCategory { get; set; } = "";
        public int NewProductValue { get; set; } = 0;
        public void SaveNewProduct()
        {
            //validate input
            //clean input strings
            var p = new Product
            {
                Category = NewProductCategory,
                Name = NewProductName,
                Value = NewProductValue
            };
            p.Save();
            Refresh();
            ClearNewProduct();
        }

        public void ClearNewProduct()
        {
            NewProductCategory = "";
            NewProductName = "";
            NewProductValue = 0;
        }

        public int GrandTotal
        {
            get
            {
                return CategoryGroups.Sum(x => x.CategoryValue);
            }
        }
        public ObservableCollection<CategoryGroup> CategoryGroups { get; set; } = new ObservableCollection<CategoryGroup>();
        public void Refresh()
        {
            CategoryGroups.Clear();
            List<Product> l = Product.Fetch();
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
            public string CategoryName { get; set; }
            public int CategoryValue { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}
