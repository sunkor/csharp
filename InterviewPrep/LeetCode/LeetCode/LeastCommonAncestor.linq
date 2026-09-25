<Query Kind="Program" />

void Main()
{
	//[3,5,1,6,2,0,8,null,null,7,4]
	var root = new TreeNode(3);
	root.left = new TreeNode(5);
	root.right = new TreeNode(1);
//	root.left.left = new TreeNode(6);
//	root.left.right = new TreeNode(2);
//	root.right.left = new TreeNode(0);
//	root.right.right = new TreeNode(8);
//	root.left.right.left = new TreeNode(7);
//	root.left.right.right = new TreeNode(4);
	
	LowestCommonAncestor(root, new TreeNode(5), new TreeNode(1)).Dump();
	//LowestCommonAncestor(root, new TreeNode(3), new TreeNode(6)).Dump();
}

public class TreeNode
{
	public int val;
	public TreeNode left;
	public TreeNode right;
	public TreeNode(int x) { val = x; }
}

TreeNode ans;

public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
{
	// Traverse the tree
	this.RecurseTree(root, p, q);
	return this.ans;
}

private bool RecurseTree(TreeNode currentNode, TreeNode p, TreeNode q)
{
	// If reached the end of a branch, return false.
	if (currentNode == null)
	{
		return false;
	}

	// Left Recursion. If left recursion returns true, set left = 1 else 0
	int left = this.RecurseTree(currentNode.left, p, q) ? 1 : 0;

	// Right Recursion
	int right = this.RecurseTree(currentNode.right, p, q) ? 1 : 0;

	// If the current node is one of p or q
	int mid = (currentNode.val == p.val || currentNode.val == q.val) ? 1 : 0;


	// If any two of the flags left, right or mid become True
	if (mid + left + right >= 2)
	{
		this.ans = currentNode;
	}

	// Return true if any one of the three bool values is True.
	return (mid + left + right > 0);
}

public TreeNode LowestCommonAncestor2(TreeNode root, TreeNode p, TreeNode q)
{
	if (root == null || p == null || q == null) return null;

	if (p.val == q.val) return p;

	if (root.val == p.val || root.val == q.val) return root;

	//Record the path
	var stack1 = new Stack<TreeNode>();
	stack1.Push(null);
	RecordNodeDFS(stack1, p, root);

	//Record the path
	var stack2 = new Stack<TreeNode>();
	stack2.Push(null);
	RecordNodeDFS(stack2, q, root);

	var dict = new Dictionary<int, TreeNode>();
	while (true)
	{
		var node = stack1.Pop();
		if (node == null) break;
		dict.Add(node.val, node);
	}

	while (true)
	{
		var node = stack2.Pop();
		if (node == null)
		{
			return null;
		}

		if (dict.TryGetValue(node.val, out var treeNode))
		{
			return treeNode;
		}
	}
}

private bool RecordNodeDFS(Stack<TreeNode> stack, TreeNode nodeToFind, TreeNode node)
{
	if (node == null) return false;

	stack.Push(node);

	if (node.val == nodeToFind.val || RecordNodeDFS(stack, nodeToFind, node.left) || RecordNodeDFS(stack, nodeToFind, node.right))
	{
		return true;
	}

	stack.Pop();
	
	return false;
}