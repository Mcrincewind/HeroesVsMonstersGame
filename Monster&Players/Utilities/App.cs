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

			////AbstractHeroes_Monsters a = CreatePlayer();
			////AbstractHeroes_Monsters b = CreatePlayer();
			////AbstractHeroes_Monsters first = TheTurn(a,b);
			////AbstractHeroes_Monsters second = (first == a) ? b : a;
			////TheFight(first, second);
			////a.ShowMore();
			////b.ShowMore();
			Arena.Hello();

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
				int roll1 = eikosapleuro.Roll() + player1.GetInitiative();
				Console.WriteLine($"eferes {roll1}");

				// rihnei ο player2
				Console.WriteLine("Player2, patise enter gia na rixeis ");
				Console.ReadLine();
				int roll2 = eikosapleuro.Roll() + player2.GetInitiative();
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
				//roll gia na htipisei	
				if (!IsHit(second))
				{
					Console.WriteLine($"astohises {first.GetTitle()}");
				}

				else
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
				}
				// roll gia na htipisei 
				if (!IsHit(first))
				{
					Console.WriteLine($"astohises {second.GetTitle()} ");
				}
				else
				{
					//epithesi deuterou pehti
					int damage2 = ChoiceOfDamage();
					Console.WriteLine($"{second.GetTitle()} ekane {damage2}");
					player1Life -= damage2;
					Console.WriteLine($"{first.GetTitle()} life :{Math.Max(player1Life, 0)}\n ");

					if (player1Life <= 0)
					{
						break;
					}
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

		// function pou dihnei ti zari hrisimopoiei o paihtis kai mas epistrefei to hit
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

		// function pou dihnei ama pernaei to AC tou pehti wste na htipisei
		public static bool IsHit(AbstractHeroes_Monsters player)
		{
			AbstractDice eikosapleuro = DiceFactory.GetInstance().CreateDice(DiceEnum.eikosapleuro);

			Console.WriteLine("rixe gia na doume ean htipas press enter kai tha ginei roll 20 pleuro");
			Console.ReadLine();
			int hit = eikosapleuro.Roll();

			if (hit >= player.GetArmorClass())
			{
				Console.WriteLine("hitares!!!");
				Console.WriteLine("\n");
				return true;
			}
			else
			{
				return false;
			}
		}

		// dimiourgia Heroe
		public static Heroes CreateHero()
		{
			Console.WriteLine("dwse onoma heroe");
			string name = Console.ReadLine();

			int hitPoints = AskForInt("dwse hit points:");
			int armorClass = AskForInt("dwse armor class:");
			int initiative = AskForInt("dwse initiative:");

			return new Heroes(name, hitPoints, armorClass, initiative);
		}

		// dimiourgia monster
		public static Monsters CreateMonster()
		{
			Console.WriteLine("dwse onoma teratos:");
			string name = Console.ReadLine();

			int hitPoints = AskForInt("dwse hit points:");
			int armorClass = AskForInt("dwse armor class:");
			int initiative = AskForInt("dwse initiative:");

			return new Monsters(name, hitPoints, armorClass, initiative);
		}

		// dimiourgia player
		public static AbstractHeroes_Monsters CreatePlayer()
		{
			int choice;
			do
			{
				Console.WriteLine("dwse typo : 1 gia Hero, 2 gia Monster");
			}
			while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2));

			if (choice == 1)
				return CreateHero();
			else
				return CreateMonster();
		}

		// function na elenhei arithmos i ohi me minima
		public static int AskForInt(string message)
		{
			int value;
			while (true)
			{
				Console.WriteLine(message);
				if (int.TryParse(Console.ReadLine(), out value))
					return value;
				Console.WriteLine("mi egiro!! dwse arithmo");
			}
		}
	}
}
