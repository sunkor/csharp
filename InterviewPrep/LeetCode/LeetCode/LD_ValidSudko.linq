<Query Kind="Program" />

void Main()
{
	var board = new char[][] {
		new char []{ '5', '3', '.', '.', '7', '.', '.', '.', '.' },
		new char []{ '6', '.', '.', '1', '9', '5', '.', '.', '.'},
		new char []{'.','9','8','.','.','.','.','6','.'},
		new char []{'8','.','.','.','6','.','.','.','3'},
		new char []{'4','.','.','8','.','3','.','.','1'},
		new char []{'7','.','.','.','2','.','.','.','6'},
		new char []{'.','6','.','.','.','.','2','8','.'},
		new char []{'.','.','.','4','1','9','.','.','5'},
		new char []{'.','.','.','.','8','.','.','7','9'}
		};
		
		new Solution().IsValidSudoku(board).Dump();
}

public class Solution
{

	//Run time: O(N). We run N time for each validator - so 3*N, hence O(N).
	public bool IsValidSudoku(char[][] board)
	{
		if(board == null || board.Length < 9) return false;
		return IsColumnValid(board) && IsRowValid(board) && IsThreeByThreeBoxValid(board);
	}
	
	private bool IsColumnValid(char[][] board)
	{
		var set = new HashSet<char>();
		for (var col = 0; col < 9; col++)
		{
			set.Clear();
			for (var row = 0; row < 9; row++)
			{
				var ch = board[row][col];
				if(ch == '.') continue;
				if(set.Contains(ch)) return false;
				set.Add(ch);
			}
		}
		return true;
	}
	
	private bool IsRowValid(char[][] board)
	{
		var set = new HashSet<char>();
		for (var row = 0; row < 9; row++)
		{
			set.Clear();
			for (var col = 0; col < 9; col++)
			{
				var ch = board[row][col];
				if (ch == '.') continue;
				if (set.Contains(ch)) return false;
				set.Add(ch);
			}
		}
		return true;
	}

	//Walk left to right, top to bottom.
	private bool IsThreeByThreeBoxValid(char[][] board)
	{
		var set = new HashSet<char>();
		for(var box = 0; box < 9; box++)
		{
			var colOffset = (box%3) * 3; //Offsets: 0, 3, 6, 0, 3, 6, 0, 3, 6
			var rowOffset = ((int)(box/3)) * 3; //Offsets: 0, 0, 0, 3, 3, 3, 6, 6, 6
			
			set.Clear();
			for(var row = 0; row < 3; row++)
			{
				var rowNum = row + rowOffset;
				for(var col = 0; col < 3; col++)
				{
					var colNum = col + colOffset;
					
					var ch = board[rowNum][colNum];
					if (ch == '.') continue;
					if (set.Contains(ch)) return false;
					set.Add(ch);
				}
			}
		}
		return true;
	}
}