<Query Kind="Program" />

void Main()
{
	//var citations = new [] {3,0, 6, 1, 5};
	//var citations = new [] {0};
	var citations = new [] {1,1};
	//var citations = new [] {100};
	HIndex(citations).Dump();
}

 public int HIndex(int[] citations) 
 {
 	if(citations == null || citations.Length == 0) return 0;
	
	Array.Sort(citations);
	
	var length = citations.Length;
	var citationCount = 0;
	var numOfCitations = 0;
	var prevNumOfCitations = 0;
	do
	{
		prevNumOfCitations = numOfCitations;
		numOfCitations = HowManyPapersWithCitation(++citationCount, citations);
	}while(numOfCitations >= citationCount);
		
	citationCount--;
	numOfCitations = prevNumOfCitations;
	
    return numOfCitations < citationCount ? numOfCitations : citationCount;
 }
 
 private int HowManyPapersWithCitation(int numOfCitations, int [] citations)
 {
	var length = citations.Length;
	for(var j = 0; j < length; j++)
	{
		if(citations[j] >= numOfCitations)
		{
			return length - j;
		}
	}
	return 0;
 }