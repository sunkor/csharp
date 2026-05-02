<Query Kind="Program" />

void Main()
{
	var words = new List<string>(){"hot","dot","dog","lot","log","cog"};
	LadderLength("hit","cog",words).Dump();
}

public int LadderLength(string beginWord, string endWord, IList<string> wordList) 
{
	if(wordList == null || wordList.Count <= 1) return 0;
	
	var set = new HashSet<string>();
	var root = new TrieNode('\0');
	foreach(var word in wordList)
	{
		TrieNode.BuildTrie(root, word);
		set.Add(word);
	}
	
	var queue = new Queue<Tuple<int,string>>();
	queue.Enqueue(new Tuple<int, string>(1, beginWord));
	
	var visited = new HashSet<string>();
	while(queue.Count > 0)
	{
		var item = queue.Dequeue();
		var curWord = item.Item2;
		
		if(visited.Contains(curWord)) continue;
		visited.Add(curWord);
		for(int i = 0; i < curWord.Length; i++)
		{
			char [] charsInNode = null;
			if(TrieNode.TryCharsInNodeAt(root, curWord, i, out charsInNode))
			{
				foreach(var ch in charsInNode)
				{
					if(ch == curWord[i]) continue;		
					var characters = curWord.ToCharArray();
					characters[i] = ch;
					var newWord = new string(characters);
					if(!visited.Contains(newWord) && set.Contains(newWord))	
					{
						var length = item.Item1;
						if(newWord == endWord)
							return length + 1;
						else
							queue.Enqueue(new Tuple<int, string>(length + 1, newWord));
					}
				}
			}
		}
	}
	
	return 0;        
}

public class TrieNode
{
	public char Val;
	public bool IsWord;
	public Dictionary<char, TrieNode> Nodes;
	
	public TrieNode(char ch)
	{
		Val = ch;
		Nodes = new Dictionary<char, TrieNode>();
	}
		
	public static void BuildTrie(TrieNode node, string word)
	{
		foreach(var ch in word)
		{
			TrieNode childNode;
			if(!node.Nodes.TryGetValue(ch, out childNode))
			{
				childNode = new TrieNode(ch);
				node.Nodes.Add(ch, childNode);
			}
			node = childNode;
		}
		node.IsWord = true;
	}
	
	public static bool TryCharsInNodeAt(TrieNode node, string word, int index, out char[] characters)
	{
		characters = null;
		var i = 0;
		var nodes = node.Nodes;
	
		while(nodes != null && i != index)
		{
			var ch = word[i++];
			TrieNode childNode;
			if(!nodes.TryGetValue(ch, out childNode)) return false;
			nodes = childNode.Nodes;
		}

		characters = nodes?.Keys.ToArray();
		return characters != null && characters.Length > 0;
	}
}

//working
//public int LadderLength(string beginWord, string endWord, IList<string> wordList) 
//{
//	if(wordList == null || wordList.Count <= 1) return 0;
//	
//	var set = new HashSet<string>();
//	var root = new TrieNode('\0');
//	foreach(var word in wordList)
//	{
//		BuildTrieNode(root, word);
//		set.Add(word);
//	}
//	
//	var queue = new Queue<Tuple<int,string>>();
//	queue.Enqueue(new Tuple<int, string>(1, beginWord));
//	
//	var visited = new HashSet<string>();
//	while(queue.Count > 0)
//	{
//		var item = queue.Dequeue();
//		var length = item.Item1;
//		var newLength = length + 1;
//		var curWord = item.Item2;
//		
//		if(visited.Contains(curWord)) continue;
//		
//		visited.Add(curWord);
//		
//		for(int i = 0; i < curWord.Length; i++)
//		{
//			var nodes = GetNodesAt(root, curWord, i);	
//			if(nodes != null)
//			{
//				foreach(var ch in nodes)
//				{
//					if(ch != curWord[i])
//					{
//						var characters = curWord.ToCharArray();
//						characters[i] = ch;
//						var newWord = new string(characters);
//						if(!visited.Contains(newWord) && set.Contains(newWord))	
//						{
//							if(newWord == endWord)
//								return length + 1;
//							else
//								queue.Enqueue(new Tuple<int, string>(newLength, newWord));
//						}
//					}
//				}
//			}
//		}
//	}
//	
//	return 0;        
//}
//
//private char[] GetNodesAt(TrieNode node, string word, int index)
//{
//	var i = 0;
//	var nodes = node.Nodes;
//
//	while(i != index)
//	{
//		var ch = word[i++];
//		TrieNode childNode;
//		if(!nodes.TryGetValue(ch, out childNode)) return null;
//		nodes = childNode.Nodes;
//	}
//	
//	return nodes?.Keys.ToArray();
//}
//
//private void BuildTrieNode(TrieNode node, string word)
//{
//	foreach(var ch in word)
//	{
//		TrieNode childNode;
//		if(!node.Nodes.TryGetValue(ch, out childNode))
//		{
//			childNode = new TrieNode(ch);
//			node.Nodes.Add(ch, childNode);
//		}
//		node = childNode;
//	}
//	node.IsWord = true;
//}
//
//public class TrieNode
//{
//	public char Val;
//	public bool IsWord;
//	public Dictionary<char, TrieNode> Nodes;
//	
//	public TrieNode(char ch)
//	{
//		this.Val = ch;
//		this.Nodes = new Dictionary<char, TrieNode>();
//	}
//}
//
//