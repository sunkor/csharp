<Query Kind="Program" />

void Main()
{
	
}

public int[] GetCombination()
{
	var combination = new List<int>();
	
	//Get the colors which are part of the set and also not part of the set.
	var set = FindColorSet();
	
	//Find one color not part of the color set
	var notPartOfSet = set[1].First();

	//Find one column at a time.
	for(var idx = 0; idx <= 3; idx++)
	{
		var colorAtCol = -1;
		//for 1st column
		//TODO: reduce the soln set at each column.
		//foreach(var color in set[0])
		for(var j = 0; j < set[0].Count - 1; j++)
		{	
			var color = set[0][j];
			
			var testSet = new [] {notPartOfSet, notPartOfSet, notPartOfSet, notPartOfSet};	
			testSet[idx] = color;
			
			var result = Guess(testSet);
			if(result.numRed == 1)
			{
				colorAtCol = color;
				break;
			}
			else
				continue;
		}
		
		if(colorAtCol == -1) 
			colorAtCol =  set[0][set.Count - 1];
		
		//Reduce soln set.
		combination.Add(colorAtCol);
		set[0].Remove(colorAtCol);
	}
	
	
		
	return combination.ToArray();
}

private List<List<int>> FindColorSet()
{
	var set = new List<int>();
	var notPartOfSet = new List<int>();
	
	//5 guesses - each color
	var colors = new [] {1,2,3,4,5};
	foreach(var color in colors)
	{
		//TODO: Do this in 5 guesses
		var result = Guess(new [] {color, color, color, color});
		if(result.numRed > 0) 
			set.Add(color);
		//else 
			//notPartOfSet.Add(color);
	}
	
	return new List<List<int>>(){set, notPartOfSet};
}

public class Result {
  public int numRed;
  public int numWhite;
}

public Result Guess(int[] colors)
{
return null;
}
