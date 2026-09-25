<Query Kind="Program" />

void Main()
{
	var node = PrepListNode(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
	PrintNodes(node);
	
	var refreshedNode = RemoveNthFromEnd(node, 1);
	PrintNodes(refreshedNode);
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

public ListNode RemoveNthFromEnd(ListNode head, int n)
{
	ListNode start = new ListNode(0);
	ListNode slow = start, fast = start;
	slow.next = head;

	//Move fast in front so that the gap between slow and fast becomes n
	for (int i = 1; i <= n + 1; i++)
	{
		fast = fast.next;
	}
	//Move fast to the end, maintaining the gap
	while (fast != null)
	{
		slow = slow.next;
		fast = fast.next;
	}
	//Skip the desired node
	slow.next = slow.next.next;
	return start.next;
}

public ListNode RemoveNthFromEndEx(ListNode head, int n)
{
	if (head == null) return null;
	if (n < 1) return null;
	if (n == 1 && head.next == null) return null;

	var list = new List<ListNode>();
	while (head != null)
	{
		list.Add(head);
		head = head.next;
	}

	var count = list.Count;

	if (n > count) return null;
	if (count == n) return list[1];

	var leftNodeIndex = count - n - 1;
	var leftNode = list[leftNodeIndex];
	var rightNodeIndex = count - n + 1;
	if (rightNodeIndex < count) 
		leftNode.next = list[rightNodeIndex];
	else
		leftNode.next = null;
		
	return list[0];
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
