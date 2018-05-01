using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Evolution.Lib
{
	public class Organism
	{
		[JsonIgnore]
		public Map Map { get; set; }

		public int X { get; set; }

		public int Y { get; set; }

		public bool IsAlive { get; set; }

		public bool IsNew { get; set; }

		public int Age { get; set; }

		public int CanEatFoodPerTurn { get; set; }

		public int EnergyForEat { get; set; }

		public int EnergyForReproduce { get; set; }

		public int Energy { get; set; }

		public Organism(Map map, int xPos, int yPos)
		{
			X = xPos;
			Y = yPos;
			Map = map;
			IsAlive = true;
			IsNew = true;
			Age = 0;
			CanEatFoodPerTurn = 10;
			EnergyForEat = 5;
			EnergyForReproduce = 2;
		}

		public Organism()
		{
			IsAlive = true;
			IsNew = true;
			Age = 0;
			CanEatFoodPerTurn = 10;
			EnergyForEat = 5;
			EnergyForReproduce = 2;
		}

		public int GetEatingPriority()
		{
			if (Age >= 20)
				return 0;
			if (Age < 10)
				return 10;
			else
				return 20 - Age;
		}

		public void StartStep()
		{
			if (!IsAlive || IsNew)
				throw new InvalidOperationException("Invalid object state");
		}

		public void FinishStep()
		{
			if (Energy <= 0 && !IsNew)
				IsAlive = false;
			else
			{
				Age++;
				IsNew = false;
			}
		}

		public void Eat(int eated)
		{
			Energy += eated;
			Energy -= EnergyForEat;
		}

		public List<Organism> Reproduce()
		{
			var result = new List<Organism>();
			int childrenCount = Energy / EnergyForReproduce;
			for (int i = 0; i < childrenCount; i++)
			{
				var organism = new Organism();
				organism.Map = Map;
				result.Add(organism);
			}
			return result;
		}
	}
}
