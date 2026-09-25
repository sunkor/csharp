<Query Kind="Program" />

void Main()
{
	int [] num;
	//Insertion
//	var num = new [] { 15, 3, 8, 7, 2, 5 };
//	num.Dump();
//	Sort(SortingAlgo.Insertion, num);
//	num.Dump();

	//Selection - find min element index, swap if required.
//	num = new [] { 15, 3, 8, 7, 2, 5 };
//	num.Dump();
//	Sort(SortingAlgo.Selection, num);
//	num.Dump();

	//Bubble - compare adjacent elements. swap if required. 
	num = new[] { 15, 3, 8, 7, 2, 5 };
	num.Dump();
	Sort(SortingAlgo.Bubble, num);
	num.Dump();
}

enum SortingAlgo
{
	Insertion = 0,
	Bubble = 1,
	Selection = 2
}

void Sort(SortingAlgo sortingAlgoType, int [] arr)
{
	if(arr.Length <= 1) return;
	
	if(sortingAlgoType == SortingAlgo.Insertion)
	{
		InsertionSort(arr);
	}
	else if(sortingAlgoType == SortingAlgo.Bubble)
	{
		BubbleSort(arr);
	}
	else if(sortingAlgoType == SortingAlgo.Selection)
	{
		SelectionSort(arr);
	}
}

void InsertionSort(int[] arr)
{
	for (int i = 1; i < arr.Length; i++)
	{
		var j = i - 1;
		while(j >= 0)
		{
			if(arr[j + 1] < arr[j])
			{
				var temp = arr[j + 1];
				arr[j + 1] = arr[j];
				arr[j] = temp;
				j--;
			}
			else
			{
				break;
			}
		}
	}
}

void BubbleSort(int [] arr)
{
	for(int i = arr.Length - 1; i >= 1; i--)
	{
		var swapRequired = false;
		
		for(int j = i; j >= 1; j--)
		{
			if(arr[j] < arr[j - 1])
			{
				var temp = arr[j - 1];
				arr[j - 1] = arr[j];
				arr[j] = temp;
				swapRequired = true;
			}
		}
		
		//optimize.
		if(!swapRequired)
		{
			//break;
		}
	}
}

void SelectionSort(int [] arr)
{
	for(int i = 0; i < arr.Length; i++)
	{
		var minIndex = i;
		for (var j = i + 1; j < arr.Length; j++)
		{
			if (arr[j] < arr[minIndex])
			{
				minIndex = j;
			}
		}
		if(minIndex > i)
		{
			var temp = arr[i];
			arr[i] = arr[minIndex];
			arr[minIndex] = temp;
		}
	}
}