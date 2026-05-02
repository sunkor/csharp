<Query Kind="Program" />

void Main()
{
	FindCelebrity(10).Dump();
}

public int FindCelebrity(int n) 
{	
	if(n <= 1) return -1;
	
	var guestsToRemove = new List<int>();
	
	var possibleCelebs = new HashSet<int>();
	var notCelebs = new HashSet<int>();
	var celebsToRemove = new List<int>();

	//Expected: -1, Output: 1
	//0 does not know 1; 1 does not know 0.
	
	possibleCelebs.Add(0);
	for(int guest = 1; guest < n; guest++)
	{	
		var isGuestAPossibleCeleb = true;
		foreach(var possibleCeleb in possibleCelebs)
		{
			
			if(Knows(possibleCeleb, guest))
			{
				celebsToRemove.Add(possibleCeleb);
			}
			else
			{
				isGuestAPossibleCeleb = false;
			}
			
			if(Knows(guest, possibleCeleb))
			{
				isGuestAPossibleCeleb = false;
			}
			else
			{
				celebsToRemove.Add(possibleCeleb);
			}
		}
		
			if(isGuestAPossibleCeleb)
			{
				foreach(var prevGuest in notCelebs)
				{
					if(Knows(guest, prevGuest))
					{
						isGuestAPossibleCeleb = false;
						break;
					}
					
					if(!Knows(prevGuest, guest))
					{
						isGuestAPossibleCeleb = false;
						break;
					}
				}
			}	
			
			foreach(var celebToRemove in celebsToRemove)
			{
				possibleCelebs.Remove(celebToRemove);
				notCelebs.Add(celebToRemove);
			}
			
			celebsToRemove.Clear();

					
		if(isGuestAPossibleCeleb) 
			possibleCelebs.Add(guest);
		else
			notCelebs.Add(guest);
	}
	return possibleCelebs.Count == 1 ? possibleCelebs.First() : -1;
}
public bool Knows(int a, int b)
{
	return false;
}
