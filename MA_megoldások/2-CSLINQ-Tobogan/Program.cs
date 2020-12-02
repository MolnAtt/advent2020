using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Tobogan
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(
					File.ReadAllLines("tobogan.txt")
						.Select(sor => sor.Split(' '))
						.Select(tomb => new
						{
							intervallum = tomb[0].Split('-').Select(s => int.Parse(s)),
							db = tomb[2].Count(c => c == tomb[1][0])
						})
						.Count(ob => ob.intervallum.First() <= ob.db && ob.db <= ob.intervallum.Last()));

			Console.WriteLine(
					File.ReadAllLines("tobogan.txt")
						.Select(sor => sor.Split(' '))
						.Select(tomb => new
						{
							intervallum = tomb[0].Split('-').Select(s => int.Parse(s)-1),
							betű = tomb[1][0],
							szó = tomb[2]
						})
						.Count(ob => (ob.szó[ob.intervallum.First()] == ob.betű) ^ (ob.szó[ob.intervallum.Last()] == ob.betű)));

			
		}
	}
}
