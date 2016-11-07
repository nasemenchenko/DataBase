using System;
using System.Linq;

namespace StudentQueries
{
	class Program
	{
		static void CopyToDatabase(Context context)
		{
			Repository repo = new Repository();
			context.Groups.AddRange(repo.Groups);
			context.Students.AddRange(repo.Students);

			context.SaveChanges();
		}

		static void FilterAndOrder(Context context)
		{
			var result = from s in context.Students
						 where s.BirthDate.Year >= 1999
						 orderby s.Rating descending, s.Name
						 select s;

			foreach (var s in result)
				Console.WriteLine("{0} {1}: {2}", s.Name, s.Surname, s.Rating);
		}

		static void Join(Context context)
		{
			var result = from s in context.Students
						 join g in context.Groups
							on s.GroupId equals g.Id
						 select new StudentViewModel
						 {
							 FullName = s.Name + " " + s.Surname,
							 Rating = s.Rating,
							 GroupName = g.Name
						 };
			foreach (var s in result)
				Console.WriteLine("{0} ({1}): rating = {2}", s.FullName, s.GroupName, s.Rating);
			
		}

		static void GroupByFirstLetter(Context context)
		{
			var result = from s in context.Students
						 group s by s.Name.Substring(0, 1) into g
						 select g;

			foreach (var group in result)
			{
				Console.WriteLine("\nFirst letter: {0}", group.Key);
				foreach (var s in group)
					Console.WriteLine("{0} {1}", s.Name, s.Surname);
			}
		}

		static void Main(string[] args)
		{
			using (Context c = new Context())
			{
				// Uncomment and run once to fill the database
				// CopyToDatabase(c);

				// Queries
				//FilterAndOrder(c);
				//GroupByFirstLetter(c);
				Join(c);
			}
			Console.ReadKey();
		}
	}
}
