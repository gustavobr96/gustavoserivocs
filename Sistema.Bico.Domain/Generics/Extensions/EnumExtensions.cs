using Sistema.Bico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sistema.Bico.Domain.Generics.Extensions
{
    public static class EnumExtensions
    {
        private const int LENGHT = 40;
        private const int LENGHT_PASSWORD = 8;
        public static string GetDescription(this Enum value)
        {
            var fi  = value.GetType().GetField(value.ToString());
            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attributes != null && attributes.Any() ? attributes.First().Description : value.ToString();
        }
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(var item in enumerable)
            {
                action(item);
            }
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public static string GenerateKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, LENGHT)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static string GeneratePassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, LENGHT_PASSWORD)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection?.Any() == true;
        }
        public static List<StatusWorker> GetStatusEmAndamento()
        {
            var lista = new List<StatusWorker>()
            {
                 StatusWorker.Iniciado,
                 StatusWorker.IntencaoServico,
                 StatusWorker.AguardandoConfirmacao,
                 StatusWorker.Contratado
               
            };

            return lista;
        }
        public static string FormataMoeda(decimal valor, string culture = "pt-BR")
        {
            return valor.ToString("C2", new CultureInfo(culture));
        }
        public static string ToSeparatedString<T>(this IEnumerable<T> instance, char separator)
        {
            var array = instance?.ToArray();
            if (array?.Any() != true) return null;

            var csv = new StringBuilder();
            array.Each(v => csv.AppendFormat("{0}{1}", v, separator));
            return csv.ToString(0, csv.Length - 1);
        }
    }
}
