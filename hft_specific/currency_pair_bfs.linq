<Query Kind="Program" />

// HFT Currency Conversion via Graph Traversal
// - Parse pairs into a bidirectional weighted graph (A->B and B->A = 1/rate)
// - BFS from source currency to target, multiplying rates along the path
// - Returns -1 on no match, cycle-only graph, or bad input
// Format: "USDAUD@0.64" means 1 USD = 0.64 AUD

void Main()
{
	var pairs = new List<string>
	{
		"USDEUR@0.92",
		"EURGBP@0.86",
		"GBPJPY@188.50",
		"AUDUSD@1.56",   // 1 AUD = 1.56 USD (inverse also available)
		"USDCHF@0.90",
	};

	// Direct pair
	Calculate(100, "USDEUR", pairs).Dump("USD→EUR (direct, expect ~92)");

	// Two hops: USD→EUR→GBP
	Calculate(100, "USDGBP", pairs).Dump("USD→GBP (2-hop, expect ~79.12)");

	// Three hops: USD→EUR→GBP→JPY
	Calculate(100, "USDJPY", pairs).Dump("USD→JPY (3-hop, expect ~14,909)");

	// Inverse pair: AUD→USD is stored as AUDUSD@1.56, we want USDAUD (reverse)
	Calculate(100, "USDAUD", pairs).Dump("USD→AUD (inverse of stored pair, expect ~64.10)");

	// No path
	Calculate(100, "USDXYZ", pairs).Dump("USD→XYZ (no path, expect -1)");

	// Same currency
	Calculate(100, "USDUSD", pairs).Dump("USD→USD (identity, expect 100)");

	// Bad input
	Calculate(100, "US", pairs).Dump("Bad pair string (expect -1)");
	Calculate(-5, "USDEUR", pairs).Dump("Negative value (expect -1)");
}

static double Calculate(double current_value, string currencyPair, List<string> pairs)
{
	// --- Input validation ---
	if (current_value < 0) return -1;
	if (string.IsNullOrWhiteSpace(currencyPair) || currencyPair.Length != 6) return -1;
	if (pairs == null || pairs.Count == 0) return -1;

	string from = currencyPair[..3].ToUpper();
	string to   = currencyPair[3..].ToUpper();

	if (from == to) return current_value; // identity

	// --- Build bidirectional adjacency graph ---
	// graph[A] = list of (neighbor, rate) where rate converts A → neighbor
	var graph = new Dictionary<string, List<(string Currency, double Rate)>>(StringComparer.OrdinalIgnoreCase);

	foreach (var entry in pairs)
	{
		// Expected format: "XXXYYY@rate"
		var atIdx = entry.IndexOf('@');
		if (atIdx != 7) continue; // 6 chars + '@' at index 6... wait, "USDEUR@0.92" → atIdx=6
		// Actually USDEUR is 6 chars, so '@' is at index 6
		if (atIdx < 6) continue;

		string pairStr  = entry[..atIdx];
		string rateStr  = entry[(atIdx + 1)..];

		if (pairStr.Length != 6) continue;
		if (!double.TryParse(rateStr, System.Globalization.NumberStyles.Any,
		                     System.Globalization.CultureInfo.InvariantCulture, out double rate)) continue;
		if (rate <= 0) continue;

		string a = pairStr[..3].ToUpper();
		string b = pairStr[3..].ToUpper();

		// A → B at rate
		if (!graph.ContainsKey(a)) graph[a] = [];
		graph[a].Add((b, rate));

		// B → A at 1/rate (inverse)
		if (!graph.ContainsKey(b)) graph[b] = [];
		graph[b].Add((a, 1.0 / rate));
	}

	// --- BFS from 'from' to 'to', tracking accumulated rate ---
	if (!graph.ContainsKey(from) || !graph.ContainsKey(to)) return -1;

	var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	// Queue holds (currentCurrency, accumulatedRate)
	var queue = new Queue<(string Currency, double AccumulatedRate)>();
	queue.Enqueue((from, 1.0));
	visited.Add(from);

	while (queue.Count > 0)
	{
		var (curr, accRate) = queue.Dequeue();

		if (!graph.TryGetValue(curr, out var neighbors)) continue;

		foreach (var (neighbor, rate) in neighbors)
		{
			double newRate = accRate * rate;

			if (string.Equals(neighbor, to, StringComparison.OrdinalIgnoreCase))
				return current_value * newRate; // found target

			if (!visited.Contains(neighbor))
			{
				visited.Add(neighbor);
				queue.Enqueue((neighbor, newRate));
			}
		}
	}

	return -1; // no path found
}

