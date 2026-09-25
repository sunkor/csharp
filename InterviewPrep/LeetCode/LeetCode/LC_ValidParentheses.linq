<Query Kind="Program" />

void Main()
{
	//IsValid("").Dump();
	//IsValid("()").Dump();
	//IsValid("([)]").Dump();
	IsValid("{[]}").Dump();
	IsValid("()[]{}").Dump();
	IsValid("()[]{[]}{}").Dump();
}

public bool IsValid(string s)
{
	if(s == null) return false;
	if(s.Trim() == "") return true;
	
	var chArray = s.ToCharArray();
	
	if(chArray.Length % 2 != 0) return false;
	
	char lastCh = chArray[0];
	var st = new Stack<char>();
	st.Push(lastCh);
	int stackCount = 1;

	for (int i = 1; i < chArray.Length; i++)
	{
		if ((lastCh == '[' && chArray[i] == ']') ||
		(lastCh == '(' && chArray[i] == ')') ||
		(lastCh == '{' && chArray[i] == '}'))
		{
			st.Pop();
			stackCount--;
			if (stackCount > 0)
			{
				lastCh = st.Peek();
			}
		}
		else
		{
			lastCh = chArray[i];
			st.Push(lastCh);
			stackCount++;
		}
	}
	
	return stackCount == 0;
}
