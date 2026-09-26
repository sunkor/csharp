<Query Kind="Program" />

// Problem brief: Search in Rotated Sorted Array
// - You are given an integer array nums, sorted in ascending order (with distinct values),
//   but then rotated at some unknown pivot index k (0 <= k < nums.length).
//   e.g. [0,1,2,4,5,6,7] rotated at k=3 becomes [4,5,6,7,0,1,2]
// - Given the rotated array and a target value, return the index of target if found, else -1.
// - Must run in O(log n) time complexity.
//
// Constraints (typical):
// - 1 <= nums.length <= 5000
// - -10^4 <= nums[i] <= 10^4
// - All values of nums are unique.
// - nums is guaranteed to be rotated at some pivot.
// - -10^4 <= target <= 10^4
//
// Examples:
//   nums = [4,5,6,7,0,1,2], target = 0  -> output: 4
//   nums = [4,5,6,7,0,1,2], target = 3  -> output: -1
//   nums = [1], target = 0              -> output: -1
//
// Your task: implement the Search(int[] nums, int target) method below.
// This script provides test cases and a validation harness so you can check your solution.

void Main()
{
	RunTests();
}

// TODO: implement your solution here
int Search(int[] arr, int target)
{
	if (arr == null || arr.Length == 0)
		return -1;

	if (arr.Length == 1)
		return arr[0] == target ? 0 : -1;

	//Find the offset where its rotated.
	var rotatedArrayIndex = FindMinimum(arr);
		
	if(arr[rotatedArrayIndex] == target)
		return rotatedArrayIndex;
	
	int lo,hi;
	if(target >= arr[0])
	{
		lo = 0;
		hi = rotatedArrayIndex == 0 ? arr.Length - 1 : rotatedArrayIndex - 1;
	}
	else
	{
		lo = rotatedArrayIndex;
		hi = arr.Length - 1;
	}

	int targetIdx = -1;

	int currentIdx = (lo + hi) / 2;

	while (currentIdx >= 0 && currentIdx <= hi)
	{
		if (arr[currentIdx] == target)
		{
			return currentIdx;
		}
		else if (currentIdx == 0 || currentIdx == hi)
		{
			//Reached the end.
			break;
		}
		else if (arr[currentIdx] < target)
		{
			lo = currentIdx;
			currentIdx = (hi + currentIdx + 1) / 2;
		}
		else
		{
			hi = currentIdx;
			currentIdx = (currentIdx - lo) / 2;
		}
	}
	return targetIdx;
}

int FindMinimum(int [] arr)
{
	int minIndex = 0;
	
	for(int i = 1; i < arr.Length; i++)
	{
		if(arr[i] < arr[minIndex])
		{
			minIndex = i;
		}
	}
	
	return minIndex;
}

record TestCase(int[] Nums, int Target, int Expected);

void RunTests()
{
	var tests = new List<TestCase>
	{
		new([4,5,6,7,0,1,2], 0, 4),
		new([4,5,6,7,0,1,2], 3, -1),
		new([1], 0, -1),
		new([1], 1, 0),
		new([5,1,3], 5, 0),
		new([3,1], 1, 1),
		new([4,5,6,7,8,1,2,3], 8, 4),
		new([1,2,3,4,5,6,7], 5, 4), // no rotation
		new([6,7,0,1,2,4,5], 4, 5),
		new([1,3], 0, -1),
	};

	var results = new List<object>();
	int pass = 0;

	foreach (var t in tests)
	{
		string status;
		int actual = -999;
		try
		{
			actual = Search((int[])t.Nums.Clone(), t.Target);
			bool ok = actual == t.Expected;
			status = ok ? "PASS" : "FAIL";
			if (ok) pass++;
		}
		catch (Exception ex)
		{
			status = $"ERROR: {ex.Message}";
		}

		results.Add(new
		{
			Nums = $"[{string.Join(",", t.Nums)}]",
			t.Target,
			t.Expected,
			Actual = actual,
			Status = status
		});
	}

	results.Dump($"Search in Rotated Sorted Array - {pass}/{tests.Count} passed");
}

