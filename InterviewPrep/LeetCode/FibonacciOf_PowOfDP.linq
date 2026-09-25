<Query Kind="Program" />

void Main()
{
	var sw = new Stopwatch();
	sw.Start();
	var fibNum = 40;
	//var list = FibonacciSeriesOf(null, fibNum);// - Increases exponentialy without DP!!
	var list = FibonacciSeriesOf(new Dictionary<int, long>(), fibNum);
	sw.Stop();
	sw.ElapsedTicks.Dump();
	list.Dump();
}
/*
	Understanding Fibonacci series:
	For Fib of 
	1, is [1]
	2, is [1, 1]
	3, is [1, 1, 2]
	4, is [1, 1, 2, 3]
	5, is [1, 1, 2 , 3, 5]
	
	For a nth sequence, we do fib of n + fibOf(n - 1)
*/

public List<long> FibonacciSeriesOf(Dictionary<int, long> dict, int n)
{
	if(n <= 0) return null;
	
	var list = new List<long>();
	
	for(int i = 1; i <= n; i++)
		list.Add(FibonacciOf(dict, i));
			
	return list;
}

private long FibonacciOf(int n)
{
	return FibonacciOf(new Dictionary<int, long>(), n);
}

private long FibonacciOf(Dictionary<int, long> dict, int n)
{
	if(n <= 0) return 0;
	if(n == 1) return 1;
	long value;
	if(dict == null || !dict.TryGetValue(n, out value))
	{
		value = FibonacciOf(dict, n - 2) + FibonacciOf(dict, n - 1);
		dict?.Add(n, value);
	}
	return value;
}
