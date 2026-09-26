<Query Kind="Program" />

// Binary Search Problem
// -----------------------------------------------------------
// PROBLEM BRIEF:
// Implement a binary search function that finds the index of a target
// value in a SORTED array of integers.
//
// Signature to implement:
//   int BinarySearch(int[] arr, int target)
//
// Requirements:
//   - `arr` is sorted in ascending order (may contain duplicates).
//   - Return the index of ANY occurrence of `target` if found.
//   - Return -1 if `target` is not present.
//   - Should run in O(log n) time.
//   - Handle edge cases: empty array, single element, target smaller
//     than all elements, target larger than all elements.
//
// Write your solution in the BinarySearch method below, then run the
// script to validate against the test cases.
// -----------------------------------------------------------

void Main()
{
	RunTests();
}

int BinarySearch(int[] arr, int target)
{
	if(arr == null || arr.Length == 0)
		return -1;
		
	if(arr.Length == 1)
		return arr[0] == target ? 0 : -1;
		
	int targetIdx = -1;
	
	int lo = 0;
	int hi = arr.Length - 1;
	
	int currentIdx = (lo + hi) / 2;
	
	while(currentIdx >= 0 && currentIdx <= hi)
	{
		if(arr[currentIdx] == target)
		{
			return currentIdx;
		}
		else if(currentIdx == 0 || currentIdx == hi)
		{
			//Reached the end.
			break;
		}
		else if(arr[currentIdx] < target)
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

record TestCase(int[] Arr, int Target, HashSet<int> AcceptableIndices, string Description);

void RunTests()
{
	var tests = new List<TestCase>
	{
		new(Array.Empty<int>(), 5, new HashSet<int>(), "Empty array"),
		new(new[] { 5 }, 5, new HashSet<int> { 0 }, "Single element, found"),
		new(new[] { 5 }, 3, new HashSet<int>(), "Single element, not found"),
		new(new[] { 1, 3, 5, 7, 9, 11 }, 7, new HashSet<int> { 3 }, "Even-length array, found middle-ish"),
		new(new[] { 1, 3, 5, 7, 9 }, 1, new HashSet<int> { 0 }, "Target is first element"),
		new(new[] { 1, 3, 5, 7, 9 }, 9, new HashSet<int> { 4 }, "Target is last element"),
		new(new[] { 1, 3, 5, 7, 9 }, 0, new HashSet<int>(), "Target smaller than all"),
		new(new[] { 1, 3, 5, 7, 9 }, 10, new HashSet<int>(), "Target larger than all"),
		new(new[] { 1, 3, 5, 7, 9 }, 4, new HashSet<int>(), "Target not present, between elements"),
		new(new[] { 2, 2, 2, 2, 2 }, 2, new HashSet<int> { 0, 1, 2, 3, 4 }, "All duplicates, found"),
		new(new[] { 1, 2, 2, 2, 5 }, 2, new HashSet<int> { 1, 2, 3 }, "Some duplicates, found"),
		new(new[] { -10, -5, 0, 3, 8, 20 }, -5, new HashSet<int> { 1 }, "Negative numbers"),
		new(Enumerable.Range(0, 1000).ToArray(), 999, new HashSet<int> { 999 }, "Large array, last element"),
	};

	var results = tests.Select(t =>
	{
		string outcome;
		int? actual = null;
		try
		{
			actual = BinarySearch(t.Arr, t.Target);
			bool expectedNotFound = t.AcceptableIndices.Count == 0;
			bool pass = expectedNotFound
				? actual == -1
				: t.AcceptableIndices.Contains(actual.Value);
			outcome = pass ? "PASS" : "FAIL";
		}
		catch (NotImplementedException)
		{
			outcome = "NOT IMPLEMENTED";
		}
		catch (Exception ex)
		{
			outcome = $"ERROR: {ex.Message}";
		}

		return new
		{
			t.Description,
			Array = t.Arr.Length <= 20 ? $"[{string.Join(", ", t.Arr)}]" : $"(length {t.Arr.Length})",
			t.Target,
			Expected = t.AcceptableIndices.Count == 0 ? "-1" : $"one of [{string.Join(", ", t.AcceptableIndices)}]",
			Actual = actual?.ToString() ?? "-",
			Outcome = outcome
		};
	}).ToList();

	results.Dump("Binary Search Test Results");

	int passCount = results.Count(r => r.Outcome == "PASS");
	$"{passCount}/{results.Count} tests passed".Dump("Summary");
}

