<Query Kind="Program" />

/* ═══════════════════════════════════════════════════════════════════════════════════
   SPAN<T> AND READONLYSPAN<T> - COMPREHENSIVE GUIDE
   ═══════════════════════════════════════════════════════════════════════════════════

   WHAT IS SPAN<T>?
   ────────────────
   Span<T> is a lightweight, stack-allocated type that provides a managed view over a
   contiguous block of memory. It can wrap:
     • Arrays
     • Stackalloc buffers
     • Unmanaged pointers (via unsafe code)
     • Heap-allocated memory

   Unlike arrays, Span is a VALUE TYPE (struct), meaning it lives on the stack and
   provides zero-copy access to data.

   WHY USE SPAN?
   ─────────────
   1. ZERO-COPY SLICING: Create views of arrays without allocating new arrays.
      Before: arr.Skip(2).Take(3).ToArray() → allocates new array
      With Span: span.Slice(2, 3) → no allocation

   2. STACK ALLOCATION: Use stackalloc for temporary buffers, avoiding heap pressure.
      • Perfect for temporary work buffers, parsing, conversions
      • Safe only for small, fixed-size data (stack overflow risk on large allocations)

   3. UNIFORM MEMORY ABSTRACTION: Write one method that works with:
      • Arrays: Span<T> span = array;
      • Strings: ReadOnlySpan<char> span = text.AsSpan();
      • Stackalloc: Span<byte> span = stackalloc byte[256];
      • Unmanaged memory: Span<T> span = new Span<T>(ptr, length);

   4. PERFORMANCE: Span enables JIT compiler optimizations:
      • Better bounds-checking elimination
      • Inlining opportunities
      • SIMD vectorization

   KEY CONCEPTS
   ────────────
   SPAN<T> vs READONLYSPAN<T>:
     • Span<T>: Mutable view; can read and modify underlying data
     • ReadOnlySpan<T>: Immutable view; can only read data

   LIFETIME SAFETY:
     • Span must not outlive the data it wraps (compiler prevents stack escaping)
     • Cannot return Span from methods (unless ref struct or ref parameter)
     • ReadOnlySpan is slightly more flexible but still has lifetime restrictions

   NO ALLOCATION GUARANTEE:
     • Span itself allocates 16 bytes on the stack (pointer + length)
     • But doesn't allocate for the data it references
     • stackalloc allocates on the stack (not heap)

   PERFORMANCE CHARACTERISTICS:
     • Stack allocation: ~0.1 ns (extremely fast)
     • Indexing Span: Same as array indexing (bounds-checked)
     • Slicing: O(1) - just pointer arithmetic, no copying
     • ToArray(): O(n) - only when you explicitly need an array

   BEST PRACTICES
   ──────────────
   ✓ DO use Span<T> for method parameters instead of arrays
     • Handles arrays, stackalloc buffers, and slices uniformly
     • Enable callers to pass data without copying

   ✓ DO use ReadOnlySpan<T> when you don't need to modify data
     • More semantically clear
     • Works with strings via .AsSpan()

   ✓ DO use stackalloc for temporary work buffers
     • Keep sizes small (< 512 bytes typically)
     • Perfect for parsing, encoding/decoding, temporary calculations

   ✗ DON'T try to return Span from methods
     • Use Memory<T> if you need to return a reference
     • Or return the data as T[], if allocation is acceptable

   ✗ DON'T use stackalloc for large buffers
     • Stack size is limited (typically 1 MB per thread)
     • Use heap allocation (arrays) for large data

   ✗ DON'T mix Span with async code (Span can't await)
     • Use Memory<T> for async operations
     • Or convert to array before async call

   REAL-WORLD USE CASES
   ────────────────────
   • Parsing: String.Split without intermediate allocations
   • Encoding: UTF-8 encoding without temporary buffers
   • Data processing: Efficient filtering, transforming, slicing
   • Game development: Frame buffers, mesh data processing
   • Cryptography: Working with sensitive data in fixed buffers
   • Interop: Passing data to P/Invoke with minimal overhead

   ═══════════════════════════════════════════════════════════════════════════════════ */
   
using System.Diagnostics;

void Main()
{
	BasicSpanUsage().Dump("1. Basic Span Usage");
	SpanSlicing().Dump("2. Slicing with Span");
	ReadOnlySpanExample().Dump("3. ReadOnlySpan");
	StackAllocExample().Dump("4. StackAlloc");
	StringSpanOperations().Dump("5. String Operations with ReadOnlySpan<char>");
	PerformanceComparison().Dump("6. Performance: Array vs Span");
	MemoryPatternsExample().Dump("7. Safe Memory Patterns");
}

object BasicSpanUsage()
{
	var results = new List<string>();
	
	// Create a Span from an array
	int[] numbers = { 1, 2, 3, 4, 5 };
	Span<int> span = numbers;
	
	results.Add($"Original array: {string.Join(", ", numbers)}");
	
	// Modify through Span
	span[0] = 99;
	results.Add($"After span[0] = 99: {string.Join(", ", numbers)}");
	
	// Iterate through Span
	var sum = 0;
	foreach (var num in span)
		sum += num;
	results.Add($"Sum of span: {sum}");
	
	// Check length and emptiness
	results.Add($"Span length: {span.Length}");
	results.Add($"Is empty: {span.IsEmpty}");
	
	return results;
}

object SpanSlicing()
{
	var results = new List<string>();
	
	int[] data = { 10, 20, 30, 40, 50, 60, 70 };
	Span<int> original = data;
	
	results.Add($"Original: {string.Join(", ", data)}");
	
	// Slice from index 2 to end
	Span<int> slice1 = original.Slice(2);
	results.Add($"Slice from index 2: {string.Join(", ", slice1.ToArray())}");
	
