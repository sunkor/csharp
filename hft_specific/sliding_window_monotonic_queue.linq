<Query Kind="Program" />

// Max Sliding Window Algorithm
// ============================
// This script demonstrates the sliding window technique using a monotonic deque to efficiently
// find the maximum element in each sliding window of size k across an array.
//
// SLIDING WINDOW CONCEPT:
// A sliding window is a fixed-size "window" that moves across an array one element at a time.
// For example, with array [1, 3, 1, 2, 0, 5] and k=3:
//   [1, 3, 1] -> max = 3
//        [3, 1, 2] -> max = 3
//           [1, 2, 0] -> max = 2
//              [2, 0, 5] -> max = 5
//
// USE CASES:
// - Performance optimization for finding max/min in subarrays (e.g., stock price peaks)
// - Real-time statistics (moving average, running max/min in streams)
// - Pattern detection in time-series data
// - Network traffic analysis (peak bandwidth in time windows)
//
// ALGORITHM:
// Uses a MonotonicQueue (deque) that maintains elements in descending order:
// - Only stores elements that could be future maxima
// - Time Complexity: O(n) - each element added/removed once
// - Space Complexity: O(k) - max window size
// - Beats the naive O(n*k) approach that checks every window

void Main()
{
	// Example usage of MaxSlidingWindow
	var nums = new int[] { 1, 3, 1, 2, 0, 5 };
	var k = 3;

	var result = MaxSlidingWindow(nums, k);

	$"Input: nums = [{string.Join(", ", nums)}], k = {k}".Dump("Input");
	result.Dump("Output (Max of each sliding window)");

	// Additional test case
	var result2 = MaxSlidingWindow(new int[] { 1 }, 1);
	result2.Dump("Test case: Single element array");
}

/// <summary>
/// Finds the maximum element in each sliding window of size k.
/// Uses a monotonic deque to achieve O(n) time complexity.
/// </summary>
/// <param name="nums">Array of integers</param>
/// <param name="k">Window size</param>
/// <returns>Array of maximum values for each window</returns>
public int[] MaxSlidingWindow(int[] nums, int k)
{
	var mq = new MonotonicQueue();
	var result = new List<int>();

	for (int i = 0; i < nums.Length; i++)
	{
		// Keep adding elements until we have a full window
		if (i < k - 1)
		{
			mq.Push(nums[i]);
		}
		else
		{
			// Window is full: add current element, record max, then remove leftmost element
			mq.Push(nums[i]);
			result.Add(mq.Max());
			mq.Pop(nums[i - k + 1]);
		}
	}

	return result.ToArray();
}

/// <summary>
/// Monotonic Deque: maintains elements in descending order.
/// Key insight: only elements that could potentially be future maxima are stored.
/// The front always contains the current window's maximum.
/// </summary>
public class MonotonicQueue
{
	private LinkedList<int> deque = new LinkedList<int>();

	/// <summary>
	/// Adds a value while maintaining descending order.
	/// Removes all smaller elements from the back (they can't be future maxima).
	/// </summary>
	public void Push(int val)
	{
		while (deque.Count > 0 && deque.Last.Value < val)
		{
			// Remove elements smaller than current (they're now useless)
			deque.RemoveLast();
		}
		deque.AddLast(val);
	}

	/// <summary>
	/// Returns the maximum element (always at the front of the deque).
	/// </summary>
	public int Max()
	{
		return deque.First.Value;
	}

	/// <summary>
	/// Removes an element if it's the current maximum (left window boundary).
	/// </summary>
	public void Pop(int val)
	{
		// Only remove if it's at the front (the max of the previous window)
		if (deque.Count > 0 && deque.First.Value == val)
		{
			deque.RemoveFirst();
		}
	}
}

