using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Players.Enum;
using Monster_Players.Heroes_Monsters;

namespace Monster_Players.Factory
{
	// ena factory wste na mas ftiaheni adikimena monsters i pehtes
	public class FactoryHeroesMonsters
	{
		// dimiougia singleton 
		private static FactoryHeroesMonsters instance;
		private FactoryHeroesMonsters() { }

		public static FactoryHeroesMonsters GetInstance()
		{
			if (instance == null)
			{
				instance = new FactoryHeroesMonsters();
			}
			return instance;
		}

		// dimiourgia abstract klaseis me orisma enum name hitpoints kai armorclass gia na kanoume epilogi ti tha ftiaxei
		public AbstractHeroes_Monsters CreateHorM (Heroes_Monsters_Enum heroes_Monsters_Enum, string name , int hitPoints, int armorClass)
		{
			switch(heroes_Monsters_Enum)
			{
				case Heroes_Monsters_Enum.heroes:
				{
					return new Heroes(name, hitPoints, armorClass);
				}

				case Heroes_Monsters_Enum.monster:
				{
					return new Monsters(name, hitPoints, armorClass);
				}
				default:
				throw new ArgumentException("unkown character!!");
			}
		}
	}
}
