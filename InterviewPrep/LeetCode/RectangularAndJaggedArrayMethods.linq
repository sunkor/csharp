<Query Kind="Program" />

void Main()
{
		var matrix = new int [,]{{1,2,3,4},
							{5,6,7,8},
							{9,10,11,12}};
		matrix.GetLength(0).Dump();
		matrix.GetLength(1).Dump();
		matrix.GetUpperBound(0).Dump();
		matrix.GetUpperBound(1).Dump();
		"".Dump();
		
		var jagged = new int [][]{new int[]{1,2,3,4},
								new int []{1,2},
								new int[]{1}};
		jagged.GetLength(0).Dump();
		jagged.GetUpperBound(0).Dump();
		jagged[1].GetUpperBound(0).Dump();
}

// Define other methods and classes here

