<Query Kind="Program" />

void Main()
{
	
}

//Definition for a binary tree node.
public class TreeNode 
{
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
 }
 
 
public IList<IList<int>> VerticalOrder(TreeNode root) 
{
	var verticalOrder = new SortedDictionary<int, IList<int>>();
	VerticalOrder(root, verticalOrder);
	
	IList<IList<int>> list = new List<IList<int>>();
	foreach(var key in verticalOrder.Keys)
	{
		list.Add(verticalOrder[key]);
	}
	return list;
}

public void VerticalOrder(TreeNode node, SortedDictionary<int, IList<int>> verticalOrder) 
{
	if(node == null) return;
	
	Dictionary<TreeNode, int> nodeCols = new Dictionary<TreeNode, int>();
	nodeCols[node] = 0;
	
	var nodes = new Queue<TreeNode>();
	nodes.Enqueue(node);
	
	IList<TreeNode> nodesToAdd = new List<TreeNode>();	
	while(nodes.Count > 0)
	{
		var childNode = nodes.Dequeue();
		
		int column;
		nodeCols.TryGetValue(childNode, out column);
		
		IList<int> list;
		if(!verticalOrder.TryGetValue(column, out list))
		{
			list = new List<int>();
			verticalOrder.Add(column, list);
		}
		list.Add(childNode.val);
			
		if(childNode.left != null)
		{
			nodeCols[childNode.left] = column - 1;
			nodesToAdd.Add(childNode.left);
		}
			
		if(childNode.right != null)
		{
			nodeCols[childNode.right] = column + 1;
			nodesToAdd.Add(childNode.right);
		}
		
		foreach(var nodeToAdd in nodesToAdd)
			nodes.Enqueue(nodeToAdd);
		
		nodesToAdd.Clear();
	}
}