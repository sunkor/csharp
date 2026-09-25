<Query Kind="Program" />

void Main()
{
	//TwoSum(new []{2, 7, 11, 15}, 9).Dump();
	TwoSum(new []{3,2,4}, 6).Dump();
}

public int[] TwoSum(int[] nums, int target)
{
	if (nums == null || nums.Length < 2) return null;
	
	var sumOfTwo = new Dictionary<int, int>();
	for(int i = 0; i < nums.Length; i++)
	{
		var num = nums[i];
		if (sumOfTwo.ContainsKey(num))
		{
			return new[] { sumOfTwo[num], i };
		}
		else
		{
			var remaining = target - num;
			if(!sumOfTwo.ContainsKey(remaining))
				sumOfTwo.Add(remaining, i);
		}
	}

	return null;
}