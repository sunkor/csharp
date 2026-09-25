<Query Kind="Program" />

void Main()
{
	LongestPalindrome("cbbd").Dump();
	LongestPalindrome("ac").Dump();
}

public string LongestPalindrome(string s)
{
	if(s == null || string.IsNullOrWhiteSpace(s)) return string.Empty;
	if(s.Trim().Length == 1) return s;
		
	string longestPalindrome = string.Empty;
	var length = s.Length;
	for(int i = 0; i < length; i++)
	{
		var evenP = LongestEvenPalindrome(s, length, i);
		var l1 = evenP?.Length;
		
		var oddP = LongestOddPalindrome(s, length, i);		
		var l2 = oddP?.Length;
		
		var max = evenP?.Length > oddP?.Length ? evenP : oddP;
		longestPalindrome = longestPalindrome.Length < max?.Length ? max : longestPalindrome;
	}
	
	return longestPalindrome;
}

private string LongestEvenPalindrome(string s, int length, int index)
{
	var left = index;
	var right = index + 1;
	
	var found = false;
	while(left >= 0 && right < length && s[left] == s[right])
	{
		found = true;
		left--;
		right++;
	}

	if (found)
	{
		left++;
		right--;
		return s.Substring(left, right - left + 1);
	}
	return string.Empty;;
}

private string LongestOddPalindrome(string s, int length, int index)
{
	var left = index - 1;
	var right = index + 1;

	var found = false;
	while (left >= 0 && right < length && s[left] == s[right])
	{
		found = true;
		left--;
		right++;
	}

	left++;
	right--;
	if (found)
	{
		return s.Substring(left, right - left + 1);
	}
	return s.Substring(left, right - left + 1);
}