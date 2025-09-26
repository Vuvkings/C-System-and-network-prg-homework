using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;
using Newtonsoft.Json;
namespace HM4
{
    public class ClientManager
    {
        private readonly string _filePath;
        private readonly object _lockObject = new object(); // Для lock и Monitor
        private readonly Mutex _mutex = new Mutex(); // Для Mutex
        private readonly Semaphore _semaphore = new Semaphore(1, 1); // Для Semaphore

        public ClientManager(string filePath)
        {
            _filePath = filePath;
        }

        public void UpdateAccount(string clientName, decimal amount)
        {
            ////пример использования lock
            //lock (_lockObject)
            //    {
            //        UpdateAccountInternal(clientName, amount);
            //    }

            //// пример использования Monitor
            //Monitor.Enter(_lockObject);
            //try
            //{
            //    UpdateAccountInternal(clientName, amount);
            //}
            //finally
            //{
            //    Monitor.Exit(_lockObject);
            //}

            //// пример использования Mutex
            //_mutex.WaitOne();
            //try
            //{
            //    UpdateAccountInternal(clientName, amount);
            //}
            //finally
            //{
            //    _mutex.ReleaseMutex();
            //}

            //// пример использования Semaphore
            //_semaphore.WaitOne();
            //try
            //{
            //    UpdateAccountInternal(clientName, amount);
            //}
            //finally
            //{
            //    _semaphore.Release();
            //}

            // пример использования WaitHandle (для ожидания)
            using (var waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset))
            {
                // Здесь можно сделать что-то, что требует ожидания
                waitHandle.Set(); // Сигнализировать о завершении
            }
        }

        private void UpdateAccountInternal(string clientName, decimal amount)
        {
            var accounts = LoadAccounts();

            if (accounts.ContainsKey(clientName))
            {
                accounts[clientName].Balance += amount;
            }
            else
            {
                accounts[clientName] = new Client { Name = clientName, Balance = amount };
            }
            SaveAccounts(accounts);
        }

        private Dictionary<string, Client> LoadAccounts()
        {
            if (!File.Exists(_filePath))
            {
                var initialAccounts = new Dictionary<string, Client>()
            {
                { "Петр", new Client {Name = "Петр", Balance = 6000m } },
                { "Азат", new Client {Name = "Азат", Balance = 2000m } },
                { "Тимур", new Client {Name = "Тимур", Balance = 333m } },
                { "Лейсан", new Client {Name = "Лейсан", Balance = 1900m } },
                { "Алекс", new Client {Name = "Алекс", Balance = 900m } }
            };

                SaveAccounts(initialAccounts);
                return initialAccounts;
            }

            var json = File.ReadAllText(_filePath);

            return JsonConvert.DeserializeObject<Dictionary<string, Client>>(json);
        }

        public void SaveAccounts(Dictionary<string, Client> accounts)
        {
            var json = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
