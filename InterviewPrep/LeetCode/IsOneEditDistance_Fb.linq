<Query Kind="Program" />

void Main()
{
	IsOneEditDistance("a", "ba").Dump();
}

public bool IsOneEditDistance(string s, string t) 
{
	if(string.IsNullOrWhiteSpace(s) && string.IsNullOrWhiteSpace(t)) return false;
	if(Math.Abs(s.Length - t.Length) > 1) return false;
	
	int i = 0, j = 0, numOfEdits = 0, sLength = s.Length, tLength = t.Length;		
	while(numOfEdits < 2 && i < sLength && j < tLength)
	{
		if(s[i++] != t[j++])
		{
			numOfEdits++;
			
			if(i < sLength && s[i] == t[j - 1])
			{
				j--;
			}
			else if(j < tLength && s[i - 1] == t[j])
			{
				i--;
			}
		}
	}
	if(numOfEdits <= 1) numOfEdits += (i < s.Length) ? s.Length - i : t.Length - j;
	return numOfEdits == 1;
}