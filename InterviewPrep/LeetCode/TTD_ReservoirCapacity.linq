<Query Kind="Program" />

void Main()
{
	
}

public int GetReservoirCapacity(int [] terrain)
{
	//We should have a minimum of 3 terrain data.
	if(terrain?.Length <= 2) return 0;
		
	var rCapacity = 0;
		
	//Iterate the terrain heights
	for(var pivot = 0; pivot < terrain.Length; pivot++)
	{
		//Find the left index
		var waterCapacity = 0;
		var left = GetLeftWallFromPivot(terrain, pivot);
		if(left != -1)
		{
			var right = GetRightWallFromPivot(terrain, pivot + 1);
			
			if(right != -1)
			{
				//Find the min(left, right)
				var min = Math.Min(left, right);
				
				//Calculate the amount of water
				while(left < right)
					waterCapacity += min - terrain[++left];
			}
		}
		rCapacity += waterCapacity;
	}
	
	return rCapacity;
}

//If we find the next terrain to be of less height, we have a candiate
private int GetLeftWallFromPivot(int [] terrain, int currIdx)
{	
	if(currIdx >= terrain.Length) return -1;
	
	var left = currIdx - 1;
	return left >= 0 && terrain[left] < terrain[currIdx] ? currIdx : -1;
}

private int GetRightWallFromPivot(int [] terrain, int currIdx)
{	
	if(currIdx >= terrain.Length - 1) return -1;
	
	var rightIdx = currIdx + 1;
	while(rightIdx < terrain.Length && terrain[currIdx] >= terrain[rightIdx++]);
	
	return rightIdx < terrain.Length ? rightIdx : -1;
}
