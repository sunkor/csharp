<Query Kind="Program" />

/*
Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, determine if s can be segmented into a space-separated sequence of one or more dictionary words. 
You may assume the dictionary does not contain duplicate words.

For example, given
s = "leetcode",
dict = ["leet", "code"].

Return true because "leetcode" can be segmented as "leet code".

Sunil:
Since words are unique in the wordDict, we can build a HashSet of words for O(1) search.

Brute force: For every word, loop the wordDict to combine with every other word and check if it matches the word.
The running time in this case would be O(n!), where n is the number of words in the diction.

Approach 1:
Looking at the problem a Trie can seen a natural candidate to identify words starting with a certain letter and checking if it is a substring.
If we consider a Trie, the space complexity for the worDict is O(n*k), where n is the number of words and k is the avg. length of each word.
The search / access time to find a word is O(n).
The advantage of Trie is it will give us all words starting from a prefix. We can them use all word combinations with the prefix to see which ones match.
Those that match can be held in a working list / queue. We can use a BFS approach to build on the list. The first candidate to match word wins.
If the queue goes empty, we return false.
Running time: 
To find all prefix words for a letter, we need O(n) time, where n is the length of the longest prefix.
Add these to a working list. Number of words is would decrease over time, and constant at at search for words. So we ignore this running time.
For each word in the list (given its not already visited), we search for words with the next letter prefix and continue doing so till no words are found. 
Worst case all words in dictionary are single character (letters).
The running time is still linear at - O(n)

Approach 2 - simpliefied:
We maintain a bool array (arr) representing the index of each letter in the word.
At any index, we maintain a bool flag value. If true, a word or combination of words has been found from the start to the index.

Loop every character (i) of the word from the beginning,
	For every word, loop (j) from left to right till end of word
		For each loop build a string (i to j) and check if it is a word in the dictionary
		If it is & there is a word in arr[i - 1], then we record till arr[j] as true, because we know arr[i - 1] + arr[j] is a word.

if arr[word.Length] returns true, we know the is a combination of words in the dictionary that forms the word.
Running time: 
For outer loop its O(n)
For inner loop its O(n - 1)
Hence, running time is O(n ^ 2)

Edge cases: 
dictionary is empty.
string is null or empty
*/

void Main()
{
	IList<string> words = null;
	//words = new List<string>(){"leet","code"};
	//WordBreak("leetcode", words).Dump();
	
	//words = new List<string>(){"a", "abc", "cd", "b"};
	WordBreak("abcd", words).Dump();
}

public bool WordBreak(string s, IList<string> wordDict) 
{
	if(wordDict?.Count < 1 || string.IsNullOrWhiteSpace(s)) return false;
		
	//Create dictionary for O(1) constant time.
	var set = new HashSet<string>();
	foreach(var word in wordDict) set.Add(word);
	
	var letterIndex = new bool[s.Length + 1];
	letterIndex[0] = true;
	
	for(int i = 0; i < s.Length; i++)
	{
		if(!letterIndex[i]) continue;
		
		for(int j = i; j < s.Length; j++)
		{
			var subWord = s.Substring(i, j - i + 1);
			if(set.Contains(subWord)) letterIndex[j + 1] = true;
		}
	}
	
	return letterIndex[s.Length];
}

//public class TrieNode
//{
//	public readonly char Letter;
//	public bool IsWord;
//	public readonly Dictionary<char, TrieNode> Nodes;
//	
//}