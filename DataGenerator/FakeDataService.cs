// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sigmade.DataGenerator.Model;
using Sigmade.Domain;
using Sigmade.Domain.Models;
using Sigmade.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigmade.DataGenerator
{
    public class FakeDataService
    {
        private readonly ApplicationDbContext _db;
        private static readonly Random _random = new();
        private int NewAccountNumber;
        private static int[] UserIds;
        private static int[] ContragentIds;

        public FakeDataService(ApplicationDbContext db)
        {
            _db = db;
        }

        private static User NewUser()
        {
            var userTypes = Enum.GetValues(typeof(UserType));

            return new()
            {
                FullName = $"{RandomArrayValue(Dictionaries.FirstNames)} {RandomArrayValue(Dictionaries.LastNames)}",
                Login = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", ""),
                Password = _random.Next(1000, 99999).ToString(),
                UserType = (UserType)userTypes.GetValue(_random.Next(userTypes.Length))
            };
        }

        private UserContragent NewUserContragent()
        {
            var userId = UserIds[_random.Next(0, UserIds.Length)];
            var city = RandomArrayValue(Dictionaries.Cities);

            if (NewAccountNumber == 0)
            {
                var lastAccountNumber = _db.UserContragents
                .OrderByDescending(l => l.AccountNumber)
                .Select(s => s.AccountNumber)
                .FirstOrDefault();

                var s = lastAccountNumber is null ? lastAccountNumber = "0" : lastAccountNumber;

                NewAccountNumber = Int32.Parse(s) + 1;
            }

            _ = NewAccountNumber++;

            return new()
            {
                UserId = userId,
                City = city,
                Name = $"{RandomArrayValue(Dictionaries.ContragentTypes)} '" +
                       $"{city} " +
                       $"{NewAccountNumber}'",
                AccountNumber = NewAccountNumber.ToString().PadLeft(6, '0')
            };
        }

        static OrderHistory NewOrderHistory()
        {
            var contragentId = ContragentIds[_random.Next(0, ContragentIds.Length)];
            var product = Dictionaries.Products.ElementAt(_random.Next(0, Dictionaries.Products.Count - 3));

            return new()
            {
                UserContragentId = contragentId,
                VendorCode = product.Key.ToString().PadLeft(6, '0'),
                Brand = product.Value,
                Count = _random.Next(1, 50)
            };
        }

        static SearchHistory NewSearchHistory()
        {
            var contragentId = ContragentIds[_random.Next(0, ContragentIds.Length)];
            var product = Dictionaries.Products.ElementAt(_random.Next(0, Dictionaries.Products.Count));

            return new()
            {
                UserContragentId = contragentId,
                VendorCode = product.Key.ToString().PadLeft(6, '0'),
                Brand = product.Value,
                UserIpAddress = $"3.15.189.{_random.Next(1, 255)}"
            };
        }

        public async Task AddUser(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _db.Users.Add(NewUser());
            }

            await _db.SaveChangesAsync();
        }

        public async Task AddUserContragent(int count)
        {
            UserIds = _db.Users.Select(u => u.Id).ToArray();

            if (UserIds.Length == 0)
            {
                throw new Exception("Users not found");
            }
            for (int i = 0; i < count; i++)
            {
                _db.UserContragents.Add(NewUserContragent());
            }

            await _db.SaveChangesAsync();
        }

        public async Task AddOrderHistory(int count)
        {
            ContragentIds = _db.UserContragents.Select(u => u.Id).ToArray();

            if (ContragentIds.Length == 0)
            {
                throw new Exception("Contragents not found");
            }
            for (int i = 0; i < count; i++)
            {
                _db.OrderHistories.Add(NewOrderHistory());
            }

            await _db.SaveChangesAsync();
        }

        //EF 00:48 300К  800mb memory
        public async Task<Diagnostic> AddSearchHistory(int count)
        {
            var diagnostic = new Diagnostic();
            var start = DateTime.Now;
            diagnostic.MemoryBeforeMb = GC.GetTotalMemory(false) / 1024 / 1024;

            ContragentIds = _db.UserContragents.Select(u => u.Id).ToArray();
            var searchHistoryList = new List<SearchHistory>(300000);
            diagnostic.GenerationBefore = GC.GetGeneration(searchHistoryList);

            if (ContragentIds.Length == 0)
            {
                throw new Exception("Contragents not found");
            }
            for (int i = 0; i < count; i++)
            {
                searchHistoryList.Add(NewSearchHistory());
            }

            _db.SearchHistories.AddRange(searchHistoryList);
            await _db.SaveChangesAsync();

            var end = DateTime.Now;
            var diff = end - start;
            diagnostic.MemoryAfterMb = GC.GetTotalMemory(false) / 1024 / 1024;
            searchHistoryList = null;
            _db.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            diagnostic.MemoryAfterGcMb = GC.GetTotalMemory(false) / 1024 / 1024;
            //diagnostic.GenerationAfter = GC.GetGeneration(searchHistoryList);
            diagnostic.TimeExecuted = diff;

            return diagnostic;
        }

        //DAPPER 2:32 - 3:32 //500- 1:22 //5000-1:13 10000-1:16  500 mb memory
        //public async Task<TimeSpan> AddSearchHistory(int count)
        //{
        //    ContragentIds = _db.UserContragents.Select(u => u.Id).ToArray();
        //    TimeSpan diff;
        //    var start = DateTime.Now;

        //    if (ContragentIds.Length == 0)
        //    {
        //        throw new Exception("Contragents not found");
        //    }

        //    var list = new List<SearchHistory>(300000);

        //    for (int i = 0; i < count; i++)
        //    {
        //        list.Add(NewSearchHistory());
        //    }

        //    var total = list.Count;
        //    var packageSize = 50000;

        //    do
        //    {
        //        var package = list
        //            .Skip(list.Count - total)
        //            .Take(packageSize);

        //        var sb = new StringBuilder("");

        //        foreach (var i in package)
        //        {
        //            sb.Append($"INSERT INTO[dbo].[SearchHistories](UserContragentId, VendorCode, Brand, UserIpAddress)  VALUES({i.UserContragentId}, '{i.VendorCode}', '{i.Brand}', '{i.UserIpAddress}') ");
        //        }

        //        total -= packageSize;

        //        using (IDbConnection db = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=AutoPartsDB;Trusted_Connection=True;"))
        //        {
        //            await db.ExecuteAsync(sb.ToString());
        //        }

        //    } while (total > 0);
        //    var end = DateTime.Now;
        //    diff = end - start;
        //    return diff;
        //}

        public async Task ClearAllTables()
        {
            await _db.Database.ExecuteSqlRawAsync(
                "DELETE FROM Users " +
                "DELETE FROM UserContragents " +
                "DELETE FROM OrderHistories " +
                "DELETE FROM SearchHistories");
        }

        static string RandomArrayValue(string[] values)
        {
            return values[_random.Next(0, values.Length)];
        }
    }
}
