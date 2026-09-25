<Query Kind="Program" />

void Main()
{
	var sw = new Stopwatch();
	sw.Start();
	var primes = FindPrimes(9999);
	sw.Stop();
	sw.ElapsedTicks.Dump();
	primes.Count.Dump();
	//primes.Last().Dump();
	primes.Dump();
}

private IList<int> FindPrimes(int n)
{
	var listOfPrimes = new List<int>();
	var range = Enumerable.Range(1, n);
	foreach(var value in range)
	{
		if(IsPrime(value)) listOfPrimes.Add(value);
	}
	return listOfPrimes;
}

//private bool IsPrime(int n)
//{
//	if(n < 2) return false;
//	if(n == 2) return true;
//	int divTill = (int)Math.Sqrt(n);
//	for(var i = 2; i <= divTill; i++) if(n % i == 0) return false;
//	return true;
//}

private bool IsPrime(int n)
{
	if(n < 2) return false;
	if(n == 2) return true;
	int divTill = (int)Math.Sqrt(n);
	var i = 1;
	while(++i <= divTill && n % i != 0);
	return i > divTill;
}