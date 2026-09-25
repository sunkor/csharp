<Query Kind="Program" />

void Main()
{
	var sw = new Stopwatch();
	sw.Start();
	
	var grid1 = new [,] {{'1','1','1','1','0'},
						{'1','1','0','1','0'},
						{'1','1','0','0','0'},
						{'0','0','0','0','0'}};
	NumIslands(grid1).Dump();

	var grid2 = new [,] {{'1','1','0','0','0'},
						{'1','1','0','0','0'},
						{'0','0','1','0','0'},
						{'0','0','0','1','1'}};
	NumIslands(grid2).Dump();

	var grid3 = new [,] {{'1','1','1'},
						{'0','1','0'},
						{'1','1','1'}};
	NumIslands(grid3).Dump();
	
	sw.Stop();
	sw.ElapsedTicks.Dump();
}

public int NumIslands(char[,] grid) 
{
	if(grid == null || (grid.GetLength(0) == 0 && grid.GetLength(1) == 0)) return 0;
	
	var numOfIslands = 0;
	
	var rows = grid.GetLength(0);
	var cols = grid.GetLength(1);
	
	var nodesToVisit = new Queue<ListNode>();
	var nodesToAdd = new List<ListNode>();
	var visited = new HashSet<string>();
			
	for(var i = 0; i < rows; i++)
	{
		for(var j = 0; j < cols; j++)
		{
			if(grid[i,j] == '1')
			{
				var key = $"{i},{j}";
				if(!visited.Contains(key))
				{
					numOfIslands++;					
					VisitIsland(grid, rows, cols, new ListNode(i,j), visited);
				}
			}
		}
	}
	
	return numOfIslands;
}

public class ListNode
{
	public int Row;
	public int Col;
	public List<ListNode> Edges;
	
	public string Key;
	
	public ListNode(int row, int col)
	{
		this.Row = row;
		this.Col = col;
		this.Key = $"{row},{col}";
		this.Edges = new List<ListNode>();
	}
}

private string GetNodeKey(int row, int col)
{
	return  $"{row},{col}";
}

public void VisitIsland(char[,] grid, int rows, int cols, ListNode node, HashSet<string> visited)
{
	if(node == null) return;
	
	visited.Add(node.Key);
	
	int row = node.Row;
	int col = node.Col;
	
	//right
	if((col + 1) < cols && grid[row, col + 1] == '1')
	{
		if(!visited.Contains(GetNodeKey(row, col + 1))) 
			VisitIsland(grid, rows, cols, new ListNode(row, col + 1), visited);
	}
	
	//left
	if((col - 1) >= 0 && grid[row, col - 1] == '1')
	{
		if(!visited.Contains(GetNodeKey(row, col - 1)))
			VisitIsland(grid, rows, cols, new ListNode(row, col - 1), visited);
	}
							
	//bottom
	if((row + 1) < rows && grid[row + 1, col] == '1')
	{
		if(!visited.Contains(GetNodeKey(row + 1, col)))
			VisitIsland(grid, rows, cols, new ListNode(row + 1, col), visited);
	}
							
	//top
	if((row - 1) >= 0 && grid[row - 1, col] == '1')
	{
		if(!visited.Contains(GetNodeKey(row - 1, col)))
			VisitIsland(grid, rows, cols, new ListNode(row - 1, col), visited);
	}
}