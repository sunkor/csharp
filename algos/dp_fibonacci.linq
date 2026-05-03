<Query Kind="Statements" />

// === Fibonacci via Dynamic Programming ===
// DP avoids the exponential blowup of naive recursion by storing
// previously computed results (memoization or bottom-up tabulation).

// ── 1. Bottom-up Tabulation (O(n) time, O(n) space) ──────────────────────────
long[] FibBottomUp(int n)
{
	var dp = new long[n + 1];
	dp[0] = 0;
	if (n > 0) dp[1] = 1;
	for (int i = 2; i <= n; i++)
		dp[i] = dp[i - 1] + dp[i - 2];
	return dp;
}

// ── 2. Space-optimised (O(n) time, O(1) space) ───────────────────────────────
long FibOptimised(int n)
{
	if (n == 0) return 0;
	long prev = 0, curr = 1;
	for (int i = 2; i <= n; i++)
		(prev, curr) = (curr, prev + curr);
	return curr;
}

// ── 3. Top-down Memoisation (recursive + cache) ──────────────────────────────
Dictionary<int, long> memo = new();
long FibMemo(int n)
{
	if (n <= 1) return n;
	if (memo.TryGetValue(n, out long cached)) return cached;
	return memo[n] = FibMemo(n - 1) + FibMemo(n - 2);
}

// ── Display results ───────────────────────────────────────────────────────────
int limit = 15;

"Bottom-up DP table".Dump();
var table = FibBottomUp(limit);
Enumerable.Range(0, limit + 1)
	.Select(i => new { n = i, Fib_n = table[i] })
	.Dump();

"Space-optimised vs Memoised (spot check)".Dump();
Enumerable.Range(0, limit + 1)
	.Select(i => new
	{
		n           = i,
		Optimised   = FibOptimised(i),
		Memoised    = FibMemo(i),
		Match       = FibOptimised(i) == FibMemo(i)
	})
	.Dump();

// ── Complexity summary ────────────────────────────────────────────────────────
new[]
{
	new { Approach = "Naive recursion",       Time = "O(2ⁿ)",  Space = "O(n) stack" },
	new { Approach = "Top-down memoisation",  Time = "O(n)",   Space = "O(n)" },
	new { Approach = "Bottom-up tabulation",  Time = "O(n)",   Space = "O(n)" },
	new { Approach = "Space-optimised loop",  Time = "O(n)",   Space = "O(1)" },
}.Dump("Complexity Comparison");

