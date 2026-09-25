<Query Kind="Program" />

void Main()
{
	var str = "taco cat";
	var table = buildCharTable(str);
	IsPermutationOfPalindrome(table).Dump();
}

private bool IsPermutationOfPalindrome(short [] characters)
{
	if(characters == null) return false;
	
	bool result = true, foundOdd = false;
	foreach (var numberOfOccurences in characters)
	{
		if (numberOfOccurences > 0)
		{
			if (numberOfOccurences % 2 == 1)
			{
				if (foundOdd)
				{
					result = false;
					break;
				}
				else
					foundOdd = true;
				
			}
		}
	}
	return result;
}

private short[] buildCharTable(string data)
{
	var characters = new short[26];
	foreach (var ch in data)
	{
		//If TAB or SPACE then continue
		if (ch == 9 || ch == 32) continue;

		var index = -1;
		if (ch >= 97 && ch <= 122)
		{
			index = ch - 97;
		}
		else if (ch >= 65 && ch <= 90)
		{
			index = ch - 65;
		}
		else
		{
			characters = null;
			break;
		}
		if (index > -1) characters[index]++;
	}
	return characters;
}