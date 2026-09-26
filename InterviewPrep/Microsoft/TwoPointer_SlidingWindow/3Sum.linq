<Query Kind="Program" />

#load "xunit"
using Xunit;

/*
 * 3SUM PROBLEM
 * ------------
 * Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]]
 * such that:
 *   - i != j, i != k, and j != k
 *   - nums[i] + nums[j] + nums[k] == 0
 *
 * The solution set must not contain duplicate triplets (order of numbers within
 * a triplet doesn't matter, and the order of triplets in the result doesn't matter).
 *
 * Example:
 *   Input:  nums = [-1, 0, 1, 2, -1, -4]
 *   Output: [[-1, -1, 2], [-1, 0, 1]]
 *
 * Typical approach: sort the array, then for each index i, use a two-pointer
 * scan over the remaining elements (O(n^2) overall), skipping duplicates.
 *
 * TODO: Implement ThreeSum below. Test cases (using xunit) will validate your solution.
 * The validation compares triplet *sets* (ignoring order of triplets and order
 * within each triplet), so your output format doesn't need to match exactly.
 */

void Main()
{
	RunTests();
}

// ---- Implement this method ----
public static IList<IList<int>> ThreeSum(int[] nums)
{
	throw new NotImplementedException("Implement ThreeSum here");
}

// ---- Helper for comparing results irrespective of ordering ----
public static HashSet<string> Normalize(IEnumerable<IList<int>> triplets) =>
	triplets
		.Select(t => string.Join(",", t.OrderBy(x => x)))
		.ToHashSet();

public class ThreeSumTests
{
	[Fact]
	public void Example_Case()
	{
		var nums = new[] { -1, 0, 1, 2, -1, -4 };
		var result = UserQuery.ThreeSum(nums);
		var expected = new List<IList<int>>
		{
			new List<int> { -1, -1, 2 },
			new List<int> { -1, 0, 1 },
		};
		Assert.Equal(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	[Fact]
	public void No_Triplets()
	{
		var nums = new[] { 0, 1, 1 };
		var result = UserQuery.ThreeSum(nums);
		Assert.Empty(result);
	}

	[Fact]
	public void All_Zeros()
	{
		var nums = new[] { 0, 0, 0 };
		var result = UserQuery.ThreeSum(nums);
		var expected = new List<IList<int>> { new List<int> { 0, 0, 0 } };
		Assert.Equal(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	[Fact]
	public void Empty_Array()
	{
		var nums = Array.Empty<int>();
		var result = UserQuery.ThreeSum(nums);
		Assert.Empty(result);
	}

	[Fact]
	public void Fewer_Than_Three_Elements()
	{
		var nums = new[] { 1, 2 };
		var result = UserQuery.ThreeSum(nums);
		Assert.Empty(result);
	}

	[Fact]
	public void Duplicates_Are_Not_Repeated()
	{
		var nums = new[] { -2, 0, 0, 2, 2 };
		var result = UserQuery.ThreeSum(nums);
		var expected = new List<IList<int>> { new List<int> { -2, 0, 2 } };
		Assert.Equal(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	[Fact]
	public void Multiple_Distinct_Triplets()
	{
		var nums = new[] { -4, -2, -2, -2, 0, 1, 2, 2, 2, 3, 4 };
		var result = UserQuery.ThreeSum(nums);
		var expected = new List<IList<int>>
		{
			new List<int> { -4, 2, 2 },
			new List<int> { -2, -2, 4 },
			new List<int> { -2, 0, 2 },
			new List<int> { -2, -1, 3 } is null ? null : new List<int> { 0, -2, 2 }, // placeholder guard, replaced below
		};
		// Recompute expected set correctly via a manual brute-force reference (independent of user's implementation)
		expected = BruteForce(nums);
		Assert.Equal(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	// Independent brute-force reference implementation used only for validating harder test cases.
	static List<IList<int>> BruteForce(int[] nums)
	{
		var set = new HashSet<(int, int, int)>();
		var n = nums.Length;
		for (int i = 0; i < n; i++)
		for (int j = i + 1; j < n; j++)
		for (int k = j + 1; k < n; k++)
			if (nums[i] + nums[j] + nums[k] == 0)
			{
				var arr = new[] { nums[i], nums[j], nums[k] };
				Array.Sort(arr);
				set.Add((arr[0], arr[1], arr[2]));
			}
		return set.Select(t => (IList<int>)new List<int> { t.Item1, t.Item2, t.Item3 }).ToList();
	}
}
