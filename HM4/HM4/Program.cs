// See https://aka.ms/new-console-template for more information
using HM4;
using System.Diagnostics;

var watch = Stopwatch.StartNew();

var clientManager = new ClientManager("accounts.json");



Task task1 = new Task(() => clientManager.UpdateAccount("Азат", 100));
task1.Start();

Task task2 = Task.Factory.StartNew(() => clientManager.UpdateAccount("Петр", -50));
Task task3 = Task.Factory.StartNew(() => clientManager.UpdateAccount("Тимур", -30));
Task task4 = Task.Run(() => clientManager.UpdateAccount("Лейсан", 40));

task1.Wait();
task2.Wait();
task3.Wait();
task4.Wait();

if (task1.IsCompletedSuccessfully && task2.IsCompletedSuccessfully &&
    task3.IsCompletedSuccessfully && task4.IsCompletedSuccessfully)
{
    Console.WriteLine("Обновления аккаунтов завершены.");
}
watch.Stop();

Console.WriteLine($"Время выполнения: {watch.ElapsedMilliseconds} мс.");
