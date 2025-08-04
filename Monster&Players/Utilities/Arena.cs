using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Monster_Players.Heroes_Monsters;

namespace Monster_Players.Utilities
{
	public  class Arena
	{
		// kalosorisma gia tin arena kai xekinima
		public static void Hello()
		{
			int userChoice = 0;
			Console.WriteLine("kalos irthes stin arena gia na paixeis prepei na etoimaseis 2 omades mia Heroes kai mia terata!!!!");
			Console.WriteLine("\n");

			// elenhos to ti dinei 
			int epilogi = App.AskForInt("theleis na xekiniseis me pia lista!!! 1 = Heroes, 2 = Monsters");
			while(true) 
			{
				if (epilogi != 1 && epilogi != 2)
				{
					Console.WriteLine("mi egiri epilogi");
					epilogi = App.AskForInt("dwse 1 gia heroe 2 gia monster:");
					continue;
				}
				else
				{
					break;
				}
			}
			
			// dimiourgia prwta heroe kai meta monster
			if (epilogi ==1)
			{
				List<AbstractHeroes_Monsters> heroes = CreateHeroes();

				List<AbstractHeroes_Monsters> monsters = CreateMonsters();
				Console.WriteLine($"dimiourgithika  heroes : {heroes.Count}  kai Monsters : {monsters.Count}! ");

				Console.WriteLine("oi heroes einai aytoi : \n");
				ShowList(heroes);
				Console.WriteLine("\n");

				Console.WriteLine("ta monsters einai aytoi : \n");
				ShowList(monsters);
				Console.WriteLine("\n");
				ShowList(GetFightList(heroes, monsters));
				StartBattle(GetFightList(heroes, monsters), heroes, monsters);



			}
			// dimourgia prwta monster kai meta heroe
			else if(epilogi ==2)
			{
				List<AbstractHeroes_Monsters> monsters = CreateMonsters();


				List<AbstractHeroes_Monsters> heroes = CreateHeroes();
				Console.WriteLine($"dimiourgithika  heroes : {monsters.Count}  kai Monsters : {heroes.Count}! ");

				Console.WriteLine("ta monsters einai aytoi : \n");
				ShowList(monsters);
				Console.WriteLine("\n");
				
				Console.WriteLine("oi heroes einai aytoi : \n");
				ShowList(heroes);
				Console.WriteLine("\n");
				ShowList(GetFightList(heroes, monsters));
				StartBattle(GetFightList(heroes,monsters),heroes,monsters);
			}


		
		}

		// klasi pou mas ftiahnei lista apo monsters
		public static List<AbstractHeroes_Monsters> CreateMonsters()
		{
			List<AbstractHeroes_Monsters>monstersList = new List<AbstractHeroes_Monsters>();
			int userInput = -1;
			
			while(userInput !=0)
			{
				Console.WriteLine("dwse 1 gia na valeis monster kai 0 gia na stamatiseis na vazeis");
				if (!int.TryParse(Console.ReadLine(), out userInput))
				{
					Console.WriteLine("edwse kati lathos re file pame xana");
				}
				if (userInput == 1)
				{
					AbstractHeroes_Monsters monster = App.CreateMonster();
					monstersList.Add(monster);
					Console.WriteLine($"prosthetike");
				}
				else if(userInput != 0) 
				{
					Console.WriteLine("vale mono 1 gia monster kai 0 gia na stamatiseis!!");
				}
			}
			return monstersList;
		}

		// klasi pou mas ftiahnei lista apo Heroes
		public static List<AbstractHeroes_Monsters> CreateHeroes()
		{
			List<AbstractHeroes_Monsters> heroesList = new List<AbstractHeroes_Monsters>();
			int userInput = -1;

			while(userInput !=0) 
			{
				Console.WriteLine("dwse 1 gia na valeis Heroes kai 0 gia na stamatiseis na vazeis.");
				if(!int.TryParse(Console.ReadLine(),out userInput))
				{
					Console.WriteLine("kati ekanes lathos");
				}
				if(userInput == 1)
				{
					AbstractHeroes_Monsters heroe = App.CreateHero();
					heroesList.Add(heroe);
					Console.WriteLine($"prostethike");
				}
				else if(userInput != 0)
				{
					Console.WriteLine("vale mono 1 gia hero kai 0 gia na stamatiseis!!");
				}
			}
			return heroesList;
		}

