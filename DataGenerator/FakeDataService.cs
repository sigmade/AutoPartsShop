﻿using Sigmade.Domain;
using Sigmade.Domain.Models;
using Sigmade.Domain.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sigmade.DataGenerator
{
    public class FakeDataService
    {
        private readonly ApplicationDbContext _db;
        private static readonly Random _random = new();
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
                Login = RandomNumber(),
                Password = RandomNumber(),
                UserType = (UserType)userTypes.GetValue(_random.Next(userTypes.Length))
            };
        }

        private UserContragent NewUserContragent()
        {
            var userId = UserIds[_random.Next(0, UserIds.Length)];
            var city = RandomArrayValue(Dictionaries.Cities);

            return new()
            {
                UserId = userId,
                City = city,
                Name = $"{RandomArrayValue(Dictionaries.ContragentTypes)} '" +
                       $"{city} " +
                       $"{RandomArrayValue(Dictionaries.ContragentName)}'",
                AccountNumber = RandomNumber()
            };
        }

        private OrderHistory NewOrderHistory()
        {
            var contragentId = ContragentIds[_random.Next(0, ContragentIds.Length)];

            return new()
            {
                UserContragentId = contragentId,
                VendorCode = _random.Next(1, 99).ToString().PadLeft(6, '0'),
                Brand = RandomArrayValue(Dictionaries.Brand),
                Count = _random.Next(0, 50)
            };
        }

        private SearchHistory NewSearchHistory()
        {
            var contragentId = ContragentIds[_random.Next(0, ContragentIds.Length)];

            return new()
            {
                UserContragentId = contragentId,
                VendorCode = _random.Next(1, 99).ToString().PadLeft(6, '0'),
                Brand = RandomArrayValue(Dictionaries.Brand),
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

        public async Task AddSearchHistory(int count)
        {
            ContragentIds = _db.UserContragents.Select(u => u.Id).ToArray();

            if (ContragentIds.Length == 0)
            {
                throw new Exception("Contragents not found");
            }

            for (int i = 0; i < count; i++)
            {
                _db.SearchHistories.Add(NewSearchHistory());
            }

            await _db.SaveChangesAsync();
        }

        private static string RandomNumber()
        {
            return _random.Next(1000, 99999).ToString();
        }

        public static string RandomArrayValue(string[] values)
        {
            return values[_random.Next(0, values.Length)];
        }
    }
}
