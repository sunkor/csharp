<Query Kind="Program" />

void Main()
{
	
}

class Signal
{		
	
	//private int trackCount;
	private Signal[] signals;
	
	//constructor - specify track count.
	//public Signal(int trackCount = 1)
	//{
	//	this.trackCount = trackCount;		
	//}

	//Initial total # of tracks = defaul to 1 in constructor.
	//public void SetTrackCount(int trackCount)
	//{
	//	this.trackCount = trackCount;
	//}
	
	//Initial child signals.
	public void InitialSignals(int numOfSignals)
	{
		this.signals = new Signal[numOfSignals];
		for(int i = 0; i < numOfSignals; i++)
		{
			this.signals[0] = new Signal();
		}
	}

	//Print the signal intent - if green - proceed, if red stop.
	public void DisplaySignal()
	{
		if (trackCount <= 0)
		{
			//Do not proceed as line / track is busy.
			Console.WriteLine("The signal is Red");
		}

		for (var trackNumber = 0; trackNumber < trackCount; trackNumber++)
		{
			if (!ContainsTrain(trackNumber))
			{
				//Can proceed on the line / track.
				Console.WriteLine($"Track # {trackNumber} is open.");
				return;
			}
		}

		//Do not proceed as line / track is busy.
		Console.WriteLine("The signal is Red");
	}
	
	private void IsTrackOpenDownstream(Signal s)
	{
				
	}
	
	//A stub - assume it returns true when train is on track, or false.
	private bool ContainsTrain(int trackNumber)
	{
		return false;
	}
}
