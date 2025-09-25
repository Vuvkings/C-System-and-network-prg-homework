using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

BenchmarkRunner.Run<EvenOdd>();

////  Посчитать колличество положительных и отрицательных чисел

//// C# использование последовательных и параллельных методов
//// и измерить производительность с помощью BenchmarkDotNet в консольном приложении

//// обычный массив Array, последовательные циклы (без потоков)
//// Parallel.For (each) (без потоков)
//// PLINQ (asParallel)
//// Task.Factory по кол-ву ядер (<>)
public class EvenOdd
{
    [Params(1_000, 100_000, 1_000_000)]
    public int n;

    // установки для случайного набора
    public readonly int max = 1000;
    public List<int>? list; // общая коллекция для всех методов
    public int[]? array;     // общий массив для всех методов

    [GlobalSetup]
    public void Setup()
    {
        var r = new Random();
        list = [.. Enumerable.Range(-n, n).Select(x => r.Next(max))];
        array = list.ToArray();
    }

    [Benchmark]
    public int Array_For()
    {
        int[] arr = array;
        int negCount = 0;
        int PositiveCount = 0;
        foreach (var nums in arr) 
        {
            if (nums < 0) {  negCount++; }
            else if (nums >= 0) { PositiveCount++; }
        }
        return PositiveCount;
    }

    [Benchmark]
    public int List_For()
    {
        int negCount = 0;
        int PositiveCount = 0;

       
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] >= 0)
            {
                PositiveCount++;
            }
            else if (list[i] < 0) { negCount++; }
        }
        return PositiveCount;
    }

    [Benchmark]
    public int Parallel_ConcurrentBag()
    {
        ///double avg = array.AsParallel().Average();
        var bag = new ConcurrentBag<int>();
        Parallel.ForEach(array, nums =>
        {
            if (nums >= 0)
                bag.Add(nums);
        });

        return bag.Count;
    }

    [Benchmark]
    public int List_Foreach()
    {
        long total = 0;
        int positiveCount = 0;
        foreach (var item in list)
            total += item;
        foreach (var item in list)
            if (item >= 0)
                positiveCount++;
        return positiveCount;
    }

    [Benchmark]
    public int Array_LINQ()
    {
        
        return array.Count(x => x >= 0);
    }

    [Benchmark]
    public int List_LINQ()
    {
        
        return list.Count(x => x >= 0);
    }
    [Benchmark]
    public int PLINQ_AutoParallel()
    { 
        return array.AsParallel().Count(x => x >= 0);
    }

    [Benchmark]
    public int PLINQ_WithDegreeOfParallelism()
    {
        return array.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount).Count(x => x >= 0);
    }

    [Benchmark]
    public int PLINQ_ForceParallel()
    {
        return array.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).Count(x => x >= 0);
    }

    [Benchmark]
    public int Tasks_Run()
    {
        int processorCount = Environment.ProcessorCount;
        int chunkSize = array.Length / processorCount;

        var sumTasks = new Task<long>[processorCount];
        for (int i = 0; i < processorCount; i++)
        {
            int start = i * chunkSize;
            int end = (i == processorCount - 1) ? array.Length : start + chunkSize;

            sumTasks[i] = Task.Run(() =>
            {
                long localSum = 0;
                for (int j = start; j < end; j++)
                    localSum += array[j];
                return localSum;
            });
        }

        long totalSum = Task.WhenAll(sumTasks).Result.Sum();

        var countTasks = new Task<int>[processorCount];
        for (int i = 0; i < processorCount; i++)
        {
            int start = i * chunkSize;
            int end = (i == processorCount - 1) ? array.Length : start + chunkSize;

            countTasks[i] = Task.Run(() =>
            {
                int localCount = 0;
                for (int j = start; j < end; j++)
                    if (array[j] >= 0) localCount++;
                return localCount;
            });
        }

        return Task.WhenAll(countTasks).Result.Sum();
    }


    [Benchmark]
    public int Tasks_Factory()
    {
        int processorCount = Environment.ProcessorCount;
        int chunkSize = array.Length / processorCount;

        var sumTasks = new Task<long>[processorCount];
        for (int i = 0; i < processorCount; i++)
        {
            int start = i * chunkSize;
            int end = (i == processorCount - 1) ? array.Length : start + chunkSize;

            sumTasks[i] = Task.Factory.StartNew(() =>
            {
                long localSum = 0;
                for (int j = start; j < end; j++)
                    localSum += array[j];
                return localSum;
            });
        }

        long totalSum = Task.WhenAll(sumTasks).Result.Sum();
        double avg = (double)totalSum / array.Length;

        var countTasks = new Task<int>[processorCount];
        for (int i = 0; i < processorCount; i++)
        {
            int start = i * chunkSize;
            int end = (i == processorCount - 1) ? array.Length : start + chunkSize;

            countTasks[i] = Task.Factory.StartNew(() =>
            {
                int localCount = 0;
                for (int j = start; j < end; j++)
                    if (array[j] > avg) localCount++;
                return localCount;
            });
        }

        return Task.WhenAll(countTasks).Result.Sum();
    }
}
