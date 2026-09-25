<Query Kind="Program" />

void Main()
{
	//var words = new [] {"wrt", "wrf", "er", "ett", "rftt"};	
	//var words = new [] {"z", "z"};
	//var words = new [] {"z", "x", "z"};
	//var words = new [] {"aac","aabb","aaba"};
	//var words = new [] {"zy","zx"};
	var words = new [] {"ac","ab","b"};
	AlienOrder(words).Dump();
}

/*
There is a new alien language which uses the latin alphabet. However, the order among letters are unknown to you. You receive a list of non-empty words from the dictionary, 
where words are sorted lexicographically by the rules of this new language. Derive the order of letters in this language.

------------------------------
Examples
------------------------------
[
  "wrt",
  "wrf",
  "er",
  "ett",
  "rftt"
]
The correct order is: "wertf".

[
  "z",
  "x"
]
The correct order is: "zx".
*/

public string AlienOrder(string[] words) 
{
	if(words?.Length == 0) return string.Empty;
	
	var linkedList = new LinkedList<char>();
	var dict = new Dictionary<char, LinkedListNode<char>>();
	
	var index = 0;
	var found = true;
	while(found)
	{
		found = false;
		for(var i = 0; i < words.Length; i++)
		{
			var word = words[i];
			if(index < word.Length)
			{
				found = true;
				var ch = word[index];
				LinkedListNode<char> node;
				if(!dict.TryGetValue(ch, out node))
				{
					node = new LinkedListNode<char>(ch);
					dict.Add(ch, node);
					linkedList.AddLast(node);
				}
				else
				{
					if(linkedList.Last.Value != ch)
					{
						linkedList.Remove(node);
						linkedList.AddLast(node);
					}
				}
			}
		}
		index++;
	}
	
	return new string(linkedList.ToArray());
}