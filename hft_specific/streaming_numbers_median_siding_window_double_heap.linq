<Query Kind="Program" />

void Main()
{
	// Demo: compute sliding window medians
	IEnumerable<int> nums1 = [1, 3, -1, -3, 5, 3, 6, 7];
	MedianSlidingWindow(nums1, 3).Dump("Medians (k=3) for [1,3,-1,-3,5,3,6,7]");
	// Expected: [1, -1, -1, 3, 5, 6]

	IEnumerable<int> nums2 = [1, 2, 3, 4, 2, 3, 1, 4, 2];
	MedianSlidingWindow(nums2, 4).Dump("Medians (k=4) for [1,2,3,4,2,3,1,4,2]");
	// Expected: [2.5, 2.5, 3.0, 2.5, 2.5, 2.5]
}

IEnumerable<double> MedianSlidingWindow(IEnumerable<int> nums, int k)
{
	var dh = new DualHeap(k);
	var window = new Queue<int>(k);

	foreach (int num in nums)
	{
		dh.Add(num);
		window.Enqueue(num);

		if (window.Count > k)
			dh.Remove(window.Dequeue());

		if (window.Count == k)
			yield return dh.GetMedian();
	}
}

public class DualHeap
{
	private PriorityQueue<int, int> small = new(); // max heap via -value
	private PriorityQueue<int, int> large = new(); // min heap
	private Dictionary<int, int> delayed = new();

	private int smallSize = 0, largeSize = 0;
	private int k;

	public DualHeap(int k)
	{
		this.k = k;
	}

	public void Add(int num)
	{
		if (small.Count == 0 || num <= small.Peek())
		{
			small.Enqueue(num, -num);
			smallSize++;
		}
		else
		{
			large.Enqueue(num, num);
			largeSize++;
		}
		Rebalance();
	}

	public void Remove(int num)
	{
		if (!delayed.ContainsKey(num))
			delayed[num] = 0;
		delayed[num]++;

		if (num <= small.Peek())
			smallSize--;
		else
			largeSize--;

		Prune(small);
		Prune(large);
		Rebalance();
	}

	private void Prune(PriorityQueue<int, int> heap)
	{
		while (heap.Count > 0)
		{
			int num = heap.Peek();
			if (delayed.ContainsKey(num) && delayed[num] > 0)
			{
				delayed[num]--;
				heap.Dequeue();
			}
			else break;
		}
	}

	private void Rebalance()
	{
		if (smallSize > largeSize + 1)
		{
			int num = small.Dequeue();
			large.Enqueue(num, num);
			smallSize--;
			largeSize++;
			Prune(small);
		}
		else if (smallSize < largeSize)
		{
			int num = large.Dequeue();
			small.Enqueue(num, -num);
			largeSize--;
			smallSize++;
			Prune(large);
		}
	}

	public double GetMedian()
	{
		if (k % 2 == 1)
			return small.Peek();
		return ((double)small.Peek() + large.Peek()) / 2.0;
	}
}

