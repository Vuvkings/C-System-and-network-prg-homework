// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


//Array28. Дан массив размера N.
//Найти минимальный элемент его элементов с четными номерами
//локальный минимум - это элемент, который меньше любого из своих соседей
//локальный максимум - это элемент, который больше любого из своих соседей

var random = new Random();
int[] sizes = { 100_000, 1_000_000, 10_000_000 };

/*  //Проверка Метода
List<int> parityList = new List<int>();
int[] nums = Enumerable.Range(0, sizes[0]).Select(x => random.Next(-100, 100)).ToArray();
foreach (int i in nums)
{
    Console.Write($"{i} ");
    
}
Console.WriteLine();
for (int i = 0; i < sizes[0]; i++)
{
    if(i%2 != 0)
    {
        Console.WriteLine(nums[i]);
        parityList.Add(nums[i]);
        //Console.WriteLine(parityList[i].);
    }
}

foreach (int i in parityList)
{
    Console.Write($"{i} ");
}
Console.WriteLine(parityList.Min());
*/

for (int i = 0; i < sizes.Length; i++)
{
    int[] nums = Enumerable.Range(0, sizes[i]).Select(x => random.Next(-1000, 1000)).ToArray();

    var watch = Stopwatch.StartNew();
    var elementsFromFor = GetMinParityElementFromFor(nums);
    watch.Stop();

        var watchForParallel = Stopwatch.StartNew();
        var elementsFromParallel = GetMinParityElementFromParallelFor(nums);
        watchForParallel.Stop();

    Console.WriteLine($"Размер массива: {sizes[i]}");
    Console.WriteLine($"Обычный метод: минимальный элемент {elementsFromFor} | Время: {watch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Параллельный метод: минимальный элемент {elementsFromParallel} | Время: {watchForParallel.ElapsedMilliseconds} ms.");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
}


static int GetMinParityElementFromFor(int[] numbers)
{
    var min = 0;
    List<int> parityList = new List<int>();

    for (int i = 0; i < numbers.Length; i++)
    {
        if (i % 2 != 0) //Т.к. первый элемент массива начинается с 0,
                        //будем искать нечетные элементы, чтобы отобразить четные
        {
            
            parityList.Add(numbers[i]);
          
        }
    }
    
    return min = parityList.Min();
}

static int GetMinParityElementFromParallelFor(int[] numbers)
{
    var min = 0;
    List<int> parityList = new List<int>();
    Parallel.For(0, numbers.Length, i =>
    {
        if (i % 2 != 0)
        {
            
            parityList.Add(numbers[i]);
            
        }
    });

    return min = parityList.Min();
}