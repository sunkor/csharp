<Query Kind="Program" />

void Main()
{
	int [] nums;
	
	//nums = new [] {23, 2, 4, 6, 7};
	//CheckSubarraySum(nums,6).Dump();
	
	//nums = new [] {0,1,0};
	//CheckSubarraySum(nums,0).Dump();
	
	//nums = new [] {0,0};
	//CheckSubarraySum(nums,0).Dump();
	
	nums = new [] {0,0,0,0,0,0};
	CheckSubarraySum(nums,1).Dump();
}

 public bool CheckSubarraySum(int[] nums, int k) 
 {
 	if(nums == null || nums.Length < 2) return false;
	
	var length = nums.Length;
	
	//Build summation
	var summation = new int[nums.Length];
	summation[0] = nums[0];
	for(int i = 1; i < length; i++)
		summation[i] = nums[i] + summation[i - 1];
		
	if(summation[length - 1] == 0) return true;
		
	for(int i = length - 1; i > 0; i--)
	{
		var current = summation[i];		
		for(int j = 0; j < i; j++)
		{
			var prev = 0;
			if((j - 1) >= 0) prev = summation[j - 1];
			var distance = current - prev;
			if((k == 0 && distance == 0) || (k != 0 && distance % k == 0)) return true;
			
		}
	}
			
	return false;
 }
