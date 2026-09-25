<Query Kind="Program" />

/*
Given an array S of n integers, are there elements a, b, c in S such that a + b + c = 0 ? 
Find all unique triplets in the array which gives the sum of zero.

For example, given array S = [-1, 0, 1, 2, -1, -4],
A solution set is:
[
  [-1, 0, 1],
  [-1, -1, 2]
]
*/

/*
For a sum of N, we need to use unique triplets. 

Brute force: 
	Iterate 3 times over, to find the required sum. Maintain list of used indexes (in a HashSet).
	Best case: O(n ^ 3). 


Optimal running time: O(n)

Are we allowed to sort the array ? If yes, then we sort
Ex array: [-1, 0, 1, 2, -1, -4]
Sorted: [-1, -1, -1, 0, 1, 2, 4]

Approach:
	We start from left to right for first element (a). To identify element b & c, we pick elements to the right of a.
	The problem with this approach is that if the numbers are large negatives, it will take a while to reach zero.
	
Approach:
	And ideal approach is for element b to start from the right of a, and element c to start from the end. 
	Loop till indexof(b) < indexof(c)

How do we increment the indexes for b & c ? 
	Ideally we pick either b or c for index increment. We should do this based on the value of sum.
		if sum < 0, we know that to reach closer to zero, we increment b (do this till value at indexof(b) does not match the prev value).
		if sum > 0, we know that to reach closer to zero, we decrement c (do this till value at indexof(c) does not match the prev value).

If we find a sum, we keep continue to increment or decrement till indexof(b) < indexof(c)
TODO: Optimize to find that we should stop looping if b & c are both positive or negative and we cannot reach 0.

Move indexof(a) from left to right. How do we find out the elements have already been used?
Move indexof(a) till it does not match the prev value. If new indexof(a) > 0, break;

Edge cases:
if array is empty or length < 3, we return null.
*/
void Main()
{
	var nums = new [] {-1, 0, 1, 2, -1, -4};
	ThreeSum(nums).Dump();
}

public IList<IList<int>> ThreeSum(int[] nums) 
{
	IList<IList<int>> set = new List<IList<int>>();
	if(nums == null || nums.Length < 3) return set;
	
	Array.Sort(nums);
		
	var length = nums.Length;
	for(int i = 0; i < length - 2; i++)
	{
		var a = nums[i];
		if(a > 0)
			break;
			
		var j = i + 1;
		var k = length - 1;
		while(j < k)
		{
			var b = nums[j];
			var c = nums[k];
			
			var sum = a + b + c;
			if(sum == 0)
				set.Add(new List<int>(){a,b,c});
			
			if(sum <= 0)
				while(++j < k && nums[j] == b);
			
			if(sum >= 0)
				while(j < --k && nums[k] == c);
		}
		
		//Keep looping till next value of a is not equal to the current value
		while((i + 1) < length - 2 && nums[i + 1] == a) i++;
	}
	
	return set;
}