<Query Kind="Program" />

void Main()
{
	var matrix = new int [,]{{1,2,3,4},
							{5,6,7,8},
							{9,10,11,12},
							{13,14,15,16}};
							
	matrix.Dump();
							
	RotateImage(matrix);
	matrix.Dump();
}

public void RotateImage(int [,] matrix)
{
	if(matrix == null || matrix.Length == 0) return;
	
	var length = matrix.GetLength(0);
	for(var layer = 1; layer <= length / 2; layer++)
	{
		var lengthOfLayer = layer * 2;
		
		//Iterate the top side cells, and rotate corresponding.
		int startRow, startCol;
		startRow = startCol = (length - lengthOfLayer) / 2;
		
		int endRow, endCol;
		endRow = endCol = startRow + lengthOfLayer - 1;
		for(var offset = 0; offset < lengthOfLayer - 1; offset++)
		{
			//top side cell value
			var temp = matrix[startRow, startCol + offset];
			
			//left to top
			matrix[startRow, startCol + offset] = matrix[endRow - offset, startCol];
			
			//bottom to left
			matrix[endRow - offset, startCol] = matrix[endRow, endCol - offset];
			
			//right to bottom
			matrix[endRow, endCol - offset] = matrix[startRow + offset, endCol];
			
			//top value to right
			matrix[startRow + offset, endCol] = temp;
		}
	}
}

/*
	Approach by layering
	# of layers - image length / 2
	Each layer rows / cols size = layer * 2 (ex: layer 1 - 2 rows & cols, 2 - 4, 3 - 9, 4 - 16
	
	For each layer - iterate each of the cell in a top row
		For each each, derive the corresponding cell off set in right, bottom and left sides
		Shift values to the right.
*/