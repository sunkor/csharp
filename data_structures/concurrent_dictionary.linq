<Query Kind="Statements" />

using System.Collections.Concurrent;
using System.Threading.Tasks;

// ============================================================================
// ConcurrentDictionary Sample Usage
// A thread-safe dictionary for use in multithreaded scenarios
// ============================================================================

"Basic Operations".Dump();
var cd = new ConcurrentDictionary<string, int>();

// TryAdd - safely add if key doesn't exist
cd.TryAdd("apple", 5);
cd.TryAdd("banana", 3);
cd.TryAdd("cherry", 7);

// TryAdd returns false if key already exists
bool added = cd.TryAdd("apple", 10);  // Returns false, doesn't overwrite
$"TryAdd('apple', 10) returned: {added}".Dump();

cd.Dump("Dictionary after initial adds");

"---".Dump();
"Getting Values".Dump();

// TryGetValue - safely retrieve a value
if (cd.TryGetValue("banana", out int value))
{
	$"Value of 'banana': {value}".Dump();
}

// Direct indexer access also works
$"Value of 'cherry': {cd["cherry"]}".Dump();

"---".Dump();
"Updating Values".Dump();

// AddOrUpdate - atomically add or update
cd.AddOrUpdate("banana", 10, (key, oldValue) => oldValue + 5);
$"After AddOrUpdate('banana'): {cd["banana"]}".Dump();

// TryUpdate - update only if current value matches
bool updated = cd.TryUpdate("apple", 20, 5);  // Update apple from 5 to 20
$"TryUpdate('apple', 20, 5) returned: {updated}".Dump();

cd.Dump("Dictionary after updates");

"---".Dump();
"Removing Values".Dump();

// TryRemove - safely remove an entry
bool removed = cd.TryRemove("cherry", out int removedValue);
$"TryRemove('cherry') removed value: {removedValue}".Dump();

cd.Dump("Dictionary after removal");

"---".Dump();
"Thread-Safe Concurrent Modifications".Dump();

// Create a fresh dictionary for concurrent demo
var scores = new ConcurrentDictionary<string, int>();
scores.TryAdd("Player A", 0);
scores.TryAdd("Player B", 0);
scores.TryAdd("Player C", 0);

"Running concurrent score updates...".Dump();

// Launch multiple tasks to update scores concurrently
var tasks = new[]
{
	Task.Run(() =>
	{
		for (int i = 0; i < 1000; i++)
		{
			scores.AddOrUpdate("Player A", 1, (k, v) => v + 1);
		}
	}),
	Task.Run(() =>
	{
		for (int i = 0; i < 1000; i++)
		{
			scores.AddOrUpdate("Player B", 1, (k, v) => v + 1);
		}
	}),
	Task.Run(() =>
	{
		for (int i = 0; i < 1000; i++)
		{
			scores.AddOrUpdate("Player C", 1, (k, v) => v + 1);
		}
	})
};

Task.WaitAll(tasks);
scores.Dump("Final scores after concurrent updates");

"---".Dump();
"Useful Properties & Methods".Dump();

// Count, Keys, Values, IsEmpty
$"Count: {cd.Count}".Dump();
$"Keys: {string.Join(", ", cd.Keys)}".Dump();
$"IsEmpty: {cd.IsEmpty}".Dump();

// GetOrAdd - get existing or add new value
int bananaCount = cd.GetOrAdd("banana", 999);  // Returns existing 8
int date = cd.GetOrAdd("date", 2);             // Adds new entry with value 2
$"GetOrAdd results - banana: {bananaCount}, date: {date}".Dump();

cd.Dump("Final dictionary");

"---".Dump();
"Performance Benefit Demo".Dump();

// ConcurrentDictionary is ideal when you have:
// • Multiple threads reading/writing simultaneously
// • High contention on the dictionary
// • Need for atomic operations like AddOrUpdate
// • No locking overhead from explicit lock() statements

$"ConcurrentDictionary provides lock-free reads for better performance in multithreaded scenarios".Dump();

var concDict = new ConcurrentDictionary<string, int>();

Task.Run(() => concDict.TryAdd("key", 1));  // ✓ Returns true
Task.Run(() => concDict.TryAdd("key", 2));  // ✓ Returns false (already exists)

// The entire operation is atomic - no race condition possible
// Only ONE thread wins; others see the key already exists