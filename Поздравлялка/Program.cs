
using System.Reflection.Metadata;

namespace Congratulations
{
	internal class Program
	{
		private static DataBase context = new DataBase();
		private static List<Birthday> birthday = context.birthdays.ToList();
		private static BirthdayManager manager = new BirthdayManager(birthday);
		static void Main(string[] args)
		{
			manager.NearBirthdays();
			int userSelect;
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Добавить новую запись - 1");
				Console.WriteLine("Удалить запись - 2");
				Console.WriteLine("Вывести все записи - 3");
				Console.WriteLine("Вывести ближайшие дни рождения - 4");
				Console.WriteLine("Поиск определенной записи - 5");
				Console.WriteLine("Корректировка записи - 6");
				Console.WriteLine("Сортировка, грядущие дни рождения - 7");
				Console.WriteLine("Сохранение всех изменений в БД - 8");
				Console.WriteLine("Завершить программу - 9");
				Console.WriteLine("Выберите номер функции");
				if(!int.TryParse(Console.ReadLine(), out userSelect))
				{
					userSelect = 0;
				}
				switch (userSelect)
				{
					case 1:
						manager.AddElement();
						break;
					case 2:
						manager.DeleteElement();
						break;
					case 3:
						manager.OutputList(birthday);
						break;
					case 4:
						manager.NearBirthdays();
						break;
					case 5:
						manager.SearchElements();
						break;
					case 6:
						manager.CorrectElement();
						break;
					case 7:
						manager.SortElements();
						break;
					case 8:
						manager.SaveChanges();
						break;
					case 9:
						return;
					default:
						Console.WriteLine("Вы ввели неправильный номер функции\n Попробуйте еще раз");
						Console.ReadKey();
						break;
				}
			}
		}
	}
}