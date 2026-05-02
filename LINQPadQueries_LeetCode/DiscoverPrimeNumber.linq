<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var numbers = new[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
	//foreach (var number in numbers)
	//	Console.WriteLine($"{number} is Prime ? {IsPrimeNumber(number)}");
	
	//ParallelEnumerable.ForAll(numbers, number => Console.WriteLine($"{number} is Prime ? {IsPrimeNumber(number)}"));
	Parallel.ForEach(numbers, number => Console.WriteLine($"{number} is Prime ? {IsPrimeNumber(number)}"));
}

bool IsPrimeNumber(int number)
{
	if(number < 2) return false;
	if(number == 2) return true;
	var range = Enumerable.Range(2, number / 2);
	foreach (var num in range)
		if(number%num == 0) return false;
	return true;
}

// Define other methods and classes here
