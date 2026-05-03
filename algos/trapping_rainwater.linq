<Query Kind="Program" />

// Trapping Rainwater Problem
// Given an array of bar heights, compute how much water can be trapped after rain.
//
// Example:  [0,1,0,2,1,0,1,3,2,1,2,1]
//            ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░
// Water =  6 units
//
// Two approaches:
//   1. Sub-optimal  O(N²) time, O(1) space  — for each bar, scan left & right for max
//   2. Optimal      O(N)  time, O(1) space  — two-pointer technique

void Main()
{
	int[] heights = [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1];

	// Visualise the input first
	VisualiseHeights(heights).Dump("Input Heights");

	int resultQuadratic = TrappingRainwaterQuadratic(heights);
	int resultLinear    = TrappingRainwaterLinear(heights);
	int resultPrecomputed = TrappingRainwaterPrecomputed(heights);

	new { SubOptimal_O_N2 = resultQuadratic, Optimal_O_N = resultLinear, Precomputed_O_N = resultPrecomputed }
		.Dump("Water Trapped (both methods should agree)");

	// Show per-bar breakdown using the O(N) precomputed arrays approach for illustration
	BuildPerBarBreakdown(heights).Dump("Per-Bar Breakdown");
}

// ─────────────────────────────────────────────────────────────────────────────
// SUB-OPTIMAL  O(N²) — naive approach
// ─────────────────────────────────────────────────────────────────────────────
// Key insight: the water above bar[i] is limited by the shorter of the tallest
// bars to its LEFT and RIGHT.  water[i] = max(0, min(maxLeft, maxRight) - h[i])
//
// Here we compute maxLeft and maxRight for EACH bar by scanning the whole array,
// giving O(N) work per bar → O(N²) overall.
int TrappingRainwaterQuadratic(int[] h)
{
	int n = h.Length;
	int total = 0;

	for (int i = 0; i < n; i++)
	{
		// Scan left  to find the highest bar to the left  of i  (inclusive)
		int maxLeft = 0;
		for (int l = 0; l <= i; l++)
			maxLeft = Math.Max(maxLeft, h[l]);

		// Scan right to find the highest bar to the right of i (inclusive)
		int maxRight = 0;
		for (int r = i; r < n; r++)
			maxRight = Math.Max(maxRight, h[r]);

		// The water level above bar i is the lower of the two walls,
		// minus the bar's own height.  Clamp to 0 (no negative water).
		total += Math.Max(0, Math.Min(maxLeft, maxRight) - h[i]);
	}

	return total;
}

// ─────────────────────────────────────────────────────────────────────────────
// OPTIMAL  O(N) — two-pointer technique
// ─────────────────────────────────────────────────────────────────────────────
// We avoid the inner scans by maintaining running maxima from both ends and
// moving whichever pointer is currently lower inward.
//
// Why does this work?
//   If maxLeft < maxRight, then the water at the LEFT pointer is fully
//   determined by maxLeft (the right side is at least as tall), so we can
//   safely compute and advance left.  Vice-versa for the right pointer.
int TrappingRainwaterLinear(int[] h)
{
	int left  = 0, right = h.Length - 1;  // two pointers starting at each end
	int maxLeft  = 0;                      // max height seen from the left so far
	int maxRight = 0;                      // max height seen from the right so far
	int total = 0;

	while (left <= right)
	{
		if (h[left] <= h[right])
		{
			// The constraining wall for position 'left' is on the left side.
			if (h[left] >= maxLeft)
				maxLeft = h[left];          // new left maximum — no water here
			else
				total += maxLeft - h[left]; // water fills up to maxLeft level

			left++;
		}
		else
		{
			// The constraining wall for position 'right' is on the right side.
			if (h[right] >= maxRight)
				maxRight = h[right];         // new right maximum — no water here
			else
				total += maxRight - h[right];// water fills up to maxRight level

			right--;
		}
	}

	return total;
}

