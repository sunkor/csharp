<Query Kind="Program" />

void Main()
{
	var a = new [,] {{1,0,0},
					 {-1,0,3}};
	//var a = new [,] {{1,0,},
	//				 {-1,3}};
	a.Dump();

	var b = new [,] {{7,0,0,0},
					 {0,0,0,0},
					 {0,0,0,1}};
	//var b = new [,] {{7,0,},
	//				 {0,0,}};
	b.Dump();
	
	Multiply(a,b).Dump();
}

 public int[,] Multiply(int[,] A, int[,] B) 
 {
    var rows = A.GetUpperBound(0) + 1;
	var cols = B.GetUpperBound(1) + 1;
	var common = A.GetUpperBound(1);

	var rowsCompressorForA = new HashSet<int>();
	for(int i = 0; i < rows; i++)
	{
		var k = A.GetUpperBound(1);
		var isZero = true;
		while(k >= 0 && isZero)
		{
			if(A[i,k--] != 0)
			{
				isZero = false;
				break;
			}
		}
		if(isZero) rowsCompressorForA.Add(i);
	}
	
	var colsCompressorForB = new HashSet<int>();
	for(int i = 0; i < cols; i++)
	{
		var k = B.GetUpperBound(0);
		var isZero = true;
		while(k >= 0 && isZero)
		{
			if(B[k--,i] != 0)
			{
				isZero = false;
				break;
			}
		}
		if(isZero) colsCompressorForB.Add(i);
	}
	
	var result = new int [rows,cols];
	
	for(int i = 0; i < rows; i++)
	{
		if(!rowsCompressorForA.Contains(i))
		{
			for(int j = 0; j < cols; j++)
			{				
				if(!colsCompressorForB.Contains(j))
				{
					var sum = 0;
					for(int k = 0; k <= common; k++)
					{
						if(A[i,k] == 0 || B[k,j] == 0) continue;
						sum += A[i,k] * B[k,j];
					}
					result[i,j] = sum;
				}
			}
		}
	}
	
	return result;
}
