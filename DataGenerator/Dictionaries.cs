// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.Collections.Generic;

namespace Sigmade.DataGenerator
{
    public class Dictionaries
    {
        public static string[] FirstNames = { "Алексей", "Иван", "Тимур" };
        public static string[] LastNames = { "Смирнов", "Петров", "Иванов" };
        public static string[] ContragentTypes = { "ИП", "АО", "ТОО", "ЗАО" };
        public static string[] Cities = { "Алматы", "Нур-Султан", "Костанай", "Шымкент" };

        public static Dictionary<int, string> Products = new()
        {
            { 1, "Mahle" },
            { 2, "Mahle" },
            { 3, "Brembo" },
            { 4, "Brembo" },
            { 5, "Brembo" },
            { 6, "Kolbenschmidt" },
            { 7, "Kolbenschmidt" },
            { 8, "Lucas" },
            { 9, "Remsa" },
            { 10, "Remsa" },
            { 11, "Remsa" },
            { 12, "Remsa" },
            { 13, "TRW" },
            { 14, "TRW" },
            { 15, "TRW" },
            { 16, "Pagid" },
            { 17, "AKG" },
            { 18, "AKG" },
            { 19, "Contitech" },
            { 20, "Contitech" },
            { 21, "Mann" },
            { 22, "Mann" },
            { 23, "Mann" },
            { 24, "Pierburg" },
            { 25, "Pierburg" }
        };
    }
}
