<Query Kind="Statements" />


int [] numbers = [2,-1,0,3,1,4,2,3];
var k = 3;

MaximumSlidingWindow(numbers,k).Dump();

//k - windowSize
int[] MaximumSlidingWindow(int [] numbers, int k)
{
	if(numbers == null || numbers.Length == 0)
	{
		return null;
	}
	
	var mq = new MonotonicQueue();
	var results = new List<int>();
	for(var i = 0; i < numbers.Length; i++)
	{
		if (i < k - 1)
		{
			mq.Push(numbers[i]);
		}
		else
		{
			mq.Push(numbers[i]);
			int maxValue = mq.Max();
			results.Add(maxValue);
			mq.PopMaximumValue(maxValue);
		}
	}
	
	return results.ToArray();
}

class MonotonicQueue
{
	private LinkedList<int> dequeue = new LinkedList<int>();
	
	//Keep pruning minimum value until the new value is the is the minimum.
	public void Push(int val)
	{
		var node = dequeue.First;
		while(dequeue.Count > 0 && dequeue.Last.Value < val)
		{
			dequeue.RemoveLast();
		}
		
		dequeue.AddLast(val);
	}
	
	public int Max()
	{
		if(dequeue.Count == 0)
			throw new InvalidOperationException("Queue is empty.");
			
		return dequeue.First.Value;
	}
	
	public void PopMaximumValue(int val)
	{
		while (dequeue.Count > 0 && dequeue.Last.Value == val)
		{
			dequeue.RemoveFirst();
		}
	}
}