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

private void RotateImage(int [,] matrix)
{
	if(matrix == null || matrix.Length == 0) return;
	var n = matrix.GetLength(0);
	for(int layer = 0; layer < n / 2; layer++)
	{
		var last = n - 1 - layer;
		for(var i = layer; i < last; i++)
		{
			var offset = i - layer;
			
			//capture top value
			var temp = matrix[layer,i];
			
			//move left to top
			matrix[layer,i] = matrix[last - offset,layer];
			
			//move bottom to left
			matrix[last - offset, layer] = matrix[last,last - offset];
			
			//move right to bottom
			matrix[last,last - offset] = matrix[i,last];
			
			//move top to right
			matrix[i,last] = temp;
									
			//var topLeft = matrix[layer,i];
			//topLeft.Dump();
			
			//var topRight = matrix[i, last];
			//topRight.Dump();
			
			//var bottomRight = matrix[last, last - i];			
			//bottomRight.Dump();
			
			//var bottomLeft = matrix[last - i, layer];
			//bottomLeft.Dump();
		}
	}
}


