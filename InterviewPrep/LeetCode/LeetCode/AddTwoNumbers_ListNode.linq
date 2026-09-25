<Query Kind="Program" />

void Main()
{
	
}

 public class ListNode
{
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }
 
public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
{
	if(l1 == null && l2 == null) return null;
	if(l1 == null) return l2;
	if(l2 == null) return l1;
	
	ListNode root = null;
	ListNode node = null;
	
	int carry = 0;
	while(l1 != null || l2 != null)
	{
		var v1 = l1 != null ? l1.val : 0;
		var v2 = l2 != null ? l2.val : 0;
		
		var result = v1 + v2 + carry;
		var val = 0;
		if(result >= 10)
		{
			val = result%10; //25, 25%10 = 5
			carry = result / 10; //25, 25%10 = 5
		}
		else
		{
			carry = 0;
			val = result;
		}
		
		if(root == null)
		{
			root = new ListNode(val);
			node = root;
		}
		else
		{
			var nextNode = new ListNode(val);
			node.next = nextNode;
			node = nextNode;
		}
		
		l1 = l1?.next;
		l2 = l2?.next;
	}
	
	if(carry > 0)
	{
		var nextNode = new ListNode(carry);
		node.next = nextNode;
		node = nextNode;
	}
			
	return root;
}