	// Slice from index 1, length 3
	Span<int> slice2 = original.Slice(1, 3);
	results.Add($"Slice(1, 3): {string.Join(", ", slice2.ToArray())}");
	
	// Modify slice (affects original array)
	slice2[0] = 999;
	results.Add($"After modifying slice[0] = 999:");
	results.Add($"Original array: {string.Join(", ", data)}");
	
	return results;
}

object ReadOnlySpanExample()
{
	var results = new List<string>();
	
	// ReadOnlySpan prevents modification
	int[] numbers = { 1, 2, 3, 4, 5 };
	ReadOnlySpan<int> rospan = numbers;
	
	results.Add($"ReadOnlySpan from array: {string.Join(", ", rospan.ToArray())}");
	
	// Can read values
	results.Add($"First element: {rospan[0]}");
	results.Add($"Last element: {rospan[rospan.Length - 1]}");
	
	// Iterate
	results.Add("Doubled values: " + string.Join(", ", rospan.ToArray().Select(x => x * 2)));
	
	// ReadOnlySpan can come from string
	ReadOnlySpan<char> text = "Hello";
	results.Add($"ReadOnlySpan<char> from string: {text}");
	
	return results;
}

object StackAllocExample()
{
	var results = new List<string>();
	
	// Allocate small arrays on the stack (no heap allocation!)
	// Safe only for small, fixed sizes
	Span<int> stackData = stackalloc int[5];
	
	for (int i = 0; i < stackData.Length; i++)
		stackData[i] = i * 10;
	
	results.Add($"Stack-allocated span: {string.Join(", ", stackData.ToArray())}");
	
	// Practical example: temporary buffer for calculations
	Span<byte> buffer = stackalloc byte[256];
	
	// Use buffer for something...
	for (int i = 0; i < buffer.Length; i++)
		buffer[i] = (byte)(i % 256);
	
	results.Add($"Stack buffer size: {buffer.Length} bytes");
	results.Add($"First 10 bytes: {string.Join(", ", buffer.Slice(0, 10).ToArray())}");
	
	results.Add("✓ Stack allocation avoids heap pressure for temporary data");
	
	return results;
}

object StringSpanOperations()
{
	var results = new List<string>();
	
	string text = "Hello, World!";
	ReadOnlySpan<char> span = text;
	
	results.Add($"Original: {text}");
	results.Add($"Length: {span.Length}");
	
	// Extract substrings efficiently
	ReadOnlySpan<char> word1 = span.Slice(0, 5);
	results.Add($"First word: {word1}");
	
	ReadOnlySpan<char> word2 = span.Slice(7, 5);
	results.Add($"Second word: {word2}");
	
	// Check if span starts/ends with substring
	results.Add($"Starts with 'Hello': {span.StartsWith("Hello")}");
	results.Add($"Ends with 'World!': {span.EndsWith("World!")}");
	
	// Trim whitespace and punctuation
	string padded = "  test  ";
	ReadOnlySpan<char> trimmed = padded.AsSpan().Trim();
	results.Add($"Original (quoted): \"{padded}\"");
	results.Add($"Trimmed (quoted): \"{trimmed}\"");
	
	return results;
}

object PerformanceComparison()
{
	var results = new List<string>();
	
	const int size = 1_000_000;
	int[] data = Enumerable.Range(0, size).ToArray();
	
	// Array approach
	var sw1 = Stopwatch.StartNew();
	long sum1 = 0;
	for (int i = 0; i < data.Length; i++)
		sum1 += data[i];
	sw1.Stop();
	
	// Span approach
	var sw2 = Stopwatch.StartNew();
	long sum2 = 0;
	Span<int> span = data;
	for (int i = 0; i < span.Length; i++)
		sum2 += span[i];
	sw2.Stop();
	
	results.Add($"Array iteration: {sw1.Elapsed.TotalMilliseconds:F3}ms (sum: {sum1})");
	results.Add($"Span iteration:  {sw2.Elapsed.TotalMilliseconds:F3}ms (sum: {sum2})");
	results.Add("Note: Span can enable better JIT optimizations");
	
	return results;
}

object MemoryPatternsExample()
{
	var results = new List<string>();
	
	results.Add("Common patterns and best practices:");
	results.Add("");
	
	// Pattern 1: Accept Span as method parameter
	void ProcessData(Span<int> data)
	{
		for (int i = 0; i < data.Length; i++)
			data[i] *= 2;
	}
	
	int[] nums = { 1, 2, 3, 4, 5 };
	ProcessData(nums);
	results.Add($"After ProcessData: {string.Join(", ", nums)}");
	
	// Pattern 2: Return ReadOnlySpan for safe slices
	ReadOnlySpan<int> GetSlice(ReadOnlySpan<int> source, int start, int length)
	{
		if (start + length > source.Length)
			return ReadOnlySpan<int>.Empty;
		return source.Slice(start, length);
	}
	
	int[] arr = { 10, 20, 30, 40, 50 };
	var slice = GetSlice(arr, 1, 2);
	results.Add($"GetSlice(arr, 1, 2): {string.Join(", ", slice.ToArray())}");
	
	// Pattern 3: Stack allocation for temporary buffers
	ProcessLargeString();
	void ProcessLargeString()
	{
		Span<char> buffer = stackalloc char[1024];
		string text = "Processing text";
		text.AsSpan().CopyTo(buffer);
		results.Add($"Copied to stack buffer: {buffer.Slice(0, text.Length)}");
	}
	
	results.Add("");
	results.Add("✓ Key benefits:");
	results.Add("  - No heap allocations (with stackalloc)");
	results.Add("  - Works with arrays, strings, unmanaged memory");
	results.Add("  - Bounds checking");
	results.Add("  - JIT can optimize better than arrays");
	
	return results;
}


