<Query Kind="Program" />

void Main()
{
	var str = "Mr John Smith	";
	URLify(str).Dump();
}

private string URLify(string data)
{
	var sb = new StringBuilder();
	int startIndex = -1, lastSeenCharAt = -1;
	for (var i = 0; i < data.Length; i++)
	{
		var ch = (int)data[i];
		if (ch == 9 || ch == 32) //TAB or SPACE
		{
			if (startIndex > -1)
			{
				lastSeenCharAt = i - 1;
				if(sb.Length > 0) sb.Append("%20");
				sb.Append(data.Substring(startIndex, lastSeenCharAt - startIndex + 1));
				startIndex = -1;
			}
			continue;
		}
		else if (startIndex == -1) startIndex = i;
	}
	if (startIndex > -1)
	{
		if (sb.Length > 0) sb.Append("%20");
		sb.Append(data.Substring(startIndex, lastSeenCharAt - startIndex + 1));
	}
	return sb.ToString();
}

// Define other methods and classes here
