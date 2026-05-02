<Query Kind="Program" />

void Main()
{
	
}

  public class Interval {
      public int start;
      public int end;
      public Interval() { start = 0; end = 0; }
      public Interval(int s, int e) { start = s; end = e; }
  }
 
public int MinMeetingRooms(Interval[] intervals) 
{
	if(intervals == null || intervals.Length == 0) return 0;
	
	var startTimes = new int[intervals.Length];	
	var endTimes = new int[intervals.Length];
	for(var index = 0; index < intervals.Length; index++)
	{
		startTimes[index] = intervals[index].start;
		endTimes[index] = intervals[index].end;
	}
	
	Array.Sort(startTimes);
	Array.Sort(endTimes);
	
	var j = 0;
	var rooms = 0;
	for(int i = 0; i < startTimes.Length; i++)
	{
		if(startTimes[i] < endTimes[j]) rooms++;
		else j++;
		
	}
		
	return rooms;
}

//public int MinMeetingRooms(Interval[] intervals) 
//{
//	if(intervals == null || intervals.Length == 0) return 0;
//	
//	var firstStart = 0;
//	var lastEnd = 0;
//	
//	foreach(var interval in intervals)
//	{
//		firstStart = firstStart > interval.start ? interval.start : firstStart;
//		lastEnd = lastEnd < interval.end ? interval.end : lastEnd;
//	}
//
//	var highestIntersection = 0;
//	for(int timeInterval = firstStart; timeInterval <= lastEnd; timeInterval++)
//	{
//		var intersection = 0;
//		foreach(var interval in intervals)
//		{
//			if(interval.start < timeInterval && interval.end > timeInterval) intersection++;
//		}
//		highestIntersection = highestIntersection < intersection ? intersection : highestIntersection;
//	}
//
//	return highestIntersection;
//}