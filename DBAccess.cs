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
        private static SQLiteConnection GetConnection()
        {
            var dir = @"C:\Users\Erin\Documents\Projects\NSApp";
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