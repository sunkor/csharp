<Query Kind="Program" />

void Main()
{
	var arr = new[] { 3, 5, 1, 9, 6, 0, 8, 11 };
	Console.WriteLine(string.Join(",", arr));

	//var sortedArr = InsertionSort(arr);
	//var sortedArr = SelectionSort(arr);
	var sortedArr = BubbleSort(arr);
	Console.WriteLine(string.Join(",", sortedArr));
}

//Comparison Sort
//Best case: O(N)
int[] BubbleSort(int[] arr)
{
	if (arr?.Length > 1)
	{
		for (int i = 0; i < arr.Length - 1; i++)
		{
			for (int j = i + 1; j < arr.Length; j++)
			{
				if (arr[i] > arr[j])
				{
					var tmp = arr[i];
					arr[i] = arr[j];
					arr[j] = tmp;
				}
			}
		}
	}
	return arr;
}

int[] SelectionSort(int[] arr)
{
	if (arr?.Length > 1)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			var numberToCompare = arr[i];
			var smallesNumberIndex = i;
			for (int j = i + 1; j < arr.Length; j++)
			{
				if(arr[j] < arr[smallesNumberIndex])
					smallesNumberIndex = j;
			}
			if (i != smallesNumberIndex)
			{
				var tmp = arr[smallesNumberIndex];
				arr[smallesNumberIndex] = arr[i];
				arr[i] = tmp;
			}
		}
	}
	return arr;
}

int [] InsertionSort(int [] arr)
{
	if (arr?.Length > 1)
	{
		for (var i = 1; i < arr.Length; i++)
		{
			var compareToIndex = i - 1;
			var j = i;
			while (compareToIndex >= 0 && arr[compareToIndex] > arr[j])
			{
				var tmp = arr[compareToIndex];
				arr[compareToIndex] = arr[j];
				arr[j] = tmp;
				j--;
				compareToIndex--;
			}
		}
	}
	return arr;	
}