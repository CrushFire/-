using Azure;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Congratulations
{
	internal class BirthdayManager
	{
		public List<Birthday> birthdays = new List<Birthday>();
		private Valid element = new Valid(1);
		private DataBase context = new DataBase();
		private List<Birthday> temp = new List<Birthday>();
		public BirthdayManager(List<Birthday> birthdays)
		{
			this.birthdays = birthdays;
		}
		public void AddElement()
		{
			element = new Valid();
			birthdays.Add(element.birthday);
			context.Add(element.birthday);
			context.UpdateRange();
		}

		public void SaveChanges()
		{
			context.UpdateRange();
			context.SaveChanges();
		}

		public void DeleteElement()
		{
			int userSelect = 0;
			bool check = false;
			if (birthdays.Count != 0)
			{
				while (!check)
				{
					Console.WriteLine("Введите номер записи, которую вы хотите удалить ");
					check = int.TryParse(Console.ReadLine(), out userSelect);
					if (!check || userSelect < 1 || userSelect > birthdays.Count)
					{
						check = false;
						Console.WriteLine("Вы ввели неправильный номер записи, попробуйте еще раз");
					}
				}
				birthdays.Remove(birthdays[userSelect - 1]);
				context.Remove(birthdays[userSelect - 1]);
				context.UpdateRange();
			}
			else
			{
				Console.WriteLine("Список пуст");
				Console.ReadKey();
			}
		}

		public void NearBirthdays()
		{
			temp.Clear();
			foreach (var birthday in birthdays.Where(x => x.Date.Month == DateTime.Today.Month && x.Date.Day <= DateTime.Today.AddDays(3).Day && x.Date.Day >= DateTime.Today.Day))
			{
				temp.Add(birthday);
			}

			OutputList(temp);
		}

		public void SearchElements()
		{
			temp.Clear();
			if (birthdays.Count != 0)
			{
				while (true)
				{
					Console.WriteLine("По какому параметру вы хотите отыскать элемент?\n1 - Порядковый номер\n2 - Имя\n3 - Категория\n4 - Дата рождения\n5 - Возраст");
					element.ValidNumber();
					switch (element.userSelect)
					{
						case 1:
							Console.WriteLine("Введите порядковый номер записи, которую вы хотите найти");
							element.ValidIndex();
							temp.Add(birthdays[element.userSelect - 1]);
							OutputList(temp);
							return;
						case 2:
							element.ValidName();
							foreach (var birthday in birthdays.Where(x => string.Compare(x.Name, element.name) == 0))
							{
								temp.Add(birthday);
							}
							OutputList(temp);
							return;
						case 3:
							element.ValidCategory();
							foreach (var birthday in birthdays.Where(x => string.Compare(x.Category, element.category) == 0))
							{
								temp.Add(birthday);
							}
							OutputList(temp);
							return;
						case 4:
							element.ValidDate();
							foreach (var birthday in birthdays.Where(x => x.Date == element.date))
							{
								temp.Add(birthday);
							}
							OutputList(temp);
							return;
						case 5:
							element.ValidNumber();
							foreach (var birthday in birthdays.Where(x => x.Age == element.userSelect))
							{
								temp.Add(birthday);
							}
							OutputList(temp);
							return;
						default:
							Console.WriteLine("Вы ввели неправильный номер функции\nПопробуйте еще раз");
							Console.ReadKey();
							break;
					}
				}
			}
			else
			{
				Console.WriteLine("Список пуст");
				Console.ReadKey();
			}
		}

		public void CorrectElement()
		{
			ConsoleKey answer;
			int userSelect;
			if (birthdays.Count != 0)
			{
				Console.WriteLine("Выберите номер записи, данные которой вы хотите изменить");
				element.ValidIndex();
				userSelect = element.userSelect - 1;
				Console.WriteLine("Хотите ли вы полностью изменить данные записи? любая клавиша - да, 0 - нет");
				answer = Console.ReadKey().Key;
				if (answer != ConsoleKey.D0)
				{
					element = new Valid();
					birthdays[userSelect].Name = element.name;
					birthdays[userSelect].Category = element.category;
					birthdays[userSelect].Date = element.date;
					birthdays[userSelect].Age = element.age;
				}
				else
				{
					Console.WriteLine("Хотите ли вы изменить имя? любая клавиша - да, 0 - нет");
					answer = Console.ReadKey().Key;
					if (answer != ConsoleKey.D0)
					{
						element.ValidName();
						birthdays[element.userSelect - 1].Name = element.name;
					}
					Console.WriteLine("Хотите ли вы изменить категорию? любая клавиша - да, 0 - нет");
					answer = Console.ReadKey().Key;
					if (answer != ConsoleKey.D0)
					{
						element.ValidCategory();
						birthdays[element.userSelect - 1].Category = element.category;
					}
					Console.WriteLine("Хотите ли вы изменить дату? любая клавиша - да, 0 - нет");
					answer = Console.ReadKey().Key;
					if (answer != ConsoleKey.D0)
					{
						element.ValidDate();
						birthdays[userSelect].Date = element.date;
						birthdays[userSelect].Age = DateTime.Today.Year - element.date.Year;
						if (DateTime.Today.Month < element.date.Month)
						{
							birthdays[userSelect].Age--;
						}
						else if (DateTime.Today.Month == element.date.Month)
						{
							if (DateTime.Today.Day < element.date.Day)
							{
								birthdays[userSelect].Age--;
							}
						}
					}
				}
			}
			else
			{
				Console.WriteLine("Список пуст");
				Console.ReadKey();
			}
		}

		public void OutputList(List<Birthday> list)
		{
			int start = 0;
			bool check = true;
			int index;
			int lim = 25;
			ConsoleKey answer;
			while (check)
			{
				Console.Clear();
				if (list.Count == 0)
				{
					Console.WriteLine("Список пуст");
				}
				for (index = start; index < lim && index < list.Count && list != null; index++)
				{
					if (list[index] != null)
					{
						Console.WriteLine($"{index + 1} {list[index].Name} {list[index].Category} {list[index].Date:D} {list[index].Age} лет");
					}
					else
					{
						break;
					}
				}
				Console.WriteLine("Esc - завершить просмотр списка, -> - перелистнуть на следующую страницу, <- - перелистнуть на предыдущую страницу");
				answer = Console.ReadKey(false).Key;
				if(answer == ConsoleKey.RightArrow)
				{
					if (index == lim)
					{
						start += 25;
						lim += 25;
					}
				}
				else if(answer == ConsoleKey.LeftArrow)
				{
					if (start >= 25)
					{
						start -= 25;
						lim -= 25;
					}
				}
				else if(answer == ConsoleKey.Escape)
				{
					return;
				}
			}
		}

		public void SortElements()
		{
			temp.Clear();
			foreach(var birthday in birthdays.Where(x => x.Date.Month >= DateTime.Today.Month && x.Date.Day >= DateTime.Today.Day).OrderBy(x => x.Date.Month).ThenBy(x => x.Date.Day))
			{
				temp.Add(birthday);
			}
			OutputList(temp);
		}
	}
}
