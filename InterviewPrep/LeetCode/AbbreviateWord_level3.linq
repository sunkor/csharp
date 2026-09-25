<Query Kind="Program" />

void Main()
{
	
}

//Now the hard part – the phrase:
//
//"butter is better than batter or is batter better than butter"
//should now abbreviate to:
//"b4r is be3r t2n ba3r or is ba3r be3r t2n b4r".
//
//Can you determine what is happening here, and then implement it?
//
//[execution time limit]
//0.5 seconds(cs)
//
//[memory limit] 1 GB
public class Solution
{
	public static void Main()
	{
		var testCases = new List<Tuple<string, string>>();
		//AddTestCase(testCases, "butter better batter", "b4r be3r ba3r");
		AddTestCase(testCases, "butter is better than batter or is batter better than butter", "b4r is be3r t2n ba3r or is ba3r be3r t2n b4r");
		AddTestCase(testCases, "butter buttyr is better than batter or is batter better than butter", "b4r bu3r is be3r t2n ba3r or is ba3r be3r t2n b4r");
		// AddTestCase(testCases, "Internationalization", "I18n");
		// AddTestCase(testCases, "I like sunny weather", "I l2e s3y w5r");
		// AddTestCase(testCases, "Kubernetes", "K8s");
		// AddTestCase(testCases, "Subaru", "S4u");
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

		var map = new Dictionary<string, string>();
		var wordsToAbbreviate = new List<string>();
		var reRunAbbreviate = new Dictionary<string, string>();

		//O(N)
		foreach (var word in words)
		{
			wordsToAbbreviate.Add(word);
		}

		do
		{
			reRunAbbreviate.Clear();

			for (int i = 0; i < wordsToAbbreviate.Count; i++)
			{
				var word = wordsToAbbreviate[i];

				var abbreviatedWord = Abbreviate(word);
				var offset = 0;
				while (map.ContainsKey(abbreviatedWord) && map[abbreviatedWord] != word)
				{
					//Console.WriteLine("reRunAbbreviate");
					if (!reRunAbbreviate.ContainsKey(map[abbreviatedWord]))
					{
						reRunAbbreviate.Add(map[abbreviatedWord], abbreviatedWord);
					}
					//Console.WriteLine("reRunAbbreviate finished");

					offset++;
					//var subString = word.Substring(offset);
					abbreviatedWord = Abbreviate(word, offset);
					if (abbreviatedWord.Length < 3)
					{
						break;
					}
				}
				//Console.WriteLine("map");
				map[abbreviatedWord] = word;
				//Console.WriteLine("map finished");
				//abbreviatedWords [i] = abbreviatedWord;
			}

			wordsToAbbreviate.Clear();
			foreach (var keyValue in reRunAbbreviate)
			{
				map.Remove(keyValue.Value);
				wordsToAbbreviate.Add(keyValue.Key);
			}

		} while (reRunAbbreviate.Count > 0);

		var reverseDictionary = new Dictionary<string, string>();
		foreach (var keyValue in map)
		{
			reverseDictionary.Add(keyValue.Value, keyValue.Key);
		}

		//insert final abbreviation.
		//O(N)
		var abbreviatedWords = new string[words.Length];
		var index = 0;
		foreach (var word in words)
		{
			abbreviatedWords[index++] = reverseDictionary[word];
		}

		//O(N)
		return string.Join(" ", abbreviatedWords);
	}

	//Abbreviate - O(1) - assuming word.Length is O(1)       
	private static string Abbreviate(string word, int offset = 0)
	{
		if (word == null || word.Length < 3)
		{
			return word;
		}

		return word.Substring(0, offset + 1) + ((word.Length - offset) - 2).ToString() + word[word.Length - 1];
	}
}
