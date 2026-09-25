<Query Kind="Program" />

void Main()
{
	
}

/**
 * Definition for an interval.*/
  public class Interval {
      public int start;
      public int end;
      public Interval() { start = 0; end = 0; }
      public Interval(int s, int e) { start = s; end = e; }
 }


    public IList<Interval> Merge(IList<Interval> intervals) 
	{
		if(intervals == null || intervals.Count <= 1) return intervals;
		
		var mergedIntervals = new List<Interval>();
		IEnumerable<Interval> sortedIntervals = intervals.OrderBy(interval => interval.start);
		Interval currentInterval = null;
		foreach(var interval in sortedIntervals)
		{
			if(currentInterval == null)
			{
				currentInterval = interval;
				mergedIntervals.Add(interval);
				continue;
			}
			//overlaps
			if(interval.start >= currentInterval.start && interval.start <= currentInterval.end)
			{
				currentInterval.end = currentInterval.end < interval.end ? interval.end : currentInterval.end;
				continue;
			}
			else
			{
				currentInterval = interval;
				mergedIntervals.Add(interval);
			}
		}
		
        return mergedIntervals;
    }
