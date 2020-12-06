using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC20206CustomCustoms
{
	class Program
	{
		public static HashSet<char> Halmazrendszer_Metszete(Stack<HashSet<char>> csoport)
		{
			HashSet<char> metszet = csoport.Pop();
			while (csoport.Count != 0)
				metszet.IntersectWith(csoport.Pop());
			return metszet;
		}

		public static HashSet<char> Halmazrendszer_Uniója(Stack<HashSet<char>> csoport)
		{
			HashSet<char> unió = new HashSet<char>();
			while (csoport.Count != 0)
				unió.UnionWith(csoport.Pop());
			return unió;
		}

		static Stack<Stack<HashSet<char>>> Betölt(string fajlnev)
		{
			Stack<Stack<HashSet<char>>> repülőgép = new Stack<Stack<HashSet<char>>>(); // lásd a következő megjegyzést
			using (StreamReader f = new StreamReader(fajlnev))
			{
				HashSet<char> ember; // matematikai értelemben vett halmaz C#-ban
				Stack<HashSet<char>> csoport = new Stack<HashSet<char>>(); // csoport = emberverem
				while (!f.EndOfStream)
				{
					ember = f.ReadLine().ToHashSet(); // eltűnnek belőle az ismétlődések
					if (ember.Count == 0)
					{
						repülőgép.Push(csoport);
						csoport = new Stack<HashSet<char>>();
					}
					else
						csoport.Push(ember);
				}
				repülőgép.Push(csoport);
			}
			return repülőgép;
		}

		static void Main(string[] args)
		{
			Stack<Stack<HashSet<char>>> repülőgép = Betölt("input.txt");
			int sum_a = 0;
			while (repülőgép.Count != 0)
				sum_a += Halmazrendszer_Uniója(repülőgép.Pop()).Count();

			repülőgép = Betölt("input.txt");
			int sum_b = 0;
			while (repülőgép.Count != 0)
				sum_b += Halmazrendszer_Metszete(repülőgép.Pop()).Count();

			Console.WriteLine($"Az 1. feladat eredménye: {sum_a}");
			Console.WriteLine($"Az 2. feladat eredménye: {sum_b}");
		}
	}
}
