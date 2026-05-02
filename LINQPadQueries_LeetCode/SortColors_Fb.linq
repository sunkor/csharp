<Query Kind="Program" />

void Main()
{
	var nums = new[]{4,5,1,3,8,9};
	Console.WriteLine(string.Join(",",nums));
	
	SortColors(nums);
	Console.WriteLine(string.Join(",",nums));
}

public void SortColors(int[] nums) 
{
    if(nums == null || nums.Length == 0) return;
	Quicksort(nums, 0, nums.Length - 1);
}
	
public static void Quicksort(int[] elements, int left, int right)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];
 
            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }
 
                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }
 
                if (i <= j)
                {
                    // Swap
                    int tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;
 
                    i++;
                    j--;
                }
            }
 
            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }
 
            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }