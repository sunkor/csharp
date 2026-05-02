<Query Kind="Program" />

void Main()
{
	LetterCombinations("23").Dump();
}

public IList<string> LetterCombinations(string digits) 
{
	if(string.IsNullOrWhiteSpace(digits)) return new List<string>();
	
	var charMap = BuildMap();
	var root = new TrieNode('\0');
		
	foreach(var digit in digits)
	{
		int ascii = (int)digit;
		if(ascii >= 50 && ascii <= 57)
		{
			var chars = charMap[ascii - 48]; //O(1)
			BuildTrieNode(root, chars);
		}
	}
	
	var words = new List<string>();
	BuildWordsFromTrieUsingDFS(root, new List<char>(), words);
	
	return words;
}

private void BuildWordsFromTrieUsingDFS(TrieNode node, List<char> characters, IList<string> words)
{
	if(node.Val != '\0') characters.Add(node.Val);
	if(node.Nodes.Count > 0)
	{	
		foreach(var childNode in node.Nodes)
		{
			BuildWordsFromTrieUsingDFS(childNode, characters, words);
		}
	}
	else{
		words.Add(new string(characters.ToArray()));
	}
	if(node.Val != '\0') characters.RemoveAt(characters.Count - 1);
}

private void BuildTrieNode(TrieNode node, char[] chars)
{
	if(node.Nodes.Count > 0)
	{
		foreach(var childNode in node.Nodes)
		{
			BuildTrieNode(childNode, chars);
		}
	}
	else
	{
		foreach(var ch in chars)
		{
			node.Nodes.Add(new TrieNode(ch));
		}
	}
}

public class TrieNode
{
	public char Val;
	public IList<TrieNode> Nodes;
	
	public TrieNode(char ch)
	{
		this.Val = ch;
		this.Nodes = new List<TrieNode>();
	}
}

public static Dictionary<int, char[]> BuildMap()
{
	var dict = new Dictionary<int, char[]>();
	dict.Add(2,new [] {'a','b','c'});
	dict.Add(3,new [] {'d','e','f'});
	dict.Add(4,new [] {'g','h','i'});
	dict.Add(5,new [] {'j','k','l'});
	dict.Add(6,new [] {'m','n','o'});
	dict.Add(7,new [] {'p','q','r','s'});
	dict.Add(8,new [] {'t','u','v'});
	dict.Add(9,new [] {'w','x','y','z'});
	return dict;
}