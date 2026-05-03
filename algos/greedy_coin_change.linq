<Query Kind="Program" />

// Greedy Coin Change Algorithm
// Given a target amount, find the minimum number of coins using a greedy approach:
// always pick the largest denomination that fits the remaining amount.
//
// NOTE: Greedy works OPTIMALLY for "canonical" coin systems (like US coins),
// but may fail for arbitrary denominations (see the counterexample below).

void Main()
{
	// --- Standard US coins ---
	int[] usCoins = [100, 50, 25, 10, 5, 1]; // cents: dollar, half, quarter, dime, nickel, penny
	
	int[] testAmounts = [41, 99, 175, 300, 1];
	
	testAmounts
		.Select(amount => GreedyChange(usCoins, amount))
		.Dump("US Coins (greedy is optimal here)");

	// --- Where greedy FAILS ---
	// Denominations: 1, 3, 4  |  Target: 6
	// Greedy picks: 4 + 1 + 1 = 3 coins
	// Optimal:      3 + 3     = 2 coins
	int[] badCoins = [4, 3, 1];
	
	new[] { 6, 7, 8 }
		.Select(amount => GreedyChange(badCoins, amount))
		.Dump("Coins {4,3,1} — greedy is NOT always optimal");

	// Side-by-side comparison for the failing case
	new[]
	{
		new { Amount = 6, Greedy = GreedyChange(badCoins, 6).CoinsUsed, Optimal = "3+3 = 2 coins" },
		new { Amount = 7, Greedy = GreedyChange(badCoins, 7).CoinsUsed, Optimal = "4+3 = 2 coins" },
	}.Dump("Greedy vs Optimal comparison for {4,3,1}");
}

// Returns a breakdown of which coins were chosen and how many
ChangeResult GreedyChange(int[] denominations, int amount)
{
	// Denominations must be sorted descending for greedy to work
	var sorted = denominations.OrderByDescending(d => d).ToArray();

	var breakdown = new List<(int Coin, int Count)>();
	int remaining = amount;

	foreach (int coin in sorted)
	{
		if (remaining <= 0) break;
		int count = remaining / coin;   // how many of this coin fit?
		if (count > 0)
		{
			breakdown.Add((coin, count));
			remaining -= coin * count;
		}
	}

	return new ChangeResult
	{
		Amount      = amount,
		CoinsUsed   = breakdown.Sum(b => b.Count),
		Solvable    = remaining == 0,
		Breakdown   = string.Join(" + ", breakdown.Select(b => $"{b.Count}×{b.Coin}¢"))
	};
}

class ChangeResult
{
	public int    Amount     { get; init; }
	public int    CoinsUsed  { get; init; }
	public bool   Solvable   { get; init; }
	public string Breakdown  { get; init; }
}

