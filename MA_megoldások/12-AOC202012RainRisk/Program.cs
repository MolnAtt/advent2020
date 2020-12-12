using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AOC202012RainRisk
{
	class Program
	{
		public class számpár
		{
			int x, y;
			public számpár(int a, int b)
			{
				x = a;
				y = b;
			}
			public void Eltol(számpár v, int szorzó)
			{
				x += szorzó * v.x;
				y += szorzó * v.y;
			}
			public void Eltol(Dictionary<char, számpár> vektorszótár, string u)
			{
				Eltol(vektorszótár[u[0]], int.Parse(u.Substring(1)));
			}
			public void Forgat(string u)
			{
				char iránykarakter = u[0];
				int fok = int.Parse(u.Substring(1));
				if (iránykarakter == 'L')
					for (int i = 0; i < fok; i += 90)
						(x, y) = (-y, x);
				else // if (iránykarakter == 'R')
					for (int i = 0; i < fok; i += 90)
						(x, y) = (y, -x);
			}
			public int Manhattan(){ return Math.Abs(x) + Math.Abs(y); }
			public override string ToString(){ return $"({x}, {y})"; }
		}

		class Komp
		{
			// azért protected, hogy úgy legyen kívülről hozzáférhető, hogy közben még a leszármazottak örököljék
			protected számpár poz;
			protected StreamReader fájl;
			protected Dictionary<char, számpár> vektorszótár;
			public Komp(string path)
			{
				poz = new számpár(0, 0);
				fájl = new StreamReader(path);
				vektorszótár = new Dictionary<char, számpár>
				{
					['N'] = new számpár(0, 1),
					['E'] = new számpár(1, 0),
					['S'] = new számpár(0, -1),
					['W'] = new számpár(-1, 0),
				};
			}

			protected void Lép(string u) { poz.Eltol(vektorszótár, u); } // mindkét komptípus ugyanúgy lép.

			public virtual void Mozog(string e){} // a két komp máshogy mozog, de mindkettő "Mozog"-nak hívja a metódust. Azért hoztuk ezt létre, hogy később lehessen őket egységesen kezelni -> POLIMORFIZMUS. Ehhez kell csinálni egy VIRTUAL függvényt, amit később felülírhatunk (OVERRIDE)
			protected void Diagnosztika(string esemény){ Console.Error.WriteLine($"pozíció: {poz.ToString()}, irány:{vektorszótár['F'].ToString()}, utasítás: {esemény}"); }

			public virtual int Bolyongás(){ return 0; } // VIRTUAL! lásd a Mozog-ot.
		}

		class Komp1 : Komp // így örököl a Komptól
		{
			public Komp1(string path) : base(path) // meg kell hívni az ős konstruktorát is!
			{
				vektorszótár.Add('F', new számpár(1, 0)); // kezdetben keletre néz	
			} 

			public override void Mozog(string e)
			{
				switch (e[0])
				{
					case 'L':
					case 'R':
						vektorszótár['F'].Forgat(e);
						break;
					default:
						Lép(e);
						break;
				}					
			}
			public override int Bolyongás()
			{
				string esemény = " -- ";
				Diagnosztika(esemény);
				while (!fájl.EndOfStream)
				{
					esemény = fájl.ReadLine();
					Mozog(esemény);
					Diagnosztika(esemény);
				}
				int mo = poz.Manhattan();
				Console.WriteLine(mo);
				return mo;
			}
		}

		class Komp2 : Komp
		{
			public Komp2(string path) : base(path)
			{
				vektorszótár.Add('F', new számpár(10, 1)); // kezdetben a (10,1) pontra néz
			}

			public override void Mozog(string e)
			{
				switch (e[0])
				{
					case 'L':
					case 'R':
						vektorszótár['F'].Forgat(e);
						break;
					case 'N':
					case 'E':
					case 'S':
					case 'W':
						vektorszótár['F'].Eltol(vektorszótár[e[0]], int.Parse(e.Substring(1)));
						break;
					case 'F':
						Lép(e);
						break;
				}
			}

			public override int Bolyongás()
			{
				string esemény = " -- ";
				Diagnosztika(esemény);
				while (!fájl.EndOfStream)
				{
					esemény = fájl.ReadLine();
					Mozog(esemény);
					Diagnosztika(esemény);
				}
				int mo = poz.Manhattan();
				Console.WriteLine(mo);
				return mo;
			}
		}
		static void Main(string[] args)
		{
			List<int> megoldások = new List<int>();
			List<Komp> kompok = new List<Komp>
				{
					new Komp1("teszt.txt"),
					new Komp1("input.txt"),
					new Komp2("teszt.txt"),
					new Komp2("input.txt")
				}; // mind a két típusú komp belefért ugyanabba listába! (Ez C#-ban nagy dolog :) ) ez azért van, mert közös ősük van, de ez még csak a kezdet: 
			foreach (Komp item in kompok)
			{
				megoldások.Add(item.Bolyongás()); // Ez a polimorfizmus (sokalakúság!) A különböző kompok máshogy értelmezik a bolyongást, de egységesen kezelhetők! Azért lehet ilyet tenni, mert az ősnél is jeleztük ezt a VIRTUAL metódusokkal!
			}
			Console.WriteLine($"Komp1 a tesztinputtal: {megoldások[0]}");
			Console.WriteLine($"Komp1 a rendes inputtal: {megoldások[1]}");
			Console.WriteLine($"Komp2 a tesztinputtal: {megoldások[2]}");
			Console.WriteLine($"Komp3 a rendes inputtal: {megoldások[3]}");
		}
	}
}
