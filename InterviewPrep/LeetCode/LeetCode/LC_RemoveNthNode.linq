<Query Kind="Program" />

void Main()
{
	
}

//1->2->3->4->5, n = 2
public ListNode RemoveNthFromEnd(ListNode head, int n)
{
	ListNode nthNode = head;
	ListNode nodeToIterateFrom = head;
	ListNode prevNode = null;
	
	if (nodeToIterateFrom != null && nodeToIterateFrom.next != null)
	{
		while (nodeToIterateFrom.next != null)
		{
			nodeToIterateFrom = nodeToIterateFrom.next;
			if (n == 0)
			{
				prevNode = nthNode;
				nthNode = nthNode.next;
			}
			else
			{
				n--;	
			}
		}
	}
	
	//Remove nth node
	if (prevNode != null)
	{
		prevNode.next = nthNode.next;		
	}
	else
	{
		head = nthNode.next;
	}

	nthNode.next = null;

	return head;
}

//Definition for singly-linked list.
public class ListNode
{
	public int val;
	public ListNode next;
	public ListNode(int x) { val = x; }
}
