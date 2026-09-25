<Query Kind="Program" />

void Main()
{
	var nums = new []{0,1};	
	//var nums = new []{0,1,0};	
	//var nums = new []{0};	
	//var nums = new []{0,1,1,1,0,0,1};	
	//var nums = new [] {0,1,1};
	//var nums = new [] {0,0,1,0,0,0,1,1};
	FindMaxLength(nums).Dump();
}

public int FindMaxLength(int[] nums) 
{
	if(nums == null || nums.Length == 0) return 0;
	
	for(var i = 0; i < nums.Length; i++)
	{
		if(nums[i] == 0) nums[i] = -1;
	}
	
	var sum = 0;
	var max = 0;
	var sumToIndex = new Dictionary<int,int>();
	sumToIndex.Add(0,-1);
	for(int i = 0; i < nums.Length; i++)
	{
		sum += nums[i];
		if(sumToIndex.ContainsKey(sum))
			max = Math.Max(max, i - sumToIndex[sum]);
		else
			sumToIndex.Add(sum, i);
	}
	return max;
}
