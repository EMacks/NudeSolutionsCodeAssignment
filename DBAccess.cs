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
                        new Product(new Guid("b3b8dc14-c1c0-4ae7-aa46-e5d2b6705ef6"),"Electronics", "TV", 2000),
                        new Product(new Guid("61769512-02ef-48d8-8268-bec70fa94e18"),"Electronics", "Playstation", 400),
                        new Product(new Guid("9ead2a03-1e92-4079-a577-fe1e4096fa2e"),"Electronics", "Stereo", 1600),
                        new Product(new Guid("e6b48fc0-98b8-4432-b74c-b885ac49bcbc"),"Clothing", "Shirts", 1100),
                        new Product(new Guid("67981ccd-ed21-4e0c-aa86-77b575d1b41c"),"Clothing", "Jeans", 1100),
                        new Product(new Guid("4cdd4ca6-6d5b-4131-a1b6-36a00fdf13a6"),"Kitchen", "Pots and Pans", 3000),
                        new Product(new Guid("c671b528-e988-4b09-bdad-bc85653fa62f"),"Kitchen", "Flatware", 500),
                        new Product(new Guid("15112a63-97a0-480d-9dc2-41adcf16e11b"),"Kitchen", "Knife Set", 500),
                        new Product(new Guid("fcb83244-abf7-439a-b471-c5fe86af9ebc"),"Kitchen", "Misc", 1000)
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