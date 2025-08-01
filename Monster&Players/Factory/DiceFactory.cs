using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Players.Dices;
using Monster_Players.Enum;

namespace Monster_Players.Factory
{
	// to dice fractory wste na mas dimiourgei zaria alla mono ena instance
	public class DiceFactory
	{
		// dimiourgia sigleton
		private static DiceFactory instance;
		private DiceFactory() { }

		public static DiceFactory GetInstance()
		{
		if (instance == null) 
		{
		instance = new DiceFactory();
		}
		return instance;

		}

		public AbstractDice	CreateDice(DiceEnum	diceEnum)
		{
			switch (diceEnum)
			{
				case DiceEnum.eikosapleuro:
				{
					D20 eikosapleuro = new D20();
					return eikosapleuro;
				}

				case DiceEnum.dodekapleuro: 
				{
					D12 dodekapleuro = new D12();
					return dodekapleuro;
				}

				case DiceEnum.dekapleuro:
				{
					D10 dekapleuro = new D10();
					return dekapleuro;
				}

				case DiceEnum.oktapleuro: 
				{
					D8 oktapleuro = new D8();
					return oktapleuro;
				}

				case DiceEnum.exapleuro:
				{
					D6 exapleuro = new D6();
					return exapleuro;
				}

				case DiceEnum.tetraleuro:
				{
					D4 tetraleuro = new D4();
					return tetraleuro;
				}

				default:
				{
					throw new Exception("wrong input....");
				}
			}
		}
	}

}
