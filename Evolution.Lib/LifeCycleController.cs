using Evolution.Utils.Random;
using System;
using System.Linq;

namespace Evolution.Lib
{
	public class LifeCycleController
	{
		private readonly Map _map;

		public LifeCycleController(Map map)
		{
			_map = map;
		}

		public void DoFinishPhase()
		{
			for (int i = 0; i < _map.Width; i++)
			{
				for (int j = 0; j < _map.Height; j++)
				{
					foreach (var organism in _map.Cells[i, j].Organisms)
					{
						organism.FinishStep();
					}
				}
			}
		}

		public void DoDeadPhase()
		{
			for (int i = 0; i < _map.Width; i++)
			{
				for (int j = 0; j < _map.Height; j++)
				{
					foreach (var dead in _map.Cells[i, j].Organisms.Where(p => !p.IsAlive).ToList())
					{
						_map.Cells[i, j].Organisms.Remove(dead);
					}
				}
			}
		}

		public void DoInitialPhase()
		{
			for (int i = 0; i < _map.Width; i++)
			{
				for (int j = 0; j < _map.Height; j++)
				{
					foreach (var organism in _map.Cells[i, j].Organisms)
					{
						organism.StartStep();
					}
				}
			}
		}

		public void DoReproductionPhase()
		{
			for (int i = 0; i < _map.Width; i++)
			{
				for (int j = 0; j < _map.Height; j++)
				{
					foreach (var parent in _map.Cells[i, j].Organisms.Where(p => !p.IsNew).ToList())
					{
						var children = parent.Reproduce();
						var nearestCells = _map.GetNearestCells(i, j);
						foreach (var child in children)
						{
							var cell = nearestCells[RandomGenerator.Random.GetRandom(nearestCells.Count)];
							child.X = cell.X;
							child.Y = cell.Y;
							cell.Organisms.Add(child);
						}
					}
				}
			}
		}

		public void DoEatingPhase()
		{
			for (int i = 0; i < _map.Width; i++)
			{
				for (int j = 0; j < _map.Height; j++)
				{
					var queue = _map.Cells[i, j].Organisms.OrderByDescending(p => p.GetEatingPriority());
					foreach (var organism in queue)
					{
						var canEat = organism.CanEatFoodPerTurn - RandomGenerator.Random.GetRandom(organism.CanEatFoodPerTurn / 2);
						var eated = Math.Min(canEat, _map.Cells[i, j].Food);
						organism.Eat(eated);
						_map.Cells[i, j].Food -= eated;
					}
				}
			}
		}
	}
}
