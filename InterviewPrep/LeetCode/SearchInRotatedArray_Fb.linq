<Query Kind="Program" />

void Main()
{
	//var nums = new [] {2, 4, 5, 6, 7, 8, 9, 10, 0, 1};
	//Search(nums, 2).Dump();
	
	//var nums = new [] {4,5,6,7,8,9,1,2,3};
	//FindPivot(nums).Dump();
	//Search(nums, 1).Dump();
	
	//var nums = new [] {1, 3, 5};
	//Search(nums, 2).Dump();
	
	//var nums = new [] {3, 1};
	//Search(nums, 0).Dump();
	
	//var nums = new [] {5,1,3};
	//Search(nums, 1).Dump();
	
	//var nums = new [] {3, 1};
	//Search(nums, 0).Dump();
	//var nums = new [] {6,7,1,2,3,4,5};
	//Search(nums, 6).Dump();
}

public int Search(int[] nums, int target) 
{
	if(nums == null || nums.Length == 0) return -1;
	
	var pivot = FindPivot(nums);
	
	int lo = 0, hi = 0;
	if(nums[pivot] == target) return pivot;
	else if(target >= nums[pivot] && target <= nums[nums.Length - 1])
	{
		lo = pivot + 1;
		hi = nums.Length - 1;
	}
	else
	{
		lo = 0;
		hi = pivot - 1;
	}
	
	while(lo <= hi)
	{
		var mid = lo + ((hi - lo) + 1) / 2;
		if(nums[mid] == target) return mid;
		else if(nums[mid] < target) lo = mid + 1;
		else hi = mid - 1;
	}
	
	return -1;
}

private int FindPivot(int[] nums) 
{
	int low = 0, hi = nums.Length - 1;
	var lastVal = nums[hi];
	while(low <= hi && nums[low] > lastVal)
	{
		var mid = low + ((hi - low) + 1) / 2;
		
		if(nums[mid] > lastVal) low = mid + 1;
		else hi = mid - 1;
	}
			
	return low;
}