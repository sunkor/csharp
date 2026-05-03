<Query Kind="Statements" />

var rb = new RingBuffer<int>(3);

rb.Add(10);
rb.Add(20);
rb.Add(30);
// [10, 20, 30]

rb.Add(40);
// [20, 30, 40]

rb.Add(50);
// [30, 40, 50]

(rb.GetValuesInOrders()).Dump();

class RingBuffer<T>
{
	T[] buffer;
	int capacity;
	int count;
	
	int index = 0;
		
	bool IsFull => count == capacity;
	bool IsEmpty => count == 0;
	
	public RingBuffer(int capacity)
	{
		this.capacity = capacity;
		buffer = new T[capacity];
	}
	
	public void Add(T number)
	{
		//if(IsFull)
			//throw new Exception("Buffer is full.");
		
		buffer[index] = number;
		index = (index + 1) % capacity;

		if (count < buffer.Length)
			count++;
	}
	
	public T[] GetValuesInOrders()
	{
		if(count == 0)
			return null;
		
		var values = new T[count];
		int startIndex = 0;
		while(!IsEmpty)
		{
			values[startIndex++] = buffer[index];
			index = (index + 1) % capacity;
			count--;
		}
		return values;
	}
	
	/// <summary>
	/// Return head value.
	/// </summary>
	public T Peek()
	{
		if (count == 0)
			return default(T);
			
		return buffer[index];
	}
}