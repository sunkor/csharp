<Query Kind="Program" />

// C# has a built-in PriorityQueue<TElement, TPriority> (min-heap by default).
// For a max heap, we use a reversed comparer.

void Main()
{
	DemoMinQueue();
	DemoMaxQueue();
}

void DemoMinQueue()
{
	// PriorityQueue dequeues the element with the LOWEST priority value first (min-heap).
	var minQueue = new PriorityQueue<string, int>();

	minQueue.Enqueue("Low priority task", 10);
	minQueue.Enqueue("Critical task", 1);
	minQueue.Enqueue("Medium priority task", 5);
	minQueue.Enqueue("High priority task", 3);
	minQueue.Enqueue("Very low task", 20);

	var results = new List<(string Task, int Priority)>();
	while (minQueue.TryDequeue(out var item, out var priority))
		results.Add((item, priority));

	results.Dump("🔼 Min Priority Queue (lowest number = highest priority, dequeued first)");
}

void DemoMaxQueue()
{
	// For a max-heap, supply a comparer that inverts the natural int ordering.
	// This makes the HIGHEST priority value dequeue first.
	var maxComparer = Comparer<int>.Create((a, b) => b.CompareTo(a));
	var maxQueue = new PriorityQueue<string, int>(maxComparer);

	maxQueue.Enqueue("Low priority task", 10);
	maxQueue.Enqueue("Critical task", 1);
	maxQueue.Enqueue("Medium priority task", 5);
	maxQueue.Enqueue("High priority task", 3);
	maxQueue.Enqueue("Very low task", 20);

	var results = new List<(string Task, int Priority)>();
	while (maxQueue.TryDequeue(out var item, out var priority))
		results.Add((item, priority));

	results.Dump("🔽 Max Priority Queue (highest number = highest priority, dequeued first)");
}

