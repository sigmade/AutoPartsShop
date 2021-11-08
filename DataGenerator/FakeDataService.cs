using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sigmade.Domain;
using Sigmade.Domain.Models;
using Sigmade.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

                _ = lastAccountNumber is null ? lastAccountNumber = "0" : lastAccountNumber;

                NewAccountNumber = Int32.Parse(lastAccountNumber) + 1;
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

        public async Task AddChildM2M()
        {
            var subchild = _db.SubChild.Where(c => c.Id == 1).FirstOrDefault();
            var subchilds = new List<SubChild> { };

            subchilds.Add(subchild);

            var main = new Main { Name = "main1" };

            var child = new Child { Name = "child2", Main = main, SubChilds = subchilds };
            var child2 = new Child { Name = "child33", Main = main, SubChilds = subchilds };

            _db.AddRange(child, child2);

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

        ////EF 00:48
        //public async Task<TimeSpan> AddSearchHistory(int count)
        //{
        //    var start = DateTime.Now;
        //    ContragentIds = _db.UserContragents.Select(u => u.Id).ToArray();

        //    if (ContragentIds.Length == 0)
        //    {
        //        throw new Exception("Contragents not found");
        //    }
        //    for (int i = 0; i < count; i++)
        //    {
        //        _db.SearchHistories.Add(NewSearchHistory());
        //    }

        //    await _db.SaveChangesAsync();
        //    var end = DateTime.Now;
        //    var diff = end - start;

        //    return diff;
        //}

        //DAPPER 2:32 - 3:32 
        public async Task<TimeSpan> AddSearchHistory(int count)
        {
            ContragentIds = _db.UserContragents.Select(u => u.Id).ToArray();
            TimeSpan diff;

            if (ContragentIds.Length == 0)
            {
                throw new Exception("Contragents not found");
            }
            using (IDbConnection db = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=AutoPartsDB;Trusted_Connection=True;"))
            {
                var start = DateTime.Now;

                for (int i = 0; i < count; i++)
                {
                    var sqlQuery = "INSERT INTO [dbo].[SearchHistories] (UserContragentId, VendorCode, Brand, UserIpAddress)  VALUES (@UserContragentId, @VendorCode, @Brand, @UserIpAddress)";
                    await db.ExecuteAsync(sqlQuery, NewSearchHistory());
                }

                var end = DateTime.Now;
                diff = end - start;

            }
                return diff;
        }

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
