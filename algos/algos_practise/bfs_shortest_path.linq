<Query Kind="Program" />

//directions - 2-dimension rectangular array.
static readonly int [,] dirs = new[,] {
		{-1,0}, //top
		{1,0}, //down
		{0,1}, //right
		{0,-1}, //left
	};
	
void Main()
{
	dirs.Dump();

	var grid = new[,] {
		{'B', '.', '#', '.', '.'},
		{'.', '#', '.', '.', '.'},
		{'A', '.', '.', '#', '.'},
	};

	ShortestPath(grid).Dump();
}

static int ShortestPath(char [,] grid)
{
	//Total rows
	var rows = grid.GetLength(0);
	
	//Total columns
	var cols = grid.GetLength(1);
	
	$"Rows: {rows}, Columns: {cols}".Dump();
	
	var queue = new Queue<(int,int,int)>();
	
	var visitedNodes = new bool[rows,cols];
	
	for(int i = 0; i < rows; i++)
	{
		for(int j = 0; j < cols; j++)
		{
			if(grid[i,j] == 'A')
			{
				queue.Enqueue((i,j,0));
				break;
			}
		}
	}
	
	while(queue.TryDequeue(out var gridPos))
	{
		(int row, int col, int pathLength) = gridPos;
		
		visitedNodes[row,col] = true;

		for (int i = 0; i < dirs.GetLength(0); i++)
		{				
			var dirRow = row + dirs[i, 0];
			var dirCol = col + dirs[i, 1];

			if (dirRow < 0 || dirRow >= rows || dirCol < 0 || dirCol >= cols)
				continue;
				
			var key = $"{dirRow}_{dirCol}";
			
			if(visitedNodes[dirRow,dirCol])
				continue;

			if (grid[dirRow, dirCol] == 'B')
			{
				return pathLength + 1;
			}
			else if (grid[dirRow, dirCol] == '.')
			{
				queue.Enqueue((dirRow, dirCol, pathLength + 1));
				continue;
			}
			else if (grid[dirRow, dirCol] == '#')
			{
				//Do not enqueue since there is not path forward.
				continue;
			}
		}
	}
	
	return -1;
}
