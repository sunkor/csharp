<Query Kind="Statements" />

// Simple PriorityQueue example
var taskQueue = new PriorityQueue<string, int>();

// Add tasks with priorities (lower numbers = higher priority)
taskQueue.Enqueue("Fix critical bug", 1);
taskQueue.Enqueue("Update documentation", 3);
taskQueue.Enqueue("Review pull request", 2);
taskQueue.Enqueue("Refactor legacy code", 4);
taskQueue.Enqueue("Deploy to production", 1);

"Tasks processed in priority order:".Dump();
"(Lower priority number = processed first)".Dump();
"".Dump();

int count = 1;
while (taskQueue.TryDequeue(out var task, out var priority))
{
    $"{count}. Priority {priority}: {task}".Dump();
    count++;
}

