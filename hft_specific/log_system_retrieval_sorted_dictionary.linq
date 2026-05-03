<Query Kind="Statements" />

// Create a log system with a sliding window of 150 time units
var ls = new LogSystem(windowSize: 150);
ls.Put(1, 100);
ls.Put(2, 150);
ls.Put(3, 150);
ls.Put(4, 200);
ls.Put(5, 300);
// After Put(5, 300): window is [150, 300], so timestamp 100 is pruned
ls.Put(6, 400);
// After Put(6, 400): window is [250, 400], so timestamps 150, 200 are pruned

new[]
{
	new { Start = 100L, End = 200L, Result = ls.Retrieve(100, 200) },
	new { Start = 150L, End = 300L, Result = ls.Retrieve(150, 300) },
	new { Start = 200L, End = 250L, Result = ls.Retrieve(200, 250) },
	new { Start =  50L, End =  99L, Result = ls.Retrieve( 50,  99) },
	new { Start = 300L, End = 400L, Result = ls.Retrieve(300, 400) },
}
.Dump("Timestamp Retrieval");

public class LogSystem
{
	// Stores log entries keyed by timestamp; each timestamp can hold multiple IDs
	private SortedDictionary<long, List<int>> logs = new();

	// The maximum span of time the log retains; entries older than (latestTimestamp - windowSize) are pruned
	private readonly long windowSize;

	/// <summary>
	/// Initialises the log system with an optional sliding window size.
	/// Defaults to <see cref="long.MaxValue"/> (no pruning).
	/// </summary>
	public LogSystem(long windowSize = long.MaxValue)
	{
		this.windowSize = windowSize;
	}

	/// <summary>
	/// Records a log entry with the given <paramref name="id"/> at the given <paramref name="timestamp"/>,
	/// then prunes any entries that fall outside the sliding window.
	/// </summary>
	public void Put(int id, long timestamp)
	{
		// Ensure a bucket exists for this timestamp before adding
		if (!logs.ContainsKey(timestamp))
			logs[timestamp] = new List<int>();

		logs[timestamp].Add(id);

		// Prune entries that fall outside the sliding window
		// Any timestamp at or before the cutoff is too old to retain
		long cutoff = timestamp - windowSize;
		while (logs.Count > 0 && logs.Keys.First() <= cutoff)
			logs.Remove(logs.Keys.First());
	}

	/// <summary>
	/// Returns all log IDs whose timestamps fall within [<paramref name="start"/>, <paramref name="end"/>] (inclusive).
	/// Only considers entries currently retained within the sliding window.
	/// </summary>
	public IList<int> Retrieve(long start, long end)
	{
		var result = new List<int>();

		// SortedDictionary iterates in ascending key order, so we can break early once past the end
		foreach (var kv in logs)
		{
			if (kv.Key < start) continue; // not yet in range
			if (kv.Key > end) break;      // past the range; no need to continue

			result.AddRange(kv.Value);
		}

		return result;
	}
}