<Query Kind="Program" />

/*
Listen carefully.

Ask questions:
	Can we change the board ?
	Can board contain letters other than a-z?
	Is the search case sensitive ? 

Tracking visited nodes.

Identify the start letter nodes
For each node, travel top, right, left, bottom to see if nodes match. If they do, enqueue them to the current node. Keep the length of the string so far.
	If at a node, the lengths equals the word length and does not match, return.

Use a DFS approach to search.
Build a TreeNode.

[
  ['A','B','C','E'],
  ['S','F','C','S'],
  ['A','D','E','E']
]
So let's search "SEE"

[
  ['S','S','S','S'],
  ['S','S','S','S'],
  ['S','S','S','S']
]
search "SSSSSS"

Find all S nodes,
List of 2 S nodes

For each node, contain the length and the letter. If the length matches the string and the node letter matches the word's last letter, we have found the wordk.

class TreeNode
	char 			Letter
	int  			Length
	List<TreeNode>  AdjNodes;

How to prevent cyclic traversal ? 
	We want to maintain a list of visited nodes for each parent.
	Keep a Dictionary<string,HashSet<string>>
	The Dictionary key is the cell location. The value is the set of visited cells.

Running time:
1st pass - identify parents - O(n) - total number of cells.
For each node, we may have 4 possible nodes. Max num of linkages if len(word). Assuming length "l", 
At first pass, we have 4 nodes, length 2
At second pass, 16 nodes, lenght 3
At third pass, 64 nodes, length 4
At fourth pass, 256 nodes, length 5
We grow by a factor of 4 at each pass.
O((4 ^ l)) ???
*/

void Main()
{
	var sw = new Stopwatch();
	sw.Start();
	
//	var board = new [,] {{'A','B','C','E'},
//						  {'S','F','C','S'},
//						  {'A','D','E','E'}};
//	
//	var word = "ABCB";
//	
	var board = new [,] {{'A','B','C','E'},
						  {'S','F','E','S'},
						  {'A','D','E','E'}};
	
	var word = "ABCB";
	var wordExists = Exist(board, word);
	
	sw.Stop();
	sw.ElapsedTicks.Dump();
	$"Word - '{word}' exists in board ? '{wordExists}'".Dump();
	
	board.Dump();
}

public bool Exist(char[,] board, string word) 
{
	if(board == null || board.Length == 0 || word?.Length == 0 || word.Length > board.Length) return false;
		
	var rowLength = board.GetUpperBound(0);
	var colLength = board.GetUpperBound(1);
	
	//DFS parent nodes.
	for(int i = 0; i < board.GetLength(0); i++)
		for(int j = 0; j < board.GetLength(1); j++)
		{
			if(board[i,j] == word[0])
				if(DoesWordExists(board, rowLength, colLength, i, j, 1, word)) return true;
		}
		
	return false;        
}

private bool DoesWordExists(char[,] board, int rowLength, int colLength, int row, int col, int length, string word)
{		
	if(row < 0 || col < 0 || row > rowLength || col > colLength || word.Length < length) return false;
	if(word[length - 1] != board[row,col]) return false;
	if(word.Length == length) return true;
		
	var character = board[row, col];
	
	board[row, col] = '.';
	
	var exists = DoesWordExists(board, rowLength, colLength, row - 1, col, length + 1, word) || 
				 DoesWordExists(board, rowLength, colLength, row + 1, col, length + 1, word) ||
				 DoesWordExists(board, rowLength, colLength, row, col - 1, length + 1, word) ||
				 DoesWordExists(board, rowLength, colLength, row, col + 1, length + 1, word);
	
	board[row, col] = character;
	
	return exists;
}