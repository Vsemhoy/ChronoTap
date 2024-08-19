using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Database;


namespace ChronoTap.Storage.Database
{
    class DatabaseService
    {
        /** Sync version of connection */
        public static SQLiteConnection DBS = new SQLiteConnection(StorageConfig.DatabasePath);
        /** Async version of connection */
        public static SQLiteAsyncConnection DB = new SQLiteAsyncConnection(StorageConfig.DatabasePath);

        public static string GenerateHashId(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString().Substring(0, 25);
            }
        }


        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "abcdefghjklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789&$#@";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        /// <summary>
        /// Create Database tables before App starts
        /// </summary>
        public static void INITIALZE_TABLES()
        {
            try
            {
                //DBS.DropTable<ChronoEvent>();
                DBS.CreateTable<EventType>();
                DBS.CreateTable<ChronoEvent>();
                DBS.CreateTable<EventCategory>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //DBS.CreateTable<EventGroup>();
            //DBS.CreateTable<EventGroupContainer>();
            //DBS.CreateTable<Ev>();
        }
    }

}
