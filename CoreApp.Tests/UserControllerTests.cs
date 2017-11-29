using Xunit;
using CoreApp.Repositories;
using CoreApp.ViewModels;
using CoreApp.Models;
using CoreApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoreApp.Tests
{
	public class UserControllerTests : IClassFixture<DbFixture>
	{
		private DbFixture fixture;

		public UserControllerTests(DbFixture fixture)
		{
			this.fixture = fixture;
		}


		/// <summary>
		/// Проверяет, что если пришли "плохие" данные (например, null), то сервер вернет ошибку
		/// </summary>
		[Fact]
		public void RegisterCurator_RequestBadData_ReturnBadRequest ()
		{
			CuratorViewModel viewModel = null;

			UserRepository repository = new UserRepository(fixture.Context);
			int oldLenght = fixture.Context.Curators.ToList().Count;
			UsersController controller = new UsersController(repository);
				
			var result = controller.RegisterCurator(viewModel);
			int newLength = fixture.Context.Curators.ToList().Count;

			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal(oldLenght, newLength);
		}

		/// <summary>
		/// Проверяет, что в случае прихода правильных данных они успешно запишутся, и придет позитивный ответ
		/// </summary>
		[Fact]
		public void RegisterCurator_RequestCurator_ReturnOkObjectResultAndSuccessfullyAddedToDb()
		{
			var user = new User
			{
				FirstName = "Алекс",
				SurName = "Баранов",
				Login = "alex123456",
				Password = "alex123456",
				Role = new Role { Name = "Куратор" },
			};

			var curator = new Curator
			{
				CuratedGroups = "41 42",
				Faculty = new Faculty { Name = "ИВТФ и ЭМФ" },
				User = user,
			};

			CuratorViewModel viewModel = new CuratorViewModel(curator);

			UserRepository repository = new UserRepository(fixture.Context);
			UsersController controller = new UsersController(repository);
			var oldLenght = fixture.Context.Curators.ToList().Count;

			var result = controller.RegisterCurator(viewModel);
			var newLength = fixture.Context.Curators.ToList().Count;

			Assert.IsType<OkObjectResult>(result);
			Assert.Equal(oldLenght + 1, newLength);
			
			
		}

    }
}