// ─────────────────────────────────────────────────────────────────────────────
// PRECOMPUTED  O(N) time, O(N) space — prefix/suffix maxima
// ─────────────────────────────────────────────────────────────────────────────
// 1. Build maxLeft[i]  = max height in h[0..i]   (left-to-right prefix scan)
// 2. Build maxRight[i] = max height in h[i..n-1]  (right-to-left suffix scan)
// 3. Water at bar i    = max(0, min(maxLeft[i], maxRight[i]) - h[i])
//
// The water above each bar is capped by the shorter of the tallest walls on
// either side.  Precomputing both scans separately makes this O(N) time at the
// cost of two extra O(N) arrays, which is the trade-off vs the two-pointer
// approach that achieves O(1) space.
int TrappingRainwaterPrecomputed(int[] h)
{
	int n = h.Length;

	// Pass 1 (left → right): maxLeft[i] = tallest bar from index 0 up to i
	int[] maxLeft = new int[n];
	maxLeft[0] = h[0];
	for (int i = 1; i < n; i++)
		maxLeft[i] = Math.Max(maxLeft[i - 1], h[i]);

	// Pass 2 (right → left): maxRight[i] = tallest bar from index i up to n-1
	int[] maxRight = new int[n];
	maxRight[n - 1] = h[n - 1];
	for (int i = n - 2; i >= 0; i--)
		maxRight[i] = Math.Max(maxRight[i + 1], h[i]);

	// Pass 3: for each bar, the effective water level is min(maxLeft, maxRight);
	// subtract the bar's own height and clamp to zero.
	int total = 0;
	for (int i = 0; i < n; i++)
		total += Math.Max(0, Math.Min(maxLeft[i], maxRight[i]) - h[i]);

	return total;
}

// ─────────────────────────────────────────────────────────────────────────────
// Helpers — per-bar breakdown table and ASCII visualisation
// ─────────────────────────────────────────────────────────────────────────────

// Builds the explanation table by precomputing prefix/suffix maxima in O(N).
// This is essentially the O(N) space variant that makes the logic explicit.
IEnumerable<object> BuildPerBarBreakdown(int[] h)
{
	int n = h.Length;

	// Precompute maxLeft[i]  = max(h[0..i])
	int[] maxLeft = new int[n];
	maxLeft[0] = h[0];
	for (int i = 1; i < n; i++)
		maxLeft[i] = Math.Max(maxLeft[i - 1], h[i]);

	// Precompute maxRight[i] = max(h[i..n-1])
	int[] maxRight = new int[n];
	maxRight[n - 1] = h[n - 1];
	for (int i = n - 2; i >= 0; i--)
		maxRight[i] = Math.Max(maxRight[i + 1], h[i]);

	return Enumerable.Range(0, n).Select(i => new
	{
		Index    = i,
		Height   = h[i],
		MaxLeft  = maxLeft[i],
		MaxRight = maxRight[i],
		WaterLevel = Math.Min(maxLeft[i], maxRight[i]),
		WaterUnits = Math.Max(0, Math.Min(maxLeft[i], maxRight[i]) - h[i])
	});
}

// Simple ASCII art — '█' for bar, '~' for water, ' ' for empty
string VisualiseHeights(int[] h)
{
	int maxH = h.Max();
	var sb = new StringBuilder();

	// Precompute water units per column (reuse linear approach)
	int n = h.Length;
	int[] maxLeft  = new int[n]; maxLeft[0] = h[0];
	for (int i = 1; i < n; i++) maxLeft[i] = Math.Max(maxLeft[i-1], h[i]);
	int[] maxRight = new int[n]; maxRight[n-1] = h[n-1];
	for (int i = n-2; i >= 0; i--) maxRight[i] = Math.Max(maxRight[i+1], h[i]);

	int[] waterLevel = Enumerable.Range(0, n)
		.Select(i => Math.Min(maxLeft[i], maxRight[i])).ToArray();

	for (int row = maxH; row >= 1; row--)
	{
		foreach (int i in Enumerable.Range(0, n))
		{
			if      (h[i] >= row)           sb.Append('█');
			else if (waterLevel[i] >= row)  sb.Append('~');
			else                            sb.Append(' ');
		}
		sb.AppendLine();
	}

	// Index row
	sb.Append(string.Concat(Enumerable.Range(0, n).Select(i => i % 10)));
	return sb.ToString();
}

