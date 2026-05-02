<Query Kind="Program" />

void Main()
{
	LengthOfLongestSubstring("").Dump();
	LengthOfLongestSubstring("  ").Dump();
	LengthOfLongestSubstring("abcdefgh").Dump();
	LengthOfLongestSubstring("abba").Dump();
	//LengthOfLongestSubstring("abcabcbb").Dump();
	LengthOfLongestSubstring("dvdf").Dump();
	LengthOfLongestSubstring("cdd").Dump();
	//LengthOfLongestSubstring("ohomm").Dump();
	//LengthOfLongestSubstring("ckilbkd").Dump();	
}

public int LengthOfLongestSubstring(string s)
{
	if(string.IsNullOrEmpty(s)) return 0;
	if(s.Trim().Length == 0) return 1; //whitespace
	
	var maxLength = 0;
	
	var charIndexMap = new int[128];	
	for(int i = 0; i < charIndexMap.Length; i++)
	{
		charIndexMap[i] = -1;
	}
	
	var charArray = s.ToCharArray();
	int stringStartIndex = 0;
	var length = 0;
	for(int index = 0; index < charArray.Length; index++)
	{
		var ch = charArray[index];
		
		var foundIndex = charIndexMap[(int)ch];
		charIndexMap[(int)ch] = index;
		
		if(foundIndex >= 0)
		{
			length = index - stringStartIndex;
			maxLength = maxLength > length ? maxLength : length;
			var newIndex = foundIndex + 1;
			stringStartIndex = newIndex > stringStartIndex ? newIndex : stringStartIndex;
		}
	}
	length = charArray.Length - stringStartIndex;
	return maxLength > length ? maxLength : length;
}
