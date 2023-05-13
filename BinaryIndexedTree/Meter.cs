using System.Diagnostics;

namespace BinaryIndexedTree;

public class Meter
{
    public List<Tuple<long, long, long>> MeasureSumsTo(int n)
    {
        var result = new List<Tuple<long, long, long>>();
        for (int i = 200; i < n; i+=100)
        {
            result.Add(MeasureSum(i));
            if (i%10000==0) Console.WriteLine(i);
        }

        return result;
    }
    
    public List<Tuple<long, long, long>> MeasureAddTo(int n)
    {
        var result = new List<Tuple<long, long, long>>();
        for (int i = 10; i < n; i+=100)
        {
            result.Add(MeasureAdd(i));
        }

        return result;
    }
    private Tuple<long, long, long> MeasureSum(int n = 1000000)
    {
        var array = new long[n];
        var random = new Random();
        for (int i = 0; i < n; i++)
        {
            array[i] = random.Next();
        }

        var right = n - 1;
        var left = (n - 1)/2;
        ProbeSumBIT(array, left, right);
        return new Tuple<long, long, long>(
            MeasureSumInSourceArray(array, left, right),
            MeasureSumInArrayOfSums(array, left, right),
            MeasureSumInBinaryIndexedTree(array, left, right));
    }

    private Tuple<long, long, long> MeasureAdd(int n = 1000000)
    {
        var array = new long[n];
        var random = new Random();
        for (int i = 0; i < n; i++)
        {
            array[i] = random.Next();
        }

        var delta = random.NextInt64();
        var index = random.Next(n);
        ProbeAddBIT(array, index, delta);
        return new Tuple<long, long, long>(
            MeasureAddInSourceArray(array, index, delta),
            MeasureAddInArrayOfSums(array, index, delta),
            MeasureAddInBinaryIndexedTree(array, index, delta));
    }

    private void ProbeSumBIT(long[] array, int left, int right)
    {
        var tree = new BinaryIndexedTree(array);
        var result = tree.GetSum(left, right);
    }
    
    private void ProbeAddBIT(long[] array, int index, long delta)
    {
        var tree = new BinaryIndexedTree(array);
        tree.Add(index, delta);
    }
    private long MeasureSumInSourceArray(long[] array, int left, int right)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        long leftSum = 0;
        long rightSum = 0;
        for (int i = 0; i < right; i++)
        {
            if (i < left) leftSum += array[i];
            rightSum += array[i];
        }

        var result = rightSum - leftSum;
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks / 1000;
    }
    
    private long MeasureSumInArrayOfSums(long[] array, int left, int right)
    {
        var prefix = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            prefix[i] += array[i];
        }
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        var result = prefix[right] - prefix[left];
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks / 1000;
    }

    private long MeasureSumInBinaryIndexedTree(long[] array, int left, int right)
    {
        var tree = new BinaryIndexedTree(array);

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var result = tree.GetSum(left, right);
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks / 1000;
    }

    private long MeasureAddInSourceArray(long[] array, int index, long delta)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        array[index] += delta;
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }
    
    private long MeasureAddInArrayOfSums(long[] array, int index, long delta)
    {
        var prefix = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            prefix[i] += array[i];
        }
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        for (int i = index; i < array.Length; i++)
        {
            prefix[i] += delta;
        }
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }

    private long MeasureAddInBinaryIndexedTree(long[] array, int index, long delta)
    {
        var tree = new BinaryIndexedTree(array);

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        tree.Add(index, delta);
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }

}