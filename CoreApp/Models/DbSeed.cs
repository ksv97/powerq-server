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
				new Role { Name = "Старший куратор"},		
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
					IsAdmin = true,
					RoleId = context.Roles.FirstOrDefault(r => r.Name == "Старший куратор").Id,
				},
				new User
				{
					FirstName = "Аня",
					SurName = "Соколова",
					Login = "ann123",
					Password = "ann123",
					IsAdmin = false,
					Role = context.Roles.FirstOrDefault(r => r.Name == "Куратор"),
				},
				new User
				{
					FirstName = "Саша",
					SurName = "Баранов",
					Login = "alex123",
					Password = "alex123",
					IsAdmin = false,
					Role = context.Roles.FirstOrDefault(r => r.Name == "Куратор"),
				},
				new User
				{
					FirstName = "Даша",
					SurName = "Волкова",
					Login = "dasha123",
					Password = "dasha123",
					IsAdmin = false,
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
					Mark = 30,
					Faculty = context.Faculties.SingleOrDefault(x => x.Name == "ЭЭФ"),
					User = context.Users.SingleOrDefault(x => x.Login == "ann123"),
				}
			};

			foreach (var curator in curators)
			{
				context.Curators.Add(curator);
			}

			context.SaveChanges();

			var events = new Event[]
			{
				new Event
				{
					Title = "Кураторский час с группой 1-41",
					Description = "Провести четвертый кч с группой 1-41. Вся подробная информация в группе ВК. Аудитория Б001в. Время - 11:40.",
					Date = new DateTime(2017, 09, 27, 11, 40, 0),
					IsDeadline = false,
				},
				new Event
				{
					Title = "Общая планерка",
					Description = "Общая планерка по 4 кч. Проводим в пятницу, в холле корпуса В. Время - 19:20. Присутствие обязательно!",
					Date = new DateTime(2017, 09, 30, 19, 20, 0),
					IsDeadline = false,
				},
				new Event
				{
					Title = "Кураторский час с группой 1-42",
					Description = "Провести четвертый кч с группой 1-42. Вся подробная информация в группе ВК. Аудитория А342. Время - 13:30.",
					Date = new DateTime(2017, 09, 28, 13, 30, 0),
					IsDeadline = false,
				},
			};

			foreach (var ev in events)
			{
				context.Events.Add(ev);
			}

			context.SaveChanges();

			var scheduleEventUsers = new ScheduledEvent[]
			{
				new ScheduledEvent
				{
					EventId = context.Events.SingleOrDefault(i => i.Title == "Кураторский час с группой 1-41").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "ann123").Id,
				},
				new ScheduledEvent
				{
					EventId = context.Events.SingleOrDefault(i => i.Title == "Общая планерка").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "ann123").Id,
				},
				new ScheduledEvent
				{
					EventId = context.Events.SingleOrDefault(i => i.Title == "Общая планерка").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "alex123").Id,
				},
				new ScheduledEvent
				{
					EventId = context.Events.SingleOrDefault(i => i.Title == "Кураторский час с группой 1-42").Id,
					UserId = context.Users.SingleOrDefault(i => i.Login == "ann123").Id,
				},
			};

			foreach (var element in scheduleEventUsers)
			{
				context.ScheduledEvents.Add(element);
			}

			context.SaveChanges();

			var feedbackForms = new FeedbackForm[]
{
				new FeedbackForm
				{
					Name = "Отчет о 1-ом КЧ",
					DeadlineDate = new DateTime(2018, 4, 15),
				},
				new FeedbackForm
				{
					Name = "Отчет о 2-ом КЧ",
					DeadlineDate = new DateTime(2018, 5, 25),
				},
};

			foreach (var feedbackForm in feedbackForms)
			{
				context.FeedbackForms.Add(feedbackForm);
			}

			context.SaveChanges();

			var feedbackQuestions = new FeedbackQuestion[]
			{
				// для формы №1
				new FeedbackQuestion { Name = "Как группа вас встретила и отреагировала на вас?", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 1-ом КЧ")},
				new FeedbackQuestion { Name = "Как вам ваши ребята?", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 1-ом КЧ")},
				new FeedbackQuestion { Name = "Что делали? (вот тут максимально подробно и со всеми косяками, если они есть)", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 1-ом КЧ")},
				new FeedbackQuestion { Name = "Как ваши эмоции после первого кураторского часа?", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 1-ом КЧ")},
				new FeedbackQuestion { Name = "Сделайте вывод о проделанной вами работе и оцените ее по шкале от 1 до 10.", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 1-ом КЧ")},

				// для формы №2
				new FeedbackQuestion { Name = "Реакция группы на веревку: как ребята воспринимали задания, какие эмоции у них были? Были ли сложности и как они с этим справлялись?", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 2-ом КЧ")},
				new FeedbackQuestion { Name = "Как проводилась верёвка с точки зрения организации: плюсы и минусы, что понравилось, а что нет", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 2-ом КЧ")},
				new FeedbackQuestion { Name = "Вмешивались ли вы в процесс проведения веревки и каким образом? Как вам самим все мероприятие и ребята с точки зрения эмоций?", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 2-ом КЧ")},
				new FeedbackQuestion { Name = "Оценочка вашей работы по шкале от 1 до 10.", FeedbackForm = context.FeedbackForms.SingleOrDefault(f => f.Name == "Отчет о 2-ом КЧ")},
			};

			foreach (var feedbackQuestion in feedbackQuestions)
			{
				context.FeedbackQuestions.Add(feedbackQuestion);
			}

			context.SaveChanges();


		}
	}
}
