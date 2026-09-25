<Query Kind="Program" />

void Main()
{
	// Two Sum problem: given an array of integers and a target, return indices of the
	// two numbers such that they add up to target. Assumes exactly one valid solution,
	// and the same element may not be used twice. Order of returned indices doesn't matter.

	var testCases = new (int[] nums, int target, int[] expected)[]
	{
		(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 }),           // basic case
		(new[] { 3, 2, 4 }, 6, new[] { 1, 2 }),                // answer not at start
		(new[] { 3, 3 }, 6, new[] { 0, 1 }),                   // duplicate values
		(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 }),    // negative numbers
		(new[] { 0, 4, 3, 0 }, 0, new[] { 0, 3 }),             // zeros
		(new[] { 1, 2, 3, 4, 5 }, 9, new[] { 3, 4 }),          // answer at end
		(new[] { 5, 75, 25 }, 100, new[] { 1, 2 }),            // larger numbers
	};

	var results = testCases.Select(tc =>
	{
		int[] actual;
		string error = null;

		try
		{
			actual = TwoSum(tc.nums, tc.target);
		}
		catch (Exception ex)
		{
			actual = null;
			error = ex.Message;
		}

		bool pass = error == null && IsValidAnswer(tc.nums, tc.target, actual);

		return new
		{
			Nums = string.Join(", ", tc.nums),
			tc.target,
			Expected = string.Join(", ", tc.expected),
			Actual = actual is null ? "(none)" : string.Join(", ", actual),
			Pass = pass,
			Error = error
		};
	}).ToList();

	results.Dump("Two Sum - Test Results");

	if (results.All(r => r.Pass))
		"All tests passed! ✅".Dump();
	else
		"Some tests failed. ❌".Dump();
}

// Validates that the returned indices are within bounds, distinct, and that the
// corresponding values actually sum to the target. This is independent of *which*
// valid pair is returned, since some inputs (e.g. duplicates) may have multiple
// valid answers.
bool IsValidAnswer(int[] nums, int target, int[] actual)
{
	if (actual == null || actual.Length != 2) return false;
	if (actual[0] == actual[1]) return false;
	if (actual[0] < 0 || actual[0] >= nums.Length) return false;
	if (actual[1] < 0 || actual[1] >= nums.Length) return false;

	return nums[actual[0]] + nums[actual[1]] == target;
}

// TODO: implement this method.
// Should return the 0-based indices of the two numbers in `nums` that add up to `target`.
int[] TwoSum(int[] nums, int target)
{
	if(nums == null || nums.Length < 2)
	{
		return null;
	}
	Array.Sort(nums);
	int left = 0;
	int right = nums.Length - 1;
	while(left < right)
	{
		var sum = nums[left] + nums[right];
		if(sum == target)
		{
			return new int[] {left,right};
		}
		else if (sum > target)
		{
			right--;
		}
		else
		{
			left++;
		}
	}
	return null;
}
