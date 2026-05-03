<Query Kind="Statements" />

// LinkedList<T> is a doubly-linked list that provides O(1) insertion/removal at known positions
// It's useful when you frequently insert/remove items in the middle of a collection

var list = new LinkedList<string>();

// Add items to the end
// AddLast: O(1) time, O(1) space
list.AddLast("Alice");
list.AddLast("Bob");
list.AddLast("Charlie");

"Initial list:".Dump();
list.Dump();

// Add to the beginning
// AddFirst: O(1) time, O(1) space
list.AddFirst("Zoe");
"After AddFirst('Zoe'):".Dump();
list.Dump();

// Get a reference to a node and insert relative to it
// Find: O(n) time, O(1) space (linear search for value)
// AddAfter: O(1) time, O(1) space (constant once node is found)
var bobNode = list.Find("Bob");
list.AddAfter(bobNode, "Bob Jr.");
"After AddAfter(Bob):".Dump();
list.Dump();

// Insert before a node
// AddBefore: O(1) time, O(1) space
list.AddBefore(bobNode, "B-list");
"After AddBefore(Bob):".Dump();
list.Dump();

// Remove specific node
// Remove: O(n) time, O(1) space (linear search for value)
list.Remove("B-list");
"After Remove('B-list'):".Dump();
list.Dump();

// Remove from specific positions
// RemoveFirst/RemoveLast: O(1) time, O(1) space
list.RemoveFirst();
list.RemoveLast();
"After RemoveFirst() and RemoveLast():".Dump();
list.Dump();

// Traverse the list
// Forward traversal: O(n) time, O(1) space
"Forward traversal:".Dump();
var node = list.First;
while (node != null)
{
    $"  {node.Value}".Dump();
    node = node.Next;
}

// Backward traversal: O(n) time, O(1) space
"Backward traversal:".Dump();
node = list.Last;
while (node != null)
{
    $"  {node.Value}".Dump();
    node = node.Previous;
}

// Contains: O(n) time, O(1) space (linear search)
$"Contains 'Bob': {list.Contains("Bob")}".Dump();
$"Contains 'Dave': {list.Contains("Dave")}".Dump();

// Clear: O(n) time, O(1) space (deallocation of all nodes)
list.Clear();
$"After Clear() - Count: {list.Count}".Dump();

