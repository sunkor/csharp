<Query Kind="Program" />

void Main()
{
	/*
	 * PROBLEM: Group Anagrams
	 * ------------------------
	 * Given an array of strings `strs`, group the anagrams together.
	 * You can return the answer in any order.
	 *
	 * An Anagram is a word or phrase formed by rearranging the letters of
	 * a different word or phrase, typically using all the original letters
	 * exactly once.
	 *
	 * Example 1:
	 *   Input: strs = ["eat","tea","tan","ate","nat","bat"]
	 *   Output: [["bat"],["nat","tan"],["ate","eat","tea"]]
	 *
	 * Example 2:
	 *   Input: strs = [""]
	 *   Output: [[""]]
	 *
	 * Example 3:
	 *   Input: strs = ["a"]
	 *   Output: [["a"]]
	 *
	 * Constraints:
	 *   - 1 <= strs.length <= 10^4
	 *   - 0 <= strs[i].length <= 100
	 *   - strs[i] consists of lowercase English letters.
	 *
	 * Edge cases to consider:
	 *   - Empty strings (e.g. "" is its own anagram group).
	 *   - Single-character strings.
	 *   - Strings that are identical (duplicates) - they belong in the
	 *     same group, and duplicates should be preserved in the output
	 *     (i.e. don't dedupe).
	 *   - All strings are anagrams of each other -> one big group.
	 *   - No two strings are anagrams of each other -> every string is
	 *     its own group.
	 *   - Case sensitivity: per constraints all lowercase, but consider
	 *     what your solution would do if that assumption were violated.
	 *
	 * TASK:
	 *   Implement the method `GroupAnagrams` below. A stub is provided
	 *   that throws NotImplementedException - replace its body with
	 *   your solution.
	 *
	 * This script will run a set of test cases against your
	 * implementation and validate the results (order-independent, both
	 * at the group level and within each group), dumping a pass/fail
	 * summary.
	 */

	var testCases = new (string Name, string[] Input, string[][] Expected)[]
	{
		("Basic example",
			["eat", "tea", "tan", "ate", "nat", "bat"],
			[["bat"], ["nat", "tan"], ["ate", "eat", "tea"]]),

		("Single empty string",
			[""],
			[[""]]),

		("Single character",
			["a"],
			[["a"]]),

		("All empty strings",
			["", "", ""],
			[["", "", ""]]),

		("Duplicates preserved",
			["abc", "cba", "abc"],
			[["abc", "cba", "abc"]]),

		("No anagrams at all",
			["abc", "def", "ghi"],
			[["abc"], ["def"], ["ghi"]]),

		("All anagrams of each other",
			["abc", "bca", "cab", "acb"],
			[["abc", "bca", "cab", "acb"]]),

		("Mixed lengths, some singletons",
			["ab", "ba", "abc", "xyz", "zyx", "a"],
			[["ab", "ba"], ["abc"], ["xyz", "zyx"], ["a"]]),
	};

	var results = testCases.Select(tc =>
	{
		string error = null;
		List<List<string>> actual = null;
		try
		{
			actual = GroupAnagrams(tc.Input).Select(g => g.ToList()).ToList();
		}
		catch (NotImplementedException)
		{
			error = "Not implemented";
		}
		catch (Exception ex)
		{
			error = $"Threw {ex.GetType().Name}: {ex.Message}";
		}

		bool passed = error == null && ValidateGroups(actual, tc.Expected);

		return new
		{
			tc.Name,
			Input = string.Join(", ", tc.Input.Select(s => $"\"{s}\"")),
			Passed = passed,
			Error = error,
			Actual = actual == null ? "" : string.Join(" | ", actual.Select(g => "[" + string.Join(",", g) + "]")),
			Expected = string.Join(" | ", tc.Expected.Select(g => "[" + string.Join(",", g) + "]")),
		};
	}).ToList();

	results.Dump("Test Results");

	var passCount = results.Count(r => r.Passed);
	$"{passCount}/{results.Count} tests passed".Dump();
}

// -----------------------------------------------------------------
// Implement this method.
// -----------------------------------------------------------------
List<List<string>> GroupAnagrams(string[] strs)
{
	throw new NotImplementedException();
}

// -----------------------------------------------------------------
// Validation helper: compares actual vs expected groups, ignoring
// order of groups and order of elements within a group, but treating
// each group as a multiset (so duplicate handling is checked properly).
// -----------------------------------------------------------------
bool ValidateGroups(List<List<string>> actual, string[][] expected)
{
	if (actual == null) return false;
	if (actual.Count != expected.Length) return false;

	// Represent each group as a sorted, comma-joined signature of its
	// (sorted) elements, so we can match groups regardless of order.
	string Signature(IEnumerable<string> group) =>
		string.Join("|", group.OrderBy(s => s, StringComparer.Ordinal));

	var actualSigs = actual.Select(Signature).OrderBy(s => s, StringComparer.Ordinal).ToList();
	var expectedSigs = expected.Select(Signature).OrderBy(s => s, StringComparer.Ordinal).ToList();

	return actualSigs.SequenceEqual(expectedSigs);
}
