using Evolution.Utils.Random;
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

		public int EatingPriority { get; set; }

		public Organism(Map map, int xPos, int yPos)
			:this()
		{
			X = xPos;
			Y = yPos;
			Map = map;
		}

		public Organism()
		{
			IsAlive = true;
			IsNew = true;
			Age = 0;
			CanEatFoodPerTurn = 10;
			EnergyForEat = 5;
			EnergyForReproduce = 2;
			EatingPriority = RandomGenerator.Random.GetRandom(10);
		}

		public int GetEatingPriority()
		{
			if (Age >= 20)
				return -1;
			if (Age < 10)
				return EatingPriority;
			else
				return Math.Max(EatingPriority + 10 - Age, 0);
		}

		public void StartStep()
		{
			if (!IsAlive)
				throw new InvalidOperationException("Invalid object state");
			IsNew = false;
		}

		public void FinishStep()
		{
			if (Energy < 0 && !IsNew)
				IsAlive = false;
			else
			{
				Age++;
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
			Energy -= childrenCount * EnergyForReproduce;
			return result;
		}
	}
}
