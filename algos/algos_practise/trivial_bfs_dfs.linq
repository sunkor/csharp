<Query Kind="Program" />

void Main()
{
	var root = new Node
	{
		Value = 1,
		ChildNodes = new List<Node>
		{
			new Node
			{
				Value = 2,
				ChildNodes = new List<Node>
				{
					new Node { Value = 4, ChildNodes = new List<Node>() },
					new Node { Value = 5, ChildNodes = new List<Node>
					{
						new Node { Value = 6, ChildNodes = new List<Node>() },
					} },
				}
			},
			new Node { Value = 3, ChildNodes = new List<Node>
			{
				new Node { Value = 7, ChildNodes = new List<Node>() },
			} },
		}
	};

	$"DFS_Iterative: ".Dump();
	DFS_Iterative(root);
	
	"".Dump();
	
	$"DFS_Recursive: ".Dump();
	DFS_Recursive(root, null);
	
	"".Dump();

	$"BFS_Iterative: ".Dump();
	BFS_Iterative(root);
}

class Node
{
	public int Value;
	public List<Node> ChildNodes;
}

void DFS_Iterative(Node node)
{
	if (node == null)
		return;

	var stack = new Stack<Node>();
	stack.Push(node);

	while (stack.Count > 0)
	{
		node = stack.Pop();
		
		Console.WriteLine($"val: {node.Value}");

		foreach (var childNode in node.ChildNodes)
		{
			stack.Push(childNode);
		}
	}
}

void DFS_Recursive(Node node, HashSet<Node> visited)
{
	if (node == null)
		return;
		
	if(visited == null)
		visited = new HashSet<Node>();
		
	if(visited.Contains(node))
		return;
		
	Console.WriteLine($"val: {node.Value}");
	
	visited.Add(node);

	foreach (var childNode in node.ChildNodes)
	{
		DFS_Recursive(childNode, visited);
	}
}

void BFS_Iterative(Node node)
{
	if (node == null)
		return;

	var queue = new Queue<Node>();
	queue.Enqueue(node);

	while (queue.Count > 0)
	{
		node = queue.Dequeue();

		Console.WriteLine($"val: {node.Value}");

		foreach (var childNode in node.ChildNodes)
		{
			queue.Enqueue(childNode);
		}
	}
}
