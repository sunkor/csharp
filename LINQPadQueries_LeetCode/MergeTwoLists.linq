<Query Kind="Program" />

void Main()
{
	var node1 = PrepListNode(new[] { 5, 3, 2, 1, 9, 8 });
	PrintNodes(node1);
	
	var node2 = PrepListNode(new[] { 2, 5, 8, 7, 3, 0, 11});
	PrintNodes(node2);
	
	var resultNode = MergeTwoLists(node1, node2);
	PrintNodes(resultNode);
}

private void PrintNodes(ListNode node)
{
	while (node != null)
	{
		Console.Write(node.val + " ");
		node = node.next;
	}
	Console.WriteLine();
}

private ListNode PrepListNode(int [] arr)
{
	ListNode rootNode = null, currentNode = null;
	Array.Sort(arr);
	for (int i = 0; i < arr.Length; i++)
	{
		var val = arr[i];
		if (rootNode == null)
		{
			currentNode = new ListNode(val);
			rootNode = currentNode;
		}
		else
		{
			currentNode.next = new ListNode(val);
			currentNode = currentNode.next;
		}	
	}
	return rootNode;
}

  //Definition for singly-linked list.
  public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }

public ListNode MergeTwoLists(ListNode l1, ListNode l2)
{
	if (l1 == null) return l2;
	if (l2 == null) return l1;

	ListNode rootNode = null, currentNode = null;

	while (l1 != null && l2 != null)
	{
		ListNode nodeToConsider;
		if (l1.val < l2.val)
		{
			nodeToConsider = l1;
			l1 = l1.next;
		}
		else
		{
			nodeToConsider = l2;
			l2 = l2.next;
		}
		if (rootNode == null)
		{
			currentNode = new ListNode(nodeToConsider.val);
			rootNode = currentNode;
		}
		else
		{
			currentNode.next = new ListNode(nodeToConsider.val);
			currentNode = currentNode.next;
		}
	}

	var nodeToPush = l1 == null ? l2 : l1;
	while (nodeToPush != null)
	{
		currentNode.next = new ListNode(nodeToPush.val);
		currentNode = currentNode.next;
		nodeToPush = nodeToPush.next;
	}

	return rootNode;
}
