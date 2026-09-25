<Query Kind="Program" />

void Main()
{
	WordBreak("leetcode",new List<string>(){"leet","code"}).Dump();	
	//WordBreak("abcd",new List<string>(){"a","abc","b","cd"}).Dump();	
	//WordBreak("a",new List<string>(){"a"}).Dump();	
	//WordBreak("aaaaaaa",new List<string>(){"aaaa","aa"}).Dump();	
}

public bool WordBreak(string s, IList<string> wordDict) 
{
	if(wordDict == null || wordDict.Count == 0 || string.IsNullOrWhiteSpace(s)) return false;
	
	var set = new HashSet<string>();
	foreach(var word in wordDict)
		set.Add(word);
		
	var f = new bool[s.Length + 1];
	f[0] = true;
	
	for(var i = 1; i <= s.Length; i++)
	{
		for(var j = 0; j < i; j++)
		{	
			if(f[j] && set.Contains(s.Substring(j, i - j)))
			{
				f[i] = true;
				break;
			}
		}
	}
	return f[s.Length];
}
