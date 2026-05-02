<Query Kind="Program" />

void Main()
{
	
}

 public IList<IList<int>> ThreeSum(int[] nums) 
 {
 	if(nums == null || nums.Length < 3) return null;
	
 	IList<IList<int>> list = new List<IList<int>>();
	
	var i = 0;
	while(i < nums.Length - 2)
	{
		if(nums[i] > 0) break;
		
		var j = i + 1;
		var k = nums.Length - 1;
		
		while(j < k)
		{
			var sum = nums[i] + nums[j] + nums[k];
			if(sum == 0) list.Add(new List<int>(){nums[i], nums[j], nums[k]});
			if(sum <= 0) while(nums[j] == nums[++j] && j < k);
			if(sum >= 0) while(nums[k--] == nums[k] && j < k);
		}
		
		//If repeating numbers, skip - because we already discovered the set.
		while(nums[i] == nums[++i] && i < nums.Length - 2);
	}
	
	return list;
 }


//Working
   
//    public IList<IList<int>> ThreeSum(int[] nums) {
//        IList<IList<int>> result = new List<IList<int>>();
//	if (nums == null || nums.Length < 3) return result;
//	Array.Sort(nums);
//	int i = 0;
//	while (i < nums.Length - 2)
//	{
//		if (nums[i] > 0) break;
//		int j = i + 1;
//		int k = nums.Length - 1;
//		while (j < k)
//		{
//			int sum = nums[i] + nums[j] + nums[k];
//			if (sum == 0) result.Add(new List<int>() { nums[i], nums[j], nums[k]});
//			if (sum <= 0) while (nums[j] == nums[++j] && j < k) ;
//			if (sum >= 0) while (nums[k--] == nums[k] && j < k) ;
//		}
//		while (nums[i] == nums[++i] && i < nums.Length - 2) ;
//	}
//	return result;
//    }