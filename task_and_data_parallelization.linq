<Query Kind="Program" />

using System.Threading.Tasks;
using System.Diagnostics;

// Task Parallelization and Data Parallelization Examples
// Demonstrates how to process work concurrently and in parallel

async Task Main()
{
	"=== PARALLELIZATION EXAMPLES ===".Dump();
	"".Dump();
	
	await TaskParallelizationExamples();
	await DataParallelizationExamples();
	await PerformanceComparison();
}

// ============================================================================
// TASK PARALLELIZATION - Running multiple independent tasks concurrently
// ============================================================================

async Task TaskParallelizationExamples()
{
	"TASK PARALLELIZATION".Dump("heading");
	"Running multiple independent operations concurrently".Dump();
	"".Dump();
	
	// Example 1: Task.WhenAll - wait for all tasks to complete
	"Example 1: Task.WhenAll".Dump("subheading");
	var sw = Stopwatch.StartNew();
	
	var task1 = FetchDataAsync("Database1", 2000);
	var task2 = FetchDataAsync("Database2", 1500);
	var task3 = FetchDataAsync("Database3", 1000);
	
	var results = await Task.WhenAll(task1, task2, task3);
	sw.Stop();
	
	new { ElapsedMs = sw.ElapsedMilliseconds, Results = results }.Dump("Result");
	"Note: Total time ~2000ms (longest task), not 4500ms (sum)".Dump("info");
	"".Dump();
	
	// Example 2: Task.WhenAny - wait for first task to complete
	"Example 2: Task.WhenAny".Dump("subheading");
	sw.Restart();
	
	var fastTask = FetchDataAsync("FastDB", 500);
	var slowTask = FetchDataAsync("SlowDB", 5000);
	
	var completedTask = await Task.WhenAny(fastTask, slowTask);
	sw.Stop();
	
	new { 
		ElapsedMs = sw.ElapsedMilliseconds,
		FirstResult = await completedTask,
		OtherTaskStillRunning = !slowTask.IsCompleted
	}.Dump("Result");
	"".Dump();
	
	// Example 3: Parallel I/O with maximum concurrency control
	"Example 3: Limiting Concurrency (SemaphoreSlim)".Dump("subheading");
	var semaphore = new System.Threading.SemaphoreSlim(2); // Max 2 concurrent tasks
	
	sw.Restart();
	var tasks = Enumerable.Range(1, 5)
		.Select(i => RunWithLimitAsync(semaphore, i))
		.ToList();
	
	var concurrencyResults = await Task.WhenAll(tasks);
	sw.Stop();
	
	new { 
		ElapsedMs = sw.ElapsedMilliseconds,
		Results = concurrencyResults
	}.Dump("Result");
	"With semaphore(2): processes 5 tasks with max 2 concurrent".Dump("info");
	"".Dump();
}

async Task<string> FetchDataAsync(string source, int delayMs)
{
	await Task.Delay(delayMs);
	return $"{source} (took {delayMs}ms)";
}

async Task<string> RunWithLimitAsync(System.Threading.SemaphoreSlim semaphore, int taskId)
{
	await semaphore.WaitAsync();
	try
	{
		var sw = Stopwatch.StartNew();
		await Task.Delay(1000);
		sw.Stop();
		return $"Task{taskId} completed in {sw.ElapsedMilliseconds}ms";
	}
	finally
	{
		semaphore.Release();
	}
}

// ============================================================================
// DATA PARALLELIZATION - Processing large datasets in parallel
// ============================================================================

