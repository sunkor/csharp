<Query Kind="Program" />

void Main()
{
	
}

/**
 * Your BSTIterator will be called like this:
 * BSTIterator i = new BSTIterator(root);
 * while (i.HasNext()) v[f()] = i.Next();
 */

//  Definition for a binary tree node.
  public class TreeNode {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
  }
  
public class BSTIterator {

	private Stack<TreeNode> p;
	
    public BSTIterator(TreeNode root) {
		p = new Stack<TreeNode>();
		PushLeftNodes(root);
    }
	
    /** @return whether we have a next smallest number */
    public bool HasNext() {
        return p.Count > 0;
    }

    /** @return the next smallest number */
    public int Next() {
		var node = p.Pop();
		PushLeftNodes(node.right);
		return node.val;
    }
	
	private void PushLeftNodes(TreeNode node)
	{
		for(;node != null; p.Push(node), node = node.left);
	}
}

/*
The ideal solution will maintain a stack. 
Add to stack while next left node is not null.
if left node is null, pop out of stack, go to right and add to stack.
*/