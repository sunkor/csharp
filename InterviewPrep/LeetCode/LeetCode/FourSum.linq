<Query Kind="Program" />

void Main()
{
	//{-2,-1,0,0,1,2}
	//FourSum(new []{1, 0, -1, 0, -2, 2}, 0).Dump();

	//{-3,-2,-1,0,0,1,2,3}
	//FourSum(new[] { -3,-2,-1,0,0,1,2,3 }, 0).Dump();

	//{-2,-1,0,0,1,2}
	//FourSum(new []{5,5,3,5,1,-5,1,-2}, 4).Dump();
	
	//FourSum(new[] { 0,0,0,0 }, 0).Dump();
	
	//{-2,-1,0,0,1,2}
	FourSum(new[] { -3,-2,-1,0,0,1,2,3  }, 0).Dump();
}

public IList<IList<int>> FourSum(int[] nums, int target)
{
	IList<IList<int>> combos = new List<IList<int>>();
	
	if (nums == null || nums.Length < 4) return combos;
	
	Array.Sort(nums);
		
	var set = new HashSet<string>();
	int? prevSum1 = null;
	for(int i = 0; i < nums.Length - 3; i++)
	{
		if(prevSum1.HasValue && prevSum1 == nums[i])
			continue;
		else
			prevSum1 = nums[i];
		
		int? prevSum2 = null;
		for(int j = i + 1; j < nums.Length - 2; j++)
		{
			if (prevSum2.HasValue && prevSum2 == nums[j])
				continue;
			else
				prevSum2 = nums[j];
				
			var left = j + 1;
			var right = nums.Length - 1;
			
			int? prevLeft = null;
			int? prevRight = null;
			while(left < right)
			{
				if (prevLeft.HasValue && prevRight.HasValue && prevLeft == nums[left] && prevRight == right)
				{
					left++;
					right--;
					
					continue;
				}
				else
				{
					prevLeft = nums[left];
					prevRight = nums[right];
				}
				
				var sum = nums[i] + nums[j] + nums[left] + nums[right];
				if(sum == target)
				{
					var num = $"{nums[i]}_{nums[j]}_{nums[left]}_{nums[right]}";
					if (!set.Contains(num))
					{
						var list = new List<int>();
						list.Add(nums[i]);
						list.Add(nums[j]);
						list.Add(nums[left]);
						list.Add(nums[right]);
						combos.Add(list);
						set.Add(num);
					}
					
					left++;
					right--;
				}
				else if(sum < target)
				{
					left++;
				}
				else if(sum > target)
				{
					right--;
				}
			}
		}
	}
	
	return combos;
}