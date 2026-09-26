<Query Kind="Program" />

void Main()
{
	// ==========================================================
	// PROBLEM: Subarray Sum Equals K
	// ==========================================================
	// Given an array of integers `nums` and an integer `k`, return
	// the total number of contiguous subarrays whose sum equals to k.
	//
	// Example 1:
	//   Input: nums = [1,1,1], k = 2
	//   Output: 2
	//
	// Example 2:
	//   Input: nums = [1,2,3], k = 3
	//   Output: 2   (subarrays [1,2] and [3])
	//
	// Constraints:
	//   1 <= nums.Length <= 2 * 10^4
	//   -1000 <= nums[i] <= 1000
	//   -10^7 <= k <= 10^7
	//
	// Notes:
	//   - nums[i] can be negative, so a simple sliding window won't work directly.
	//   - Classic O(n) approach uses a running prefix sum and a dictionary
	//     counting how many times each prefix sum has occurred.
	//
	// Why not a sliding window?
	// A sliding window assumes that widening the window always raises the sum and shrinking it always lowers it.That's only true when all numbers are positive. This problem allows negatives and zeros, so the sum can go up or down as the window grows, and you can't tell which way to move. Prefix sums with a hashmap don't depend on that, which is why this is the standard approach.
	// For comparison, brute force(try every start and end pair) is O(n²). The hashmap version collapses the inner loop into a single dictionary lookup.
	
	// TODO: Implement your solution in the method below.
	// ==========================================================

	RunTests();
}

/// <summary>
/// Implement this method: return the number of contiguous subarrays
/// of `nums` that sum to `k`.
/// </summary>
int SubarraySum(int[] nums, int k)
{
	int count = 0;
	int sum = 0;

	//prefix sum storage
	var dict = new Dictionary<int, int>()
	{
		[0] = 1
	};

	foreach(var num in nums)
	{
		sum += num;
		
		if(dict.TryGetValue((sum - k), out var times))
		{
			count += times;
		}
		
		dict[sum] = dict.GetValueOrDefault(sum) + 1;
	}
	
	return count;
}

// ---------------- Test harness ----------------

record TestCase(int[] Nums, int K, int Expected, string Description);

void RunTests()
{
	var cases = new List<TestCase>
	{
		new(new[] {1, 1, 1}, 2, 2, "Basic positive example"),
		new(new[] {1, 2, 3}, 3, 2, "Basic positive example 2"),
		new(new[] {1}, 0, 0, "Single element, no match"),
		new(new[] {1}, 1, 1, "Single element, exact match"),
		new(new[] {1, -1, 0}, 0, 3, "Negative numbers and zeros"),
		new(new[] {-1, -1, 1}, 0, 1, "Negative numbers, sum to zero"),
		new(new[] {0, 0, 0, 0, 0}, 0, 15, "All zeros, many subarrays"),
		new(new[] {3, 4, 7, 2, -3, 1, 4, 2}, 7, 4, "Mixed positives/negatives"),
		new(new[] {1, 2, 1, 2, 1}, 3, 4, "Repeated pattern"),
		new(Enumerable.Repeat(1, 100).ToArray(), 5, 96, "Long array performance sanity check"),
	};

	var results = cases.Select(tc =>
	{
		int actual;
		string status;
		try
		{
			actual = SubarraySum(tc.Nums, tc.K);
			status = actual == tc.Expected ? "PASS" : "FAIL";
		}
		catch (NotImplementedException)
		{
			actual = -1;
			status = "NOT IMPLEMENTED";
		}
		catch (Exception ex)
		{
			actual = -1;
			status = $"ERROR: {ex.Message}";
		}

		return new
		{
			tc.Description,
			Nums = string.Join(",", tc.Nums),
			tc.K,
			tc.Expected,
			Actual = actual,
			Status = status
		};
	}).ToList();

	results.Dump("Subarray Sum Equals K — Test Results");

	int passed = results.Count(r => r.Status == "PASS");
	$"{passed}/{results.Count} tests passed".Dump();
}
