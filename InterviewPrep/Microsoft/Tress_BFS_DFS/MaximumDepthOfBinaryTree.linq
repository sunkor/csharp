<Query Kind="Program" />

#load "xunit"
using Xunit;

// "Maximum Depth of Binary Tree" (LeetCode #104)
//
// Brief:
// Given the root of a binary tree, return its maximum depth.
// A binary tree's maximum depth is the number of nodes along the longest
// path from the root node down to the farthest leaf node.
//
// Example 1:
//   root = [3,9,20,null,null,15,7]
//        3
//       / \
//      9  20
//        /  \
//       15   7
//   Output: 3
//
// Example 2:
//   root = [1,null,2]
//   Output: 2
//
// Constraints:
//   - The number of nodes in the tree is in the range [0, 10^4].
//   - -100 <= Node.val <= 100

void Main()
{
	RunTests();
}

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

public class Solution
{
	public int MaxDepth(TreeNode root)
	{		
		return MaximumDepth(root);
	}
	
	public int MaximumDepth(TreeNode node)
	{
		if (node == null)
		{
			return 0;
		}

		if (node.left == null && node.right == null)
		{
			return 1;
		}
		
		int leftDepth = MaximumDepth(node.left);
		int rightDepth = MaximumDepth(node.right);
		
		return Math.Max(leftDepth, rightDepth) + 1;
	}
}

// Helper to build a tree from a level-order array (LeetCode style, with nulls for missing children)
static TreeNode BuildTree(int?[] values)
{
	if (values == null || values.Length == 0 || values[0] == null)
		return null;

	var root = new TreeNode(values[0].Value);
	var queue = new Queue<TreeNode>();
	queue.Enqueue(root);

	int i = 1;
	while (queue.Count > 0 && i < values.Length)
	{
		var current = queue.Dequeue();

		if (i < values.Length)
		{
			var leftVal = values[i++];
			if (leftVal.HasValue)
			{
				current.left = new TreeNode(leftVal.Value);
				queue.Enqueue(current.left);
			}
		}

		if (i < values.Length)
		{
			var rightVal = values[i++];
			if (rightVal.HasValue)
			{
				current.right = new TreeNode(rightVal.Value);
				queue.Enqueue(current.right);
			}
		}
	}

	return root;
}

[Fact]
public void Example1()
{
	var root = BuildTree(new int?[] { 3, 9, 20, null, null, 15, 7 });
	Assert.Equal(3, new Solution().MaxDepth(root));
}

[Fact]
public void Example2()
{
	var root = BuildTree(new int?[] { 1, null, 2 });
	Assert.Equal(2, new Solution().MaxDepth(root));
}

[Fact]
public void EmptyTree()
{
	Assert.Equal(0, new Solution().MaxDepth(null));
}

[Fact]
public void SingleNode()
{
	var root = new TreeNode(5);
	Assert.Equal(1, new Solution().MaxDepth(root));
}

[Fact]
public void LeftSkewed()
{
	// 1 -> 2 -> 3 -> 4 (all left children)
	var root = new TreeNode(1, new TreeNode(2, new TreeNode(3, new TreeNode(4))));
	Assert.Equal(4, new Solution().MaxDepth(root));
}

[Fact]
public void RightSkewed()
{
	// 1 -> 2 -> 3 (all right children)
	var root = new TreeNode(1, null, new TreeNode(2, null, new TreeNode(3)));
	Assert.Equal(3, new Solution().MaxDepth(root));
}

[Fact]
public void BalancedTreeWithMixedDepths()
{
	// Tree where one subtree is deeper than the other
	//        1
	//       / \
	//      2   3
	//     /
	//    4
	//   /
	//  5
	var root = new TreeNode(1,
		new TreeNode(2, new TreeNode(4, new TreeNode(5)), null),
		new TreeNode(3));
	Assert.Equal(4, new Solution().MaxDepth(root));
}

