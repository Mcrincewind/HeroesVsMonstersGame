using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Players.Interface;

namespace Monster_Players.Dices
{
	// i mana class pou tha ehei ta harahtiristika gia dice 
	public abstract class AbstractDice : IRollable
	{
		protected int sides{  get; set; }
		private Random random =  new Random();

		public virtual int Roll()
		{
			return random.Next(1, sides + 1);
		}
	}
}
