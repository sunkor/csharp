<Query Kind="Program" />

void Main()
{
	var nums = new [] {1, -1, 5, -2, 3};
	//var nums = new [] {0,0};
	//var nums = new [] {-1, 1};
	//var nums = new [] {-2,1,-3,4,-1,2,1,-5,4};
	nums.Dump();
	
	var k = 3;
	MaxSubArrayLen(nums,k).Dump();
}

public int MaxSubArrayLen(int[] nums, int k) {
	if(nums?.Length == 0) return 0;
	if(nums.Length == 1 && nums[0] == k) return 1;
	
    int sum = 0, max = 0;
    var map = new Dictionary<int, int>();
    for (int i = 0; i < nums.Length; i++) {
        sum = sum + nums[i];
        if (sum == k) max = i + 1;
        else if (map.ContainsKey(sum - k)) max = Math.Max(max, i - map[sum - k]);
        if (!map.ContainsKey(sum)) map.Add(sum, i);
    }
    return max;
}

public int MaxSubArrayLenEx(int[] nums, int k) 
{
	if(nums?.Length == 0) return 0;

	var	maxLength = 0;
	var length = nums.Length;
	for(var i = 0; i < length; i++)
	{
		var sum = nums[i];
		
		for(var j = i + 1; j < length; j++)
		{
			sum += nums[j];
			
			if(sum == k)
			{
				var subArrLength = j - i + 1;	
				maxLength = maxLength < subArrLength ? subArrLength : maxLength;
			}
		}
		
		if(maxLength == 0 && nums[i] == k) maxLength = 1;
	}
	
    return maxLength;
}
