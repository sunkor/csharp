<Query Kind="Statements" />

using System.Buffers;

// This script demonstrates several techniques for reducing MEMORY PRESSURE
// when working with strings in .NET. Strings are immutable reference types,
// so naive string manipulation (concatenation in loops, unnecessary
// Substring calls, etc.) can generate large amounts of garbage and trigger
// frequent GC collections. Below are practical patterns to avoid this.

"=== 1. StringBuilder instead of += in loops ===".Dump();

// BAD: each += creates a brand new string object, discarding the old one.
// For N iterations this creates O(N) garbage strings (often O(N^2) total
// characters copied).
string BadConcat(int count)
{
	string result = "";
	for (int i = 0; i < count; i++)
		result += i.ToString();
	return result;
}

// GOOD: StringBuilder maintains a resizable internal char buffer,
// avoiding repeated allocations. Pre-sizing the capacity avoids even
// the buffer's own internal reallocations.
string GoodConcat(int count)
{
	var sb = new StringBuilder(capacity: count * 3); // rough estimate
	for (int i = 0; i < count; i++)
		sb.Append(i);
	return sb.ToString();
}

var badBytesBefore = GC.GetAllocatedBytesForCurrentThread();
BadConcat(5000);
var badBytesAfter = GC.GetAllocatedBytesForCurrentThread();

var goodBytesBefore = GC.GetAllocatedBytesForCurrentThread();
GoodConcat(5000);
var goodBytesAfter = GC.GetAllocatedBytesForCurrentThread();

new
{
	BadConcatBytesAllocated = badBytesAfter - badBytesBefore,
	GoodConcatBytesAllocated = goodBytesAfter - goodBytesBefore,
}.Dump("Allocation comparison: += vs StringBuilder");

"=== 2. Span<char> / ReadOnlySpan<char> to avoid Substring allocations ===".Dump();

// BAD: Substring allocates a brand new string for every slice.
string csv = "alpha,beta,gamma,delta,epsilon";
string[] BadSplitParts(string s)
{
	var parts = new List<string>();
	int start = 0;
	for (int i = 0; i <= s.Length; i++)
	{
		if (i == s.Length || s[i] == ',')
		{
			parts.Add(s.Substring(start, i - start)); // allocation per field
			start = i + 1;
		}
	}
	return parts.ToArray();
}

// GOOD: ReadOnlySpan<char> lets you slice without allocating; only
// materialize a string when you actually need one (or avoid it entirely
// by working directly with spans, e.g. for parsing/comparison).
void GoodSplitParts(ReadOnlySpan<char> s, Action<ReadOnlySpan<char>> onField)
{
	int start = 0;
	for (int i = 0; i <= s.Length; i++)
	{
		if (i == s.Length || s[i] == ',')
		{
			onField(s.Slice(start, i - start)); // zero-allocation view
			start = i + 1;
		}
	}
}

BadSplitParts(csv).Dump("Bad: Substring-based split (allocates per field)");

var fieldsFound = new List<string>();
GoodSplitParts(csv, field => fieldsFound.Add(field.ToString())); // only allocate here if truly needed
fieldsFound.Dump("Good: span-based split (only allocates when materializing)");

"=== 3. String interning / caching for repeated values ===".Dump();

// If the same strings recur many times (e.g. parsed from a large file:
// country codes, status values), interning avoids storing many duplicate
// copies of identical text in memory.
string[] rawStatuses = Enumerable.Range(0, 10_000)
	.Select(i => new string(i % 3 == 0 ? "ACTIVE".ToCharArray() : "INACTIVE".ToCharArray())) // force distinct allocations
	.ToArray();

// BAD: keeps 10,000 separate string instances even though only 2 distinct values exist.
long distinctInstancesBad = rawStatuses.Select(s => (object)s).Distinct().Count(); // reference-based distinct
// (Note: .Distinct() on strings uses value equality by default; forcing
// reference identity above shows how many *separate objects* exist.)

// GOOD: intern (or use your own dictionary-based cache) so all equal
// strings share one instance.
string[] internedStatuses = rawStatuses.Select(string.Intern).ToArray();
long distinctInstancesGood = internedStatuses.Select(s => (object)s).Distinct().Count();

new
{
	DistinctObjectInstancesBeforeIntern = distinctInstancesBad,
	DistinctObjectInstancesAfterIntern = distinctInstancesGood,
}.Dump("Interning collapses duplicate strings to shared instances");

"=== 4. string.Create for building strings with a single allocation ===".Dump();

// BAD: formatting via interpolation of many pieces can create several
// intermediate strings before the final result.
string BadFormat(int a, int b) => "Sum of " + a + " and " + b + " is " + (a + b);

// GOOD: string.Create allocates the final string exactly once and writes
// directly into its buffer, with zero intermediate allocations.
string GoodFormat(int a, int b)
{
	int sum = a + b;
	// Rough max length calculation for the fixed layout below.
	Span<char> buffer = stackalloc char[64];
	int pos = 0;
	"Sum of ".AsSpan().CopyTo(buffer); pos += 7;
	a.TryFormat(buffer[pos..], out int w1); pos += w1;
	" and ".AsSpan().CopyTo(buffer[pos..]); pos += 5;
	b.TryFormat(buffer[pos..], out int w2); pos += w2;
	" is ".AsSpan().CopyTo(buffer[pos..]); pos += 4;
	sum.TryFormat(buffer[pos..], out int w3); pos += w3;
	return new string(buffer[..pos]); // single allocation for the result
}

new { BadFormat = BadFormat(3, 4), GoodFormat = GoodFormat(3, 4) }.Dump();

"=== 5. Avoid unnecessary ToString() / boxing in hot paths ===".Dump();

// Each call to ToString() on a value type allocates a new string. If you
// only need to compare or format occasionally, defer it; if you need it
// repeatedly, cache the result once rather than recomputing.
int code = 42;
string cachedCode = code.ToString(); // compute once
for (int i = 0; i < 3; i++)
	cachedCode.Dump("Reused cached string instead of re-calling ToString()");

"=== Summary ===".Dump();
new[]
{
	"Use StringBuilder (with pre-sized capacity) instead of += in loops.",
	"Use Span<char>/ReadOnlySpan<char> to slice/parse without allocating substrings.",
	"Intern or cache strings that repeat frequently with the same value.",
	"Use string.Create or manual span-writing to build a string in one allocation.",
	"Avoid redundant ToString() calls in hot paths; compute once and reuse.",
}.Dump("Techniques to reduce string-related memory pressure");

