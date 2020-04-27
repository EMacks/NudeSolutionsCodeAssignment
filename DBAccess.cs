using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSApp
{
    public class DBAccess
    {
        public static void Init()
        {
            try
            {
                DBAccess.ExecuteScalar("SELECT SQLITE_VERSION()");
                if (!DoesTableExist("Product"))
                {
                    ExecuteNonQuery(@"CREATE TABLE [Product] ([Id] uniqueidentifier NOT NULL, [Name] text NOT NULL, [Value] bigint NOT NULL, [Category] text NOT NULL, CONSTRAINT [sqlite_autoindex_Product_1] PRIMARY KEY ([Id]));");
                    var l = new List<Product>()
                    {
                        new Product(Guid.NewGuid(),"Electronics", "TV", 2000),
                        new Product(Guid.NewGuid(),"Electronics", "Playstation", 400),
                        new Product(Guid.NewGuid(),"Electronics", "Stereo", 1600),
                        new Product(Guid.NewGuid(),"Clothing", "Shirts", 1100),
                        new Product(Guid.NewGuid(),"Clothing", "Jeans", 1100),
                        new Product(Guid.NewGuid(),"Kitchen", "Pots and Pans", 3000),
                        new Product(Guid.NewGuid(),"Kitchen", "Flatware", 500),
                        new Product(Guid.NewGuid(),"Kitchen", "Knife Set", 500),
                        new Product(Guid.NewGuid(),"Kitchen", "Misc", 1000)
                    };
                    foreach (var i in l)
                    {
                        i.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                System.Diagnostics.Debugger.Break();
            }
        }
        private static bool DoesTableExist(string tableName)
        {
            var x=(long) ExecuteScalar($"SELECT count(name) FROM sqlite_master WHERE type='table' AND name='{tableName}';");
            return x > 0;
        }
      
        private static SQLiteConnection GetConnection()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;//@"C:\Users\Erin\Documents\Projects\NSApp";
            string cs = $@"URI=file:{dir}\NSApp.db";

            var con = new SQLiteConnection(cs);
            con.Open();

            return con;
        }
        public static SQLiteDataReader ExecuteQuery(string query)
        {
            SQLiteDataReader dr = null;
            var conn = GetConnection();
            var cmd = new SQLiteCommand(conn);

            cmd.CommandText = query;
            dr = cmd.ExecuteReader();

            return dr;
        }

        internal static async Task<DbDataReader> ExecuteQueryAsync(string query)
        {
            DbDataReader dr = null;
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = query;
                    dr = await cmd.ExecuteReaderAsync();
                }
            }
            return dr;
        }

        public static object ExecuteScalar(string query)
        {
            object i = 0;
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = query;
                    i = cmd.ExecuteScalar();
                }
            }
            return i;
        }
        public static void ExecuteNonQuery(string query)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}