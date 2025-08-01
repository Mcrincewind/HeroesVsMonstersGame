using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Players.Heroes_Monsters
{
	// klasi abstract wste na hrisimopoioume gia na ftiaxoume adikimena kai me koina stoihia 
	public abstract class AbstractHeroes_Monsters
	{
		protected int hitPoints { set; get; }
		protected int armorClass { set; get; }
		protected string name { set; get; }

		public string GetName()
		{
			return name;
		}

		public void SetName(string name)
		{
			this.name = name;
		}
		public int GetHitPoints()
		{
			return hitPoints;
		}

		public void SetHitPoints(int value)
		{
			hitPoints = value;
		}

		public int GetArmorClass()
		{
			return armorClass;
		}

		public void SetArmorClass(int value)
		{
			armorClass = value;
		}



		public AbstractHeroes_Monsters(string name, int hitpoints, int ArmorClass)
		{
		this.name = name;
		this.hitPoints = hitpoints;
		this.armorClass = ArmorClass;
		}

		public abstract string GetTitle();
		public abstract void ShowMore();



	}
}
