<Query Kind="Statements" />

var rb = new RingBuffer(3);

rb.Add(10);
rb.Add(20);
rb.Add(30);
// [10, 20, 30]

rb.Add(40);
// [20, 30, 40]

rb.Add(50);
// [30, 40, 50]

public class RingBuffer
{
	private readonly int[] buffer;
	private int index = 0;
	private int count = 0;

	public RingBuffer(int capacity)
	{
		buffer = new int[capacity];
	}

	public void Add(int value)
	{
		buffer[index] = value;
		index = (index + 1) % buffer.Length;

		if (count < buffer.Length)
			count++;
	}

	public IReadOnlyList<int> GetValuesInOrder()
	{
		var result = new List<int>();

		int start = count == buffer.Length ? index : 0;

		for (int i = 0; i < count; i++)
		{
			int actualIndex = (start + i) % buffer.Length;
			result.Add(buffer[actualIndex]);
		}

		return result;
	}
}