<Query Kind="Statements" />

using System.Collections.Generic;

// Create a queue of integers
var queue = new Queue<int>();

"Queue Operations Demo".Dump();

// Enqueue items
"Enqueuing values: 10, 20, 30, 40, 50".Dump();
queue.Enqueue(10);
queue.Enqueue(20);
queue.Enqueue(30);
queue.Enqueue(40);
queue.Enqueue(50);

$"Queue count: {queue.Count}".Dump();

// Peek at the first item without removing it
$"Peek (first item): {queue.Peek()}".Dump();

// Dequeue items
"Dequeuing items:".Dump();
while (queue.Count > 0)
{
	int item = queue.Dequeue();
	$"Dequeued: {item}, Remaining: {queue.Count}".Dump();
}

// Demonstrate with a queue of strings
"".Dump();
"Queue of Strings Example".Dump();
var stringQueue = new Queue<string>();
stringQueue.Enqueue("First");
stringQueue.Enqueue("Second");
stringQueue.Enqueue("Third");

"Processing string queue:".Dump();
while (stringQueue.Count > 0)
{
	$"Processing: {stringQueue.Dequeue()}".Dump();
}
