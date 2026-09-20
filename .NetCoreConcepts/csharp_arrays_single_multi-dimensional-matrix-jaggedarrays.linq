<Query Kind="Statements" />

// ==========================================================
// Array Type Examples in C#
// Demonstrates: single-dimensional, multi-dimensional (matrix),
// jagged arrays, and array of objects/records
// ==========================================================

// ---------- 1. Single-dimensional array ----------
int[] numbers = { 10, 20, 30, 40, 50 };
numbers.Dump("1. Single-dimensional array");

// ---------- 2. Multi-dimensional array (rectangular / matrix) ----------
// A true matrix: fixed rows x columns, all rows same length
int[,] matrix = new int[3, 4];
for (int r = 0; r < matrix.GetLength(0); r++)
	for (int c = 0; c < matrix.GetLength(1); c++)
		matrix[r, c] = r * 10 + c;

matrix.Dump("2. Multi-dimensional array (3x4 matrix)");

// LINQPad can even swap rows/columns for display:
Util.Pivot(matrix).Dump("2b. Pivoted matrix");

// ---------- 3. Jagged array (array of arrays, rows can differ in length) ----------
int[][] jagged = new int[4][];
jagged[0] = new[] { 1 };
jagged[1] = new[] { 1, 2 };
jagged[2] = new[] { 1, 2, 3 };
jagged[3] = new[] { 1, 2, 3, 4 };

jagged.Dump("3. Jagged array (Pascal-triangle-ish)");

// Jagged arrays can also be initialized inline:
string[][] jaggedStrings =
{
	new[] { "a", "b" },
	new[] { "c" },
	new[] { "d", "e", "f" }
};
jaggedStrings.Dump("3b. Jagged string array");

// ---------- 4. Three-dimensional array ----------
int[,,] cube = new int[2, 2, 2];
int val = 0;
for (int x = 0; x < 2; x++)
	for (int y = 0; y < 2; y++)
		for (int z = 0; z < 2; z++)
			cube[x, y, z] = val++;

cube.Dump("4. 3D array (2x2x2 cube)");

// ---------- 5. Jagged array of jagged arrays (nested jagged) ----------
int[][][] jaggedOfJagged = new int[2][][];
jaggedOfJagged[0] = new int[][] { new[] { 1, 2 }, new[] { 3 } };
jaggedOfJagged[1] = new int[][] { new[] { 4, 5, 6 } };

jaggedOfJagged.Dump("5. Jagged array of jagged arrays");

// ---------- 6. Array of a custom record type ----------
Point[] points =
{
	new Point(0, 0),
	new Point(1, 2),
	new Point(3, 4)
};
points.Dump("6. Array of records");

// ---------- 7. Jagged array of records (variable-length rows of Points) ----------
Point[][] jaggedPoints =
{
	new[] { new Point(0,0), new Point(1,1) },
	new[] { new Point(2,2) },
	new[] { new Point(3,3), new Point(4,4), new Point(5,5) }
};
jaggedPoints.Dump("7. Jagged array of records");

// ---------- 8. Array covariance / object array ----------
object[] mixed = { 1, "hello", 3.14, true, new Point(9,9) };
mixed.Dump("8. Array of object (mixed types)");

// ---------- 9. Matrix operations example: transpose using LINQ ----------
int[,] Transpose(int[,] m)
{
	int rows = m.GetLength(0), cols = m.GetLength(1);
	var result = new int[cols, rows];
	for (int r = 0; r < rows; r++)
		for (int c = 0; c < cols; c++)
			result[c, r] = m[r, c];
	return result;
}

Transpose(matrix).Dump("9. Manually transposed matrix (compare to 2b)");

// ---------- 10. Converting jagged <-> rectangular ----------
int[,] JaggedToRectangular(int[][] jaggedArr)
{
	int rows = jaggedArr.Length;
	int cols = jaggedArr.Max(row => row.Length);
	var rect = new int[rows, cols]; // pads with default(int) = 0
	for (int r = 0; r < rows; r++)
		for (int c = 0; c < jaggedArr[r].Length; c++)
			rect[r, c] = jaggedArr[r][c];
	return rect;
}

JaggedToRectangular(jagged).Dump("10. Jagged array converted to padded rectangular array");

// ---------- Type declarations (must come after top-level statements) ----------
record Point(int X, int Y);

