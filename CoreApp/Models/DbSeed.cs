using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
	public static class DbSeed
	{
		public static void Seed(Context context)
		{
			context.Database.EnsureCreated();

			if (context.Faculties.Any())
			{
				return;
			}

			var faculties = new Faculty[]
			{
				new Faculty {  Name = "ЭЭФ"},
				new Faculty {  Name = "ИФФ и ФЭУ" },
				new Faculty { Name = "ТЭФ" },
				new Faculty { Name = "ИВТФ и ЭМФ"}
			};

			foreach (var faculty in faculties)
			{
				context.Faculties.Add(faculty);
			}

			context.SaveChanges();

			var roles = new Role[]
			{
				new Role { Name = "Куратор" },
				new Role { Name = "Старший куратор" },
				new Role { Name = "Руководитель" },				
			};

			foreach (var role in roles)
			{
				context.Roles.Add(role);
			}
			context.SaveChanges();

			var users = new User[]
			{
				new User
				{
					FirstName = "Родион",
					SurName = "Косов",
					Login = "ksv97",
					Password = "ksv97",
					RoleId = context.Roles.FirstOrDefault(r => r.Name == "Старший куратор").Id,
				},
				new User
				{
					FirstName = "Аня",
					SurName = "Соколова",
					Login = "ann123",
					Password = "ann123",
					Role = context.Roles.FirstOrDefault(r => r.Name == "Куратор"),
				},
				new User
				{
					FirstName = "Саша",
					SurName = "Баранов",
					Login = "alex123",
					Password = "alex123",
					Role = context.Roles.FirstOrDefault(r => r.Name == "Куратор"),
				},
				new User
				{
					FirstName = "Даша",
					SurName = "Волкова",
					Login = "dasha123",
					Password = "dasha123",
					Role = context.Roles.FirstOrDefault(r => r.Name == "Старший куратор"),
				}
			};

			foreach (var user in users)
			{
				context.Users.Add(user);
			}

			context.SaveChanges();

			var curators = new Curator[]
			{
				new Curator
				{
					CuratedGroups = "41 42",
					Faculty = context.Faculties.SingleOrDefault(x => x.Name == "ЭЭФ"),
					User = context.Users.SingleOrDefault(x => x.Login == "ann123"),
				}
			};

			foreach (var curator in curators)
			{
				context.Curators.Add(curator);
			}

			context.SaveChanges();

			var events = new ScheduleEvent[]
			{
				new ScheduleEvent
				{
					Title = "Кураторский час с группой 1-41",
					Description = "Провести четвертый кч с группой 1-41. Вся подробная информация в группе ВК. Аудитория Б001в. Время - 11:40.",
					Date = new DateTime(2017, 09, 27, 11, 40, 0),
					IsDeadline = false,
				},
				new ScheduleEvent
				{
					Title = "Общая планерка",
					Description = "Общая планерка по 4 кч. Проводим в пятницу, в холле корпуса В. Время - 19:20. Присутствие обязательно!",
					Date = new DateTime(2017, 09, 30, 19, 20, 0),
					IsDeadline = false,
				},
				new ScheduleEvent
				{
					Title = "Кураторский час с группой 1-42",
					Description = "Провести четвертый кч с группой 1-42. Вся подробная информация в группе ВК. Аудитория А342. Время - 13:30.",
					Date = new DateTime(2017, 09, 28, 13, 30, 0),
					IsDeadline = false,
				},
			};

			foreach (var ev in events)
			{
				context.ScheduleEvents.Add(ev);
			}

			context.SaveChanges();

			var scheduleEventUsers = new ScheduleEventUser[]
			{
				new ScheduleEventUser
				{
					ScheduleEventId = context.ScheduleEvents.SingleOrDefault(i => i.Title == "Кураторский час с группой 1-41").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "ann123").Id,
				},
				new ScheduleEventUser
				{
					ScheduleEventId = context.ScheduleEvents.SingleOrDefault(i => i.Title == "Общая планерка").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "ann123").Id,
				},
				new ScheduleEventUser
				{
					ScheduleEventId = context.ScheduleEvents.SingleOrDefault(i => i.Title == "Общая планерка").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "alex123").Id,
				},
				new ScheduleEventUser
				{
					ScheduleEventId = context.ScheduleEvents.SingleOrDefault(i => i.Title == "Кураторский час с группой 1-42").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "ann123").Id,
				},
			};

			foreach (var element in scheduleEventUsers)
			{
				context.ScheduleEventUsers.Add(element);
			}

			context.SaveChanges();

		}
	}
}
