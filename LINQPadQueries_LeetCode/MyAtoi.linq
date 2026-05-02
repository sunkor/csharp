<Query Kind="Program" />

void Main()
{
	MyAtoi("+1").Dump();
}


public int MyAtoi(string str)
{
	if (string.IsNullOrWhiteSpace(str)) return 0;

	int? sign = null;
	int? number = null;

	try
	{
		foreach (var ch in str)
		{
			var ascii = (int)ch;

			if (ascii >= 48 && ascii <= 57)
			{
				if (number == null) number = 0;
				number = checked(number * 10 + (ascii - 48));
			}
			else if (ascii == 43 || ascii == 45) //45 is minus '-'
			{
				if (sign == null)
					sign = ch;
				else
					return 0;
			}
			else if (ascii == 32)  //space
			{
				if (number != null || sign != null)
					break;
				else
					continue;
			}
			else
				break;
		}
	}
	catch (System.OverflowException)
	{
		number = (sign.HasValue && sign == '-') ? -2147483648 : int.MaxValue;
	}

	number = number == null ? 0 : number;
	return (sign.HasValue && sign == '-') ? number.Value * -1 : number.Value;
}

