using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC20208HandheldHalting
{

	class Konzol
	{
		class Bootsor
		{
			public string cmd;
			public int arg;
			public Bootsor(string[] sortömb)
			{
				this.cmd = sortömb[0];
				this.arg = int.Parse(sortömb[1]);
			}
			public static List<Bootsor> lista = new List<Bootsor>();
		}

		private int time = 0;
		private int i = 0;
		private int acc = 0;
		private Stack<(int, int, int)> mem = new Stack<(int, int, int)>();
		private List<int> Naplo = new List<int>();
		private HashSet<int> Naplohalmaz = new HashSet<int>();
		private bool volt_már_kör = false;

		public Konzol(string input)
		{
			Bootsor.lista = File.ReadAllLines(input).Select(x => new Bootsor(x.Split(' '))).ToList();
		}

		private void Végrehajt()
		{
			Naplo.Add(i);
			Naplohalmaz.Add(i);

			Console.Error.WriteLine(Állapotkiírás());
			switch (Bootsor.lista[i].cmd)
			{
				case "acc":
					acc += Bootsor.lista[i++].arg;
					break;
				case "jmp":
					if (!volt_már_kör)
						Elment();
					i += Bootsor.lista[i].arg;
					break;
				case "nop":
					if (!volt_már_kör)
						Elment();
					i++;
					break;
			}
			time++;
		}

		private void Átír()
		{ 
			Bootsor.lista[i].cmd = Bootsor.lista[i].cmd == "jmp" ? "nop" : "jmp"; 
		}

		private void Elment()
		{
			mem.Push((time, i, acc));
		}

		private string Állapotkiírás()
		{
			return $"{time + 1} | {i}: {Bootsor.lista[i].cmd}_{Bootsor.lista[i].arg} ({acc})";
		}

		private void Betölt()
		{
			if (volt_már_kör)
			{
				(time, i, acc) = mem.Pop(); // Visszaugrik oda, ahol a legutóbb babrált, a mentések közül pedig törli ezt a pozíciót.
				Console.Error.WriteLine($"visszaugrottam visszajavítani ezt: {Állapotkiírás()}");
				Átír(); // visszajavítja
				Console.Error.WriteLine($"visszajavítva: {Állapotkiírás()}");
			}

			(time, i, acc) = mem.Peek(); // odaállítja a fejet, ahol a legutóbbi - még nem babrált - elágazás volt.
			Console.Error.WriteLine($"visszalépek: {Állapotkiírás()}");

			Naplo = Naplo.GetRange(0, time); // törli a napló i< utáni részét
			Naplohalmaz = Naplo.ToHashSet(); // kialakítja a halmazt a gyors elemvizsgálatokhoz
		}

		public int Run()
		{
			while (true)
			{
				if (i == Bootsor.lista.Count)
					return acc;

				if (Naplohalmaz.Contains(i))
				{
					Console.Error.WriteLine("körbe futnék!");
					Betölt();
					Átír();
					Console.Error.WriteLine($"Átírtam erre: {Állapotkiírás()}");
					volt_már_kör = true;
				}

				Végrehajt();
			}
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(new Konzol("teszt.txt").Run());
			Console.WriteLine("---------------------------");
			Console.WriteLine(new Konzol("input.txt").Run());
		}
	}
}
