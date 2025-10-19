using ChatApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChatApp.Services
{
    public static class UserService
    {
        private static readonly string UsersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");
        private static readonly List<User> _users = new List<User>();

        static UserService()
        {
            LoadUsersFromFile();
        }

        public static bool Authenticate(string login, string password)
        {
            var user = _users.FirstOrDefault(u => u.Login == login);
            if (user != null && user.Password == password)
            {
                user.IsOnline = true;
                SaveUsersToFile(); // Обновляем статус в файле
                return true;
            }
            return false;
        }

        public static bool Register(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return false;

            if (_users.Any(u => u.Login == login))
                return false;

            _users.Add(new User { Login = login, Password = password });
            SaveUsersToFile();
            return true;
        }

        public static List<string> GetOnlineUserLogins(string currentUser)
        {
            return _users
                .Where(u => u.IsOnline && u.Login != currentUser)
                .Select(u => u.Login)
                .ToList();
        }

        public static void SetUserOffline(string login)
        {
            var user = _users.FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                user.IsOnline = false;
                SaveUsersToFile();
            }
        }

        private static void LoadUsersFromFile()
        {
            if (File.Exists(UsersFilePath))
            {
                try
                {
                    var json = File.ReadAllText(UsersFilePath);
                    var loaded = JsonConvert.DeserializeObject<List<User>>(json);
                    if (loaded != null)
                        _users.AddRange(loaded);
                }
                catch (Exception ex)
                {
                    // В случае ошибки — создаём новый файл
                    Console.WriteLine($"Ошибка загрузки users.json: {ex.Message}");
                }
            }

            // Если файл пуст или повреждён — добавим демо-пользователя
            if (!_users.Any())
            {
                _users.Add(new User { Login = "admin", Password = "123" });
                SaveUsersToFile();
            }
        }

        private static void SaveUsersToFile()
        {
            try
            {
                var json = JsonConvert.SerializeObject(_users, Formatting.Indented);
                File.WriteAllText(UsersFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения users.json: {ex.Message}");
            }
        }
    }
}
