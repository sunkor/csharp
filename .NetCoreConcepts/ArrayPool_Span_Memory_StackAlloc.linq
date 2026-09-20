<Query Kind="Statements" />

// Tutorial: ArrayPool<T>, stackalloc, Span<T> and Memory<T>
// Demonstrates:
//  - ArrayPool<T>.Shared: renting/returning arrays to reduce GC pressure
//  - stackalloc: allocating small buffers on the stack via Span<T>
//  - Span<T>: a stack-only, ref-struct view over contiguous memory (arrays, stackalloc, strings)
//  - Memory<T>: a heap-allocatable counterpart to Span<T>, usable in async methods / fields / lambdas

using System.Buffers;
using System.Threading.Tasks;

"=== 1. Span<T> basics ===".Dump();

// Span<T> over an array - no copy, just a view
int[] source = { 1, 2, 3, 4, 5, 6, 7, 8 };
Span<int> span = source;
Span<int> slice = span.Slice(2, 3); // {3,4,5}, no allocation
slice[0] = 99; // mutates the underlying array!
source.Dump("source after mutating slice");

// Span<T> over a string (as ReadOnlySpan<char>)
ReadOnlySpan<char> charSpan = "Hello, LINQPad!".AsSpan();
charSpan.Slice(7, 8).ToString().Dump("Substring via Span (no allocation until ToString)");

"=== 2. stackalloc ===".Dump();

// stackalloc allocates memory on the stack - very fast, but must be small
// and CANNOT escape the method (Span<T> is a ref struct, enforced by compiler)
Span<int> StackAllocDemo()
{
	Span<int> buffer = stackalloc int[5];
	for (int i = 0; i < buffer.Length; i++)
		buffer[i] = i * i;

	// Copy result to a normal array to return safely (stackalloc memory dies with the frame)
	return buffer.ToArray();
}

StackAllocDemo().Dump("Squares computed via stackalloc buffer");

// A more realistic use-case: parsing without heap allocation
static int SumDigits(ReadOnlySpan<char> text)
{
	Span<int> digitBuffer = stackalloc int[16];
	int count = 0;
	foreach (char c in text)
	{
		if (char.IsDigit(c) && count < digitBuffer.Length)
			digitBuffer[count++] = c - '0';
	}

	int sum = 0;
	foreach (var d in digitBuffer.Slice(0, count))
		sum += d;
	return sum;
}

SumDigits("a1b2c3d4").Dump("Sum of digits (parsed via stackalloc, zero heap allocation)");

"=== 3. ArrayPool<T> ===".Dump();

// ArrayPool<T>.Shared lets you rent/return arrays instead of allocating new ones each time.
// Useful in hot paths (loops, servers) to reduce GC pressure.
// IMPORTANT: rented arrays may be LARGER than requested, and may contain old data - always track your own length.

void ArrayPoolDemo()
{
	var pool = ArrayPool<byte>.Shared;

	byte[] buffer = pool.Rent(minimumLength: 256);
	try
	{
		$"Rented array length: {buffer.Length} (>= 256, pool rounds up to power-of-2 buckets)".Dump();

		// Simulate filling buffer with data (e.g. from a stream read)
		for (int i = 0; i < 100; i++)
			buffer[i] = (byte)(i % 256);

		// Use Span to work with just the "valid" portion
		Span<byte> validData = buffer.AsSpan(0, 100);
		validData.Length.Dump("Length of valid data slice");
	}
	finally
	{
		// Always return the array, ideally clearing it if it held sensitive data
		pool.Return(buffer, clearArray: false);
	}
}

ArrayPoolDemo();

"=== 4. Memory<T> ===".Dump();

// Memory<T> is like Span<T> but is NOT a ref struct - it can be:
//  - stored in fields
//  - captured by lambdas / used across await boundaries
//  - passed to async methods
// You "unwrap" it to a Span via .Span when you need to actually manipulate the data synchronously.

async Task<int> SumEvenAsync(Memory<int> memory)
{
	// Simulate an async boundary (Span<T> could NOT be used here directly)
	await Task.Delay(10);

	Span<int> span = memory.Span; // safe to get Span after the await, since we're back on a thread
	int sum = 0;
	foreach (var v in span)
		if (v % 2 == 0) sum += v;
	return sum;
}

int[] numbers = Enumerable.Range(1, 10).ToArray();
Memory<int> memory = numbers;
(await SumEvenAsync(memory)).Dump("Sum of even numbers 1..10, computed via Memory<T> across an async boundary");

"=== 5. Combining ArrayPool + Memory for async I/O-like workloads ===".Dump();

async Task<string> ReadPooledAsync(int size)
{
	var pool = ArrayPool<char>.Shared;
	char[] buffer = pool.Rent(size);
	try
	{
		// Simulate async "fill" operation
		await Task.Delay(5);
		"LINQPad rocks!".AsSpan().CopyTo(buffer);

		Memory<char> mem = buffer.AsMemory(0, "LINQPad rocks!".Length);
		return mem.Span.ToString();
	}
	finally
	{
		pool.Return(buffer);
	}
}

(await ReadPooledAsync(64)).Dump("Result read into pooled buffer, exposed via Memory<char>");

"""
=== Summary ===
- Span<T>/ReadOnlySpan<T>: ref structs, stack-only, zero-allocation views over arrays/strings/stackalloc memory. Cannot be used in async methods, iterators, or stored in fields/classes.
- stackalloc: allocates a small buffer on the stack; wrap it in a Span<T> to use it safely (bounds-checked). Great for small, short-lived buffers (parsing, hashing, etc.) - avoid large sizes (risk of stack overflow).
- Memory<T>/ReadOnlyMemory<T>: heap-friendly, can cross async boundaries, be stored in fields, or passed to lambdas/closures; call .Span to get a Span<T> for actual work.
- ArrayPool<T>.Shared: rent/return arrays to avoid repeated allocations in hot paths; combine with Span<T>/Memory<T> to safely constrain the "used" portion of the (possibly oversized) rented array. Always return arrays in a finally block.
""".Dump();

