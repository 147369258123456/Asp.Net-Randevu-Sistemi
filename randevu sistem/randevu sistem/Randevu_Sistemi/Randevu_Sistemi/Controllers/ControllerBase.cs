using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Randevu_Sistemi.Data;
using Randevu_Sistemi.Models;
using Randevu_Sistemi.ViewModels;

namespace Randevu_Sistemi.Controllers
{
	public class ControllerBase : Controller
	{
		private readonly UygulamaDbContext _uygulamaDbcontext;//veritabanına erişmek için kullanlılan kod

		public ControllerBase(UygulamaDbContext context)//Constructor
		{
			_uygulamaDbcontext = context;//private readonly alanına atar
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var value = HttpContext.Session.GetString("UserSessionInfo");//Kullanıcının oturumda saklanan UserSessionInfo bilgilerini alır.
			if (value == null)
			{
				filterContext.Result = RedirectToAction("Index", "Login");
				return;
			}
			var ses = JsonConvert.DeserializeObject<SessionVM>(value);//Oturum bilgisi JSON formatındadır.

			//var descriptor = (ControllerActionDescriptor)filterContext.ActionDescriptor;
			var action = filterContext.RouteData.Values["action"].ToString();
			var controller = filterContext.RouteData.Values["controller"] as string;

			var yetki = _uygulamaDbcontext.RolePages.Include(i => i.PageRole).Where(x => x.PageRole.Id == ses.UserRoleId && x.ControllerName == controller && x.ActionName == action).FirstOrDefault();
			if (yetki == null)
			{

				filterContext.Result = RedirectToAction("Index", "Login");
				return;
			}
			AddUserLog(ses.UserId, controller, action);
			base.OnActionExecuting(filterContext);
		}

		public void AddUserLog(int userId, string controller, string action)
		{
			var ip = "127.0.0.1";
			var log = new UserLog();
			log.UserId = userId;
			log.Controller = controller;
			log.Action = action;
			log.LogTime = DateTime.Now;
			log.IpAdress = ip;

			_uygulamaDbcontext.UserLogs.Add(log);
			_uygulamaDbcontext.SaveChanges();
		}
	}
}
