<Query Kind="Program" />

void Main()
{
	var nums = new [] {5, 7, 7, 8, 8, 10};
	//var nums = new int[1]{0};
	var range = SearchRange(nums, 8);
	range.Dump();
}

 public int[] SearchRange(int[] nums, int target) {
 		if(nums == null || nums.Length == 0) return new[]{-1,-1};
 
  		var index = FindIndex(nums,target,1,nums.Length) - 1;
        //return new[]{index,index};
		
		if(index >= 0 && index < nums.Length)
		{
			var lowerRange = index;
			while((lowerRange - 1) >= 0 && nums[lowerRange - 1] == target)
				lowerRange--;	
				
			var upperRange = index;
			while((upperRange + 1) < nums.Length && nums[upperRange + 1] == target)
				upperRange++;	
				
			return new[]{lowerRange,upperRange};			
		}
		else
			return new[]{-1,-1};
    }
	
public int FindIndex(int[] nums, int target, int start, int end)
{
	var index = -1;
	if(start > end) return index;
	
	int midPoint = (start + end) / 2;
	int midPointVal = nums[midPoint - 1];
	if(midPointVal == target) index = midPoint;
	else if(midPointVal > target)
		index = FindIndex(nums, target, start, midPoint - 1);
	else if(midPointVal < target)
		index = FindIndex(nums, target, midPoint + 1, end);
	return index;
}
