using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Players.Heroes_Monsters;

namespace Monster_Players.Utilities
{
	public  class Arena
	{
		public static void Hello()
		{
			int userChoice = 0;
			Console.WriteLine("kalos irthes stin arena gia na paixeiw prepei na etoimaseis 2 omades mai Heroes kai 1 terata!!!!");
			Console.WriteLine("\n");

			Console.WriteLine("theleis na xekiniseis me pia lista 1 = Heroes, 2 = Monsters");
			string input = Console.ReadLine();
			if(!int.TryParse(input, out userChoice) || (userChoice != 1 && userChoice != 2))
			{
				if (userChoice == 1) 
				{
					
				}

			}
		
		}
	}
}
