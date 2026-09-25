<Query Kind="Program" />

void Main()
{
	//MyAtoi("42").Dump();
	//MyAtoi("-95").Dump();
	//MyAtoi("a42").Dump();
	//MyAtoi("4193 with words").Dump();
	MyAtoi("words and 987").Dump();
	MyAtoi("-912834723327").Dump();
	MyAtoi("912834723327").Dump();
	MyAtoi("+-2").Dump();
	MyAtoi("-  234").Dump();
}

public int MyAtoi(string str)
{
	if(str == null || str.Trim().Length == 0) return 0;
			
	var charArray = str.ToCharArray();
	
	var numericalIndex = -1;
	bool? isNegative = null;
	for(int i = 0; i < charArray.Length; i++)
	{
		var ascii = (int)charArray[i];
		if(ascii >= 48 && ascii <= 57)
		{
			numericalIndex = i;
			break;
		}
		if (ascii == ' ')
		{
			if (isNegative.HasValue) //do not accept "- 234", or "-   234". "-234" is okay
			{
				return 0;
			}
			else
			{
				continue;
			}
		}
		if(ascii == '+' || ascii == '-')
		{
			if(isNegative.HasValue)
				return 0;
				
			isNegative = ascii == '-';
			continue;
		}
		return 0; //If we find anything other than whitespace, +/-, we break
	}

	if(numericalIndex == -1 || numericalIndex >= charArray.Length) return 0;

	int num = 0;
	for (int i = numericalIndex; i < charArray.Length; i++)
	{
		var ascii = (int)charArray[i];
		if (ascii >= 48 && ascii <= 57)
		{
			try
			{
				var digit = ascii - 48;
				num = checked(num * 10 + digit);
			}
			catch (System.OverflowException)
			{
				return isNegative.HasValue && isNegative.Value ? int.MinValue : int.MaxValue;
			}
		}
		else
		{
			break; //If we find any non-digit characters, we break. ex: "-234abc258" should return "-234" and not "-234258"
		}
	}
	
	return isNegative.HasValue && isNegative.Value ? num * -1 : num;
}
