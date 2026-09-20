<Query Kind="Program" />

using System;
using System.Collections.Generic;

/// <summary>
/// ⚡ One-liner intuition
/// BFS = “closest first”
/// DFS = “go deep and explore everything”
/// 
/// BFS:
/// Uses a Queue
/// Visits nodes level by level
/// 
/// 1. Shortest path (unweighted graphs)
/// 
///BFS guarantees the minimum number of edges
///Classic: shortest path in a grid, word ladder, social network hops
/// 
///👉 If the question says:
/// 
///“minimum steps”
///“shortest path”
///“fewest moves”
/// 
///➡️ Think BFS immediately
/// 
///2.Level - by - level traversal
/// 
///Binary tree level order traversal
///Anything involving “distance from source”
/// 
///3.Multi - source expansion
/// 
///Fire spread, infection problems
///Start from multiple points simultaneously
/// 
///4.When the solution is closest to the start
/// 
///BFS finds the shallowest solution first
/// 
/// DFS:
/// Uses recursion(stack)
/// Goes deep first, then backtracks
/// 
/// When to use DFS
///
///1.Explore all possibilities / backtracking
///
///Permutations, combinations
///Sudoku, N - Queens
///Path enumeration
///
///2.Structure - related problems
///
///Detect cycles
///Topological sort
///Connected components
///
///3.When you need full traversal
///Counting islands
///Flood fill
/// 
/// 🧠 Interview Heuristics (what to do fast)
/// Grid + shortest path ? → BFS
/// All combinations / paths ? → DFS
/// Tree level order? → BFS
/// Cycle detection / topo sort ? → DFS
/// Huge branching(wide graph) ? → DFS(BFS may blow memory)
/// </summary>
class BFS_DFS_Example
{
	static HashSet<int> DFS_Iterative(Dictionary<int, List<int>> graph, int start)
	{
		var visited = new HashSet<int>();
		var stack = new Stack<int>();

		stack.Push(start);

		while (stack.Count > 0)
		{
			int node = stack.Pop();

			if (visited.Contains(node))
				continue;

			visited.Add(node);
			Console.Write(node + " ");

			foreach (var neighbor in graph[node])
			{
				stack.Push(neighbor);
			}
		}

		return visited;
	}

	static void DFS_Recursive(Dictionary<int, List<int>> graph, int node, HashSet<int> visited)
	{
		if (visited.Contains(node))
			return;

		visited.Add(node);
		Console.Write(node + " ");

		foreach (var neighbor in graph[node])
		{
			DFS_Recursive(graph, neighbor, visited);
		}
	}
	
	static HashSet<int> BFS(Dictionary<int, List<int>> graph, int start)
	{
		var visited = new HashSet<int>();
		var queue = new Queue<int>();

		queue.Enqueue(start);
		visited.Add(start);

		while (queue.Count > 0)
		{
			int node = queue.Dequeue();
			Console.Write(node + " ");

			foreach (var neighbor in graph[node])
			{
				if (!visited.Contains(neighbor))
				{
					visited.Add(neighbor);
					queue.Enqueue(neighbor);
				}
			}
		}
		
		return visited;
	}

	static void Main()
	{
		var graph = new Dictionary<int, List<int>>
		{
			{1, new List<int> {2, 3}},
			{2, new List<int> {4}},
			{3, new List<int> {4}},
			{4, new List<int>()}
		};

		Console.Write("BFS: ");
		BFS(graph, 1); // Output: 1 2 3 4
		
		Console.WriteLine();
		
		Console.Write("DFS recursive: ");
		var dfs_recursive_nodes = new HashSet<int>();
		DFS_Recursive(graph, 1, new HashSet<int>()); // Output: 1 2 4 3
				
		Console.WriteLine();

		Console.Write("DFS iterative: ");
		DFS_Iterative(graph, 1); // Output: 1 2 3 4
	}
}