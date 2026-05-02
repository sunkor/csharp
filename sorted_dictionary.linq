<Query Kind="Statements" />

//SortedDictionary vs Dictionary
//Underlying Implementation
//
//Dictionary: Hash table with separate chaining
//•	Uses hash functions to compute bucket indices
//•	O(1) average-case lookup, insert, delete
//
//SortedDictionary: Red - Black tree(self - balancing binary search tree)
//•	Maintains items in sorted order by key
//•	O(log n) for all operations (lookup, insert, delete)
//
//Performance Comparison
//Operation	Dictionary	SortedDictionary
//Lookup	O(1) avg	O(log n)
//Insert	O(1) avg	O(log n)
//Delete	O(1) avg	O(log n)
//
//Iteration	Random order	Sorted order (free)
//Memory	Lower overhead	Higher overhead
//Worst-case	O(n) with collisions	O(log n) guaranteed
//
//When to Use
//
//Use Dictionary˂TKey, TValue˃ when:
//•	✓ Fast lookups are critical
//•	✓ You don't need sorted order
//•	✓ You have millions of items
//•	✓ Memory efficiency matters
//•	✓ Average performance over worst-case
//
//Use SortedDictionary˂TKey, TValue˃ when:
//•	✓ You need automatic key ordering
//•	✓ You iterate over items frequently in sorted order
//•	✓ You need range queries (e.g., "all keys from A to Z")
//•	✓ Worst-case O(log n) guarantee is important
//•	✓ You need predictable performance (no hash collisions)
//
//Key insight: If sorted iteration is essential, SortedDictionary saves you from having to sort separately—it's the sort cost built into every operation. For read-heavy, iteration-heavy workflows, it can be worth the lookup trade-off.


// SortedDictionary<TKey, TValue> - maintains items in sorted order by key
// Useful when you need automatic key ordering with dictionary operations

"Creating and populating a SortedDictionary:".Dump();

var students = new SortedDictionary<string, int>
{
    { "Charlie", 85 },
    { "Alice", 92 },
    { "Bob", 88 },
    { "Diana", 95 }
};

students.Dump("Students by name (sorted alphabetically)");

// Add more items
students["Eve"] = 78;
students.Add("Frank", 91);

students.Dump("After adding Eve and Frank");

"\n--- Basic Operations ---".Dump();

// Check if key exists
$"Contains 'Alice': {students.ContainsKey("Alice")}".Dump();
$"Contains 'Zoe': {students.ContainsKey("Zoe")}".Dump();

// Get value by key
if (students.TryGetValue("Bob", out int bobScore))
    $"Bob's score: {bobScore}".Dump();

// Count
$"Total students: {students.Count}".Dump();

"\n--- Iteration (automatically sorted) ---".Dump();

students.Dump("All entries");

foreach (var (name, score) in students)
{
    $"{name}: {score}".Dump();
}

"\n--- Removing items ---".Dump();

students.Remove("Frank");
$"After removing Frank: {students.Count} students remain".Dump();

"\n--- Keys and Values Collections ---".Dump();

students.Keys.Dump("Keys (in sorted order)");
students.Values.Dump("Values");

"\n--- Numeric Keys Example ---".Dump();

var scores = new SortedDictionary<int, string>
{
    { 100, "Excellent" },
    { 70, "Good" },
    { 50, "Fair" },
    { 90, "Very Good" }
};

scores.Dump("Numeric keys are also sorted");

"\n--- Custom Comparer Example ---".Dump();

// Sort in descending order
var descending = new SortedDictionary<string, int>(
    Comparer<string>.Create((a, b) => b.CompareTo(a))
)
{
    { "Charlie", 85 },
    { "Alice", 92 },
    { "Bob", 88 }
};

descending.Dump("Descending alphabetical order");



