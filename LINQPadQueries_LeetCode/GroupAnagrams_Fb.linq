<Query Kind="Program" />

void Main()
{
	var strings = new [] {"eat", "tea", "tan", "ate", "nat", "bat"};
	GroupAnagrams(strings).Dump();
}

public IList<IList<string>> GroupAnagrams(string[] strs) 
{
	if(strs == null || strs.Length == 0) return null;
	
	var dict = new Dictionary<string,IList<string>>();
	
	foreach(var str in strs)
	{
		var chars = str.ToCharArray();
		Array.Sort(chars);
		var word = new string(chars);
		
		IList<string> list = null;
		if(!dict.TryGetValue(word, out list))
		{
			list = new List<string>();
			dict.Add(word, list);
		}
		
		list.Add(str);
	}
		
	return new List<IList<string>>(dict.Values);
}

//private bool IsAnagram(Dictionary<string, short[]> wordCharacterMap, short [] letters, string word)
//{
//	var letters2 = BuildLetterMap(wordCharacterMap, word);
//	
//	var i = 26;
//	while(--i >= 0)
//	{
//		if(letters[i] != letters2[i]) return false;
//	}
//	
//	return true;
//}
//
//private short[] BuildLetterMap(Dictionary<string, short[]> wordCharacterMap, string word)
//{	
//	short[] letters = null;
//	if(wordCharacterMap.TryGetValue(word, out letters)) return letters;
//		
//		letters = new short[26];
//		foreach(var ch in word)
//		{
//			int ascii = (short)ch;
//			if(ascii >= 65 && ascii <= 90)
//				letters[ascii - 65]++;
//			else if(ascii >= 97 && ascii <= 122)
//				letters[ascii - 97]++;
//		}
//		wordCharacterMap.Add(word, letters);
//		
//	return letters;
//}