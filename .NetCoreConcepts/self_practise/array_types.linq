<Query Kind="Program" />

// Difference:
// - int[,] is a true multi-dimensional (rectangular) array: a single block of memory
//   with fixed dimensions (here 3 rows x 2 columns). All rows must have the same length.
// - int[][] is a jagged array: an array of arrays, where each inner array is a separate
//   object and can have a different length. It's really an array of *references*.
//
// Why use one over the other:
// - Use int[,] when you have genuinely rectangular/grid-like data and want simpler,
//   more compact memory layout and slightly better cache locality/performance for
//   uniform access patterns.
// - Use int[][] when row lengths can vary, when you need to pass/return individual
//   rows as arrays, or when interop with LINQ/other APIs (which mostly work with
//   arrays of arrays, not multi-dim arrays) is important. Jagged arrays also allow
//   lazy/independent allocation of rows and are generally faster for row-based access
//   because indexing avoids the multiplication needed for rectangular array access.

void Main()
{
	int[,] twoDimensionalArray = new int[,] {
		{1,2},
		{3,4},
		{5,6}
	};

	twoDimensionalArray.Dump();

	var array = new int[][] {
		new int[] {1,2},
		new int[] {3,4},
		new int[] {5,6}
	};

	array.Dump();
}