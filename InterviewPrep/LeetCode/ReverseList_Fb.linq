<Query Kind="Program" />

void Main()
{
	
}

/**
 * Definition for singly-linked list.
 */
  public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }
 
 
 /*
 Linear time algorithm - O(n)
 
 Edge cases:
 If head is null return null. 
 If size of linked list is 1, return head.
 
 Algo:
 Approach 1: Push them to a stack and then pop each one out.
 Approach 2: 
 For each current node, point it to the previous node (if it exists).
 So first node's next becomes NULL and hence the last node. The second node points to the first and hence the second last node.
 We continue this way (suggesting a loop structure), till end of list.
 */
 
 public ListNode ReverseList(ListNode head) 
 {
 	ListNode root = null;
	if(head != null)
	{	
		ListNode current, prev;
		current = prev = null;
		while(head != null)
		{
			current = head;
			head = current.next;
			current.next = prev;
			prev = current;
		}
		root = current;
	}
    return root;
 }
