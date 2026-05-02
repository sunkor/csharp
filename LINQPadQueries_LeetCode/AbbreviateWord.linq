<Query Kind="Program" />

void Main()
{
	
}

public class Solution
{
	public static void Main()
	{
		var testCases = new List<Tuple<string, string>>();
		AddTestCase(testCases, "Internationalization", "I18n");
		AddTestCase(testCases, "I like sunny weather", "I l2e s3y w5r");
		//AddTestCase(testCases, "Kubernetes", "K8s");
		//AddTestCase(testCases, "Subaru", "S4u");
		// AddTestCase(testCases, "Cat", "C1t");
		// AddTestCase(testCases, "A", "A");
		// AddTestCase(testCases, "Ab", "Ab");
		// AddTestCase(testCases, null, null);
		// AddTestCase(testCases, "", "");

		var testCaseCount = testCases.Count;
		var matchCount = 0;
		var mismatchCount = 0;

		foreach (var testCase in testCases)
		{
			var sentence = testCase.Item1;
			var expectedsentence = testCase.Item2;
			var abbreviatedsentence = AbbreviateSentence(sentence);

			Console.WriteLine($"Word: {sentence}");
			Console.WriteLine($"Abbreviated word: {abbreviatedsentence}");
			Console.WriteLine($"Expected Word: {expectedsentence}");

			if (expectedsentence != abbreviatedsentence)
			{
				mismatchCount++;

				Console.WriteLine($"******Does NOT match.******");
			}
			else
			{
				//Console.WriteLine($"Matches.");    
				matchCount++;
			}

			Console.WriteLine();
		}

		Console.WriteLine($"Finished. Total test cases: {testCaseCount}, match count: {matchCount}, mismatch: {mismatchCount}");
	}

	private static void AddTestCase(List<Tuple<string, string>> testCases, string testValue, string expectedValue)
	{
		testCases.Add(new Tuple<string, string>(testValue, expectedValue));
	}

	private static string AbbreviateSentence(string sentence)
	{
		if (sentence == null || sentence.Length < 3)
		{

			return sentence;
		}

		//Tokenize - O(N)
		var words = sentence.Split(" ");

		var abbreviatedWords = new string[words.Length];

		//O(N)
		for (int i = 0; i < words.Length; i++)
		{
			abbreviatedWords[i] = Abbreviate(words[i]);
		}

		//O(N)
		return string.Join(" ", abbreviatedWords);
	}

	//Abbreviate - O(1) - assuming word.Length is O(1)       
	private static string Abbreviate(string word)
	{
		if (word == null || word.Length < 3)
		{
			return word;
		}

		return word[0] + (word.Length - 2).ToString() + word[word.Length - 1];
	}
}

