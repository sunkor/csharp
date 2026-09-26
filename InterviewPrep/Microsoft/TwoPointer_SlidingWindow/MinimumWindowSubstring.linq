<Query Kind="Program" />

void Main()
{
	// Run this after you've implemented MinWindow() below.
	RunTests();
}

/*
=====================================================================
 PROBLEM: Minimum Window Substring (LeetCode 76)
=====================================================================
Given two strings s and t of lengths m and n respectively, return the
minimum window substring of s such that every character in t
(including duplicates) is included in the window. If there is no
such substring, return the empty string "".

The testcases will be generated such that the answer is unique.

-----------------------------------------------------------------
Constraints:
  - m == s.Length
  - n == t.Length
  - 1 <= m, n <= 10^5
  - s and t consist of uppercase and lowercase English letters.
  

-----------------------------------------------------------------
Examples:
  Input:  s = "ADOBECODEBANC", t = "ABC"
  Output: "BANC"
  Explanation: The minimum window substring "BANC" includes 'A', 'B',
  and 'C' from string t.

  Input:  s = "a", t = "a"
  Output: "a"

  Input:  s = "a", t = "aa"
  Output: ""
  Explanation: Both 'a's from t must be included in the window.
  Since the largest window of s only has one 'a', return empty string.

-----------------------------------------------------------------
Follow up: Could you find an algorithm that runs in O(m + n) time?

-----------------------------------------------------------------
Approach hint (classic sliding window):
  1. Build a frequency map (Dictionary<char,int>) of characters
     needed from t.
  2. Use two pointers (left, right) over s, expanding right to
     include characters, and track how many "required" distinct
     characters currently have their frequency satisfied.
  3. Once all required characters are satisfied, try to shrink
     from the left to find the smallest valid window, updating the
     best answer as you go.
  4. Continue until right reaches the end of s.
=====================================================================
*/

/// <summary>
/// Implement this method. Given strings s and t, return the minimum
/// window substring of s containing all characters of t (with
/// multiplicity), or "" if no such window exists.
/// </summary>
string MinWindow(string s, string t)
{
	if(s == null || t == null || s.Length < t.Length)
		return "";
	
	Span<int> need = stackalloc int[128];
	foreach(var ch in t)
	{
		need[ch]++;
	}
	
	var left = 0;
	var bestStart = 0;
	var bestLen = int.MaxValue;
	var missing = t.Length;
	
	for(int right = 0; right < s.Length; right++)
	{
		//Found in 
		if(need[s[right]]-- > 0) 
		{
			missing--;
		}

		while (left <= right && missing == 0)
		{
			if(right - left + 1 < bestLen)
			{
				bestLen = right - left + 1;
				bestStart = left;
			}
						
			if (++need[s[left]] > 0) 
			{
				missing++;
			}
			
			//reduce window
			left++;
		}
	}
	
	return bestLen == int.MaxValue ? "" : s.Substring(bestStart, bestLen);
}

// ---------------------------------------------------------------
// Test harness / validation
// ---------------------------------------------------------------

record TestCase(string S, string T, string Expected);

List<TestCase> GetTestCases() => new()
{
	new("ADOBC", "DB", "DOB"),
	
	// Classic example
	new("ADOBECODEBANC", "ABC", "BANC"),

	// Single character match
	new("a", "a", "a"),

	// Not enough characters available
	new("a", "aa", ""),

	// t longer than s
	new("a", "abc", ""),

	// Entire s is the answer
	new("abc", "abc", "abc"),

	// Duplicate characters in t
	new("aa", "aa", "aa"),

	// Window at the end of s
	new("kalsk", "akl", "kal"),

	// Case sensitivity (uppercase vs lowercase distinct)
	new("AaAaAaA", "AA", "AaA"),

	// No match at all
	new("xyz", "abc", ""),

	// t is a single repeated char, s has just enough
	new("bbaa", "aba", "baa"),

	// Whole string required, minimal already
	new("ab", "ab", "ab"),

	// Larger random-ish case
	new("this is a test string", "tist", "t stri"),
};

void RunTests()
{
	var cases = GetTestCases();
	var results = new List<object>();

	int passed = 0;
	foreach (var (i, tc) in cases.Select((c, i) => (i, c)))
	{
		string actual = "";
		string status;
		try
		{
			actual = MinWindow(tc.S, tc.T);
			bool ok = actual == tc.Expected;
			status = ok ? "PASS" : "FAIL";
			if (ok) passed++;
		}
		catch (Exception ex)
		{
			actual = "<exception>";
			status = $"ERROR: {ex.Message}";
		}

		results.Add(new
		{
			Case = i + 1,
			S = tc.S,
			T = tc.T,
			Expected = tc.Expected,
			Actual = actual,
			Status = status
		});
	}

	results.Dump($"Minimum Window Substring — {passed}/{cases.Count} passed");
}
