namespace AdventOfCode.Puzzles.Year_2024.Day_22;

public class Part_02 : Day_22
{
    public override int Part => 2;

    public override string ResultMessage => "Sum of most bananas earned for the best sequence";

    private readonly int _cycleCount = 2000;
	private List<long> _buyerSecrets;

	public override void PrepareData(List<string> input)
	{
		_buyerSecrets = ParseSecretNumbers(input);
	}

    public override string GetResult()
    {
        return CalculateSumOfBestSalesSequence(_buyerSecrets, _cycleCount).ToString();
    }

    private static long CalculateSumOfBestSalesSequence(List<long> buyerSecrets, int cycleCount)
    {
        var sequenceValueMap = new Dictionary<(long, long, long, long), long>();

        foreach (var buyerSecret in buyerSecrets)
        {
            var currentSecret = buyerSecret;
            // Only need to keep track of first appearance of sequence
            var seenBuyerSequences = new HashSet<(long, long, long, long)>();

            var previousPrice = currentSecret % 10;
            // Keep track of previous 4 diffs only
            var diffs = new List<long>();

            for (var _ = 0; _ < cycleCount + 1; _++)
            {
                currentSecret = GenerateNextSecret(currentSecret);
                var currentPrice = currentSecret % 10;

                diffs.Add(previousPrice - currentPrice);
                previousPrice = currentPrice;

                if (diffs.Count >= 4)
                {
                    var sequenceKey = (diffs[0], diffs[1], diffs[2], diffs[3]);
                    diffs.RemoveAt(0);

                    if (seenBuyerSequences.Contains(sequenceKey))
                        continue;

                    seenBuyerSequences.Add(sequenceKey);

                    if (!sequenceValueMap.TryAdd(sequenceKey, currentPrice))
                        sequenceValueMap[sequenceKey] += currentPrice;
                }
            }
        }

        return sequenceValueMap
            .Select(t => t.Value)
            .OrderByDescending(t => t)
            .First();
    }
}