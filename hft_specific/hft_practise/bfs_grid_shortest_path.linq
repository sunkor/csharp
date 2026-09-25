<Query Kind="Program" />

class GridShortestPath
{
	public static int ShortestPath(char[,] grid)
	{
		//edge cases
		if(grid == null || grid.Length == 0)
			return -1;
				
		var queue = new Queue<(int r, int c, int dist)>();
						
		// get the number of rows (first dimension) and columns (second dimension)
		int rowLength = grid.GetLength(0);
		int colLength = grid.GetLength(1);
		
		var visited = new bool[rowLength,colLength];
		
		//find A
		for(int row = 0; row < rowLength; row++)
		{
			for(int col = 0; col < colLength; col++)
			{
				if(grid[row,col] == 'A')
				{
					queue.Enqueue((row,col,0));
				}
			}
		}

		//row,column
		int[][] directions =
		{
			new []{0,1}, //right
			new []{0,-1}, //left
			new []{1,0}, //up
			new []{-1,0} //down
		};

		while (queue.Count() > 0)
		{
			(int r, int c, int dist) = queue.Dequeue();

			if (visited[r, c])
			{
				continue;
			}

			visited[r, c] = true;

			if (grid[r, c] == 'B')
			{
				return dist;
			}
			else if (grid[r, c] == '#') //hit the wall
			{
				visited[r, c] = true;
				continue;
			}

			foreach (var direction in directions)
			{
				var nr = r + direction[0];
				var nc = c + direction[1];

				if (nr < 0 || nc < 0 || nr >= rowLength || nc >= colLength)
				{
					continue;
				}

				queue.Enqueue((nr, nc, dist + 1));
			}
		}

		while (queue.Count() > 0)
		{
			(int row, int col, int dist) = queue.Dequeue();
		}
		
		return -1;
	}
}

void Main()
{
	//example grid
	char[,] grid =
	{
		{ 'A', '.', '.', '#' },
		{ '#', '#', '.', '#' },
		{ '.', '.', '.', 'B' }
	};

	Console.WriteLine(GridShortestPath.ShortestPath(grid)); // 5
}