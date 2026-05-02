<Query Kind="Program" />

void Main()
{
	var strPairs = new[,] { { "pale", "ple" },
							{ "pales", "pale" },
							{ "pale", "bale" },
							{ "pale", "bake" },
							{"",""},
							{"","a"},
							{"b",""},
							{"","ab"},
							{null,"a"}};

							
	for(int i = 0; i <= strPairs.GetUpperBound(0); i++)
	{
		Console.WriteLine($"{strPairs[i, 0]}, {strPairs[i, 1]} => {IsOneEditAway(strPairs[i, 0], strPairs[i, 1])}");
	}
}

private bool IsOneEditAway(string str1, string str2)
{
	if (str1 == null || str2 == null) return false;
	if ((string.IsNullOrEmpty(str1) && str2.Length == 1) || (str1.Length == 1 && string.IsNullOrEmpty(str2))) return true;

	var numberOfEdits = 0;
	int i = 0, j = 0;

	while (numberOfEdits < 2 && i < str1.Length && j < str2.Length)
	{
		if (str1[i] == str2[j])
		{
			i++;
			j++;
			continue;
		}
		else
		{
			if ((j + 1) < str2.Length && str1[i] == str2[j + 1]) //insert or delete
			{
				j++;
				numberOfEdits++;
			}
			else if ((i + 1) < str1.Length && str1[i + 1] == str2[j]) //insert or delete
			{
				i++;
				numberOfEdits++;
			}
			else if ((i + 1) < str1.Length && (j + 1) < str2.Length && str1[i + 1] == str1[j + 1]) //replace
			{
				i++;
				j++;
				numberOfEdits++;
			}
		}
	}
	
	if(i < str1.Length) numberOfEdits += str1.Length - i;
	if(j < str2.Length) numberOfEdits += str2.Length - j;
	
	return numberOfEdits == 1;
}


