<Query Kind="Program" />

// MERGE INTERVALS - Problem brief, test cases & validation harness
//
// PROBLEM:
// Given an array of intervals where intervals[i] = [start_i, end_i],
// merge all overlapping intervals, and return an array of the
// non-overlapping intervals that cover all the intervals in the input.
//
// Intervals are considered overlapping if they touch or intersect,
// e.g. [1,3] and [2,6] overlap -> merge to [1,6].
// Also touching intervals like [1,4] and [4,5] are considered
// overlapping (merge to [1,5]) - adjust this rule if you want strict
// overlap only (see NOTE below).
//
// Input is not guaranteed to be sorted by start.
//
// EXAMPLES:
//   Input:  [[1,3],[2,6],[8,10],[15,18]]
//   Output: [[1,6],[8,10],[15,18]]
//
//   Input:  [[1,4],[4,5]]
//   Output: [[1,5]]
//
// CONSTRAINTS:
//   0 <= intervals.length <= 10^4
//   intervals[i].length == 2
//   0 <= start_i <= end_i <= 10^5 (assume ints; can be negative too)
//
// NOTE: If you want "touching but not overlapping" (e.g. [1,2],[3,4])
// to NOT merge, that's the standard behavior below already (only
// overlap/adjacency where next.start <= current.end triggers merge).

void Main()
{
	RunTests();
}

// ---- Implement this method ----
int[][] MergeIntervals(int[][] intervals)
{
	throw new NotImplementedException("Implement me!");
}

// ---- Test harness ----
record TestCase(string Name, int[][] Input, int[][] Expected);

void RunTests()
{
	var cases = new List<TestCase>
	{
		new("Example 1 - basic overlap",
			[[1,3],[2,6],[8,10],[15,18]],
			[[1,6],[8,10],[15,18]]),

		new("Example 2 - touching intervals merge",
			[[1,4],[4,5]],
			[[1,5]]),

		new("Empty input",
			[],
			[]),

		new("Single interval",
			[[5,7]],
			[[5,7]]),

		new("No overlap at all",
			[[1,2],[3,4],[5,6]],
			[[1,2],[3,4],[5,6]]),

		new("All merge into one",
			[[1,10],[2,3],[4,5],[6,7],[8,9]],
			[[1,10]]),

		new("Unsorted input",
			[[5,6],[1,3],[2,4],[15,18],[8,10]],
			[[1,4],[5,6],[8,10],[15,18]]),

		new("Duplicate intervals",
			[[1,3],[1,3],[1,3]],
			[[1,3]]),

		new("Negative numbers",
			[[-5,-2],[-3,0],[1,2]],
			[[-5,0],[1,2]]),

		new("Fully contained interval",
			[[1,10],[2,3]],
			[[1,10]]),

		new("Adjacent but not overlapping (strict) - should NOT merge",
			[[1,2],[3,4]],
			[[1,2],[3,4]]),
	};

	var results = new List<object>();
	int passCount = 0;

	foreach (var tc in cases)
	{
		string status;
		string details = "";
		try
		{
			// clone input in case implementation mutates it
			var inputCopy = tc.Input.Select(a => (int[])a.Clone()).ToArray();
			var actual = MergeIntervals(inputCopy);
			bool pass = IntervalsEqual(actual, tc.Expected);
			status = pass ? "PASS" : "FAIL";
			if (pass) passCount++;
			details = $"Expected: {Format(tc.Expected)} | Actual: {Format(actual)}";
		}
		catch (NotImplementedException)
		{
			status = "NOT IMPLEMENTED";
		}
		catch (Exception ex)
		{
			status = "ERROR";
			details = ex.Message;
		}

		results.Add(new { tc.Name, Status = status, Input = Format(tc.Input), Details = details });
	}

	results.Dump("Merge Intervals - Test Results");
	$"{passCount}/{cases.Count} tests passed".Dump();
}

static bool IntervalsEqual(int[][] a, int[][] b)
{
	if (a == null || b == null) return a == b;
	if (a.Length != b.Length) return false;
	for (int i = 0; i < a.Length; i++)
	{
		if (a[i].Length != 2 || b[i].Length != 2) return false;
		if (a[i][0] != b[i][0] || a[i][1] != b[i][1]) return false;
	}
	return true;
}

static string Format(int[][] intervals) =>
	"[" + string.Join(",", intervals.Select(iv => $"[{iv[0]},{iv[1]}]")) + "]";

