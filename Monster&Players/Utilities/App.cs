using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Players.Dices;
using Monster_Players.Enum;
using Monster_Players.Factory;
using Monster_Players.Heroes_Monsters;

namespace Monster_Players.Utilities
{
	//i klasi app opou tha xekinaei to programma me orismata 
	public class App
	{
	    // klasi run i opoia einai void kai tha xekinaei to programma apo edw 
		public static void Run() {

			AbstractHeroes_Monsters a = CreateCharacter();
			AbstractHeroes_Monsters b = CreateCharacter();

			AbstractHeroes_Monsters first = TheTurn(a,b);
			AbstractHeroes_Monsters second = (first == a) ? b : a;

			TheFight(first, second);


			a.ShowMore();
			b.ShowMore();

		}

		public static AbstractHeroes_Monsters CreateCharacter()
		{
			//var factory = FactoryHeroesMonsters.GetInstance();
			int userChoice = 0;
			int hitPoints = 0;
			int armorClass = 0;
			bool flag = true;

			// xekinima ma epalitheusi gia lathos wste na mou ftiaxei to katalilo adikimeno
			do
			{
				Console.WriteLine("dwse typo pehti 1 gia heroes 2 gia monster");
				string input = Console.ReadLine();
				if (!int.TryParse(input, out userChoice) || (userChoice != 1 && userChoice != 2))
				{
					Console.WriteLine("edwse lathos file prospathise pali");
					continue;
				}
				
				
			} while ((userChoice != 1) && (userChoice != 2));

			// elenhos me enum gia na doume pio adikimeno tha hrisimopoisisoume
			Heroes_Monsters_Enum heroes_Monsters_Enum = (Heroes_Monsters_Enum) userChoice;

			// onoma paihti 
			Console.WriteLine("dwse onoma tou paihti");
			string name = Console.ReadLine();

			// hit points me elenho wste na min boun gramata
			do
			{
				Console.WriteLine("dwse hit points");
				string userinput = Console.ReadLine();
				if (!int.TryParse(userinput, out hitPoints))
				{
					Console.WriteLine("dwse noumero parakalw : ");
					continue;
				}
				else
				{
					flag = false;
				}
			} while (flag != false);

			// armor class me elenho wste na min boun gramata
			do
			{
				Console.WriteLine("dwse armorclass");
				string userinput2 = Console.ReadLine();
				if (!int.TryParse(userinput2, out armorClass))
				{
					Console.WriteLine("dwse noumero parkalw : ");
					continue;
				}
				else
				{
					flag = false;
				}
			} while (flag != false);

			// dimiourgia heroe i monster analoga tin epilogi

			return FactoryHeroesMonsters.GetInstance().CreateHorM(heroes_Monsters_Enum, name, hitPoints, armorClass);

		}

		// function epilogi seiras
		public static AbstractHeroes_Monsters TheTurn(AbstractHeroes_Monsters player1, AbstractHeroes_Monsters player2)
		{
			Console.WriteLine("pame na doume pios tha paixei prwtos!");

			AbstractDice eikosapleuro = DiceFactory.GetInstance().CreateDice(DiceEnum.eikosapleuro);

			while (true)
			{
				// rihnei ο player1
				Console.WriteLine("Player1, patise enter gia na rixeis ");
				Console.ReadLine();
				int roll1 = eikosapleuro.Roll();
				Console.WriteLine($"eferes {roll1}");

				// rihnei ο player2
				Console.WriteLine("Player2, patise enter gia na rixeis ");
				Console.ReadLine();
				int roll2 = eikosapleuro.Roll();
				Console.WriteLine($"Player2 efere {roll2}");

				if (roll1 == roll2)
				{
					Console.WriteLine("isopalia pame xana!");
				}
				else if (roll1 > roll2)
				{
					Console.WriteLine($" O {player1.GetTitle()} paizei prwtos!");
					return player1;
				}
				else
				{
					Console.WriteLine($" O {player2.GetTitle()} paizei prwtos!");
					return player2;
				}
			}
		}

		// function to paihnidi
		public static void TheFight(AbstractHeroes_Monsters first ,AbstractHeroes_Monsters second ) 
		{
			int player1Life = first.GetHitPoints();
			int player2Life = second.GetHitPoints();

			Console.WriteLine($"The batlle begin's {first.GetTitle()} VS {second.GetTitle()} \n");

			while (player1Life > 0 && player2Life > 0)
			{
				// epithesi prwtou pehti 
				int damage1 = ChoiceOfDamage();
				Console.WriteLine($"O {first.GetTitle()} ekane {damage1}");
				player2Life -= damage1;
				Console.WriteLine($"{second.GetTitle()} life: {Math.Max(player2Life, 0)}\n ");

				if (player2Life <= 0) 
				{
					break;
				}

				//epithesi deuterou pehti
				int damage2 = ChoiceOfDamage();
				Console.WriteLine($"{second.GetTitle()} ekane {damage2}");
				player1Life -= damage2;
				Console.WriteLine($"{first.GetTitle()} life :{Math.Max(player1Life, 0)}\n ");

				if(player1Life <= 0)
				{
					break;
				}
			} 

			// nikitis
			if (player1Life > 0)
			{
				Console.WriteLine($"o {first.GetTitle()}   nikise");
			}
			else
			{
				Console.WriteLine($"o {second.GetTitle()} nikise");
			}
			

		}

		// elenhos enum
		private static bool IsValid(int value)
		{
			return System.Enum.IsDefined(typeof(DiceEnum), value);	
		}


		public static int ChoiceOfDamage()
		{
			//swsth eisagwgh
			Console.WriteLine("dialexe zari gia dmg!! apo 4,6,8,12");
			string userInput = Console.ReadLine();
			int value;
			// loopa epivevewshs
			while (!int.TryParse(userInput, out value) || !(IsValid(value)))
			{
				Console.WriteLine("dwse se parakalw arithmo zariou 4, 6, 8, 12");
				userInput = Console.ReadLine();
			}
			// ftiahnoyme to zari simfona me to ti mas edwse o hristis
			AbstractDice damagezari = DiceFactory.GetInstance().CreateDice((DiceEnum) value);
			return  damagezari.Roll();
		}

	}
}
