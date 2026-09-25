<Query Kind="Program" />

void Main()
{
	long numOfLookups = 100 * 100 * 100;
	
	$"Number of lookups - {numOfLookups.ToString("#,##0")}".Dump();
	
	DictionaryLookup(numOfLookups);
	ArrayIndexLookup(numOfLookups);
}

private void DictionaryLookup(long numOfLookups)
{
	var fraudDataProviders = new Dictionary<int, string>() {
			{ 1, "miaozhen" },
			{ 2, "adbug" },
			{ 3, "rtbasia" }
		};

	var listOfFraudProviders = new[] { 3 };

	var sw = new Stopwatch();
	sw.Start();
	for (int i = 1; i <= numOfLookups; i++)
	{
		//lookup
		foreach (var fraudProvider in listOfFraudProviders)
		{
			if (fraudDataProviders.TryGetValue(fraudProvider, out var value))
			{

			}
		}
	}
	sw.Stop();
	var elapsedTime = sw.ElapsedMilliseconds;
	$"Time for dictionary lookups - {elapsedTime} (ms)".Dump();
}

private void ArrayIndexLookup(long numberOfLookups)
{
	var fraudDataProviders = new[] { "miaozhen", "adbug", "rtbasia" };

	//pass this to UserCacheclient
	var listOfFraudProviders = new[] { FraudDataProvider.RtbAsiaFraudImport };

	var sw = new Stopwatch();
	sw.Start();
	for (int i = 1; i <= numberOfLookups; i++)
	{
		//lookup
		foreach (var fraudProvider in listOfFraudProviders)
		{
			var value = fraudDataProviders[(int)fraudProvider];
		}
	}
	sw.Stop();
	var elapsedTime = sw.ElapsedMilliseconds;
	$"Time for array indexed lookups - {elapsedTime} (ms)".Dump();
}

public enum FraudDataProvider
{
	MiaozhenFraudImport = 0,
	AdBugFraudImport = 1,
	RtbAsiaFraudImport = 2
}

public enum TargetDataSource
{
	Unknown = 0,
	DynamicCreative = 1,
	XDeviceLog = 2,
	LogExtractor = 3,
	Beacon = 4,
	BlueKai = 5,
	AdvertiserApi = 6,
	ThirdPartyApi = 7,
	TurnApi = 8,
	UBXApi = 9,
	DeveloperTooling = 10,
	TapAd = 11,
	Bidder = 12,
	Adobe = 13,
	AdvertiserApiUserState = 14,
	ThirdPartyApiUserState = 15,
	IpAddressApi = 16,
	LiverampThirdParty = 17,
	LiverampAdvertiser = 18,
	ColdStorageDataElementThawing = 19,
	MiaozhenFraudImport = 20,
	AdBugFraudImport = 21,
	RtbAsiaFraudImport = 22,
}
