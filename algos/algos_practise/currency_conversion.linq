<Query Kind="Program" />

void Main()
{
	// ================================================================
	// EXERCISE: Currency Conversion
	// ================================================================
	// You are given a dictionary of exchange rates, keyed by currency
	// pair (e.g. "USD/EUR" means "1 USD = rate EUR").
	//
	// Implement the method Convert(amount, fromCurrency, toCurrency, rates)
	// below (look for the TODO) so that it:
	//
	//   1. Returns the converted amount using the direct rate if the
	//      pair "FROM/TO" exists in the dictionary.
	//   2. If FROM == TO, returns the amount unchanged (no lookup needed).
	//   3. If the direct pair isn't found, but the INVERSE pair "TO/FROM"
	//      exists, use 1 / rate as the conversion factor.
	//   4. If neither the pair nor its inverse exists, throw an
	//      ArgumentException with a helpful message.
	//   5. Throws ArgumentOutOfRangeException if amount is negative.
	//   6. Currency codes should be treated case-insensitively
	//      (e.g. "usd" and "USD" are the same).
	//
	// A set of test cases is run automatically at the bottom of Main()
	// and results are Dump()'d, showing PASS/FAIL for each case.
	// ================================================================

	var pairs = new List<string>
	{
		"USDEUR@0.92",
		"EURGBP@0.86",
		"GBPJPY@188.50",
		"AUDUSD@1.56",   // 1 AUD = 1.56 USD (inverse also available)
		"USDCHF@0.90",
	};

	var rates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
	foreach (var pair in pairs)
	{
		var parts = pair.Split('@');
		var codes = parts[0];
		var rate = decimal.Parse(parts[1]);
		var from = codes.Substring(0, 3);
		var to = codes.Substring(3, 3);
		rates[$"{from}/{to}"] = rate;
	}
	
	rates.Dump();

	RunTests(rates);
}

// TODO: implement this method according to the spec in the comment above.
decimal Convert(decimal amount, string fromCurrency, string toCurrency, Dictionary<string, decimal> rates)
{
	var visitedNodes = new HashSet<string>();
	var conversion = Convert(amount, fromCurrency, toCurrency, rates, visitedNodes);
	if(conversion == -1)
		throw new ArgumentException(nameof(amount), "Amount cannot be negative.");
	return conversion;
}

decimal Convert(decimal amount, string fromCurrency, string toCurrency, Dictionary<string, decimal> rates, HashSet<string> visitedNodes)
{
	if (amount < 0)
		throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

	if (string.Equals(fromCurrency, toCurrency, StringComparison.OrdinalIgnoreCase))
		return amount;

	// visitedNodes now tracks *currencies* (not pair strings), which avoids
	// building extra key strings just for tracking visitation, and prevents
	// revisiting the same currency node in a cycle.
	if (!visitedNodes.Add(fromCurrency))
		return -1;

	if (rates.TryGetValue(string.Concat(fromCurrency, "/", toCurrency), out var conversion))
		return amount * conversion;

	// inverse
	if (rates.TryGetValue(string.Concat(toCurrency, "/", fromCurrency), out conversion))
		return amount * (1 / conversion);

	// Search the graph for a path via an intermediate currency, without
	// allocating a "prefix" string per candidate: split each key once.
	foreach (var kvp in rates)
	{
		var slashIndex = kvp.Key.IndexOf('/');
		var keyFrom = kvp.Key.AsSpan(0, slashIndex);
		var keyTo = kvp.Key.AsSpan(slashIndex + 1);

		string intermediate;
		decimal amountAtIntermediate;

		if (keyFrom.Equals(fromCurrency, StringComparison.OrdinalIgnoreCase))
		{
			intermediate = keyTo.ToString();
			amountAtIntermediate = amount * kvp.Value;
		}
		else if (keyTo.Equals(fromCurrency, StringComparison.OrdinalIgnoreCase))
		{
			intermediate = keyFrom.ToString();
			amountAtIntermediate = amount * (1 / kvp.Value);
		}
		else
		{
			continue;
		}

		if (visitedNodes.Contains(intermediate))
			continue;

		var result = Convert(amountAtIntermediate, intermediate, toCurrency, rates, visitedNodes);
		if (result > -1)
			return result;
	}

	return -1;
}

// ---------------------------------------------------------------
// Test harness - do not need to modify, but feel free to add cases
// ---------------------------------------------------------------
void RunTests(Dictionary<string, decimal> rates)
{
	var results = new List<object>();

	void Case(string name, Action test)
	{
		try
		{
			test();
			results.Add(new { Case = name, Result = "PASS", Detail = "" });
		}
		catch (Exception ex)
		{
			results.Add(new { Case = name, Result = "FAIL", Detail = ex.Message });
		}
	}

	void AssertEqual(decimal expected, decimal actual, decimal tolerance = 0.0001m)
	{
		if (Math.Abs(expected - actual) > tolerance)
			throw new Exception($"Expected {expected}, got {actual}");
	}

	Case("Direct pair USD->EUR", () =>
	{
		var result = Convert(100m, "USD", "EUR", rates);
		AssertEqual(92m, result);
	});

	Case("Same currency USD->USD", () =>
	{
		var result = Convert(50m, "USD", "usd", rates);
		AssertEqual(50m, result);
	});

	Case("Inverse pair EUR->USD", () =>
	{
		var result = Convert(92m, "EUR", "USD", rates);
		AssertEqual(100m, result);
	});

	Case("Case-insensitive lookup usd->eur", () =>
	{
		var result = Convert(10m, "usd", "eur", rates);
		AssertEqual(9.2m, result);
	});

	Case("Chained pair EUR->JPY via EUR->GBP->JPY", () =>
	{
		var result = Convert(1m, "EUR", "JPY", rates);
		AssertEqual(162.11m, result);
	});

	Case("Inverse of EUR/JPY -> JPY->EUR", () =>
	{
		var result = Convert(171.5m, "JPY", "EUR", rates);
		AssertEqual(1.057924m, result, 0.0001m);
	});

	Case("Unknown pair throws ArgumentException (USD->XYZ, no path)", () =>
	{
		bool threw = false;
		try { Convert(10m, "USD", "XYZ", rates); }
		catch (ArgumentException) { threw = true; }
		if (!threw) throw new Exception("Expected ArgumentException was not thrown.");
	});

	Case("Negative amount throws ArgumentOutOfRangeException", () =>
	{
		bool threw = false;
		try { Convert(-5m, "USD", "EUR", rates); }
		catch (ArgumentOutOfRangeException) { threw = true; }
		if (!threw) throw new Exception("Expected ArgumentOutOfRangeException was not thrown.");
	});

	Case("Zero amount returns zero", () =>
	{
		var result = Convert(0m, "USD", "EUR", rates);
		AssertEqual(0m, result);
	});

	results.Dump("Test Results");
}
