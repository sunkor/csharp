<Query Kind="Program" />

void Main()
{
	// ============================================================
	// PROBLEM: Valid Parentheses
	// ============================================================
	// Given a string `s` containing just the characters
	// '(', ')', '{', '}', '[' and ']', determine if the input
	// string is valid.
	//
	// An input string is valid if:
	//   1. Open brackets must be closed by the same type of bracket.
	//   2. Open brackets must be closed in the correct order.
	//   3. Every close bracket has a corresponding open bracket
	//      of the same type.
	//
	// Example 1:
	//   Input:  "()"
	//   Output: true
	//
	// Example 2:
	//   Input:  "()[]{}"
	//   Output: true
	//
	// Example 3:
	//   Input:  "(]"
	//   Output: false
	//
	// Example 4:
	//   Input:  "([)]"
	//   Output: false
	//
	// Example 5:
	//   Input:  "{[]}"
	//   Output: true
	//
	// Constraints:
	//   - 1 <= s.length <= 10^4
	//   - s consists only of parentheses characters '()[]{}'.
	// ============================================================

	RunTests();
}

// ------------------------------------------------------------
// YOUR SOLUTION GOES HERE
// ------------------------------------------------------------
bool IsValid(string s)
{
	if(s == null || s.Length == 0)
		return true;
		
	
	var stack = new Stack<char>();
	
	foreach(var ch in s)
	{
		switch (ch)
		{
			case '(':
			case '[':
			case '{':
				stack.Push(ch);
				break;
			case ')':
				if (stack.Count == 0 || stack.Pop() != '(') return false;
				break;
			case ']':
				if (stack.Count == 0 || stack.Pop() != '[') return false;
				break;
			case '}':
				if (stack.Count == 0 || stack.Pop() != '{') return false;
				break;
		}
	}

	return stack.Count == 0;
}

// ------------------------------------------------------------
// Test cases and validation
// ------------------------------------------------------------
void RunTests()
{
	var testCases = new (string Input, bool Expected)[]
	{
		("()", true),
		("()[]{}", true),
		("(]", false),
		("([)]", false),
		("{[]}", true),
		("", true),           // edge case: empty string
		("(", false),         // unmatched open
		(")", false),         // unmatched close
		("]", false),
		("(((((((())))))))", true),
		("(){}}{", false),
		("[({})]", true),
		("[(])", false),
	};

	var results = testCases.Select(tc =>
	{
		bool actual;
		string error = null;
		try
		{
			actual = IsValid(tc.Input);
		}
		catch (Exception ex)
		{
			actual = false;
			error = ex.GetType().Name + ": " + ex.Message;
		}

		return new
		{
			Input = tc.Input,
			Expected = tc.Expected,
			Actual = actual,
			Pass = error == null && actual == tc.Expected,
			Error = error
		};
	}).ToList();

	results.Dump("Test Results");

	int passCount = results.Count(r => r.Pass);
	$"{passCount}/{results.Count} tests passed".Dump();
}
