<Query Kind="Program" />

using System;
using System.Collections.Generic;

/// <summary>
/// Use BFS for shortest path in an unweighted grid.
/// Because each move costs the same, BFS explores distance 1, then 2, then 3, etc. The first time it reaches B, that is the shortest path.
/// </summary>
public class GridShortestPath
{
	public static int ShortestPath(char[,] grid)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);

		var queue = new Queue<(int r, int c, int dist)>();
		var visited = new bool[rows, cols];

		// Find A
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (grid[r, c] == 'A')
				{
					queue.Enqueue((r, c, 0));
					visited[r, c] = true;
				}
			}
		}

		int[][] dirs =
		{
			new[] { 1, 0 },   // down
            new[] { -1, 0 },  // up
            new[] { 0, 1 },   // right
            new[] { 0, -1 }   // left
        };

		while (queue.Count > 0)
		{
			var (r, c, dist) = queue.Dequeue();

			if (grid[r, c] == 'B')
				return dist;

			foreach (var d in dirs)
			{
				int nr = r + d[0];
				int nc = c + d[1];

				if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
					continue;

				if (visited[nr, nc])
					continue;

				if (grid[nr, nc] == '#') // wall
					continue;

				visited[nr, nc] = true;
				queue.Enqueue((nr, nc, dist + 1));
			}
		}

		return -1; // no path
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