<Query Kind="Program" />

void Main()
{
	IsIntegerAPalindrome(-2147483648).Dump();
}

public bool IsIntegerAPalindrome(int x)
{
	var isPalindrome = false;
	//x = x < 0 ? Math.Abs(x) : x;
	var list = new List<int>();
	var copy = x;
	while (copy != 0)
	{
		list.Add(copy % 10);
		copy /= 10;
	}

	if (list.Count <= 1) return true;

	isPalindrome = true;
	int start = 0, end = list.Count - 1;
	while (start < end)
	{
		if (list[start++] != list[end--])
		{
			isPalindrome = false;
			break;
		}
	}

	return isPalindrome;
}
