<Query Kind="Program" />

void Main()
{
	FirstMissingPositive(new[] {1,2,0}).Dump(); //3
	//FirstMissingPositive(new[] {3,4,-1,1}).Dump(); //2
}

public int FirstMissingPositive(int[] nums) 
{
	if(nums == null || nums.Length == 0) return 1;
	
	Array.Sort(nums);
	
	//Remove negatives from consideration.
	var index = -1;
	while(++index < nums.Length && nums[index] < 0);
	if(index == nums.Length) return 1;
	
	var firstWholeNumIndex = index;
	if(nums[firstWholeNumIndex] > 1) return 1;
	
	//Loop till we find distance between two whole numbers is greater than 1. We have found our missing positive.
	while((firstWholeNumIndex + 1) < nums.Length && nums[firstWholeNumIndex + 1] - nums[firstWholeNumIndex] <= 1) firstWholeNumIndex++;
		
    return nums[firstWholeNumIndex] + 1;
}