async Task DataParallelizationExamples()
{
	"DATA PARALLELIZATION (PLINQ)".Dump("heading");
	"Processing large sequences using multiple cores".Dump();
	"".Dump();
	
	var data = Enumerable.Range(1, 10000).ToList();
	
	// Example 1: Sequential LINQ
	"Example 1: Sequential Processing".Dump("subheading");
	var sw = Stopwatch.StartNew();
	
	var sequentialResult = data
		.Where(x => IsPrime(x))
		.Select(x => x * x)
		.Take(10)
		.ToList();
	
	sw.Stop();
	new { 
		Method = "Sequential",
		Results = sequentialResult,
		ElapsedMs = sw.ElapsedMilliseconds
	}.Dump("Result");
	"".Dump();
	
	// Example 2: Parallel LINQ (PLINQ)
	"Example 2: Parallel Processing (AsParallel)".Dump("subheading");
	sw.Restart();
	
	var parallelResult = data
		.AsParallel()
		.Where(x => IsPrime(x))
		.Select(x => x * x)
		.Take(10)
		.ToList();
	
	sw.Stop();
	new { 
		Method = "Parallel (AsParallel)",
		Results = parallelResult,
		ElapsedMs = sw.ElapsedMilliseconds
	}.Dump("Result");
	"".Dump();
	
	// Example 3: Controlling parallelism
	"Example 3: Controlling Parallelism".Dump("subheading");
	sw.Restart();
	
	var controlledResult = data
		.AsParallel()
		.WithDegreeOfParallelism(2)  // Use only 2 cores
		.Where(x => IsPrime(x))
		.Select(x => x * x)
		.Take(10)
		.ToList();
	
	sw.Stop();
	new { 
		Method = "Parallel (2 cores only)",
		Results = controlledResult,
		ElapsedMs = sw.ElapsedMilliseconds
	}.Dump("Result");
	"".Dump();
	
	// Example 4: Partitioned processing (for better performance on large datasets)
	"Example 4: Partitioned Parallel Processing".Dump("subheading");
	sw.Restart();
	
	var partitioner = System.Collections.Concurrent.Partitioner.Create(data, true);
	var partitionedResult = new System.Collections.Concurrent.ConcurrentBag<int>();
	
	Parallel.ForEach(partitioner, x =>
	{
		if (IsPrime(x))
			partitionedResult.Add(x * x);
	});
	
	sw.Stop();
	new { 
		Method = "Parallel.ForEach",
		Results = partitionedResult.Take(10).OrderBy(x => x).ToList(),
		ElapsedMs = sw.ElapsedMilliseconds
	}.Dump("Result");
	"".Dump();
}

bool IsPrime(int number)
{
	if (number < 2) return false;
	if (number == 2) return true;
	if (number % 2 == 0) return false;
	for (int i = 3; i * i <= number; i += 2)
		if (number % i == 0) return false;
	return true;
}

// ============================================================================
// PERFORMANCE COMPARISON - Side-by-side comparison
// ============================================================================

async Task PerformanceComparison()
{
	"PERFORMANCE COMPARISON".Dump("heading");
	"Comparing different parallelization strategies".Dump();
	"".Dump();
	
	// Heavy workload data
	var heavyData = Enumerable.Range(1, 5000).ToList();
	var results = new List<dynamic>();
	
	// 1. Sequential processing
	var sw = Stopwatch.StartNew();
	var seq = heavyData.Where(x => IsPrime(x)).Count();
	sw.Stop();
	results.Add(new { 
		Strategy = "Sequential LINQ",
		Count = seq,
		ElapsedMs = sw.ElapsedMilliseconds 
	});
	
	// 2. Parallel LINQ
	sw.Restart();
	var par = heavyData.AsParallel().Where(x => IsPrime(x)).Count();
	sw.Stop();
	results.Add(new { 
		Strategy = "PLINQ (AsParallel)",
		Count = par,
		ElapsedMs = sw.ElapsedMilliseconds 
	});
	
	// 3. Parallel.ForEach
	sw.Restart();
	int parCount = 0;
	Parallel.ForEach(heavyData, x => 
	{
		if (IsPrime(x))
			Interlocked.Increment(ref parCount);
	});
	sw.Stop();
	results.Add(new { 
		Strategy = "Parallel.ForEach",
		Count = parCount,
		ElapsedMs = sw.ElapsedMilliseconds 
	});
	
	// 4. Task parallelization with multiple chunks
	sw.Restart();
	var chunkSize = heavyData.Count / Environment.ProcessorCount;
	var chunks = heavyData
		.Select((x, i) => new { x, i })
		.GroupBy(xi => xi.i / chunkSize)
		.Select(g => g.Select(xi => xi.x).ToList())
		.ToList();
	
	var chunkTasks = chunks.Select(chunk => Task.Run(() => 
		chunk.AsParallel().Where(x => IsPrime(x)).Count()
	)).ToArray();
	
	var chunkResults = await Task.WhenAll(chunkTasks);
	sw.Stop();
	results.Add(new { 
		Strategy = "Task Parallelization (Chunked)",
		Count = chunkResults.Sum(),
		ElapsedMs = sw.ElapsedMilliseconds 
	});
	
	results.Dump("Strategy Comparison");
	
	"".Dump();
	"GUIDELINES:".Dump("heading");
	"• Use PLINQ when processing large, in-memory collections".Dump();
	"• Use Task.WhenAll for concurrent I/O operations".Dump();
	"• Use SemaphoreSlim to limit concurrency and control resources".Dump();
	"• Use Parallel.ForEach for CPU-bound work on collections".Dump();
	"• Overhead: parallelization has overhead—only worthwhile for significant work".Dump();
	"• Granularity: tasks should be large enough to justify overhead".Dump();
}

