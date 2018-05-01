using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Lib
{
	public class Map
	{
		public Map(int width, int height)
		{
			Cells = new Cell[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Cells[i, j] = new Cell(this, i, j);
				}
			}
		}

		public int Width { get { return Cells.GetLength(0); } }
		public int Height { get { return Cells.GetLength(1); } }

		public Cell[,] Cells { get; set; }

		public List<Cell> GetNearestCells(int x, int y)
		{
			var result = new List<Cell>();
			if (!(x >= 0 && y >= 0 && x < Cells.GetLength(0) && y < Cells.GetLength(1)))
				return result;
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (x + i >= 0 && x + i < Cells.GetLength(0)
						&& y + j >= 0 && y + j < Cells.GetLength(1))
					{
						result.Add(Cells[x + i, y + j]);
					}
				}
			}
			return result;
		}

		public void DoNextStep()
		{
			foreach (var cell in Cells)
			{
				cell.DoNextStep();
			}
		}
	}
}
