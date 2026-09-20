<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
</Query>

void Main()
{
	StaticAllocSumDigitDemo("a3b4c").Dump();
	//ArrayPoolDemo();	
}

static int StaticAllocSumDigitDemo(string text)
{
	Span<int> arr = stackalloc int[16];
	
	int count = 0;
	foreach(var ch in text)
		if(char.IsDigit(ch) && count < arr.Length)
			arr[count++] = ch - '0';
		
	int sum = 0;
	for(int i = 0; i < count; i++)
	{
		sum += arr[i];
	}
	return sum;
}

static void ArrayPoolDemo()
{
	var rand = new Random();

	ArrayPool<int> arrayPool = ArrayPool<int>.Shared;
	int[] buffer = null;

	try
	{
		int length = 10;

		//Prepare
		buffer = arrayPool.Rent(length);
		var span = buffer.AsSpan(0, length);

		var enumeratedRange = Enumerable.Range(1, 5);

		for (int i = 0; i < length; i++)
		{
			span[i] = rand.Next();
		}

		//Act
		//Make calls to manipuate array
		span.Dump();

		var subRangeSpan = span.Slice(2, 5);
		subRangeSpan.Sort();

		subRangeSpan.Dump();
		span.Dump();
	}
	finally
	{
		//Release
		ArrayPool<int>.Shared.Return(buffer, clearArray: true);
	}
}