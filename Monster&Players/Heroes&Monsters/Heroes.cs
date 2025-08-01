using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Players.Heroes_Monsters
{
	// i klasi heroe pou klironomei tin abstract 
	public class Heroes : AbstractHeroes_Monsters
	{
		private readonly string title = "Heroe";

		public Heroes(string name, int hitPoints, int armorClass, int iniviative)
		: base(name, hitPoints, armorClass, iniviative)
		{
		}

		public override string GetTitle()
		{
			return title;
		}

		public override void ShowMore()
		{
			Console.WriteLine($"{name} --> HP : {hitPoints} kai armor {armorClass} kai to initiative einai {initiative}");
		}

	}
}
