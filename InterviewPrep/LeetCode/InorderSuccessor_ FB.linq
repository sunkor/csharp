<Query Kind="Program" />

void Main()
{
	
}

//  Definition for a binary tree node.
  public class TreeNode {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
  }
 
public class Solution {
    public TreeNode InorderSuccessor(TreeNode root, TreeNode p) {
			TreeNode successor = null;
			while(root != null)
			{
				if(root.val > p.val)
				{
					successor = root;
					root = root.left;
				}
				else
					root = root.right;
			}
			return successor;
    }
}
