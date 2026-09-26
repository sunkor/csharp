<Query Kind="Program" />

#load "xunit"
using Xunit;

/*
 * Problem: Longest Substring Without Repeating Characters
 * ---------------------------------------------------------
 * Given a string s, find the length of the longest substring
 * without repeating characters.
 *
 * Example:
 *   Input: "abcabcbb"  -> Output: 3 ("abc")
 *   Input: "bbbbb"     -> Output: 1 ("b")
 *   Input: "pwwkew"    -> Output: 3 ("wke")
 *   Input: ""          -> Output: 0
 *
 * Typical approach: sliding window with a set/dictionary tracking
 * last seen index of each character, O(n) time, O(min(n, charset)) space.
 *
 * Implement your solution in the `LengthOfLongestSubstring` method below.
 * Tests will validate it via RunTests().
 */

void Main()
{
	RunTests();
}

static int LengthOfLongestSubstring(string s)
{
	if(s == null || s.Length == 0)
	{
		return 0;
	}

	var slidingWindow = new Dictionary<char,int>();
	int max = 0, start = 0;
	
	for(int i = 0; i < s.Length; i++)
	{
		var ch = s[i];
		
		if(slidingWindow.TryGetValue(ch, out var prev) && prev >= start)
		{
			start = prev + 1;
		}
		
		slidingWindow[ch] = i;
		
		max = Math.Max(max, i - start + 1);
	}
	
	return max;
}

public class LongestSubstringTests
{
	[Theory]
	[InlineData("abcabcbb", 3)]
	[InlineData("bbbbb", 1)]
	[InlineData("pwwkew", 3)]
	[InlineData("", 0)]
	[InlineData(" ", 1)]
	[InlineData("au", 2)]
	[InlineData("dvdf", 3)]
	[InlineData("abba", 2)]
	[InlineData("tmmzuxt", 5)]
	public void Test(string input, int expected)
	{
		var actual = UserQuery.LengthOfLongestSubstring(input);
		Assert.Equal(expected, actual);
	}
}
