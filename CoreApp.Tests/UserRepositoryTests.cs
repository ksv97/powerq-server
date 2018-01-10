using System;
using Xunit;
using CoreApp.Repositories;
using CoreApp.ViewModels;
using CoreApp.Models;

namespace CoreApp.Tests
{
	public class UserRepositoryTests : IClassFixture<DbFixture>
	{
		private DbFixture fixture;

		public UserRepositoryTests(DbFixture fixture)
		{
			this.fixture = fixture;
		}

		/// <summary>
		/// Проверка: если подается объект UserViewModel, который равен Null, То на выходе должен быть null
		/// </summary>
		[Fact]
		public void TryLogIn_NullInput_ShouldReturnNull()
		{
			DbSeed.Seed(this.fixture.Context);
			UserViewModel input = null;
			UserRepository repository = new UserRepository(fixture.Context);

			Assert.Null(repository.TryLogIn(input));
		}

		/// <summary>
		/// Проверяем, что если на вход подать объект, который есть в базе данных, метод вернет объект типа UserViewModel
		/// </summary>
		[Fact]
		public void TryLogIn_UserInputWithExistiingCredentials_ShouldReturnUserViewModel ()
		{
			DbSeed.Seed(this.fixture.Context);
			User user = new User
			{
				FirstName = "Даша",
				SurName = "Волкова",
				Login = "dasha123",
				Password = "dasha123",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.IsType<UserViewModel>(result);
			
		}

		/// <summary>
		/// Проверка: если на вход приходит пользователь с такими данными, что в базе нет совпадений, хотя бы по одному полю то вернется null
		/// </summary>
		[Theory]
		[InlineData("123","123")]
		[InlineData("dasha123", "dontexist")]
		[InlineData("dontexist", "dasha123")]
		public void TryLogIn_UserInputWithNotExistingCredentials_ShouldReturnNull(string login, string password)
		{
			DbSeed.Seed(this.fixture.Context);
			User user = new User
			{
				Login = login,
				Password = password,
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.Null(result);
		}

		/// <summary>
		/// Проверяем, что если введены данные прописными буквами, тогда как в базе точно такие же данные введены строчными, должно вернутья null
		/// </summary>
		[Fact] 
		public void TryLogIn_UserInputWithCapitalizedExistingCredentials_ShouldReturnNull ()
		{
			DbSeed.Seed(this.fixture.Context);
			User user = new User
			{
				FirstName = "Даша",
				SurName = "Волкова",
				Login = "DASHA123",
				Password = "DASHA123",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.Null(result);
		}

		/// <summary>
		/// Проверяем: если в базе данных не будет записей, то метод должен вернуть Null
		/// </summary>
		[Fact]
		public void TryLogIn_ContextContainsNoUsers_ShouldReturnNull ()
		{
			User user = new User
			{
				FirstName = "Даша",
				SurName = "Волкова",
				Login = "dontexist",
				Password = "dontexist",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.Null(result);
		}


		/// <summary>
		/// Если на вход пришли данные null, то на выходе должен быть false
		/// </summary>
		[Fact]
		public void RegisterUser_NullInput_ShouldReturnFalse()
		{
			DbSeed.Seed(this.fixture.Context);
			UserViewModel input = null;
			UserRepository repository = new UserRepository(fixture.Context);

			Assert.False(repository.RegisterUser(input));
		}


		/// <summary>
		/// Подать на вход данные, в которых указан логин, уже содержащийся в базе. Получить false
		/// </summary>
		[Fact]
		public void RegisterUser_UserInputWithAlreadyTakenLogin_ShouldReturnFalse()
		{
			DbSeed.Seed(this.fixture.Context);
			User newUser = new User
			{
				FirstName = "Даша",
				SurName = "Волкова",
				Login = "dasha123",
				Password = "dasha123",
			};
			UserViewModel inputUser = new UserViewModel(newUser);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.RegisterUser(inputUser);

			Assert.False(result);

		}

		/// <summary>
		/// Подать на вход данные, в которых указан логин, уже содержащийся в базе. Получить false
		/// </summary>
		[Fact]
		public void RegisterUser_UserInputWithUniqueLogin_ShouldReturnTrue()
		{
			DbSeed.Seed(this.fixture.Context);
			User newUser = new User
			{
				FirstName = "Даша",
				SurName = "Волкова",
				Login = "this_login_is_unique",
				Password = "dasha123",
			};
			UserViewModel inputUser = new UserViewModel(newUser);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.RegisterUser(inputUser);

			Assert.True(result);

		}


		/// <summary>
		/// Подать на вход данные, при условии, что в базе нет записей. Вернется True
		/// </summary>
		[Fact]
		public void RegisterUser_ContextContainsNoUsers_ShouldReturnTrue()
		{
			User user = new User
			{
				FirstName = "Даша",
				SurName = "Волкова",
				Login = "dontexist",
				Password = "dontexist",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.RegisterUser(inputUser);

			Assert.True(result);
		}

	}
}
