<Query Kind="Program" />

// DP Coin Change Problem abc
// Given a set of coin denominations and a target amount,
// find the MINIMUM number of coins needed to make that amount.
// If it's not possible, return -1.

int[] coins = { 1, 5, 10, 25 };  // US coin denominations (cents)
int amount = 41;

// ── Visualise the DP table ────────────────────────────────────────────────────
void Main()
{
	// Try a few examples
	new[]
	{
		(coins: new[] { 1, 5, 10, 25 }, amount: 41,  label: "US coins → 41¢"),
		(coins: new[] { 1, 5, 10, 25 }, amount: 0,   label: "US coins → 0¢"),
		(coins: new[] { 2 },            amount: 3,   label: "Only 2s  → 3¢ (impossible)"),
		(coins: new[] { 1, 3, 4 },      amount: 6,   label: "{1,3,4}  → 6"),
		(coins: new[] { 186,419,83,408},amount: 6249,label: "Large denominations → 6249"),
	}
	.Select(e => new
	{
		e.label,
		Coins      = string.Join(", ", e.coins),
		Amount     = e.amount,
		MinCoins   = CoinChange(e.coins, e.amount),
		Breakdown  = Breakdown(e.coins, e.amount)
	})
	.Dump("Coin Change – Minimum Coins");

	// Show the full DP array for the first example
	BuildDPTable(coins, amount).Dump($"DP array for coins=[{string.Join(",",coins)}], amount={amount}");
}

// ── Core DP algorithm ─────────────────────────────────────────────────────────
// dp[i] = minimum coins needed to make amount i
// Recurrence: dp[i] = min(dp[i], dp[i - coin] + 1)  for each coin ≤ i
// Base case:  dp[0] = 0
// Time: O(amount × |coins|)   Space: O(amount)
static int CoinChange(int[] coins, int amount)
{
	if (amount == 0) return 0;

	const int INF = int.MaxValue / 2;
	var dp = new int[amount + 1];
	Array.Fill(dp, INF);
	dp[0] = 0;

	for (int i = 1; i <= amount; i++)
		foreach (int coin in coins)
			if (coin <= i && dp[i - coin] + 1 < dp[i])
				dp[i] = dp[i - coin] + 1;

	return dp[amount] == INF ? -1 : dp[amount];
}

// ── Reconstruct which coins were used ─────────────────────────────────────────
static string Breakdown(int[] coins, int amount)
{
	if (amount == 0) return "none";
	const int INF = int.MaxValue / 2;
	var dp = new int[amount + 1];
	Array.Fill(dp, INF);
	dp[0] = 0;

	// Track which coin was chosen at each step
	var from = new int[amount + 1];

	for (int i = 1; i <= amount; i++)
		foreach (int coin in coins)
			if (coin <= i && dp[i - coin] + 1 < dp[i])
			{
				dp[i]   = dp[i - coin] + 1;
				from[i] = coin;
			}

	if (dp[amount] == INF) return "impossible";

	// Walk back through 'from' to collect coins used
	var used = new List<int>();
	for (int cur = amount; cur > 0; cur -= from[cur])
		used.Add(from[cur]);

	return string.Join(" + ", used.OrderByDescending(x => x));
}

// ── Build a displayable DP snapshot ──────────────────────────────────────────
static object BuildDPTable(int[] coins, int amount)
{
	const int INF = int.MaxValue / 2;
	var dp = new int[amount + 1];
	Array.Fill(dp, INF);
	dp[0] = 0;

	return Enumerable.Range(0, amount + 1).Select(i =>
	{
		foreach (int coin in coins)
			if (coin <= i && dp[i - coin] + 1 < dp[i])
				dp[i] = dp[i - coin] + 1;

		return new { Amount = i, MinCoins = dp[i] == INF ? (object)"∞" : dp[i] };
	}).ToList();
}

