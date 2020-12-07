using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace AOC20207HandyHaversacks
{

	class Hova
	{
		public int db;
		public string hova;

		public Hova(string hová, int szám)
		{
			this.hova = hová;
			this.db = szám;
		}
	}

	class Program
	{
		static Dictionary<string, List<Hova>> BeFelfele(string fájlnév)
		{
			Dictionary<string, List<Hova>> élnyilvántartás = new Dictionary<string, List<Hova>>();
			using (StreamReader f = new StreamReader(fájlnév))
			{
				string sor;
				string tartalmazó;
				string honnan;
				Hova él;
				while (!f.EndOfStream)
				{
					sor = f.ReadLine();
					tartalmazó = Regex.Match(sor, @"^(.*) bags contain").Groups[1].ToString();					
					foreach (Match tartalmazott in Regex.Matches(sor, @"(\d) (\w+ \w+) bag"))
					{
						honnan = tartalmazott.Groups[2].ToString();
						él = new Hova(tartalmazó, int.Parse(tartalmazott.Groups[1].ToString()));
						if (!élnyilvántartás.ContainsKey(él.hova))
							élnyilvántartás.Add(él.hova, new List<Hova> {});
						if (élnyilvántartás.ContainsKey(honnan))
							élnyilvántartás[honnan].Add(él);
						else
							élnyilvántartás.Add(honnan, new List<Hova> { él });
					}
				}
			}
			return élnyilvántartás;
		}

		static Dictionary<string, List<Hova>> BeLefele(string fájlnév)
		{
			Dictionary<string, List<Hova>> élnyilvántartás = new Dictionary<string, List<Hova>>();
			using (StreamReader f = new StreamReader(fájlnév))
			{
				string sor;
				string tartalmazó;
				while (!f.EndOfStream)
				{
					sor = f.ReadLine();
					tartalmazó = Regex.Match(sor, @"^(.*) bags contain").Groups[1].ToString();
					if (Regex.Match(sor, @"^.*? bags contain no other bags.$").Success)
						élnyilvántartás.Add(tartalmazó, new List<Hova> { });
					else
					{
						Hova él;
						foreach (Match tartalmazott in Regex.Matches(sor, @"(\d) (\w+ \w+) bag"))
						{
							él = new Hova(tartalmazott.Groups[2].ToString(), int.Parse(tartalmazott.Groups[1].ToString()));
							if (élnyilvántartás.ContainsKey(tartalmazó))
								élnyilvántartás[tartalmazó].Add(él);
							else
								élnyilvántartás.Add(tartalmazó, new List<Hova> { él });
						}
					}
				}
			}
			return élnyilvántartás;
		}


		static HashSet<string> GeneráltRészGráf (string csúcs, Dictionary<string, List<Hova>> élnyilvántartás)
		{
			HashSet<string> gráf = new HashSet<string>() { csúcs };
			foreach (Hova él in élnyilvántartás[csúcs])
				gráf.UnionWith(GeneráltRészGráf(él.hova, élnyilvántartás));
			return gráf;
		}

		static int Részfaösszeg(string csúcs, Dictionary<string, List<Hova>> élnyilvántartás) // fa, mert az összefutó éleket többször kell számolni. (Unraveling...)
		{
			int sum = 1; // mert ez a táska már 1.
			foreach (Hova él in élnyilvántartás[csúcs])
				sum += él.db * Részfaösszeg(él.hova, élnyilvántartás);
			return sum;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("1. rész");
			Console.WriteLine(GeneráltRészGráf("shiny gold", BeFelfele("input.txt")).Count - 1);

			Console.WriteLine("2. rész");
			Console.WriteLine(Részfaösszeg("shiny gold", BeLefele("input.txt")) - 1);
		}
	}
}
