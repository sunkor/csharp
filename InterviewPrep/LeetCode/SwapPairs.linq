<Query Kind="Program" />

void Main()
{
	var node = PrepListNode(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
	//var node = PrepListNode(new[] { 1, 2, 3, 4 });
	PrintNodes(node);

	var swappedPairs = SwapPairs(node);
	PrintNodes(swappedPairs);
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

//Definition for singly-linked list.
public class ListNode
{
	public int val;
	public ListNode next;
	public ListNode(int x) { val = x; }
}

public ListNode SwapPairs(ListNode head)
{
	return SwapPairs(null, head);
}

public ListNode SwapPairs(ListNode prevNode, ListNode node1)
{
	if(node1 == null) return null;
	
	var node2 = node1.next;
	if(node2 == null) return node1;
	
	//Swap nodes
	node1.next = node2.next;
	node2.next = node1;
	
	//Point previous node to node2
	if(prevNode != null) 
		prevNode.next = node2;
	else
		prevNode = node2;
	
	SwapPairs(node1, node1.next);
	
	return prevNode;
}

private ListNode PrepListNode(int[] arr)
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