using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations
{
	internal class Valid
	{
		public DateTime date;
		public string name;
		public string category;
		public int age;
		public int userSelect;
		static public DataBase context = new DataBase();
		private List<Birthday> birthdays = context.birthdays.ToList();
		public Birthday birthday;
		public Valid()
		{
			ValidDate();
			ValidName();
			ValidCategory();
			age = DateTime.Today.Year - date.Year;
			if(DateTime.Today.Month < date.Month) {
				age--;
			}
			else if(DateTime.Today.Month == date.Month)
			{
				if(DateTime.Today.Day < date.Day)
				{
					age--;
				}
			}
			birthday = new Birthday { Name = name, Category = category, Date = date, Age = age };
		}
		public Valid(int userSelect)
		{

		}
		public void ValidDate()
		{
			bool check = true;
			while (check)
			{
				Console.WriteLine("Введите дату рождени\n Год, месяц, день, через дефис");
				string temp = Console.ReadLine();
				if (DateTime.TryParse(temp, out date) != true || date < DateTime.Parse("1900-01-01") || date > DateTime.Parse("2025-01-01"))
				{
					Console.WriteLine("Вы не правильно ввели данные\n Попробуйте еще раз");
					Console.ReadKey();
				}
				else check = false;
			}
		}

		public void ValidName()
		{
			Console.WriteLine("Введите имя именника");
			name = Console.ReadLine();
		}

		public void ValidCategory()
		{
			bool check = false;
			while (!check)
			{
				Console.WriteLine("Выберите к какой категории относится именниник");
				Console.WriteLine("1 - Знакомый\n2 - Друг\n3 - Семья\n4 - Работа");
				check = int.TryParse(Console.ReadLine(), out userSelect);
				check = Category.allCategory.ContainsKey(userSelect);
				if (check != false)
				{
					category = Category.allCategory[userSelect];
				}
				else
				{
					Console.WriteLine("Вы не правильно ввели данные\nПопробуйте еще раз");
					Console.ReadKey();
				}
			}
		}

		public void ValidNumber()
		{
			bool check = false;
			while (!check)
			{
				check = int.TryParse(Console.ReadLine(), out userSelect);
				if (!check)
				{
					Console.WriteLine("Вы не правильно ввели данные\nПопробуйте еще раз");
				}
			}
		}

		public void ValidIndex()
		{
			bool check = false;
			while (!check)
			{
				check = int.TryParse(Console.ReadLine(), out userSelect);
				if (!check || userSelect < 1 || userSelect > birthdays.Count)
				{
					Console.WriteLine("Вы не правильно ввели данные\nПопробуйте еще раз");
				}
			}
		}
	}
}
