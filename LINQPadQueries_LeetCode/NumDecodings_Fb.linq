<Query Kind="Program" />

void Main()
{
	NumDecodings("101").Dump();
}

public int NumDecodings(string s) 
{
	var decodings = new Dictionary<string, int>();
	return NumDecodings(s, decodings);
}

private int NumDecodings(string s, Dictionary<string, int> decodings)
{
	if(s == null || s.Length == 0) return 0;
	if(s.Length == 1 && s[0] == '0') return 0;
	
	//Memoization
	if(decodings.ContainsKey(s)) return decodings[s];
	
	int numOfDecodings = 0;
	if(s.Length <= 2)
	{
		
		var value = int.Parse(s);
		
		if(s[0] == '0') numOfDecodings = 0;
		else if(value == 0) numOfDecodings = 0;
		else if(value >= 1 && value <= 10) numOfDecodings = 1;
		else if(value >= 11 && value <= 26) numOfDecodings = 2;
		else if(value % 10 == 0) numOfDecodings = 0;
		else if(value >= 27 && value <= 99) numOfDecodings = 1;
		
		if(!decodings.ContainsKey(s)) decodings[s] = numOfDecodings;
		
		return numOfDecodings;
	}
	else
	{
		int midPoint = s.Length / 2;
		var l1 = NumDecodings(s.Substring(0, midPoint));
		var l2 = NumDecodings(s.Substring(midPoint, s.Length - midPoint));	
		
		l1.Dump();
		l2.Dump();
		"next".Dump();
		
		if(l1 == 0 || l2 == 0)
			numOfDecodings = 0;
		else // if(l1 == 1 && l2 == 1)
		{
			numOfDecodings = l1 + l2;
		}
				
		if(!decodings.ContainsKey(s)) decodings[s] = numOfDecodings;
			
		return numOfDecodings;
	}
}