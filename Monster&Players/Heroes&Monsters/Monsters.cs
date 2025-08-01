using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Players.Heroes_Monsters
{
	// klasi monsters i opoia klironomi tin abstract kai tin showmore anagastika 
	public class Monsters:AbstractHeroes_Monsters
	{
		private readonly string title = "Monster";
		public Monsters(string name, int hitPoints, int armorClass)
		:base(name, hitPoints, armorClass)
		{
		}
		public override string GetTitle()
		{
			return title;
		}
		public override void ShowMore()
		{
			Console.WriteLine($"{name} --> HP : {hitPoints} kai armor {armorClass}");
		}

	}
}