		// function pou mas fernei ta onomata apo listes genika
		public static void ShowList(List<AbstractHeroes_Monsters> lista)
		{
			int i = 1;

			if (lista == null || lista.Count == 0)
			{
				Console.WriteLine("i lista einai adia");
				return;
			}
			foreach (AbstractHeroes_Monsters character in lista)
			{
				Console.WriteLine($"{i} : "+character.GetName());
				i++;
			}
		}

		// edw kanoume merge tis 2 listes 
		public static List <AbstractHeroes_Monsters> GetFightList(List<AbstractHeroes_Monsters> Heroes, List<AbstractHeroes_Monsters> Monsters)
		{
			return Heroes.Concat(Monsters)
			.OrderByDescending(i => i.GetInitiative())
			.ToList();
		}

		//function na mas epistrefei tous zwntanous adipalous
		public static List<AbstractHeroes_Monsters> GetAliveTargets(
		AbstractHeroes_Monsters attacker,
		List<AbstractHeroes_Monsters> heroesList,
		List<AbstractHeroes_Monsters> monstersList)
		{
			if(attacker is Heroes)
			{
				return monstersList.Where(m => m.GetHitPoints() > 0)
				.Cast<AbstractHeroes_Monsters>()
				.ToList();
			}
			else
			{
				return heroesList.Where(h => h.GetHitPoints() > 0)
				.Cast<AbstractHeroes_Monsters>()
				.ToList();
			}
		}

		//function epilogi traarget apo tin lista
		public static AbstractHeroes_Monsters ChooseTarget(List<AbstractHeroes_Monsters> possibleTargets)
		{
			Console.WriteLine("dialexe stoho");
			for (int i = 0; i < possibleTargets.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {possibleTargets[i].GetName()} (HP: {possibleTargets[i].GetHitPoints()})");
			}

			int choice;
			while (true)
			{
				choice = App.AskForInt("epelexe arithmo stohou");
				if (choice > 0 && choice <= possibleTargets.Count)
					break;
				Console.WriteLine("mi egiri epilogi prospathise pali");
			}
			return possibleTargets[choice - 1];
		}

		// function vlepw ean o stohos ehei pethanei
		public static bool IsDeadByAttack(AbstractHeroes_Monsters attacker, AbstractHeroes_Monsters target)
		{
			int damage = App.ChoiceOfDamage();
			Console.WriteLine($"{attacker.GetName()} epitithete ston {target.GetName} kai ekane {damage}");
			target.SetHitPoints(target.GetHitPoints() - damage);

			if (target.GetHitPoints() <= 0)
			{
				return true;
			}
			else
			{
				Console.WriteLine($"{target.GetName()} ehei twra {target.GetHitPoints()} HP.\n");
				return false;
			}
		}

		// elenhos ean teliwse i mahi 
		public static bool IsBattleOver(List<AbstractHeroes_Monsters> heroes, List<AbstractHeroes_Monsters> monsters)
		{
			return !(heroes.Any(h => h.GetHitPoints() > 0) && monsters.Any(m => m.GetHitPoints() > 0));
		}


		// kyria fight kai vronhos tis mahis

		public static void StartBattle(
		List<AbstractHeroes_Monsters> orderList,
		List<AbstractHeroes_Monsters> heroesList,
		List<AbstractHeroes_Monsters> monstersList)
		{
			int round = 1;
			while(!IsBattleOver(heroesList, monstersList))
			{
				Console.WriteLine("xekinaei o gyros \n");

				foreach(var attacker in orderList)
				{
					if(attacker.GetHitPoints() <= 0)
					{
						continue;
					}

					var targets = GetAliveTargets(attacker,heroesList, monstersList);

					if(targets.Count == 0)
					{
						break;
					}

					Console.WriteLine($"\n{attacker.GetName()} (initiative {attacker.GetInitiative()}) etoimazete na epithethei");

					var target = ChooseTarget(targets);
					IsDeadByAttack(attacker, target);

					if(IsBattleOver(heroesList, monstersList))
					{
						break;
					}
				}
				round++;
			}
			if(heroesList.Any(h => h.GetHitPoints() > 0)) 
			{
				Console.WriteLine("oi heroes nikisan");
			}
			else
			{
				Console.WriteLine("ta terata nikisan");
			}
		}
	}

	
}
