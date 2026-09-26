<Query Kind="Program" />

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
	RunSimpleTests();
}

// ---- Implement this method ----
public static IList<IList<int>> ThreeSum(int[] nums, int target)
{
	if (nums == null || nums.Length < 3)
		return new List<IList<int>>();

	Array.Sort(nums);

	IList<IList<int>> result = new List<IList<int>>();

	for (int i = 0; i < nums.Length - 2; i++)
	{
		//If greater than target, break.
		if (nums[i] + nums[i + 1] + nums[i + 2] > target) 
		{
			continue;
		}
		
		//If highest numbers combined is lower than target, return.
		if (nums[i] + nums[nums.Length - 2] + nums[nums.Length - 1] < target) 
		{
			continue;
		}
		
		var low = i + 1;
		var high = nums.Length - 1;
		
		while(low < high)
		{
			//ignore repeating numbers.
			while(low < high && nums[low] == nums[low + 1])
			{
				low++;
				continue;
			}

			while (low < high && nums[high] == nums[high - 1])
			{
				high--;
				continue;
			}

			if (nums[i] + nums[low] + nums[high] > target)
			{
				high--;
			}
			else if (nums[i] + nums[low] + nums[high] < target)
			{
				low++;	
			}
			else
			{
				var list = new List<int>();
				list.Add(nums[i]);
				list.Add(nums[low]);
				list.Add(nums[high]);
				
				result.Add(list);
				
				low++;
				continue;
			}
		}
	}
	
	return result;
}

// ---- Helper for comparing results irrespective of ordering ----
public static HashSet<string> Normalize(IEnumerable<IList<int>> triplets) =>
	triplets
		.Select(t => string.Join(",", t.OrderBy(x => x)))
		.ToHashSet();

// ---- Simple (non-xunit) test runner ----
public static class ThreeSumTests
{
	public static void RunAll()
	{
		Run("Example_Case", Example_Case);
		Run("No_Triplets", No_Triplets);
		Run("All_Zeros", All_Zeros);
		Run("Empty_Array", Empty_Array);
		Run("Fewer_Than_Three_Elements", Fewer_Than_Three_Elements);
		Run("Duplicates_Are_Not_Repeated", Duplicates_Are_Not_Repeated);
		Run("Distinct_Indices_Required", Distinct_Indices_Required);
	}

	static void Run(string name, Action test)
	{
		try
		{
			test();
			$"PASS: {name}".Dump();
		}
		catch (Exception ex)
		{
			$"FAIL: {name} - {ex.Message}".Dump();
		}
	}

	static void AssertSetEqual(HashSet<string> expected, HashSet<string> actual)
	{
		if (!expected.SetEquals(actual))
			throw new Exception($"Expected [{string.Join(" | ", expected)}] but got [{string.Join(" | ", actual)}]");
	}

	static void AssertEmpty(IList<IList<int>> result)
	{
		if (result == null || result.Count != 0)
			throw new Exception("Expected empty result");
	}

	public static void Example_Case()
	{
		var nums = new[] { -1, 0, 1, 2, -1, -4 };
		var result = UserQuery.ThreeSum(nums, 0);
		var expected = new List<IList<int>>
		{
			new List<int> { -1, -1, 2 },
			new List<int> { -1, 0, 1 },
		};
		AssertSetEqual(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	public static void No_Triplets()
	{
		var nums = new[] { 0, 1, 1 };
		var result = UserQuery.ThreeSum(nums,0);
		AssertEmpty(result);
	}

	public static void All_Zeros()
	{
		var nums = new[] { 0, 0, 0 };
		var result = UserQuery.ThreeSum(nums,0);
		var expected = new List<IList<int>> { new List<int> { 0, 0, 0 } };
		AssertSetEqual(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	public static void Empty_Array()
	{
		var nums = Array.Empty<int>();
		var result = UserQuery.ThreeSum(nums,0);
		AssertEmpty(result);
	}

	public static void Fewer_Than_Three_Elements()
	{
		var nums = new[] { 1, 2 };
		var result = UserQuery.ThreeSum(nums,0);
		AssertEmpty(result);
	}

	public static void Duplicates_Are_Not_Repeated()
	{
		var nums = new[] { -2, 0, 0, 2, 2 };
		var result = UserQuery.ThreeSum(nums,0);
		var expected = new List<IList<int>> { new List<int> { -2, 0, 2 } };
		AssertSetEqual(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	public static void Distinct_Indices_Required()
	{
		// Only one zero and one pair (1,-1) exists - a correct solution must not
		// reuse the same index twice (e.g. treating the single 0 as both nums[j] and nums[k]).
		var nums = new[] { 1, -1, 0 };
		var result = UserQuery.ThreeSum(nums, 0);
		var expected = new List<IList<int>> { new List<int> { -1, 0, 1 } };
		AssertSetEqual(UserQuery.Normalize(expected), UserQuery.Normalize(result));
	}

	public static void Multiple_Distinct_Triplets()
	{
		var nums = new[] { -4, -2, -2, -2, 0, 1, 2, 2, 2, 3, 4 };
		var result = UserQuery.ThreeSum(nums,0);
		var expected = BruteForce(nums);
		AssertSetEqual(UserQuery.Normalize(expected), UserQuery.Normalize(result));
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

public static void RunSimpleTests()
{
	ThreeSumTests.RunAll();
}
