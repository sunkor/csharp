<Query Kind="Program" />

void Main()
{
	//AddBinary("11","1").Dump();
	//AddBinary("0","0").Dump();
	//AddBinary("1","111").Dump();
	AddBinary("1111","1111").Dump();
}

public string AddBinary(string a, string b) 
{
	var l1 = a.Length;
	var l2 = b.Length;
	var isCarryOver = false;
	var stack = new Stack<char>();
	
	while(l1 > 0 && l2 > 0)
	{
		var ch1 = a[--l1];
		var ch2 = b[--l2];
		char newChar;
		
		if(ch1 == ch2)
		{
			newChar = isCarryOver ? '1' : '0';
			isCarryOver = ch1 == '1' ? true : false;
		}
		else //1 & 0
		{
			newChar = isCarryOver ? '0' : '1';			
		}
		
		stack.Push(newChar);
	}
	
	string remaining = null;
	int remainingLength = 0;
	if(l1 > 0) 
	{
		remainingLength = l1;
		remaining = a;
	}
	else if(l2 > 0)
	{
		remainingLength = l2;
		remaining = b;
	}
		
	while(remainingLength > 0)
	{		
		var ch = remaining[--remainingLength];
		char newChar;
		if(ch == '1')
		{
			newChar = isCarryOver ? '0' : '1';
		}
		else
		{
			newChar = isCarryOver ? '1' : '0';
			isCarryOver = false;
		}
		
		stack.Push(newChar);
	}
	
	if(isCarryOver) stack.Push('1');
	
	return new string(stack.ToArray());
}