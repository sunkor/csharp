<Query Kind="Statements" />

// ── Palindrome Algorithms ──────────────────────────────────────────────────

// 1. Classic two-pointer check (O(n) time, O(1) space)
bool IsPalindromeTwoPointer(string s)
{
	int left = 0, right = s.Length - 1;
	while (left < right)
		if (s[left++] != s[right--]) return false;
	return true;
}

// 2. Reverse & compare (O(n) time, O(n) space)
bool IsPalindromeReverse(string s) =>
	s.SequenceEqual(s.Reverse());

// 3. Ignore case and non-alphanumeric (real-world variant)
bool IsPalindromeNormalized(string s)
{
	var clean = s.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray();
	int left = 0, right = clean.Length - 1;
	while (left < right)
		if (clean[left++] != clean[right--]) return false;
	return true;
}

// 4. Longest palindromic substring — Expand Around Center (O(n²))
string LongestPalindromicSubstring(string s)
{
	if (s.Length == 0) return "";
	int start = 0, maxLen = 1;

	void Expand(int l, int r)
	{
		while (l >= 0 && r < s.Length && s[l] == s[r])
		{
			if (r - l + 1 > maxLen) { start = l; maxLen = r - l + 1; }
			l--; r++;
		}
	}

	for (int i = 0; i < s.Length; i++)
	{
		Expand(i, i);     // odd-length palindromes
		Expand(i, i + 1); // even-length palindromes
	}
	return s.Substring(start, maxLen);
}

// 5. Count all palindromic substrings (O(n²))
int CountPalindromicSubstrings(string s)
{
	int count = 0;
	void Expand(int l, int r) { while (l >= 0 && r < s.Length && s[l--] == s[r++]) count++; }
	for (int i = 0; i < s.Length; i++) { Expand(i, i); Expand(i, i + 1); }
	return count;
}

// ── Demo ──────────────────────────────────────────────────────────────────

var testWords = new[] { "racecar", "hello", "level", "world", "madam", "noon", "A" };
testWords
	.Select(w => new
	{
		Word        = w,
		TwoPointer  = IsPalindromeTwoPointer(w),
		Reverse     = IsPalindromeReverse(w),
	})
	.Dump("Palindrome Check");

var sentences = new[]
{
	"A man, a plan, a canal: Panama",
	"race a car",
	"Was it a car or a cat I saw?",
	"hello"
};
sentences
	.Select(s => new { Sentence = s, IsPalindrome = IsPalindromeNormalized(s) })
	.Dump("Normalized Palindrome Check (ignores case & punctuation)");

var substringSamples = new[] { "babad", "cbbd", "racecar", "abacaba" };
substringSamples
	.Select(s => new
	{
		Input                     = s,
		LongestPalindromicSubstr  = LongestPalindromicSubstring(s),
		PalindromicSubstringCount = CountPalindromicSubstrings(s),
	})
	.Dump("Longest Palindromic Substring & Count");

