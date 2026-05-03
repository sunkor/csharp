<Query Kind="Statements" />

var ls = new LogSystem();
ls.Put(1, 100);
ls.Put(2, 150);
ls.Put(3, 150);
ls.Put(4, 200);
ls.Put(5, 300);

new[]
{
	new { Start = 100L, End = 200L, Result = ls.Retrieve(100, 200) },
	new { Start = 150L, End = 300L, Result = ls.Retrieve(150, 300) },
	new { Start = 200L, End = 250L, Result = ls.Retrieve(200, 250) },
	new { Start =  50L, End =  99L, Result = ls.Retrieve( 50,  99) },
}
.Dump("Timestamp Retrieval");

public class LogSystem
{
	private SortedDictionary<long, List<int>> logs = new();

	public void Put(int id, long timestamp)
	{
		if (!logs.ContainsKey(timestamp))
			logs[timestamp] = new List<int>();

		logs[timestamp].Add(id);
	}

	public IList<int> Retrieve(long start, long end)
	{
		var result = new List<int>();

		foreach (var kv in logs)
		{
			if (kv.Key < start) continue;
			if (kv.Key > end) break;

			result.AddRange(kv.Value);
		}

		return result;
	}
}