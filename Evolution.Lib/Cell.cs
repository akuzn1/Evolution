using Newtonsoft.Json;
using System.Collections.Generic;

namespace Evolution.Lib
{
	public class Cell
	{
		public Cell(Map map, int x, int y)
			:this()
		{
			Map = map;
			X = x;
			Y = y;
		}

		private Cell()
		{
			Organisms = new List<Organism>();
		}

		public Cell(Map map, int x, int y, int food, int maxFood, int increaseSpeed)
			:this(map, x, y)
		{
			Food = food;
			MaxFood = maxFood;
			IncreaseSpeed = increaseSpeed;
		}

		[JsonIgnore]
		public Map Map { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int Food { get; set; }
		public int MaxFood { get; set; }
		public int IncreaseSpeed { get; set; }
		public List<Organism> Organisms { get; set; }

		public void DoNextStep()
		{
			Food += IncreaseSpeed;
			if (Food > MaxFood)
				Food = MaxFood;
		}
	}
}
