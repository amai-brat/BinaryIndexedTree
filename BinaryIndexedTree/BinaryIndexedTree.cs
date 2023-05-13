namespace BinaryIndexedTree;

public class BinaryIndexedTree
{
    private readonly long[] _tree;

    public BinaryIndexedTree(int size)
    {
        _tree = new long[size];
    }
    
    public BinaryIndexedTree(long[] array, bool linearInit = true) : this(array.Length)
    {
        if (!linearInit)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Add(i, array[i]);
            }
        }
        else
        {
            _tree[0] = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                _tree[i] = _tree[i - 1] + array[i];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                var index = (i & (i + 1)) - 1;
                if (index < 0) continue;
                _tree[i] -= _tree[index];
            }
        }
    }
    public void Add(int i, long delta)
    {
        while (i < _tree.Length)
        {
            _tree[i] += delta;
            i |= i + 1;
        }
    }

    public void Print()
    {
        Console.WriteLine(string.Join(' ', _tree));
    }
    
    /// <summary>
    /// Возвращает сумму с left до right элемента включительно.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public long GetSum(int left, int right)
    {
        var leftSum = GetSum(left-1);
        var rightSum = GetSum(right);
        return rightSum - leftSum;
    }

    /// <summary>
    /// Возвращает сумму с 0 до index элемента включительно.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private long GetSum(int index)
    {
        long result = 0;
        while (index >= 0)
        {
            result += _tree[index];

            index = (index & (index + 1)) - 1;
        }

        return result;
    }
}