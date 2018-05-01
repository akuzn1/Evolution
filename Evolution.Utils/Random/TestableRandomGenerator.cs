namespace Evolution.Utils.Random
{
	public class TestableRandomGenerator
	{
		private readonly System.Random _rnd = new System.Random();

		public bool IsDebug { get; set; }

		public int GetRandom(int max)
		{
			return IsDebug ? 1 : _rnd.Next(max);
		}

		public double GetRandomDouble()
		{
			return IsDebug ? 0.5 : _rnd.NextDouble();
		}
	}
}
