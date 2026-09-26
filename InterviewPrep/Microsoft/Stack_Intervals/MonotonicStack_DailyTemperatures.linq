<Query Kind="Program" />

// Daily Temperatures problem
// -----------------------------------------------------------------------
// Given an array of integers `temperatures` representing daily temperatures,
// return an array `answer` such that answer[i] is the number of days you
// have to wait after the i-th day to get a warmer temperature. If there is
// no future day for which this is possible, answer[i] == 0.
//
// Example:
//   Input:  temperatures = [73,74,75,71,69,72,76,73]
//   Output: [1,1,4,2,1,1,0,0]
//
// Constraints:
//   1 <= temperatures.length <= 10^5
//   30 <= temperatures[i] <= 100
//
// Typical optimal solution runs in O(n) time using a monotonic stack.
// -----------------------------------------------------------------------

void Main()
{
	RunTests();
}

// ---- YOUR SOLUTION GOES HERE ----
int[] DailyTemperatures(int[] temperatures)
{
	if(temperatures == null || temperatures.Length == 0)
		return null;
		
	var n = temperatures.Length;
	var ans = new int[n]; // defaults to 0 for days with no warmer future day
		
	var stack = new int[n];
	var top = -1;
	
	for(int i = 0; i < n; i++)
	{
		while(top >= 0 && temperatures[i] > temperatures[stack[top]])
		{
			var prev = stack[top--];
			ans[prev] = i - prev;
		}
		
		stack[++top] = i;	
	}
	
	return ans;
}

// ---- TEST CASES ----
record TestCase(int[] Input, int[] Expected, string Description);

void RunTests()
{
	var cases = new List<TestCase>
	{
		new (
			[73,74,75,71,69,72,76,73],
			[1,1,4,2,1,1,0,0],
			"Classic LeetCode example"
		),
		new (
			[30,40,50,60],
			[1,1,1,0],
			"Strictly increasing temperatures"
		),
		new (
			[30,60,90],
			[1,1,0],
			"Increasing, larger jumps"
		),
		new (
			[90,80,70,60],
			[0,0,0,0],
			"Strictly decreasing temperatures - never warmer"
		),
		new (
			[55],
			[0],
			"Single day, no future days"
		),
		new (
			[70,70,70,70],
			[0,0,0,0],
			"All equal temperatures - never strictly warmer"
		),
		new (
			[73,74,75,71,69,72,76,73,69,70,71,72],
			[1,1,4,2,1,1,0,0,1,1,1,0],
			"Longer mixed sequence"
		),
	};

	var results = new List<object>();

	foreach (var tc in cases)
	{
		string status;
		int[] actual = null;
		string errorMsg = null;

		try
		{
			actual = DailyTemperatures(tc.Input.ToArray()); // copy, in case of in-place mutation
			status = actual.SequenceEqual(tc.Expected) ? "PASS" : "FAIL";
		}
		catch (NotImplementedException)
		{
			status = "NOT IMPLEMENTED";
		}
		catch (Exception ex)
		{
			status = "ERROR";
			errorMsg = ex.Message;
		}

		results.Add(new
		{
			tc.Description,
			Input = string.Join(",", tc.Input),
			Expected = string.Join(",", tc.Expected),
			Actual = actual is null ? "-" : string.Join(",", actual),
			Status = status,
			Error = errorMsg ?? ""
		});
	}

	results.Dump("Daily Temperatures - Test Results");

	int passCount = results.Count(r => ((dynamic)r).Status == "PASS");
	$"{passCount} / {cases.Count} tests passed".Dump("Summary");
}

