using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSApp
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Value { get; set; }
        public string Category { get; set; }

        public Product() { }
        public Product(Guid id,string category, string name, int value)
        {
            Id = id;
            Category = category;
            Name = name;
            Value = value;
        }
        internal static List<Product> Fetch()
        {
            var l = new List<Product>();
            using (var dr =  DBAccess.ExecuteQuery("SELECT Id, Category, Name, Value FROM Product"))
            {
                while (dr.Read())
                {
                    l.Add(new Product(dr.GetGuid(0),dr.GetString(1), dr.GetString(2), dr.GetInt32(3)));
                }
            }
            return l;
            //return GetTestValues();
        }

        internal void Save()
        {
            //validate
            string query;
            if (this.Exists())
            {
                query = $@"UPDATE [Product] SET [Id] = '{Id}',[Name] = '{Name}',[Value] = {Value},[Category] = '{Category}' WHERE Id='{Id}';";
            }
            else
            {
                query = $@"INSERT INTO [Product] (Id, Name, Value, Category) VALUES ('{Id}','{Name}',{Value},'{Category}');";
            }
            DBAccess.ExecuteNonQuery(query);

        }

        internal void Clear()
        {
            Id = Guid.NewGuid();
            Category = "";
            Name = "";
            Value = 0;
        }

        private bool Exists()
        {
            var count = DBAccess.ExecuteScalar($"SELECT Count(id) FROM Product where Id='{Id}';");
            return (long)count > 0;
        }
        internal void Delete()
        {
            string query = $@"DELETE FROM Product WHERE Id='{Id}';";
            DBAccess.ExecuteNonQuery(query);
        }
    }
}
