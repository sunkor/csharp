<Query Kind="Statements" />

// Dictionary Usage Sample
// Demonstrates common operations and patterns with C# dictionaries

// 1. CREATING DICTIONARIES
var emptyDict = new Dictionary<string, int>();
var initializedDict = new Dictionary<string, string>
{
    { "apple", "a red fruit" },
    { "banana", "a yellow fruit" },
    { "cherry", "a small red fruit" }
};

"1. Creating Dictionaries".Dump();
initializedDict.Dump("Dictionary initialized with values");

// 2. ADDING AND UPDATING
emptyDict["count"] = 1;
emptyDict["count"] = 2;  // Updates existing key
emptyDict["total"] = 5;

"2. Adding and Updating".Dump();
emptyDict.Dump("Added values (count was updated)");

// 3. ACCESSING VALUES
var fruit = initializedDict["apple"];
fruit.Dump("Retrieved value for 'apple'");

// 4. SAFE RETRIEVAL WITH TryGetValue
if (initializedDict.TryGetValue("banana", out var bananaDesc))
{
    bananaDesc.Dump("Safely retrieved 'banana' description");
}
else
{
    "Key not found".Dump();
}

// 5. CHECKING FOR KEY EXISTENCE
initializedDict.ContainsKey("apple").Dump("Does 'apple' exist?");
initializedDict.ContainsKey("orange").Dump("Does 'orange' exist?");

// 6. REMOVING ITEMS
var removed = initializedDict.Remove("cherry");
removed.Dump("Did removal succeed?");
initializedDict.Dump("Dictionary after removing 'cherry'");

// 7. ITERATING THROUGH DICTIONARIES
"7. Iterating Through Dictionaries".Dump();

"Using foreach with KeyValuePair:".Dump();
foreach (var kvp in initializedDict)
{
    $"{kvp.Key} → {kvp.Value}".Dump();
}

"Using Keys property:".Dump();
initializedDict.Keys.Dump("All keys");

"Using Values property:".Dump();
initializedDict.Values.Dump("All values");

// 8. DICTIONARY COUNTS AND PROPERTIES
"8. Dictionary Properties".Dump();
initializedDict.Count.Dump("Number of items");
emptyDict.Count.Dump("Count of emptyDict (after additions)");

// 9. CLEARING A DICTIONARY
var dictToClear = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
dictToClear.Clear();
dictToClear.Count.Dump("Count after Clear()");

// 10. GETTING OR ADDING (Pattern)
"10. GetValueOrDefault Pattern".Dump();
var languageDict = new Dictionary<string, string>
{
    { "en", "English" },
    { "fr", "French" }
};

var unknownLang = languageDict.GetValueOrDefault("de", "Unknown");
unknownLang.Dump("GetValueOrDefault returns 'Unknown' for missing key");

languageDict.GetValueOrDefault("en").Dump("GetValueOrDefault returns value if key exists");

// 11. LINQ WITH DICTIONARIES
"11. LINQ Operations on Dictionaries".Dump();
languageDict
    .Where(kvp => kvp.Key.Length == 2)
    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToUpper())
    .Dump("Filtered and transformed dictionary");

var scores = new Dictionary<string, int>
{
    { "Alice", 95 },
    { "Bob", 87 },
    { "Charlie", 92 }
};

scores
    .OrderByDescending(kvp => kvp.Value)
    .Select(kvp => $"{kvp.Key}: {kvp.Value}")
    .Dump("Scores sorted by value (descending)");

// 12. DICTIONARY INITIALIZATION WITH LINQ
"12. Creating Dictionary from LINQ".Dump();
Enumerable.Range(1, 5)
    .ToDictionary(i => $"item{i}", i => i * 10)
    .Dump("Dictionary from range");

// 13. MERGING DICTIONARIES
"13. Merging Dictionaries".Dump();
var dict1 = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
var dict2 = new Dictionary<string, int> { { "c", 3 }, { "d", 4 } };

var merged = dict1.Concat(dict2).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
merged.Dump("Two dictionaries merged");

// 14. CASE-INSENSITIVE DICTIONARY
"14. Case-Insensitive Dictionary".Dump();
var caseInsensitiveDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    { "Key1", "value1" },
    { "KEY2", "value2" }
};

caseInsensitiveDict["key1"].Dump("Case-insensitive lookup works");

