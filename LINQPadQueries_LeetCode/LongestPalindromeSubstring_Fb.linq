<Query Kind="Program" />

void Main()
{
	var s = "abracaabbadabra";
	LongestPalindrome(s).Dump();
}

public string LongestPalindrome(string s) 
{
	if(s?.Length < 2) return s;
	var maxLength = 0;
	var index = 0;
	
	for(int i = 0; i < s.Length; i++)
	{
		ExpandToCheckPalindrome(s, i, i, ref maxLength, ref index); //odd		
		ExpandToCheckPalindrome(s, i, i + 1, ref maxLength, ref index); //even
	}
	
	return s.Substring(index, maxLength);
}

private void ExpandToCheckPalindrome(string s, int j, int k, ref int maxLength, ref int index)
{
	while(j >= 0 && k < s.Length && s[j] == s[k])
	{
		j--;
		k++;
	}
	var length = k - j - 1;
	if(maxLength < length)
	{
		index = j + 1;
		maxLength = length;
	}
}

 
 