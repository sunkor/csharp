<Query Kind="Program" />

void Main()
{
	var board = new char[,]{{'1','2','3','4'},
							{'5','6','7','8'},
							{'9','1','2','3'},
							{'4','5','6','7'}};
	IsValidSudoku(board).Dump();
}

//BCR - Sqrt(i * j)
//Average - i * j
public bool IsValidSudoku(char[,] board) 
{
	if(board == null | board.GetLength(0) == 0) return false;
	
	var length = board.GetLength(0);
	
	var totalSquares = (int)Math.Sqrt(length);
	var set = new HashSet<int>();	
	
	for(int row = 0; row < totalSquares; row++)
	{
		for(int col = 0; col < totalSquares; col++)	
		{
			if(!IsValidSudokuSquare(set, board, row * totalSquares, col * totalSquares, totalSquares)) return false;
			set.Clear();
		}
	}
	
	//Check each horizontal line
	for(var row = 1; row <= length; row++)
	{
		set.Clear();
		for(var col = 1; col <= length; col++)
		{
			var val = board[row - 1, col - 1];
			if(val == '.')
				continue;
			else if(set.Contains(val)) 
				return false;
			set.Add(val);
		}
	}
	
	//Check each vertical line
	for(var col = 1; col <= length; col++)
	{
		set.Clear();
		for(var row = 1; row <= length; row++)
		{
			var val = board[row - 1, col - 1];
			if(val == '.')
				continue;
			else if(set.Contains(val)) 
				return false;
			set.Add(val);
		}
	}
	
	return true;
}

public bool IsValidSudokuSquare(HashSet<int> set, char[,] board, int i , int j, int size)
{
	for(var row = i; row < i + size; row++)
	{
		for(var col = j; col < j + size; col++)
		{
			var val = board[row, col];
			if(val == '.')
				continue;
			else if(set.Contains(val)) 
				return false;
			set.Add(val);
		}
	}
	return true;
}
