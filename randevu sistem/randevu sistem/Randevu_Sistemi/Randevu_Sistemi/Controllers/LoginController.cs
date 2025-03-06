using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Randevu_Sistemi.Data;
using Randevu_Sistemi.ViewModels;

namespace Randevu_Sistemi.Controllers
{
	public class LoginController : Controller
	{
		private readonly UygulamaDbContext _uygulamaDbcontext;//veritabanına erişmek için kullanlılan kod
		public LoginController(UygulamaDbContext context)//Constructor
		{
			_uygulamaDbcontext = context;//private readonly alanına atar
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]


		public IActionResult Index(LoginVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var user = _uygulamaDbcontext.Kullanicilar.Include(i => i.UserRole).Where(a => a.Username == vm.Username && a.Password == vm.Password).FirstOrDefault();
			if (user != null)
			{
				SessionVM session = new SessionVM();
				session.Fullname = user.FullName;
				session.Username = user.Username;
				session.UserId = user.Id;
				session.UserRoleId = user.UserRole.Id;
				session.UserRole = user.UserRole.Name;

				var jsonString = JsonConvert.SerializeObject(session);
				HttpContext.Session.SetString("UserSessionInfo", jsonString);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return View();
			}



		}

	}
		
}
