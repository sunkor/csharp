<Query Kind="Program" />

void Main()
{
	var l1 = new ListNode(2);
	l1.next = new ListNode(4);
	l1.next.next = new ListNode(3);

	var l2 = new ListNode(5);
	l2.next = new ListNode(6);
	l2.next.next = new ListNode(4);
	
	var l3 = Solution.AddTwoNumbers(l1, l2);
}

public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }

public static class Solution
{
	public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
	{
		var stack1 = new Stack<int>();
		while (l1 != null)
		{
			stack1.Push(l1.val);
			l1 = l1.next;
		}

		var stack2 = new Stack<int>();
		while (l2 != null)
		{
			stack2.Push(l2.val);
			l2 = l2.next;
		}

		ListNode sumNode = null;
		ListNode prevNode = null;

		int largestCount = stack1.Count > stack2.Count ? stack1.Count : stack2.Count;

		bool isCarryOver = false;
		for (int i = 0; i < largestCount; i++)
		{
			int sum = 0;
			if (stack1.Count > 0)
				sum += stack1.Pop();

			if (stack2.Count > 0)
				sum += stack2.Pop();

			if (isCarryOver) sum += 1;
			isCarryOver = false;

			int finalNumber = 0;

			if (sum > 9)
			{
				finalNumber = sum % 10;
				isCarryOver = true;
			}
			else
			{
				finalNumber = sum;
			}

			var node = new ListNode(finalNumber);
			if (sumNode == null)
				sumNode = node;
			else
				prevNode.next = node;

			prevNode = node;
		}

		return sumNode;
	}
}