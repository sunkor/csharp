<Query Kind="Program" />

void Main()
{
	var arr = new []{0, 1, 0, 3, 12};
	//var arr = new [] {2,1};
	arr.Dump();
	MoveZeroes(arr);
	arr.Dump();
}

public void MoveZeroes(int[] nums) 
{
	//Edge case
	if(nums?.Length <= 1) return;
	
	int insertPos = 0;
    foreach(int num in nums) {
        if (num != 0) nums[insertPos++] = num;
    }        

    while (insertPos < nums.Length) {
        nums[insertPos++] = 0;
    }
}

public void MoveZeroes_SunilSln(int[] nums) 
{
	//Edge case
	if(nums?.Length <= 1) return;
	
	var length = nums.Length;
		
	//Find the first zero index
	int zeroIndex = -1;
	int idx = 0;
	while(zeroIndex == -1 && idx < length)
	{
		if(nums[idx] == 0)
			zeroIndex = idx;
		idx++;
	}
	
	//Move zeros
	if(zeroIndex > -1)
	{
		for(int i = zeroIndex + 1; i < length; i++)
		{
			//if(nums[i] == 0) continue;
			
			if(nums[i] == 0)
			{
				do
				{
					i++;
				}while(i < length && nums[i] == 0);
				if(i == length) break;
			}
			
			//Check for the first non-zero integer & swap
			var tmp = nums[zeroIndex];
			nums[zeroIndex] = nums[i];
			nums[i] = tmp;
			zeroIndex++;
		}
	}
}