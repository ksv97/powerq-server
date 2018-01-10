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
		/// ��������: ���� �������� ������ UserViewModel, ������� ����� Null, �� �� ������ ������ ���� null
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
		/// ���������, ��� ���� �� ���� ������ ������, ������� ���� � ���� ������, ����� ������ ������ ���� UserViewModel
		/// </summary>
		[Fact]
		public void TryLogIn_UserInputWithExistiingCredentials_ShouldReturnUserViewModel ()
		{
			DbSeed.Seed(this.fixture.Context);
			User user = new User
			{
				FirstName = "����",
				SurName = "�������",
				Login = "dasha123",
				Password = "dasha123",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.IsType<UserViewModel>(result);
			
		}

		/// <summary>
		/// ��������: ���� �� ���� �������� ������������ � ������ �������, ��� � ���� ��� ����������, ���� �� �� ������ ���� �� �������� null
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
		/// ���������, ��� ���� ������� ������ ���������� �������, ����� ��� � ���� ����� ����� �� ������ ������� ���������, ������ �������� null
		/// </summary>
		[Fact] 
		public void TryLogIn_UserInputWithCapitalizedExistingCredentials_ShouldReturnNull ()
		{
			DbSeed.Seed(this.fixture.Context);
			User user = new User
			{
				FirstName = "����",
				SurName = "�������",
				Login = "DASHA123",
				Password = "DASHA123",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.Null(result);
		}

		/// <summary>
		/// ���������: ���� � ���� ������ �� ����� �������, �� ����� ������ ������� Null
		/// </summary>
		[Fact]
		public void TryLogIn_ContextContainsNoUsers_ShouldReturnNull ()
		{
			User user = new User
			{
				FirstName = "����",
				SurName = "�������",
				Login = "dontexist",
				Password = "dontexist",
			};
			UserViewModel inputUser = new UserViewModel(user);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.TryLogIn(inputUser);

			Assert.Null(result);
		}


		/// <summary>
		/// ���� �� ���� ������ ������ null, �� �� ������ ������ ���� false
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
		/// ������ �� ���� ������, � ������� ������ �����, ��� ������������ � ����. �������� false
		/// </summary>
		[Fact]
		public void RegisterUser_UserInputWithAlreadyTakenLogin_ShouldReturnFalse()
		{
			DbSeed.Seed(this.fixture.Context);
			User newUser = new User
			{
				FirstName = "����",
				SurName = "�������",
				Login = "dasha123",
				Password = "dasha123",
			};
			UserViewModel inputUser = new UserViewModel(newUser);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.RegisterUser(inputUser);

			Assert.False(result);

		}

		/// <summary>
		/// ������ �� ���� ������, � ������� ������ �����, ��� ������������ � ����. �������� false
		/// </summary>
		[Fact]
		public void RegisterUser_UserInputWithUniqueLogin_ShouldReturnTrue()
		{
			DbSeed.Seed(this.fixture.Context);
			User newUser = new User
			{
				FirstName = "����",
				SurName = "�������",
				Login = "this_login_is_unique",
				Password = "dasha123",
			};
			UserViewModel inputUser = new UserViewModel(newUser);

			UserRepository repository = new UserRepository(fixture.Context);

			var result = repository.RegisterUser(inputUser);

			Assert.True(result);

		}


		/// <summary>
		/// ������ �� ���� ������, ��� �������, ��� � ���� ��� �������. �������� True
		/// </summary>
		[Fact]
		public void RegisterUser_ContextContainsNoUsers_ShouldReturnTrue()
		{
			User user = new User
			{
				FirstName = "����",
				SurName = "�������",
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
