<Query Kind="Program" />

void Main()
{
	/*
	=== Longest Consecutive Sequence ===

	Given an unsorted array of integers `nums`, return the length of the longest
	consecutive elements sequence.

	You must write an algorithm that runs in O(n) time.

	Example 1:
	  Input: nums = [100,4,200,1,3,2]
	  Output: 4
	  Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is 4.

	Example 2:
	  Input: nums = [0,3,7,2,5,8,4,6,0,1]
	  Output: 9

	Constraints:
	  - 0 <= nums.Length <= 10^5
	  - -10^9 <= nums[i] <= 10^9

	Implement:
	  int LongestConsecutive(int[] nums)

	Fill in the method body below, then run the script to validate against the test cases.
	*/

	var testCases = new (int[] Input, int Expected)[]
	{
		(new int[] { 100, 4, 200, 1, 3, 2 }, 4),
		(new int[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 }, 9),
		(new int[] { }, 0),
		(new int[] { 1 }, 1),
		(new int[] { 1, 2, 0, 1 }, 3),
		(new int[] { -1, -2, -3, 0, 1, 2, -4 }, 7),
		(new int[] { 9, 1, 4, 7, 3, -2, 6, 8, 5, 2, 0 }, 10),
		(Enumerable.Range(-50000, 100000).Reverse().ToArray(), 100000),
	};

	var results = testCases.Select((tc, i) =>
	{
		int actual;
		bool threw = false;
		string error = null;
		try
		{
			actual = LongestConsecutive(tc.Input);
		}
		catch (Exception ex)
		{
			actual = -1;
			threw = true;
			error = ex.Message;
		}

		return new
		{
			Test = i + 1,
			InputPreview = tc.Input.Length <= 15 ? string.Join(",", tc.Input) : $"[{tc.Input.Length} items]",
			Expected = tc.Expected,
			Actual = actual,
			Pass = !threw && actual == tc.Expected,
			Error = error
		};
	}).ToList();

	results.Dump("Longest Consecutive Sequence — Test Results");

	var passCount = results.Count(r => r.Pass);
	$"{passCount}/{results.Count} tests passed".Dump();
}

int LongestConsecutive(int[] nums)
{
	if(nums == null)
		return 0;
	
	Array.Sort(nums);
	
	var longestConsecutiveSequence = 0;
	
	int idx = 0;
	
	while(idx < nums.Length)
	{
		//start of a new sequence
		var seqCount = 1;
						
		while(++idx < nums.Length)
		{
			if (nums[idx] == nums[idx - 1]) //If number did not change, continue;
			{
				continue;
			}
			else if (nums[idx] == (nums[idx - 1] + 1)) //If next sequence matches.
			{
				seqCount++;
				continue;
			}
			
			break;
		}
		
		longestConsecutiveSequence = Math.Max(longestConsecutiveSequence, seqCount);
	}
	return longestConsecutiveSequence;
}
