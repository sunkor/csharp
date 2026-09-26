<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

// Binary Tree Level Order Traversal
//
// Given the root of a binary tree, return the level order traversal of its nodes' values
// (i.e., from left to right, level by level).
//
// Example:
//        3
//       / \
//      9  20
//         / \
//        15  7
//
// Input: root = [3,9,20,null,null,15,7]
// Output: [[3],[9,20],[15,7]]
//
// Example:
// Input: root = [1]
// Output: [[1]]
//
// Example:
// Input: root = []
// Output: []
//
// Constraints:
// - The number of nodes in the tree is in the range [0, 2000].
// - -1000 <= Node.val <= 1000

public class TreeNode
{
	public int val;
	public TreeNode left;
	public TreeNode right;
	public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
	{
		this.val = val;
		this.left = left;
		this.right = right;
	}
}

// Helper: build a tree from a level-order array representation (LeetCode style),
// where "null" indicates a missing child. Pass values as int?[] with null for gaps.
TreeNode BuildTree(int?[] values)
{
	if (values == null || values.Length == 0 || values[0] == null) return null;

	var root = new TreeNode(values[0].Value);
	var queue = new Queue<TreeNode>();
	queue.Enqueue(root);
	int i = 1;

	while (queue.Count > 0 && i < values.Length)
	{
		var node = queue.Dequeue();

		if (i < values.Length)
		{
			var leftVal = values[i++];
			if (leftVal != null)
			{
				node.left = new TreeNode(leftVal.Value);
				queue.Enqueue(node.left);
			}
		}

		if (i < values.Length)
		{
			var rightVal = values[i++];
			if (rightVal != null)
			{
				node.right = new TreeNode(rightVal.Value);
				queue.Enqueue(node.right);
			}
		}
	}

	return root;
}

// TODO: Implement this method.
// Return a list of levels, each level being a list of node values left-to-right.
List<List<int>> LevelOrder(TreeNode root)
{
	if(root == null)
	{
		return new List<List<int>>();
	}

	var list = new List<List<int>>();
	
	var listOfNums = new List<int>();
	listOfNums.Add(root.val);
	list.Add(listOfNums);
	
	LevelOrder(root, list);
	
	return list;
}

void LevelOrder(TreeNode node, List<List<int>> list)
{
	var queue = new Queue<(int, TreeNode)>();
	var dict = new Dictionary<int, List<int>>();
	
	int height = 1;
	queue.Enqueue((height, node));

	while (queue.TryDequeue(out var nodeVal))
	{
		(height, node) = nodeVal;
		
		List<int> listOfNums;
		if(!dict.TryGetValue(height, out listOfNums))
		{
			listOfNums = new List<int>();
		}
		
		if (node.left != null)
		{
			listOfNums.Add(node.left.val);
			queue.Enqueue((height + 1,nodeVal.Item2.left));
		}

		if (node.right != null)
		{
			listOfNums.Add(node.right.val);
			queue.Enqueue((height + 1,nodeVal.Item2.right));
		}

		if (listOfNums.Count > 0)
		{
			if (!dict.ContainsKey(height))
			{
				list.Add(listOfNums);
				dict.Add(height, listOfNums);
			}
		}
	}
}

void Main()
{
	RunTests();
}

void RunTests()
{
	var testCases = new (int?[] input, List<List<int>> expected, string name)[]
	{
		(
			new int?[] { 3, 9, 20, null, null, 15, 7 },
			new List<List<int>> { new() { 3 }, new() { 9, 20 }, new() { 15, 7 } },
			"Standard tree"
		),
		(
			new int?[] { 1 },
			new List<List<int>> { new() { 1 } },
			"Single node"
		),
		(
			Array.Empty<int?>(),
			new List<List<int>>(),
			"Empty tree"
		),
		(
			new int?[] { 1, 2, 3, 4, 5, 6, 7 },
			new List<List<int>> { new() { 1 }, new() { 2, 3 }, new() { 4, 5, 6, 7 } },
			"Perfect tree, 3 levels"
		),
		(
			new int?[] { 1, null, 2, null, 3, null, 4 },
			new List<List<int>> { new() { 1 }, new() { 2 }, new() { 3 }, new() { 4 } },
			"Right-skewed tree"
		),
		(
			new int?[] { -1, -2, -3 },
			new List<List<int>> { new() { -1 }, new() { -2, -3 } },
			"Negative values"
		),
	};

	var results = new List<object>();
	int passCount = 0;

	foreach (var (input, expected, name) in testCases)
	{
		List<List<int>> actual;
		bool passed;
		string error = null;

		try
		{
			var tree = BuildTree(input);
			actual = LevelOrder(tree);
			passed = LevelsEqual(actual, expected);
		}
		catch (Exception ex)
		{
			actual = null;
			passed = false;
			error = ex.Message;
		}

		if (passed) passCount++;

		results.Add(new
		{
			Test = name,
			Passed = passed,
			Expected = FormatLevels(expected),
			Actual = actual == null ? "(error)" : FormatLevels(actual),
			Error = error
		});
	}

	results.Dump($"Level Order Traversal - {passCount}/{testCases.Length} tests passed");
}

bool LevelsEqual(List<List<int>> a, List<List<int>> b)
{
	if (a == null || b == null) return a == b;
	if (a.Count != b.Count) return false;
	for (int i = 0; i < a.Count; i++)
	{
		if (a[i] == null || b[i] == null) return a[i] == b[i];
		if (!a[i].SequenceEqual(b[i])) return false;
	}
	return true;
}

string FormatLevels(List<List<int>> levels) =>
	"[" + string.Join(", ", levels.Select(level => "[" + string.Join(",", level) + "]")) + "]";

