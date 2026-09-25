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
					//key.Dump();
					numOfIslands++;					
					nodesToVisit.Enqueue(new ListNode(i,j));	
					while(nodesToVisit.Count > 0)
					{
						while(nodesToVisit.Count > 0)
						{
							var node = nodesToVisit.Dequeue();
							visited.Add(node.Key);
							
							int row = node.Row;
							int col = node.Col;
							
							//right
							if((col + 1) < cols && grid[row, col + 1] == '1')
							{
								if(!visited.Contains(GetNodeKey(row, col + 1))) 
									nodesToAdd.Add(new ListNode(row, col + 1));
							}
							
							//left
							if((col - 1) >= 0 && grid[row, col - 1] == '1')
							{
								if(!visited.Contains(GetNodeKey(row, col - 1)))
									nodesToAdd.Add(new ListNode(row, col - 1));
							}
							
							//bottom
							if((row + 1) < rows && grid[row + 1, col] == '1')
							{
								if(!visited.Contains(GetNodeKey(row + 1, col)))
									nodesToAdd.Add(new ListNode(row + 1, col));
							}
							
							//top
							if((row - 1) >= 0 && grid[row - 1, col] == '1')
							{
								if(!visited.Contains(GetNodeKey(row - 1, col)))
									nodesToAdd.Add(new ListNode(row - 1, col));
							}
						}
																		
						foreach(var node in nodesToAdd)
						{
							nodesToVisit.Enqueue(node);
						}
						
						nodesToAdd.Clear();
					}
				}
			}
		}
	}
	
	return numOfIslands;
}
