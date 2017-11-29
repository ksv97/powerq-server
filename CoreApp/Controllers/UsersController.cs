using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.ViewModels;
using CoreApp.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UsersController : Controller
	{
		private IUserRepository repository;

		public UsersController(IUserRepository repository)
		{
			this.repository = repository;
		}

		[HttpPost]
		[Route("check")]
		public IActionResult CheckLoginOfUser([FromBody] UserViewModel userVM)
		{
			bool isAvailable = repository.CheckIfLoginAvailable(userVM.Login);
			if (isAvailable)
				return Ok(isAvailable);
			else return BadRequest("Пользователь с таким логином уже существует!");
		}

		[HttpPost]
		[Route("register/elder")]
		public IActionResult RegisterElderCurator([FromBody] ElderCuratorViewModel elderCuratorVM)
		{
			int? result = repository.RegisterElderCurator(elderCuratorVM);
			if (result != null)
				return Ok(result);
			else return BadRequest("Error!");
		}

		[HttpPost]
		[Route("register/curator")]
		public IActionResult RegisterCurator([FromBody] CuratorViewModel curatorViewModel)
		{
			int? result = repository.RegisterCurator(curatorViewModel);
			if (result != null)
				return Ok(result);
			else return BadRequest("Error!");
		}

		[HttpPost]
		[Route("login")]
		public IActionResult LogIn ([FromBody] UserViewModel userVM)
		{
			UserViewModel existingUser = repository.TryLogIn(userVM);
			if (existingUser != null)
			{
				return Ok(existingUser);
			}
			else return NotFound("Неверные имя пользователя или пароль!");
		}
    }
}
