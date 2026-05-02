<Query Kind="Statements">
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// Trying to count concurrent increments
var dict = new Dictionary<string, int>();
dict["count"] = 0;

// 5 threads, each incrementing 1000 times
var tasks = new List<Task>();
for (int i = 0; i < 5; i++)
{
	tasks.Add(Task.Run(() =>
	{
		for (int j = 0; j < 1000; j++)
		{
			int current = dict["count"];
			dict["count"] = current + 1;  // Lost updates!
		}
	}));
}

await Task.WhenAll(tasks);

dict["count"].Dump();

// Result: Maybe 2500 instead of 5000 (lost increments)
// ConcurrentDictionary.AddOrUpdate() would get exactly 5000

// Trying to count concurrent increments
var concurrent_dict = new ConcurrentDictionary<string, int>();
concurrent_dict["count"] = 0;

// 5 threads, each incrementing 1000 times
tasks = new List<Task>();
for (int i = 0; i < 5; i++)
{
	tasks.Add(Task.Run(() =>
	{
		for (int j = 0; j < 1000; j++)
		{
			int current = concurrent_dict["count"];
			concurrent_dict.AddOrUpdate("count", 1, (key, oldValue) => oldValue + 1);  // Atomic increment
		}
	}));
}

await Task.WhenAll(tasks);

concurrent_dict["count"].Dump();